using Microsoft.VisualStudio.TestTools.UnitTesting;
using Training;

namespace Training.UnitTests
{
    [TestClass]
    public class SubStringTest
    {
        private readonly SubString subString;

        public SubStringTest()
        {
            subString = new SubString();
        }

        [TestMethod]
        public void SubstringTesting()
        {
            var result = subString.IsSubstring("a", "b");

            Assert.IsFalse(result, "a is not a substring of b");
        }
    }
}
