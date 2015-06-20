#region

using System.Collections.Generic;
using GTR.Core.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace GTR.Core.UnitTests.UtilTests
{
    [TestClass]
    public class MultisetTests
    {
        [TestMethod]
        public void EmptyMultiset()
        {
            Multiset<int> set = new Multiset<int>();
            Assert.AreEqual(0, set.TotalCount);
        }

        [TestMethod]
        public void NonexistentItem()
        {
            Multiset<int> set = new Multiset<int>();
            Assert.AreEqual(0, set.Count(1));
        }

        [TestMethod]
        public void SingleItem()
        {
            int item = 1;

            Multiset<int> set = new Multiset<int>();
            set.Add(item);
            Assert.AreEqual(1, set.Count(item));
        }

        [TestMethod]
        public void SingleItemTotal()
        {
            int item = 1;

            Multiset<int> set = new Multiset<int>();
            set.Add(item);
            Assert.AreEqual(1, set.TotalCount);
            Assert.AreEqual(1, set.UniqueItems.Count);
        }

        [TestMethod]
        public void MultipleOfItem()
        {
            int item = 50;
            int count = 100;

            Multiset<int> set = new Multiset<int>();
            for (int i = 0; i < count; i++)
            {
                set.Add(item);
            }
            Assert.AreEqual(count, set.Count(item));
            Assert.AreEqual(1, set.UniqueItems.Count);
        }

        [TestMethod]
        public void MultipleOfItemAtOnce()
        {
            int item = 50;
            int count = 100;

            Multiset<int> set = new Multiset<int>();
            set.Add(item, count);
            Assert.AreEqual(count, set.Count(item));
            Assert.AreEqual(1, set.UniqueItems.Count);
        }

        [TestMethod]
        public void MultipleOfItemTotal()
        {
            int item = 50;
            int count = 100;

            Multiset<int> set = new Multiset<int>();
            for (int i = 0; i < count; i++)
            {
                set.Add(item);
            }
            Assert.AreEqual(count, set.TotalCount);
        }

        [TestMethod]
        public void MultipleItemsTotal()
        {
            int uniqueItemCount = 50;
            int itemCount = 100;
            string[] items = new string[uniqueItemCount];

            Multiset<string> set = new Multiset<string>();

            for (int i = 0; i < uniqueItemCount; i++)
            {
                string item = "Item #" + i;
                items[i] = item;
                set.Add(item, itemCount);
            }

            int totalCount = itemCount*uniqueItemCount;
            Assert.AreEqual(uniqueItemCount, set.UniqueItems.Count);
            Assert.AreEqual(totalCount, set.TotalCount);
        }

        [TestMethod]
        public void MultipleItemsCounts()
        {
            int uniqueItemCount = 50;
            int itemCount = 100;
            string[] items = new string[uniqueItemCount];
            Multiset<string> set = new Multiset<string>();

            for (int i = 0; i < uniqueItemCount; i++)
            {
                string item = "Item #" + i;
                items[i] = item;
                set.Add(item, itemCount);
            }

            int totalCount = itemCount*uniqueItemCount;

            foreach (string item in items)
            {
                Assert.AreEqual(itemCount, set.Count(item));
            }

            Assert.AreEqual(totalCount, set.TotalCount);
        }

        [TestMethod]
        public void MultipleItemsIterator()
        {
            int uniqueItemCount = 50;
            int itemCount = 100;
            HashSet<string> items = new HashSet<string>();
            Multiset<string> set = new Multiset<string>();

            for (int i = 0; i < uniqueItemCount; i++)
            {
                string item = "Item #" + i;
                items.Add(item);
                set.Add(item, itemCount);
            }

            foreach (string item in set.UniqueItems)
            {
                bool exists = items.Remove(item);
                Assert.IsTrue(exists);
            }
            Assert.AreEqual(0, items.Count);
        }
    }
}