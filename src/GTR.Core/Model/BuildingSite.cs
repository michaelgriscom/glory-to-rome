#region

using System.Collections.Specialized;
using GTR.Core.Buildings;
using GTR.Core.CardCollections;
using GTR.Core.Game;

#endregion

namespace GTR.Core.Model
{
    public class BuildingSite : CardModelBase
    {
        private BuildingFoundation _building;
        private BoundedCardTarget<OrderCardModel> _materials;

        public BuildingSite(MaterialType materialType, SiteType siteType = SiteType.InsideRome)
        {
            MaterialType = materialType;
            SiteType = siteType;

            Materials = new BoundedCardTarget<OrderCardModel>(MaterialType.MaterialWorth());
            Materials.CollectionChanged += MaterialsOnCollectionChanged;
            Materials.LocationName = "Building materials";

            BuildingFoundation = new BuildingFoundation();
        }

        public BoundedCardTarget<OrderCardModel> Materials
        {
            get { return _materials; }
            set
            {
                _materials = value;
                RaisePropertyChanged();
            }
        }

        public BuildingFoundation BuildingFoundation
        {
            get { return _building; }
            private set
            {
                _building = value;
                RaisePropertyChanged();
            }
        }

        public MaterialType MaterialType { get; }
        public SiteType SiteType { get; }

        public override string Name
        {
            get { return string.Concat(MaterialType, " building site"); }
        }

        private void MaterialsOnCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            if (notifyCollectionChangedEventArgs.Action != NotifyCollectionChangedAction.Add)
            {
                return;
            }
            if (Materials.MaxCapacity != Materials.Count)
            {
                return;
            }
            if (Complete != null)
            {
                // TODO: what should these args be?
                var buildingCompleteEventArgs = new BuildingCompletedEventArgs {BuildingSite = this};
                Complete(this, buildingCompleteEventArgs);
            }
        }

        internal event BuildingCompletedHandler Complete;
    }
}