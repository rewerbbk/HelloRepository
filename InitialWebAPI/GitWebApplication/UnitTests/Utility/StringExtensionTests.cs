using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;
using Utility.Extensions;

namespace TemplateUnitTests.UtilityTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class StringExtensionTests
    {

        [TestMethod]
        public void StringTailTest()
        {
            string testString = "abcdefghijk";
            string result = testString.Tail(3);

            Assert.AreEqual("ijk", result);
        }

        [TestMethod]
        public void StringTailTest_Max()
        {
            string testString = "abcdefghijk";
            string result = testString.Tail(11);

            Assert.AreEqual("abcdefghijk", result);
        }

        [TestMethod]
        public void StringTailTest_None()
        {
            string testString = "abcdefghijk";
            string result = testString.Tail(0);

            Assert.AreEqual("", result);
        }

        [ExpectedException(typeof(IndexOutOfRangeException))]
        [TestMethod]
        public void StringTailTest_ToLong()
        {
            string testString = "abcdefghijk";
            string result = testString.Tail(20);
        }

        [ExpectedException(typeof(IndexOutOfRangeException))]
        [TestMethod]
        public void StringTailTest_Negative()
        {
            string testString = "abcdefghijk";
            string result = testString.Tail(-5);
        }
    }
}