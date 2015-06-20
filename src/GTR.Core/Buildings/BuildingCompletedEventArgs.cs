#region

using System;
using GTR.Core.Game;
using GTR.Core.Model;

#endregion

namespace GTR.Core.Buildings
{
    public delegate void BuildingActivatedHandler(object sender, BuildingActivatedEventArgs args);

    public delegate void BuildingCompletedHandler(object sender, BuildingCompletedEventArgs args);

    public delegate void BuildingDeactivatedHandler(object sender, BuildingDeactivatedEventArgs args);

    public delegate void PlayerActionHandler(object sender, PlayerActionEventArgs args);

    public class BuildingCompletedEventArgs
    {
        public BuildingSite BuildingSite;
        internal GameTable GameTable { get; set; }
        internal Player Player { get; set; }
    }

    public class PlayerActionEventArgs : EventArgs
    {
        internal GameTable GameTable { get; set; }
        internal Player Player { get; set; }
    }
}