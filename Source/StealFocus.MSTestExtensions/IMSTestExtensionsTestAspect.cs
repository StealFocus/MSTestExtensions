// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IMSTestExtensionsTestAspect.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the IMSTestExtensionsTestAspect type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSTestExtensions
{
    using System.Runtime.Remoting.Messaging;

    internal interface IMSTestExtensionsTestAspect
    {
        /// <summary>
        /// Add a message sink.
        /// </summary>
        /// <param name="messageSink">An <see cref="IMessageSink"/>.</param>
        void AddMessageSink(IMessageSink messageSink);
    }
}