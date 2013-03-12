// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="TestTimerAttributeTests.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the TestTimerAttributeTests type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSTestExtensions.Tests
{
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestTimerAttributeTests : MSTestExtensionsTestClass
    {
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
    }
}
