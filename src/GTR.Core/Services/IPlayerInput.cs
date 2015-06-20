#region

using System.Collections.Generic;
using GTR.Core.Action;
using GTR.Core.CardCollections;
using GTR.Core.Game;
using GTR.Core.Model;

#endregion

namespace GTR.Core.Services
{
    public interface IPlayerInput
    {
        RoleType GetLeadRole(ICollection<RoleType> availableLeads);
        ICollection<HandCardModel> SelectCards(ICollection<HandCardModel> cards);
        RoleType GetRole(ICollection<RoleType> collection);
        ICardSource<HandCardModel> GetSource(List<ICardSource<HandCardModel>> availableSources);
        ActionType GetLead();
        ActionType GetFollow();
        ICollection<HandCardModel> SelectLeadCards(List<HandCardModel> cardOptions);
        ICollection<HandCardModel> SelectFollowCards(List<HandCardModel> cardOptions, RoleType role);
        IAction GetMove(MoveSpace moveSpace);
    }
}