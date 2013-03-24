MSTestExtensions
================
Extensions for Microsoft Test (MSTest) framework.

Examples:

    namespace Acme
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

Downloading
-----------
You can download the application from NuGet: [http://nuget.org/packages/StealFocus.MSTestExtensions](http://nuget.org/packages/StealFocus.MSTestExtensions)

Help
----
Contact the mailing list:
- <StealFocus-MSTestExtensions@yahoogroups.co.uk>
- [http://uk.groups.yahoo.com/group/StealFocus-MSTestExtensions](http://uk.groups.yahoo.com/group/StealFocus-MSTestExtensions)

You can see build status here: [http://build01.stealfocus.co.uk/ccnet/ViewFarmReport.aspx](http://build01.stealfocus.co.uk/ccnet/ViewFarmReport.aspx)
