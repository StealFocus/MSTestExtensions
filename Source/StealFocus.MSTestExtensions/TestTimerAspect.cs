// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="TestTimerAspect.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the TestTimerAspect type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSTestExtensions
{
    using System;
    using System.Globalization;
    using System.Runtime.Remoting.Messaging;
    using System.Security;

    internal class TestTimerAspect : MSTestExtensionsTestAspect<TestTimerAttribute>, IMessageSink, IMSTestExtensionsTestAspect
    {
        public IMessageSink NextSink
        {
            [SecurityCritical] get; private set;
        }

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

            DateTime timeBeforeTest = DateTime.Now;
            IMessage returnMessage = this.NextSink.SyncProcessMessage(msg);
            DateTime timeAfterTest = DateTime.Now;
            TestTimerAttribute testTimerAttribute = GetAttribute(msg);
            if (testTimerAttribute != null)
            {
                TimeSpan targetTestLength = testTimerAttribute.TestLength;
                TimeSpan actualTestLength = timeAfterTest - timeBeforeTest;
                if (actualTestLength > targetTestLength)
                {
                    string exceptionMessage = string.Format(CultureInfo.CurrentCulture, "The test exceeded the TestTimer constraint. Target test length '{0}', actual test length '{1}'.", targetTestLength, actualTestLength);
                    throw new MSTestExtensionsException(exceptionMessage);
                }
            }

            return returnMessage;
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
