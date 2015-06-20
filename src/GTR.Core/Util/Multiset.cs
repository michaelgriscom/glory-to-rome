#region

using System.Collections.Generic;

#endregion

namespace GTR.Core.Util
{
    public class Multiset<T>
    {
        private readonly Dictionary<T, int> _itemCounts;

        public Multiset()
        {
            _itemCounts = new Dictionary<T, int>();
        }

        public ICollection<T> UniqueItems
        {
            get { return _itemCounts.Keys; }
        }

        public int TotalCount { get; private set; }

        public void Add(T item, int amount = 1)
        {
            if (_itemCounts.ContainsKey(item))
            {
                _itemCounts[item] += amount;
            }
            else
            {
                _itemCounts.Add(item, amount);
            }
            TotalCount += amount;
        }

        public bool Remove(T item)
        {
            if (!_itemCounts.ContainsKey(item))
            {
                return false;
            }
            if (_itemCounts[item] == 1)
            {
                _itemCounts.Remove(item);
            }
            else
            {
                _itemCounts[item]--;
            }
            TotalCount--;
            return true;
        }

        public int Count(T item)
        {
            if (_itemCounts.ContainsKey(item))
            {
                return _itemCounts[item];
            }
            return 0;
        }
    }
}