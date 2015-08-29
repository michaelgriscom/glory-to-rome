#region

using GTR.Core.CardCollections;

#endregion

namespace GTR.Core.Model
{
    public class Vault : BoundedCardTarget<OrderCardModel>
    {
        public Vault(string playerName = "") : base(string.Format("Player {0} vault", playerName))
        {
        }
    }
}