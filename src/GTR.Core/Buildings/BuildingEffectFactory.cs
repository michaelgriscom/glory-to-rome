#region

using System;
using System.Collections.Generic;

#endregion

namespace GTR.Core.Buildings
{
    public class BuildingEffectFactory
    {
        private readonly Dictionary<string, Func<BuildingEffectBase>> _typeDictionary
            = new Dictionary<string, Func<BuildingEffectBase>>();

        public BuildingEffectFactory()
        {
            RegisterEffects();
        }

        private void RegisterEffects()
        {
            Register<BarEffect>("BAR");
            Register<DockEffect>("DOCK");
            Register<FountainEffect>("FOUNTAIN");
            Register<TempleEffect>("TEMPLE");
        }

        private void Register<T>(string buildingName) where T : BuildingEffectBase, new()
        {
            buildingName = buildingName.ToUpper();
            if (_typeDictionary.ContainsKey(buildingName))
            {
                // TODO: we should just log an error and ignore the new one
                throw new ArgumentException(string.Format("Duplicate building: {0}", buildingName));
            }
            _typeDictionary.Add(buildingName, () => new T());
        }

        public BuildingEffectBase Create(string buildingName)
        {
            buildingName = buildingName.ToUpper();
            Func<BuildingEffectBase> buildingEffectCtor;
            _typeDictionary.TryGetValue(buildingName, out buildingEffectCtor);
            if (buildingEffectCtor == null)
            {
                return new NullBuildingEffect();
                //  throw new ArgumentException(String.Format("Building not found: {0}", buildingName));
            }
            return buildingEffectCtor();
        }
    }
}