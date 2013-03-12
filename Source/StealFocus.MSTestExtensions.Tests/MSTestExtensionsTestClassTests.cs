// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MSTestExtensionsTestClassTests.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the MSTestExtensionsTestClassTests type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSTestExtensions.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MSTestExtensionsTestClassTests : MSTestExtensionsTestClass
    {
        [TestMethod]
        [ExpectedException(typeof(MSTestExtensionsException))]
        public void UnitTestInterceptionWithNoCustomAttributes()
        {
            Console.WriteLine("Inside 'TestInterceptionWithNoAttributes' method.");
            throw new MSTestExtensionsException(); // Throw (and expect) an exception to be sure this method gets called
        }
    }
}
