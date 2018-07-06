using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;
using Utility.Extensions;

namespace TemplateUnitTests.UtilityTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class DateTimeTests
    {
        [TestMethod]
        public void YearTests_test()
        {
            DateTime start = new DateTime(2014, 1, 1);

            int result = start.YearDifference("02/01/2017").Value;

            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void MonthTests_test()
        {
            DateTime start = new DateTime(2014, 1, 1);

            int result = start.MonthDifference("02/01/2017").Value;

            Assert.AreEqual(37, result);
        }

        [TestMethod]
        public void DayTests_test()
        {
            DateTime start = new DateTime(2014, 1, 1);

            int result = start.DayDifference("02/01/2017").Value;

            Assert.AreEqual(1127, result);
        }
    }
}