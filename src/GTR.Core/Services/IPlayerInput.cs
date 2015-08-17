#region

using System.Collections.Generic;
using System.Threading.Tasks;
using GTR.Core.Action;
using GTR.Core.CardCollections;
using GTR.Core.Game;
using GTR.Core.Model;

#endregion

namespace GTR.Core.Services
{
    public interface IPlayerInput
    {
        Task<RoleType> GetLeadRole(ICollection<RoleType> availableLeads);
        Task<ICollection<HandCardModel>> SelectCards(ICollection<HandCardModel> cards);
        Task<RoleType> GetRole(ICollection<RoleType> collection);
        Task<ICardSource<HandCardModel>> GetSource(List<ICardSource<HandCardModel>> availableSources);
        Task<ActionType> GetLead();
        Task<ActionType> GetFollow();
        Task<ICollection<HandCardModel>> SelectLeadCards(List<HandCardModel> cardOptions);
        Task<ICollection<HandCardModel>> SelectFollowCards(List<HandCardModel> cardOptions, RoleType role);
        Task<IAction> GetMove(MoveSpace moveSpace);
    }
}