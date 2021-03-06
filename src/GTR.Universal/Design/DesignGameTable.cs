﻿#region

using System.Collections.ObjectModel;
using GTR.Core.Engine;
using GTR.Core.Game;
using GTR.Core.Model;
using GTR.Core.Model.CardCollections;

#endregion

namespace GTR.Universal.Design
{
    public class DesignGameTable : GameTable
    {
        public DesignGameTable() : base()
        {
            OrderDeck = new OrderDeck();
            JackDeck = new JackDeck();
            Players = new ObservableCollection<Player> {new DesignPlayer()};
            for (int i = 0; i < 50; i++)
            {
                string cardName = string.Format("Card #{0}", i);
                string description = "this is a fake description";
                RoleType role = RoleType.Architect;
                switch (i%4)
                {
                    case 0:
                        role = RoleType.Architect;
                        break;
                    case 1:
                        role = RoleType.Craftsman;
                        break;
                    case 2:
                        role = RoleType.Laborer;
                        break;
                    default:
                        role = RoleType.Legionnaire;
                        break;
                }
                OrderDeck.Add(new OrderCardModel(cardName, description, role));
            }

            for (int i = 0; i < 5; i++)
            {
                JackDeck.Add(new JackCardModel());
            }
        }
    }
}