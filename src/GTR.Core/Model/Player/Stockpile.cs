#region

using GTR.Core.CardCollections;

#endregion

namespace GTR.Core.Model
{
    public class Stockpile : CardSourceTarget<OrderCardModel>
    {
        private readonly string _locationName;

        public Stockpile(string playerName = "")
        {
            _locationName = string.Format("Player {0} stockpile", playerName);
        }

        public override string LocationName
        {
            get { return _locationName; }
        }
    }
}