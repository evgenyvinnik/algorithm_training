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
            Assert.AreEqual(cacheDictionary.FindEntry(1).Value, orig);
        }

        [TestMethod]
        public void TestCacheTestCacheDictionariesNumber()
        {

        }
    }
}
