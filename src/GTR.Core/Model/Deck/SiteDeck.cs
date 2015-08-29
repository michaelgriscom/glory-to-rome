#region

using GTR.Core.Game;

#endregion

namespace GTR.Core.Model
{
    public class SiteDeck : Deck<BuildingSite>
    {
        public SiteDeck(MaterialType materialType)
        {
            MaterialType = materialType;
        }

        public MaterialType MaterialType { get; private set; }
    }
}