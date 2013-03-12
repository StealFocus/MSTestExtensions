// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="TestTransactionAttribute.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the TestTransactionAttribute type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSTestExtensions
{
    using System;
    using System.Transactions;

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestTransactionAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestTransactionAttribute"/> class.
        /// </summary>
        public TestTransactionAttribute() : this(false, TransactionScopeOption.Required, new TransactionOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestTransactionAttribute"/> class.
        /// </summary>
        /// <param name="commit">Whether to commit.</param>
        public TestTransactionAttribute(bool commit) : this(commit, TransactionScopeOption.Required, new TransactionOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestTransactionAttribute"/> class.
        /// </summary>
        /// <param name="commit">Whether to commit.</param>
        /// <param name="transactionScopeOption">The Transaction Scope Option.</param>
        /// <param name="transactionOptions">The Transaction Options.</param>
        public TestTransactionAttribute(bool commit, TransactionScopeOption transactionScopeOption, TransactionOptions transactionOptions)
        {
            this.Commit = commit;
            this.TransactionScopeOption = transactionScopeOption;
            this.TransactionOptions = transactionOptions;
        }

        public bool Commit
        {
            get; private set;
        }

        public TransactionScopeOption TransactionScopeOption
        {
            get; private set;
        }

        public TransactionOptions TransactionOptions
        {
            get; private set;
        }
    }
}
