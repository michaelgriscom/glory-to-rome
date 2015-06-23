#region

using System;
using System.Collections;
using System.Collections.Generic;
using GTR.Core.Model;
using GTR.Core.Services;

#endregion

namespace GTR.Core.Buildings
{
    public static class BuildingEffectService
    {
        public static BuildingEffectBase GetBuildingEffect(OrderCardModel orderCard)
        {
            switch (orderCard.Name.ToUpper())
            {
                case "BAR":
                    return new BarEffect();
                case "FOUNTAIN":
                    return new FountainEffect();
                case "TEMPLE":
                    return new TempleEffect();
                case "DOCK":
                    return new DockEffect();
                default:
             Game.Game.MessageProvider.Display("Unknown building: " + orderCard.Name, MessageType.Error);
                    return new NullBuildingEffect();
            }
        }
    }
}