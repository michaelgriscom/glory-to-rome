#region

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using GTR.Core.CardCollections;
using GTR.Core.Game;
using GTR.Core.Model;
using GTR.Core.Util;

#endregion

namespace GTR.Core.Buildings
{
    internal class Building : ICardTarget<OrderCardModel>
    {
        private BuildingSite _buildingSite;
        private BuildingEffectBase _effect;
        private Stack<OrderCardModel> _materials;

        internal Building(string name, BuildingEffectBase effect, MaterialType material)
        {
            Name = name;
            _effect = effect;
            Material = material;
        }

        public MaterialType Material { get; private set; }
        public string Name { get; private set; }

        public void Add(OrderCardModel card)
        {
            _materials.Push(card);
            if (IsComplete())
            {
                if (Complete != null)
                {
                    Complete(this, null);
                }
                if (Activated != null)
                {
                    Activated(this, null);
                }
            }
        }

        public bool CanAdd(OrderCardModel card)
        {
            return (card.GetMaterialType() == _buildingSite.MaterialType) && !IsComplete();
        }

        public IEnumerator<OrderCardModel> GetEnumerator()
        {
            return _materials.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public string LocationName
        {
            get { return Name; }
        }

        internal event BuildingActivatedHandler Activated;
        internal event BuildingCompletedHandler Complete;
        internal event BuildingDeactivatedHandler Deactivated;

        public void Deactivate()
        {
            if (Deactivated != null)
            {
                Deactivated(this, null);
            }
        }

        internal bool IsComplete()
        {
            Debug.Assert(_materials.Count <= GetRequiredMaterialCount(), "Building has too many materials.");
            return GetRequiredMaterialCount() <= _materials.Count;
        }

        internal void LayFoundation(BuildingSite site)
        {
            _buildingSite = site;
            _materials = new Stack<OrderCardModel>(GetRequiredMaterialCount());
        }

        private int GetRequiredMaterialCount()
        {
            return Material.MaterialWorth();
        }
    }
}