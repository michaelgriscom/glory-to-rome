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
        private readonly SiteType _siteType;
        private BoundedSourceTarget<OrderCardModel> _building;
        private BoundedCardTarget<OrderCardModel> _materials;

        public BuildingSite(MaterialType materialType, SiteType siteType = SiteType.InsideRome)
        {
            MaterialType = materialType;
            _siteType = siteType;

            Materials = new BoundedCardTarget<OrderCardModel>(MaterialType.MaterialWorth());
            Materials.CollectionChanged += MaterialsOnCollectionChanged;
            Materials.LocationName = "Building materials";

            BuildingFoundation = new BoundedSourceTarget<OrderCardModel>(1) {LocationName = "Building foundation"};
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

        public BoundedSourceTarget<OrderCardModel> BuildingFoundation
        {
            get { return _building; }
            private set
            {
                _building = value;
                RaisePropertyChanged();
            }
        }

        public MaterialType MaterialType { get; private set; }

        public SiteType SiteType
        {
            get { return _siteType; }
        }

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