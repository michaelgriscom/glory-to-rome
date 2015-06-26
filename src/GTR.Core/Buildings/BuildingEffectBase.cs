#region

using System;
using System.Collections.Generic;
using GTR.Core.Game;

#endregion

namespace GTR.Core.Buildings
{
    public abstract class BuildingEffectBase
    {
        public abstract MaterialType Material { get; }
        public abstract void ActivateBuilding(Player player, GameTable gameTable);
        public abstract void CompleteBuilding(Player player, GameTable gameTable);
        public abstract void DeactivateBuilding(Player player, GameTable gameTable);
    }
}