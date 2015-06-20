#region

using System;
using System.Collections.Generic;
using GTR.Core.CardCollections;
using GTR.Core.Game;
using GTR.Core.Model;
using GTR.Core.Services;

#endregion

namespace GTR.Core.UnitTests.Services
{
    internal class PlayerInputForTest : IPlayerInput
    {
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

        public IMove<CardModelBase> GetMove(MoveSpace moveSpace)
        {
            throw new NotImplementedException();
        }
    }
}