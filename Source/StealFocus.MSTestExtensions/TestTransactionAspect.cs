// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="TestTransactionAspect.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the TestTransactionAspect type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSTestExtensions
{
    using System;
    using System.Runtime.Remoting.Messaging;
    using System.Security;
    using System.Transactions;

    internal class TestTransactionAspect : MSTestExtensionsTestAspect<TestTransactionAttribute>, IMessageSink, IMSTestExtensionsTestAspect
    {
        public IMessageSink NextSink { [SecurityCritical] get; private set; }

        /// <summary>
        /// SyncProcessMessage Method.
        /// </summary>
        /// <param name="msg">An <see cref="IMessage"/>. The message.</param>
        /// <returns>An <see cref="IMessage"/>. The reply.</returns>
        [SecurityCritical]
        public IMessage SyncProcessMessage(IMessage msg)
        {
            if (msg == null)
            {
                throw new ArgumentNullException("msg");
            }

            TestTransactionAttribute testTransactionAttribute = GetAttribute(msg);
            IMessage returnMethod;
            if (testTransactionAttribute != null)
            {
                using (TransactionScope scope = new TransactionScope(testTransactionAttribute.TransactionScopeOption, testTransactionAttribute.TransactionOptions))
                {
                    returnMethod = this.NextSink.SyncProcessMessage(msg);
                    if (testTransactionAttribute.Commit)
                    {
                        Console.WriteLine("Committing Transaction in Test Method.");
                        scope.Complete();
                    }
                }
            }
            else
            {
                returnMethod = this.NextSink.SyncProcessMessage(msg);
            }

            return returnMethod;
        }

        /// <summary>
        /// AsyncProcessMessage Method.
        /// </summary>
        /// <param name="msg">An <see cref="IMessage"/>. The message.</param>
        /// <param name="replySink">An <see cref="IMessageSink"/>. The reply.</param>
        /// <returns>An <see cref="IMessageSink"/>. The return.</returns>
        [SecurityCritical]
        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Adds a message sink.
        /// </summary>
        /// <param name="messageSink">An <see cref="IMessageSink"/>. The message sink.</param>
        public void AddMessageSink(IMessageSink messageSink)
        {
            this.NextSink = messageSink;
        }
    }
}
