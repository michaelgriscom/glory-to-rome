#region

using GTR.Core.Game;

#endregion

namespace GTR.Core.Buildings
{
    internal abstract class BuildingEffectBase
    {
        protected Building Building;
        public abstract MaterialType Material { get; }
        public abstract void ActivateBuilding(Player player, GameTable gameTable);
        public abstract void DeactivateBuilding(Player player, GameTable gameTable);

        internal void Attach(Building building)
        {
            Building = building;
            building.Activated += OnBuildingActivated;
            building.Complete += OnCompleted;
            building.Deactivated += OnBuildingDectivated;
        }

        protected virtual void OnBuildingActivated(object sender, BuildingActivatedEventArgs args)
        {
            // do nothing, let subclasses optionally choose what to do
        }

        protected virtual void OnBuildingDectivated(object sender, BuildingDeactivatedEventArgs args)
        {
        }

        protected virtual void OnCompleted(object sender, BuildingCompletedEventArgs args)
        {
        }
    }
}