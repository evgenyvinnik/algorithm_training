// © Evgeny Vinnik

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cache.Tests
{
    [TestClass]
    public class MainStoreUnitTest
    {
        [TestMethod]
        public void TestMainStoreAdd()
        {
            var mainStore = new MainStore<int, int>();

            mainStore.PutValue(1, 1);
            mainStore.PutValue(1, 2);

            Assert.AreEqual(mainStore.Count(), 1);

            mainStore.PutValue(2, 1);
            Assert.AreEqual(mainStore.Count(), 2);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TestMainStoreGet()
        {
            var mainStore = new MainStore<int, int>();

            mainStore.PutValue(1, 2);

            Assert.AreEqual(mainStore.GetValue(1), 2);

            mainStore.PutValue(2, 1);
            Assert.AreNotEqual(mainStore.GetValue(2), 2);

            var n = mainStore.GetValue(3);
        }


        [TestMethod]
        public void TestMainStoreDelete()
        {
            var mainStore = new MainStore<int, int>();

            mainStore.PutValue(1, 2);
            mainStore.PutValue(2, 2);

            Assert.AreEqual(mainStore.DeleteValue(1), true);
            Assert.AreEqual(mainStore.Count(), 1);

            Assert.AreEqual(mainStore.DeleteValue(3), false);
        }
    }
}
