#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GTR.Core.Game;
using GTR.Core.Model;
using GTR.Core.Services;

#endregion

namespace GTR.Core.ManipulatableRules
{
    /// <summary>
    ///     Encapsulates the logic of leading and following for a given player.
    /// </summary>
    internal class LeadFollowManager
    {
        private const int CardsToActAsJack = 3;
        private readonly IPlayerInput _inputService;

        public LeadFollowManager(IPlayerInput inputService)
        {
            _inputService = inputService;
        }

        public ICollection<HandCardModel> GetFollowCards(Hand hand, RoleType role)
        {
            var cardOptions = hand.OrderCards.Cast<HandCardModel>().ToList();
            cardOptions.AddRange(hand.JackCards.Cast<HandCardModel>());

            ICollection<HandCardModel> cards;
            do
            {
                cards = _inputService.SelectFollowCards(cardOptions, role);
            } while (!IsValidFollow(cards, role));

            return cards;
        }

        private bool IsValidFollow(ICollection<HandCardModel> cards, RoleType role)
        {
            bool isValid = false;
            if (cards == null)
            {
                return false;
            }
            if (cards.Count == 1)
            {
                HandCardModel card = cards.ElementAt(0);
                isValid = card is JackCardModel ||
                          ((OrderCardModel) card).RoleType == role;
            }
            else if (cards.Count == CardsToActAsJack)
            {
                isValid = CardsOfSameRole(cards);
            }
            return isValid;
        }

        public RoleType GetLeadRole(ICollection<HandCardModel> leadCards)
        {
            ICollection<RoleType> availableLeads = GetAvailableLeads(leadCards);
            RoleType role = _inputService.GetLeadRole(availableLeads);
            return role;
        }

        private bool IsValidLead(ICollection<HandCardModel> cards)
        {
            bool isValid = false;
            if (cards == null)
            {
                return false;
            }
            if (cards.Count == 1)
            {
                isValid = true;
            }
            else if (cards.Count == CardsToActAsJack)
            {
                isValid = CardsOfSameRole(cards);
            }
            return isValid;
        }

        private ICollection<RoleType> GetAvailableLeads(ICollection<HandCardModel> leadCards)
        {
            IList<RoleType> availableLeads = new List<RoleType>();
            if (leadCards.ElementAt(0) is JackCardModel || leadCards.Count > 1)
            {
                availableLeads = EnumExtensions.GetEnumList<RoleType>();
            }
            else
            {
                OrderCardModel orderCard = leadCards.ElementAt(0) as OrderCardModel;
                Debug.Assert(orderCard != null, "orderCard != null");
                availableLeads.Add(orderCard.RoleType);
            }
            return availableLeads;
        }

        /// <summary>
        ///     Determines whether the given cards are all of the same role. Jacks are treated similarly to database nulls,
        ///     i.e., false is returned if there are any Jacks.
        /// </summary>
        /// <param name="cards">A collection of cards assumed to contain more than 1.</param>
        private bool CardsOfSameRole(IEnumerable<HandCardModel> cards)
        {
            if (cards == null) throw new ArgumentNullException("cards");
            RoleType role = RoleType.Architect; // assign to value to satisfy compiler
            bool firstIter = true;
            foreach (HandCardModel card in cards)
            {
                OrderCardModel orderCard = card as OrderCardModel;
                if (orderCard == null)
                {
                    return false;
                }
                if (firstIter)
                {
                    role = orderCard.RoleType;
                    firstIter = false;
                }
                else if (orderCard.RoleType != role)
                {
                    return false;
                }
            }
            return true;
        }

        internal ICollection<HandCardModel> GetLeadCards(Hand hand)
        {
            var cardOptions = hand.OrderCards.Cast<HandCardModel>().ToList();
            cardOptions.AddRange(hand.JackCards.Cast<HandCardModel>());

            ICollection<HandCardModel> cards;
            do
            {
                cards = _inputService.SelectLeadCards(cardOptions);
            } while (!IsValidLead(cards));

            return cards;
        }
    }
}