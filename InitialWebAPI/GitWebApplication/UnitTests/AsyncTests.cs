using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class AsyncTests
    {
        #region NotMocked

        [TestMethod]
        public async Task NotMocked_1()
        {
            WithAsyncStuff wClass = new WithAsyncStuff();
            int result = await wClass.MethodOne(1);
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public async Task NotMocked_3()
        {
            IWithAsyncStuff wClass = new WithAsyncStuff();
            int result = await wClass.MethodThree(string.Empty, 1, DateTime.Now);
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException), "Allowed a null date.")]
        public void NotMocked_3_ThrowEx_1()
        {
            IWithAsyncStuff wClass = new WithAsyncStuff();
            int result = wClass.MethodThree(string.Empty, 1, null).Result;
        }

        [TestMethod]
        public void NotMocked_3_ThrowEx_2()
        {
            IWithAsyncStuff wClass = new WithAsyncStuff();
            try
            {
                int result = wClass.MethodThree(string.Empty, 1, null).Result;
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.GetBaseException().GetType(), typeof(ArgumentNullException));
            }
        }

        #endregion

        #region NotMockedWrapped

        [TestMethod]
        public void NotMockedWrapped_3()
        {
            WithAsyncStuff aClass = new WithAsyncStuff();
            ClassUnderTest woaClass = new ClassUnderTest(aClass);
            int result = woaClass.MethodThree(string.Empty, 1, DateTime.Now);
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Allowed a null date.")]
        public void NotMockedWrapped_3_ThrowEx()
        {
            WithAsyncStuff aClass = new WithAsyncStuff();
            ClassUnderTest woaClass = new ClassUnderTest(aClass);
            int result = woaClass.MethodThree(string.Empty, 1, null);
        }

        #endregion

        #region Loose vs Strict

        [TestMethod]
        public void MockedTestMethod_Loose()
        {
            Mock<IWithAsyncStuff> mockAClass = new Mock<IWithAsyncStuff>(MockBehavior.Loose);

            int result = mockAClass.Object.MethodThree("", 1, DateTime.Now).Result;

            //Note: not 2 but zero.  Without a setup Loose returns defualt values which is 0 for int.
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        [ExpectedException(typeof(Moq.MockException), "Strick requires setup")]
        public void MockedTestMethod_Strict()
        {
            Mock<IWithAsyncStuff> mockAClass = new Mock<IWithAsyncStuff>(MockBehavior.Strict);

            int result = mockAClass.Object.MethodThree("", 1, DateTime.Now).Result;

            //Never makes it to assert.  Strict throws exception since we didn't setup MethodThree.
            Assert.IsTrue(false);
        }

        #endregion

        [TestMethod]
        public void MockedAndSetup_3()
        {
            Mock<IWithAsyncStuff> mockAClass = new Mock<IWithAsyncStuff>();

            //multiple setups for same method will match and return their specified value.
            mockAClass.Setup(m => m.MethodThree("Required", It.IsAny<int>(), It.IsAny<DateTime>())).Returns(Task.FromResult(10));
            mockAClass.Setup(m => m.MethodThree(It.IsAny<string>(), 5, It.IsAny<DateTime>())).Returns(Task.FromResult(20));

            ClassUnderTest woaClass = new ClassUnderTest(mockAClass.Object);

            int requiredString = woaClass.MethodThree("Required", 1, DateTime.Now);
            Assert.AreEqual(10, requiredString);

            int requiredInt = woaClass.MethodThree("", 5, DateTime.Now);
            Assert.AreEqual(20, requiredInt);

            //Last setup to match takes precidence so a catch all IsAny type setup has to be after more specific setups are used.
            mockAClass.Setup(m => m.MethodThree(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>())).Returns(Task.FromResult(30));

            int anyInput = woaClass.MethodThree("", 1, DateTime.Now);
            Assert.AreEqual(30, anyInput);

            mockAClass.Verify();
        }

        [TestMethod]
        public void MockedAndSetupWithCallback_3()
        {
            int i = 10;

            Mock<IWithAsyncStuff> mockAClass = new Mock<IWithAsyncStuff>();
            mockAClass.Setup(m => m.MethodThree(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>()))
                //.Callback(() => { }) //dont have to capture inputs
                .Callback<string, int, DateTime?>((aString, aInt, aDateTime) =>
                    {
                        //can do work here with passed in values
                        i += aInt;
                        Assert.IsTrue(!string.IsNullOrEmpty(aString));
                    })
                //.Returns(Task.FromResult(i + 30));     //Note i == 10 still in Returns section.
                //.ReturnsAsync(() => { return i + 30; }); //Note i == 15 still in ReturnsAsync section.
                .ReturnsAsync((string aString, int aInt, DateTime aDateTime) => 
                    {
                        //Note i == 15 already in this style Returns section.
                        return i + 30 + aInt;
                    }); 

            ClassUnderTest woaClass = new ClassUnderTest(mockAClass.Object);
            int result = woaClass.MethodThree("something", 5, DateTime.Now);

            //i got changed in the Callback section
            Assert.AreEqual(15, i);

            //Returns section controls actual results.  
            Assert.AreEqual(50, result); 

            mockAClass.Verify();
        }

    }

    public class ClassUnderTest
    {
        IWithAsyncStuff asyncStuff;
        public ClassUnderTest(IWithAsyncStuff asyncStuff)
        {
            this.asyncStuff = asyncStuff;
        }

        public int MethodThree(string someString, int input, DateTime? dateTime)
        {
            if (dateTime == null)
                throw new ArgumentNullException("Null dateTime");

            return asyncStuff.MethodThree(someString, input, dateTime).Result;
        }
    }

    public class WithAsyncStuff : IWithAsyncStuff
    {
        public WithAsyncStuff() { }

        /// <summary>
        /// Adds 1 to input. Not in interface.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> MethodOne(int input)
        {
            await Task.Delay(2000);
            return 1 + input;
        }

        /// <summary>
        /// This empliments the interface
        /// </summary>
        public async Task<int> MethodThree(string someString, int input, DateTime? dateTime)
        {
            if (dateTime == null)
                throw new ArgumentNullException("Null dateTime");

            await Task.Delay(2000);
            return 1 + input;
        }
    }

    public interface IWithAsyncStuff
    {
        /// <summary>
        /// Adds 1 to input.
        /// </summary>
        Task<int> MethodThree(string someString, int input, DateTime? dateTime);
    }
}
