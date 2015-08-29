﻿#region

using GTR.Core.CardCollections;

#endregion

namespace GTR.Core.Model
{
    public class CompletedBuildings : CardSourceTarget<OrderCardModel>
    {
        public CompletedBuildings(string player = "") : base(string.Format("Player {0} completed buildings", player))
        {
        }
    }
}