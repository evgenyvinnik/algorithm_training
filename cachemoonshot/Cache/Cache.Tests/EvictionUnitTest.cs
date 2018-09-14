// © Evgeny Vinnik

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cache.Tests
{
    [TestClass]
    public class EvictionUnitTest
    {
        [TestMethod]
        public void TestCacheTestCacheLru()
        {
            uint nWay = 4;
            var eviction = new LruEvictionAlgorithm<int, int>();

            CacheDictionary<int, int> cacheDictionary = new CacheDictionary<int, int>(nWay, eviction);

            cacheDictionary.InsertEntry(1, 1);
            cacheDictionary.InsertEntry(2, 2);
            cacheDictionary.InsertEntry(3, 3);
            cacheDictionary.InsertEntry(4, 4);

            var entry = cacheDictionary.FindEntry(1);
            Assert.AreEqual(1, entry.Value);

            cacheDictionary.InsertEntry(5, 5);

            var entryMissing = cacheDictionary.FindEntry(1);
            Assert.IsNull(entryMissing);
        }

        [TestMethod]
        public void TestCacheTestCacheMru()
        {
            uint nWay = 4;
            var eviction = new MruEvictionAlgorithm<int, int>();

            CacheDictionary<int, int> cacheDictionary = new CacheDictionary<int, int>(nWay, eviction);

            cacheDictionary.InsertEntry(1, 1);
            cacheDictionary.InsertEntry(2, 2);
            cacheDictionary.InsertEntry(3, 3);
            cacheDictionary.InsertEntry(4, 4);

            var entry = cacheDictionary.FindEntry(1);
            Assert.AreEqual(1, entry.Value);

            cacheDictionary.InsertEntry(5, 5);

            var entryMissing = cacheDictionary.FindEntry(1);
            Assert.IsNull(entryMissing);
        }

        [TestMethod]
        public void TestCacheTestCacheMru2()
        {
            uint nWay = 4;
            var eviction = new MruEvictionAlgorithm<int, int>();

            CacheDictionary<int, int> cacheDictionary = new CacheDictionary<int, int>(nWay, eviction);

            cacheDictionary.InsertEntry(1, 1);
            cacheDictionary.InsertEntry(2, 2);
            cacheDictionary.InvalidateAndDelete(2, InvalidationSource.User);
            cacheDictionary.InsertEntry(3, 3);
            cacheDictionary.InsertEntry(4, 4);

            var entry = cacheDictionary.FindEntry(1);
            Assert.AreEqual(1, entry.Value);

            cacheDictionary.InsertEntry(5, 5);

            var entryMissing = cacheDictionary.FindEntry(1);
            Assert.IsNotNull(entryMissing);
        }
    }
}
