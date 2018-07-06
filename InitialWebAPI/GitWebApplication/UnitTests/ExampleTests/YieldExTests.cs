using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using BusinessLayer;

namespace UnitTests.ExampleTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class YieldTests
    {
        [TestMethod]
        public void YieldTest()
        {
            int[] values = YieldEx.GetNumbers(10);
            Assert.IsTrue(values.Length == 10);

            int[] manyValues = YieldEx.GetNumbers(500);
            Assert.IsTrue(manyValues.Length == 100);
        }
    }
}
