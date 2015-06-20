#region

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace GTR.Core.Buildings
{
    internal interface IBuildingReader : IEnumerable<Building>
    {
    }

    internal class TestBuildingCreator : IBuildingReader
    {
        private readonly List<Type> _availableBuildingEffects = new List<Type>
        {
            typeof (BarEffect),
            typeof (TempleEffect),
            typeof (DockEffect)
        };

        private readonly int cardCount = 60;

        public IEnumerator<Building> GetEnumerator()
        {
            for (int cardNum = 0; cardNum < cardCount; cardNum++)
            {
                Type buildingEffectType = _availableBuildingEffects[cardNum%_availableBuildingEffects.Count];
                BuildingEffectBase buildingEffect = (BuildingEffectBase) (Activator.CreateInstance(buildingEffectType));
                Building building = new Building(buildingEffectType.Name, buildingEffect, buildingEffect.Material);
                yield return building;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}