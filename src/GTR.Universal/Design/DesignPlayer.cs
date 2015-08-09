using GTR.Core.Game;

namespace GTR.Universal.Design
{
    public class DesignPlayer : Player
    {
        public DesignPlayer() : base("DesignPlayer", null, null)
        {
            this.Board = new DesignBoard();
        }
    }
}
