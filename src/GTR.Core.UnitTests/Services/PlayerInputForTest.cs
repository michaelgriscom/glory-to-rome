#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GTR.Core.Action;
using GTR.Core.CardCollections;
using GTR.Core.Game;
using GTR.Core.Model;
using GTR.Core.Services;

#endregion

namespace GTR.Core.UnitTests.Services
{
    internal class PlayerInputForTest : IPlayerInput
    {
        public delegate void OnMove(object sender, OnMoveEventArgs eventArgs);

        public Task<RoleType> GetLeadRole(ICollection<RoleType> availableLeads)
        {
            return Task.FromResult(RoleType.Patron);
        }

        public Task<ICollection<HandCardModel>> SelectCards(ICollection<HandCardModel> cards)
        {
            throw new NotImplementedException();
        }

        public Task<RoleType> GetRole(ICollection<RoleType> collection)
        {
            throw new NotImplementedException();
        }

        public Task<ICardSource<HandCardModel>> GetSource(List<ICardSource<HandCardModel>> availableSources)
        {
            throw new NotImplementedException();
        }

        public Task<ActionType> GetLead()
        {
            throw new NotImplementedException();
        }

        public Task<ActionType> GetFollow()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<HandCardModel>> SelectLeadCards(List<HandCardModel> cardOptions)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<HandCardModel>> SelectFollowCards(List<HandCardModel> cardOptions, RoleType role)
        {
            throw new NotImplementedException();
        }

        public Task<IAction> GetMove(MoveSpace moveSpace)
        {
            if (OnMoveEventHandler != null)
            {
                var eventArgs = new OnMoveEventArgs {MoveSpace = moveSpace};
                OnMoveEventHandler(this, eventArgs);
            }
            var returnVal = !moveSpace.Any() ? null : moveSpace.ElementAt(0);
            return Task.FromResult(returnVal);
        }

        public event OnMove OnMoveEventHandler;
    }

    public class OnMoveEventArgs : EventArgs
    {
        public MoveSpace MoveSpace { get; set; }
    }
}