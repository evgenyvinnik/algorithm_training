// © Evgeny Vinnik

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cache.Tests
{
    [TestClass]
    public class CacheUnitTest
    {
        [TestMethod]
        public void TestCacheCheckExceptions()
        {
            var mainStore = new MainStore<int, int>();
            uint nWay;
            uint cacheEntries;

            var ex = Assert.ThrowsException<ArgumentException>(() => new Cache<int, int>(null));
            StringAssert.Contains(ex.Message, "Main data store should isn't specified!");

            nWay = 6;
            cacheEntries = 128;
            ex = Assert.ThrowsException<ArgumentException>(() => new Cache<int, int>(mainStore, nWay, cacheEntries));
            StringAssert.Contains(ex.Message, "nWay ways should be a power of two!");

            nWay = 256;
            cacheEntries = 128;
            ex = Assert.ThrowsException<ArgumentException>(() => new Cache<int, int>(mainStore, nWay, cacheEntries));
            StringAssert.Contains(ex.Message, $"nWay ways should be less or equal {Cache<int, int>.MaxNWays}");

            nWay = 4;
            cacheEntries = 129;
            ex = Assert.ThrowsException<ArgumentException>(() => new Cache<int, int>(mainStore, nWay, cacheEntries));
            StringAssert.Contains(ex.Message, "Number of total cache entries should be a power of two!");

            nWay = 4;
            cacheEntries = 0xFFFFFFFF;
            ex = Assert.ThrowsException<ArgumentException>(() => new Cache<int, int>(mainStore, nWay, cacheEntries));
            StringAssert.Contains(ex.Message,
                $"Number of total cache entries should be less or equal than {Cache<int, int>.MaxCacheEntries}");

            nWay = 4;
            cacheEntries = 128;
            ex = Assert.ThrowsException<ArgumentException>(() =>
                new Cache<int, int>(mainStore, nWay, cacheEntries, null));
            StringAssert.Contains(ex.Message, "Eviction algorithm isn't specified!");

            nWay = 8;
            cacheEntries = 4;
            ex = Assert.ThrowsException<ArgumentException>(() => new Cache<int, int>(mainStore, nWay, cacheEntries));
            StringAssert.Contains(ex.Message,
                $"Number of total cache entries {cacheEntries} should more or equal than {nWay}");
        }

        [TestMethod]
        public void TestCacheTestCacheDictionariesNumber()
        {
            var mainStore = new MainStore<int, int>();
            var cache = new Cache<int, int>(mainStore);

            Assert.AreEqual(32, cache.GetCacheSets());
            Assert.AreEqual((uint)4, cache.nWay);
            Assert.AreEqual((uint)128, cache.TotalCacheEntries);
        }
    }
}
