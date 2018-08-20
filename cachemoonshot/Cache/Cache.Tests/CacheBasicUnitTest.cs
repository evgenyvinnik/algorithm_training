using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cache.Tests
{
    [TestClass]
    public class CacheBasicUnitTest
    {
        [TestMethod]
        public void TestModTwo()
        {
            uint n;
            uint d;

            n = 2;
            d = 4;
            Assert.AreEqual(CacheUtils.ModTwo(n, d), n % d);

            n = 3;
            d = 4;
            Assert.AreEqual(CacheUtils.ModTwo(n, d), n % d);

            n = 3;
            d = 8;
            Assert.AreEqual(CacheUtils.ModTwo(n, d), n % d);

            n = 0;
            d = 8;
            Assert.AreEqual(CacheUtils.ModTwo(n, d), n % d);

            n = 8;
            d = 8;
            Assert.AreEqual(CacheUtils.ModTwo(n, d), n % d);
        }

        [TestMethod]
        public void TestIsPowerTwo()
        {
            Assert.AreNotEqual(CacheUtils.IsPowerOfTwo(0), true);
            Assert.AreEqual(CacheUtils.IsPowerOfTwo(2), true);
            Assert.AreNotEqual(CacheUtils.IsPowerOfTwo(3), true);
            Assert.AreEqual(CacheUtils.IsPowerOfTwo(4), true);
        }


        //[TestMethod]
        //public void TestSmallCacheInt()
        //{


        //    var cache = new Cache<int, int>();
        //}


        //[TestMethod]
        //public void TestSmallCacheInt()
        //{


        //    var cache = new Cache<int,int>();
        //}

    }
}
