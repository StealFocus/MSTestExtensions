// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="TestTransactionAttributeTests.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the TestTransactionAttributeTests type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSTestExtensions.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestTransactionAttributeTests : MSTestExtensionsTestClass
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
    }
}
