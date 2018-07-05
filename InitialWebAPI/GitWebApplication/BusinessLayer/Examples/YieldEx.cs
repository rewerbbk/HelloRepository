using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLayer
{
    public static class YieldEx
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

        public static int[] GetNumbers(int amount)
        {
            List<int> results = new List<int>();
            foreach(int n in GenerateNumbers())
            {
                if (n >= amount)
                    break;

                results.Add(n);
            }

            return results.ToArray();
        }

        private static IEnumerable GenerateNumbers()
        {
            for(int i = 0; i < 100; i++)
            {
                yield return i;
            }
        }
    }

}
