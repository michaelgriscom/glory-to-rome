#region

using System.Collections.Generic;
using System.Linq;
using GTR.Core.Action;
using GTR.Core.CardCollections;
using GTR.Core.Game;
using GTR.Core.Model;
using GTR.Core.Services;

#endregion

namespace GTR.Core.AIController
{
    internal class AiPlayerInput : IPlayerInput
    {
        public RoleType GetLeadRole(ICollection<RoleType> availableLeads)
        {
            return availableLeads.ElementAt(0);
        }

        public ICollection<HandCardModel> SelectCards(ICollection<HandCardModel> cards)
        {
            var selection = new List<HandCardModel>();
            if (cards != null && cards.Count > 0)
            {
                selection.Add(cards.ElementAt(0));
            }
            return selection;
        }

        public RoleType GetRole(ICollection<RoleType> collection)
        {
            return collection.ElementAt(0);
        }

        public ICardSource<HandCardModel> GetSource(List<ICardSource<HandCardModel>> availableSources)
        {
            return availableSources.ElementAt(0);
        }

        public ActionType GetLead()
        {
            return ActionType.HandPlay;
        }

        public ActionType GetFollow()
        {
            return ActionType.Thinker;
        }

        public ICollection<HandCardModel> SelectLeadCards(List<HandCardModel> cardOptions)
        {
            var cards = new List<HandCardModel> {cardOptions.ElementAt(0)};
            return cards;
        }

        public ICollection<HandCardModel> SelectFollowCards(List<HandCardModel> cardOptions, RoleType role)
        {
            var cards = new List<HandCardModel> {cardOptions.ElementAt(0)};
            return cards;
        }

        public IAction GetMove(MoveSpace moveSpace)
        {
            return moveSpace.ElementAt(0);
        }
    }
}