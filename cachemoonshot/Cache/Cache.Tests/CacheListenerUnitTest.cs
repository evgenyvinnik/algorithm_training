// © Evgeny Vinnik

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cache.Tests
{
    [TestClass]
    public class CacheListenerUnitTest
    {
        [TestMethod]
        public void TestCacheListenerTesting()
        {
            var mainStore = new MainStore<int, int>();
            uint nWay = 2;
            uint cacheEntries = 4;
            var cache = new Cache<int, int>(mainStore, nWay, cacheEntries);

            List<EventArgs> missEvents = new List<EventArgs>();

            cache.CacheMissListener += delegate (object sender, EventArgs e)
            {
                missEvents.Add(e);
            };

            List<InvalidationSource> invalidationEvents = new List<InvalidationSource>();
            cache.CacheEvictionListener += delegate (object sender, InvalidationEventArgs e)
            {
                invalidationEvents.Add(e.Source);
            };

            Assert.AreEqual((uint) 2, cache.CacheSets);

            mainStore.PutValue(1, 1);
            mainStore.PutValue(2, 2);
            mainStore.PutValue(3, 3);
            mainStore.PutValue(4, 4);
            mainStore.PutValue(5, 5);

            cache.PutValue(1, 1);
            cache.PutValue(2, 2);
            cache.PutValue(3, 3);
            cache.PutValue(4, 4);

            // not in the cache
            int value;
            value = cache.TryGetValue(5);
            Assert.AreEqual(5, value);
            //value 5 isn't in the cache this prompts us to go to the store and reinsert it.
            Assert.AreEqual(InvalidationSource.Eviction, invalidationEvents[0]);

            Assert.AreEqual(1, missEvents.Count);
            cache.PutValue(7, 7);
            Assert.AreEqual(InvalidationSource.Eviction, invalidationEvents[1]);

            cache.DeleteValue(2);
            Assert.AreEqual(InvalidationSource.User, invalidationEvents[2]);

            cache.PutValue(4, 16);
            Assert.AreEqual(InvalidationSource.Replacement, invalidationEvents[3]);
        }
    }
}
