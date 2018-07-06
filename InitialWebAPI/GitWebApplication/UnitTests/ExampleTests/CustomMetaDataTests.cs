using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace UnitTests.ExampleTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class CustomMetaDataTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            BusinessLayer.Examples.Person testPerson = new BusinessLayer.Examples.Person();
            testPerson.Age = 20;
            testPerson.Height = 2;
            
        }
    }
}
