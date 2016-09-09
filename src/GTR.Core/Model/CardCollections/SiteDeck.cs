#region

using System;
using System.Linq;
using GTR.Core.Game;

#endregion

namespace GTR.Core.Model.CardCollections
{
    public class SiteDeck : Deck<BuildingSite>
    {
        public SiteDeck(MaterialType materialType)
        {
            MaterialType = materialType;
        }

        public SiteDeck(ICardCollection<BuildingSite> sites)
        {
            var materialType = sites.First().MaterialType;
            foreach (var site in sites)
            {
                if (site.MaterialType != materialType)
                {
                    throw new ArgumentException($"Invalid site type: {site.MaterialType} expected {materialType}");
                }
                this.Add(site);
            }
        }

        public MaterialType MaterialType { get; private set; }
    }
}