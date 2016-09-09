#region

using System;
using GTR.Core.Engine;
using GTR.Core.Model;

#endregion

namespace GTR.Core.Buildings
{
    public delegate void BuildingCompletedHandler(object sender, BuildingCompletedEventArgs args);

    public delegate void PlayerActionHandler(object sender, PlayerActionEventArgs args);

    public class BuildingCompletedEventArgs
    {
        public BuildingSite BuildingSite;
    }

    public class PlayerActionEventArgs : EventArgs
    {
        internal GameTable GameTable { get; set; }
        internal Player Player { get; set; }
    }
}