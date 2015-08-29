#region

using System.Collections.Generic;
using GTR.Core.Model;

#endregion

namespace GTR.Core.CardCollections
{
    public class CardSourceTarget<T> : CardLocation<T>, ICardTarget<T>, ICardSource<T> where T : CardModelBase
    {

        public CardSourceTarget(string id = "") : base(id)
        {
        }

        public CardSourceTarget(IEnumerable<T> cards) : base(cards)
        {
        }

        public T ElementAt(int index)
        {
            return this[index];
        }

        public virtual bool CanAdd(T card)
        {
            return true;
        }
    }
}