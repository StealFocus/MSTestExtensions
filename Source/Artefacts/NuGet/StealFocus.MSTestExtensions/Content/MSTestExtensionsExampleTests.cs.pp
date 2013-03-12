namespace $rootnamespace$
{
    using System;
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using StealFocus.MSTestExtensions;

    /// <remarks>
    /// Needs to inherit from <see cref="MSTestExtensionsTestClass" />.
    /// </remarks>
    [TestClass]
    public class MSTestExtensionsExampleTests : MSTestExtensionsTestClass
    {
        [TestMethod]
        [TestTransaction]
        public void UnitTestInterceptionWithTestTransaction()
        {
            Console.WriteLine("Inside 'TestInterceptionWithTestTransaction' method.");
        }

        [TestMethod]
        [TestTransaction(true)]
        public void UnitTestInterceptionWithTestTransactionCommitted()
        {
            Console.WriteLine("Inside 'TestInterceptionWithTestTransaction' method.");
        }

        [TestMethod]
        [TestTimer(500)]
        public void UnitTestInterceptionWithTestTimerSuccess()
        {
        }

        [TestMethod]
        [ExpectedException(typeof(MSTestExtensionsException))]
        [TestTimer(10)]
        public void UnitTestInterceptionWithTestTimerFail()
        {
            Thread.Sleep(100);
        }

        [TestMethod]
        [ExpectedExceptionMessage(typeof(TimeoutException), "My expected exception message.")]
        public void UnitTestInterceptionWithExpectedExceptionMessage()
        {
            throw new TimeoutException("My expected exception message.");
        }

        [TestMethod]
        [ExpectedException(typeof(MSTestExtensionsException))]
        [ExpectedExceptionMessage(typeof(TimeoutException), "My expected exception message.")]
        public void UnitTestInterceptionWithExpectedExceptionMessageFail()
        {
            throw new TimeoutException("My UNexpected exception message.");
        }
    }
}
