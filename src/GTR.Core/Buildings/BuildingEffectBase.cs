#region

using GTR.Core.Engine;
using GTR.Core.Game;
using GTR.Core.Model;

#endregion

namespace GTR.Core.Buildings
{
    public abstract class BuildingEffectBase
    {
        public abstract MaterialType Material { get; }
        public abstract void ActivateBuilding(PlayerEngine playerEngine, GameTable gameTable);
        public abstract void CompleteBuilding(PlayerEngine playerEngine, GameTable gameTable);
        public abstract void DeactivateBuilding(PlayerEngine playerEngine, GameTable gameTable);
    }
}