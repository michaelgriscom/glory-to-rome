#region

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GTR.Core.Action;
using GTR.Core.CardCollections;
using GTR.Core.Game;
using GTR.Core.Model;
using GTR.Core.Services;

#endregion

namespace GTR.Core.AIController
{
    public class AiPlayerInput
    {
        public async Task<RoleType> GetLeadRole(ICollection<RoleType> availableLeads)
        {
            return availableLeads.ElementAt(0);
        }

        public async Task<ICollection<HandCardModel>> SelectCards(ICollection<HandCardModel> cards)
        {
            var selection = new List<HandCardModel>();
            if (cards != null && cards.Count > 0)
            {
                selection.Add(cards.ElementAt(0));
            }
            return selection;
        }

        public async Task<RoleType> GetRole(ICollection<RoleType> collection)
        {
            return collection.ElementAt(0);
        }

        public async Task<ICardSource<HandCardModel>> GetSource(List<ICardSource<HandCardModel>> availableSources)
        {
            return availableSources.ElementAt(0);
        }

        public async Task<ActionType> GetLead()
        {
            return ActionType.HandPlay;
        }

        public async Task<ActionType> GetFollow()
        {
            return ActionType.Thinker;
        }

        public async Task<ICollection<HandCardModel>> SelectLeadCards(List<HandCardModel> cardOptions)
        {
            var cards = new List<HandCardModel> {cardOptions.ElementAt(0)};
            return cards;
        }

        public async Task<ICollection<HandCardModel>> SelectFollowCards(List<HandCardModel> cardOptions, RoleType role)
        {
            var cards = new List<HandCardModel> {cardOptions.ElementAt(0)};
            return cards;
        }

        public async Task<IAction> GetMove(MoveSpace moveSpace)
        {
            return moveSpace.Any() ? moveSpace.ElementAt(0) : null;
        }
    }
}