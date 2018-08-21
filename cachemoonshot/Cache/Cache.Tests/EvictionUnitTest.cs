// © Evgeny Vinnik

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
            cacheDictionary.InsertEntry(5, 5);

            Assert.AreEqual((uint)4, cacheDictionary.EntryCount);
        }

        [TestMethod]
        public void TestCacheTestCacheDictionariesNumber4()
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
            var res = cacheDictionary.Invalidate(2);
            Assert.AreEqual(true, res);
            cacheDictionary.InsertEntry(5, 5);

            Assert.AreEqual((uint)4, cacheDictionary.EntryCount);
        }

        [TestMethod]
        public void TestCacheTestCacheDictionariesNumber5()
        {
            uint nWay = 4;
            var eviction = new LruEvictionAlgorithm<int, int>();

            CacheDictionary<int, int> cacheDictionary = new CacheDictionary<int, int>(nWay, eviction);

            cacheDictionary.InsertEntry(1, 1);
            cacheDictionary.InsertEntry(2, 2);
            cacheDictionary.InsertEntry(3, 3);

            var res = cacheDictionary.Invalidate(4);
            Assert.AreEqual(false, res);


            Assert.AreEqual((uint)3, cacheDictionary.EntryCount);
        }

        [TestMethod]
        public void TestCacheTestCacheDictionariesNumber6()
        {
            uint nWay = 4;
            var eviction = new LruEvictionAlgorithm<int, int>();

            CacheDictionary<int, int> cacheDictionary = new CacheDictionary<int, int>(nWay, eviction);

            cacheDictionary.InsertEntry(1, 1);
            cacheDictionary.InsertEntry(2, 2);
            cacheDictionary.InsertEntry(3, 3);

            var entry = cacheDictionary.FindEntry(4);
            Assert.IsNull(entry);


            Assert.AreEqual((uint)3, cacheDictionary.EntryCount);
        }

        [TestMethod]
        public void TestCacheTestCacheDictionariesNumber7()
        {
            uint nWay = 4;
            var eviction = new LruEvictionAlgorithm<int, int>();

            CacheDictionary<int, int> cacheDictionary = new CacheDictionary<int, int>(nWay, eviction);

            cacheDictionary.InsertEntry(1, 1);
            cacheDictionary.InsertEntry(2, 2);
            cacheDictionary.InsertEntry(3, 3);

            var res = cacheDictionary.Invalidate(3);
            Assert.AreEqual(true, res);

            var entry = cacheDictionary.FindEntry(3);
            Assert.IsNull(entry);
        }


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

            var entryMissing = cacheDictionary.FindEntry(2);
            Assert.IsNull(entryMissing);
        }

        [TestMethod]
        public void TestCacheTestCacheDictionariesNumber8()
        {
            uint nWay = 4;
            var eviction = new LruEvictionAlgorithm<int, int>();

            CacheDictionary<int, int> cacheDictionary = new CacheDictionary<int, int>(nWay, eviction);

            cacheDictionary.InsertEntry(1, 1);
            cacheDictionary.InsertEntry(2, 2);
            cacheDictionary.InsertEntry(3, 3);
            cacheDictionary.InsertEntry(3, 4);

            var entry = cacheDictionary.FindEntry(3);
            Assert.AreEqual(4, entry.Value);
        }
    }
}
