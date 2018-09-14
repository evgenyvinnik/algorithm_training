// © Evgeny Vinnik

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
            Assert.AreEqual(n % d, CacheUtils.ModTwo(n, d));

            n = 3;
            d = 4;
            Assert.AreEqual(n % d, CacheUtils.ModTwo(n, d));

            n = 3;
            d = 8;
            Assert.AreEqual(n % d, CacheUtils.ModTwo(n, d));

            n = 0;
            d = 8;
            Assert.AreEqual(n % d, CacheUtils.ModTwo(n, d));

            n = 8;
            d = 8;
            Assert.AreEqual(n % d, CacheUtils.ModTwo(n, d));
        }

        [TestMethod]
        public void TestIsPowerTwo()
        {
            Assert.AreNotEqual(true, CacheUtils.IsPowerOfTwo(0));
            Assert.AreEqual(true, CacheUtils.IsPowerOfTwo(2));
            Assert.AreNotEqual(true, CacheUtils.IsPowerOfTwo(3));
            Assert.AreEqual(true, CacheUtils.IsPowerOfTwo(4));
        }
    }
}
