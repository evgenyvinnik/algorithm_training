// © Evgeny Vinnik

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cache.Tests
{
    [TestClass]
    public class CacheDictionaryUnitTest
    {
        [TestMethod]
        public void TestCacheCheckExceptions()
        {
            uint nWay = 4;
            var eviction = new LruEvictionAlgorithm<int, int>();

            CacheDictionary<int, int> cacheDictionary = new CacheDictionary<int, int>(nWay, eviction);

            var orig = 1;
            cacheDictionary.InsertEntry(1, orig);
            Assert.AreEqual(orig, cacheDictionary.FindEntry(1).Value);
        }

        [TestMethod]
        public void TestCacheTestCacheDictionariesNumber()
        {
            uint nWay = 4;
            var eviction = new LruEvictionAlgorithm<int, int>();

            CacheDictionary<int, int> cacheDictionary = new CacheDictionary<int, int>(nWay, eviction);

            cacheDictionary.InsertEntry(1, 2);
            cacheDictionary.InsertEntry(2, 2);
            cacheDictionary.InsertEntry(3, 2);
            cacheDictionary.InsertEntry(4, 2);

            cacheDictionary.InsertEntry(5, 2);

            Assert.AreEqual((uint)4, cacheDictionary.EntryCount);
        }

        [TestMethod]
        public void TestCacheTestCacheDictionariesNumber2()
        {
            uint nWay = 4;
            var eviction = new LruEvictionAlgorithm<int, int>();

            CacheDictionary<int, int> cacheDictionary = new CacheDictionary<int, int>(nWay, eviction);

            cacheDictionary.InsertEntry(1, 1);
            cacheDictionary.InsertEntry(2, 2);
            cacheDictionary.InsertEntry(3, 3);
            cacheDictionary.InsertEntry(4, 4);
            cacheDictionary.InsertEntry(5, 5);

            Assert.AreEqual((uint)4, cacheDictionary.EntryCount);
        }

        [TestMethod]
        public void TestCacheTestCacheDictionariesNumber3()
        {
            uint nWay = 4;
            var eviction = new LruEvictionAlgorithm<int, int>();

            CacheDictionary<int, int> cacheDictionary = new CacheDictionary<int, int>(nWay, eviction);

            cacheDictionary.InsertEntry(1, 1);
            cacheDictionary.InsertEntry(2, 2);
            cacheDictionary.InsertEntry(3, 3);
            var entry = cacheDictionary.FindEntry(1);
            Assert.AreEqual(1, entry.Value);
            cacheDictionary.InsertEntry(4, 4);

            Assert.AreEqual((uint)4, cacheDictionary.EntryCount);
        }
    }
}
