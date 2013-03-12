// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="ExpectedExceptionMessageAttributeTests.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the ExpectedExceptionMessageAttributeTests type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSTestExtensions.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ExpectedExceptionMessageAttributeTests : MSTestExtensionsTestClass
    {
        [TestMethod]
        [ExpectedExceptionMessage(typeof(TimeoutException), "My expected exception message.")]
        public void UnitTestInterceptionWithExpectedExceptionMessage()
        {
            throw new TimeoutException("My expected exception message.");
        }

        [TestMethod]
        [ExpectedException(typeof(MSTestExtensionsException), "An unexpected exception type was thrown.")]
        [ExpectedExceptionMessage(typeof(TimeoutException), "My expected exception message.")]
        public void UnitTestInterceptionWithExpectedExceptionMessageFail()
        {
            throw new TimeoutException("My UNexpected exception message.");
        }
    }
}
