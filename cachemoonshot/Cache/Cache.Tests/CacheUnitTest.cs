// © Evgeny Vinnik

using System;
using System.Collections.Generic;
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
            StringAssert.Contains(ex.Message, "Main data store isn't specified!");

            nWay = 6;
            cacheEntries = 128;
            ex = Assert.ThrowsException<ArgumentException>(() => new Cache<int, int>(mainStore, nWay, cacheEntries));
            StringAssert.Contains(ex.Message, "N-Way should be a power of two!");

            nWay = 256;
            cacheEntries = 128;
            ex = Assert.ThrowsException<ArgumentException>(() => new Cache<int, int>(mainStore, nWay, cacheEntries));
            StringAssert.Contains(ex.Message, $"N-Way should be less or equal {Cache<int, int>.MaxNWays}");

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
            ex = Assert.ThrowsException<ArgumentException>(() => new Cache<int, int>(mainStore, nWay, cacheEntries, null));
            StringAssert.Contains(ex.Message, "Eviction algorithm isn't specified!");

            nWay = 8;
            cacheEntries = 4;
            ex = Assert.ThrowsException<ArgumentException>(() => new Cache<int, int>(mainStore, nWay, cacheEntries));
            StringAssert.Contains(ex.Message,
                $"Number of total cache entries {cacheEntries} should more or equal than {nWay} ways");
        }

        [TestMethod]
        public void TestCacheTestCacheDictionariesNumber()
        {
            var mainStore = new MainStore<int, int>();
            var cache = new Cache<int, int>(mainStore);

            Assert.AreEqual((uint)4, cache.NWay);
            Assert.AreEqual((uint)128, cache.TotalCacheEntries);
            Assert.AreEqual((uint)32, cache.CacheSets);
        }

        [TestMethod]
        public void TestCachePutValue()
        {
            var mainStore = new MainStore<int, int>();
            uint nWay = 4;
            uint cacheEntries = 8;
            var cache = new Cache<int, int>(mainStore, nWay, cacheEntries);

            Assert.AreEqual((uint) 2, cache.CacheSets);

            mainStore.PutValue(1, 1);
            mainStore.PutValue(2, 2);
            mainStore.PutValue(3, 3);
            mainStore.PutValue(4, 4);
            mainStore.PutValue(5, 5);
            mainStore.PutValue(6, 6);
            mainStore.PutValue(7, 7);
            mainStore.PutValue(8, 8);
            mainStore.PutValue(9, 9);
            mainStore.PutValue(10, 10);
            mainStore.PutValue(11, 11);
            mainStore.PutValue(12, 12);

            cache.PutValue(1, 1);
            cache.PutValue(2, 2);
            cache.PutValue(3, 3);
            cache.PutValue(4, 4);
            cache.PutValue(5, 5);
            cache.PutValue(6, 6);
            cache.PutValue(7, 7);
            cache.PutValue(8, 8);

            int value;

            // all good
            value = cache.TryGetValue(1);
            Assert.AreEqual(1, value);
            value = cache.TryGetValue(2);
            Assert.AreEqual(2, value);
            value = cache.TryGetValue(3);
            Assert.AreEqual(3, value);
            value = cache.TryGetValue(4);
            Assert.AreEqual(4, value);
            value = cache.TryGetValue(5);
            Assert.AreEqual(5, value);
            value = cache.TryGetValue(6);
            Assert.AreEqual(6, value);
            value = cache.TryGetValue(7);
            Assert.AreEqual(7, value);
            value = cache.TryGetValue(8);
            Assert.AreEqual(8, value);

            // not in the cache
            value = cache.TryGetValue(9);
            Assert.AreEqual(9, value);

            //check that 1 got evicted
            var invalidateResult = cache.DeleteValue(1);
            Assert.AreEqual(false, invalidateResult);

            invalidateResult = cache.DeleteValue(2);
            Assert.AreEqual(true, invalidateResult);

            //check exception on search value that is not present
            var ex = Assert.ThrowsException<KeyNotFoundException>(() => cache.TryGetValue(13));
            StringAssert.Contains(ex.Message, "The given key was not present in the dictionary");
        }
    }
}
