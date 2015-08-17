#region

using GTR.Core.Game;

#endregion

namespace GTR.Universal.Design
{
    public class DesignPlayer : Player
    {
        public DesignPlayer() : base("DesignPlayer", null, null)
        {
            Board = new DesignBoard();
        }
    }
}