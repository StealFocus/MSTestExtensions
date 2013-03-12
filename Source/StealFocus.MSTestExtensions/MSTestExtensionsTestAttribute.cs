// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MSTestExtensionsTestAttribute.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the MSTestExtensionsTestAttribute type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSTestExtensions
{
    using System;
    using System.Runtime.Remoting.Activation;
    using System.Runtime.Remoting.Contexts;
    using System.Security;

    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class MSTestExtensionsTestAttribute : ContextAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MSTestExtensionsTestAttribute"/> class. 
        /// </summary>
        public MSTestExtensionsTestAttribute() : base("MSTestExtensionsTest")
        {
        }

        /// <summary>
        /// Gets the Properties for the new Context.
        /// </summary>
        /// <param name="msg">The message.</param>
        [SecurityCritical]
        public override void GetPropertiesForNewContext(IConstructionCallMessage msg)
        {
            if (msg == null)
            {
                throw new ArgumentNullException("msg");
            }

            msg.ContextProperties.Add(new MSTestExtensionsTestProperty<TestTimerAspect>());
            msg.ContextProperties.Add(new MSTestExtensionsTestProperty<TestTransactionAspect>());
            msg.ContextProperties.Add(new MSTestExtensionsTestProperty<ExpectedExceptionMessageAspect>());
        }
    }
}