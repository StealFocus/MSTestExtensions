// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="ExpectedExceptionMessageAspect.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the ExpectedExceptionMessageAspect type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSTestExtensions
{
    using System;
    using System.Globalization;
    using System.Runtime.Remoting.Messaging;
    using System.Security;

    internal class ExpectedExceptionMessageAspect : MSTestExtensionsTestAspect<ExpectedExceptionMessageAttribute>, IMessageSink, IMSTestExtensionsTestAspect
    {
        public IMessageSink NextSink
        {
            [SecurityCritical] get; private set;
        }

        /// <summary>
        /// SyncProcessMessage Method.
        /// </summary>
        /// <param name="msg">An <see cref="IMessage"/>. The message.</param>
        /// <returns>An <see cref="IMessage"/>. The returned message.</returns>
        [SecurityCritical]
        public IMessage SyncProcessMessage(IMessage msg)
        {
            if (msg == null)
            {
                throw new ArgumentNullException("msg");
            }

            ExpectedExceptionMessageAttribute attribute = GetAttribute(msg);
            IMessage returnMethod;
            if (attribute != null)
            {
                returnMethod = this.NextSink.SyncProcessMessage(msg);
                ReturnMessage returnMessage = returnMethod as ReturnMessage;
                if (returnMessage != null)
                {
                    string actualExceptionAssemblyQualifiedName = returnMessage.Exception.GetType().AssemblyQualifiedName;
                    string expectedExceptionAssemblyQualifiedName = attribute.ExceptionType.AssemblyQualifiedName;
                    if (actualExceptionAssemblyQualifiedName != expectedExceptionAssemblyQualifiedName)
                    {
                        string exceptionMessage = string.Format(CultureInfo.CurrentCulture, "The type of exception thrown ('{0}') did not match the type expected ('{1}').", actualExceptionAssemblyQualifiedName, expectedExceptionAssemblyQualifiedName);
                        throw new MSTestExtensionsException(exceptionMessage);
                    }

                    if (returnMessage.Exception.Message != attribute.ExceptionMessage)
                    {
                        string exceptionMessage = string.Format(CultureInfo.CurrentCulture, "The exception message thrown ('{0}') did not match the message expected ('{1}').", returnMessage.Exception.Message, attribute.ExceptionMessage);
                        throw new MSTestExtensionsException(exceptionMessage);
                    }

                    returnMethod = this.FakeTargetResponse(msg);
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
