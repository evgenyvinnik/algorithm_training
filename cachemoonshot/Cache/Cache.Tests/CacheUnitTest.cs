// © Evgeny Vinnik

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cache.Tests
{
    [TestClass]
    public class CacheUnitTest
    {
        [TestMethod]
        public void TestCacheAdd()
        {
            var mainStore = new MainStore<int, int>();
            var cache = new Cache<int,int>(mainStore, 2, 4);

            //Assert.ThrowsException<>()
        }
    }
}
