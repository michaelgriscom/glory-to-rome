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
        private OrderCardModel _card;
        private BoundedCardTarget<OrderCardModel> _materials;

        public BuildingSite(MaterialType materialType, SiteType siteType = SiteType.InsideRome)
        {
            MaterialType = materialType;
            _siteType = siteType;

            Materials = new BoundedCardTarget<OrderCardModel>(MaterialType.MaterialWorth());
            Materials.CollectionChanged += MaterialsOnCollectionChanged;

            BuildingFoundation = new BoundedSourceTarget<OrderCardModel>(1);
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

        public OrderCardModel Card
        {
            get { return _card; }
            private set
            {
                _card = value;
                RaisePropertyChanged();
            }
        }

        public SiteType SiteType
        {
            get { return _siteType; }
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
                Complete(this, null);
            }
        }

        internal event BuildingCompletedHandler Complete;
    }
}