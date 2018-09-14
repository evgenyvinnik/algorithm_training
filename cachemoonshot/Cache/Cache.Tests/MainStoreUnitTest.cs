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

            Assert.AreEqual(1, mainStore.Count());

            mainStore.PutValue(2, 1);
            Assert.AreEqual(2, mainStore.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TestMainStoreGet()
        {
            var mainStore = new MainStore<int, int>();

            mainStore.PutValue(1, 2);

            Assert.AreEqual(2, mainStore.GetValue(1));

            mainStore.PutValue(2, 1);
            Assert.AreNotEqual(2, mainStore.GetValue(2));

            var n = mainStore.GetValue(3);
        }


        [TestMethod]
        public void TestMainStoreDelete()
        {
            var mainStore = new MainStore<int, int>();

            mainStore.PutValue(1, 2);
            mainStore.PutValue(2, 2);

            Assert.AreEqual(true, mainStore.DeleteValue(1));
            Assert.AreEqual(1, mainStore.Count());

            Assert.AreEqual(false, mainStore.DeleteValue(3));
        }
    }
}
