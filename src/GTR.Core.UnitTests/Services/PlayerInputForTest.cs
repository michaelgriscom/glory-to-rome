#region

using System;
using System.Collections.Generic;
using System.Linq;
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

        public RoleType GetLeadRole(ICollection<RoleType> availableLeads)
        {
            return RoleType.Patron;
        }

        public ICollection<HandCardModel> SelectCards(ICollection<HandCardModel> cards)
        {
            throw new NotImplementedException();
        }

        public RoleType GetRole(ICollection<RoleType> collection)
        {
            throw new NotImplementedException();
        }

        public ICardSource<HandCardModel> GetSource(List<ICardSource<HandCardModel>> availableSources)
        {
            throw new NotImplementedException();
        }

        public ActionType GetLead()
        {
            throw new NotImplementedException();
        }

        public ActionType GetFollow()
        {
            throw new NotImplementedException();
        }

        public ICollection<HandCardModel> SelectLeadCards(List<HandCardModel> cardOptions)
        {
            throw new NotImplementedException();
        }

        public ICollection<HandCardModel> SelectFollowCards(List<HandCardModel> cardOptions, RoleType role)
        {
            throw new NotImplementedException();
        }

        public IAction GetMove(MoveSpace moveSpace)
        {
            if (OnMoveEventHandler != null)
            {
                var eventArgs = new OnMoveEventArgs {MoveSpace = moveSpace};
                OnMoveEventHandler(this, eventArgs);
            }
            return !moveSpace.Any() ? null : moveSpace.ElementAt(0);
        }

        public event OnMove OnMoveEventHandler;
    }

    public class OnMoveEventArgs : EventArgs
    {
        public MoveSpace MoveSpace { get; set; }
    }
}