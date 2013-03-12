// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MSTestExtensionsTestProperty.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the MSTestExtensionsTestProperty type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSTestExtensions
{
    using System;
    using System.Runtime.Remoting.Contexts;
    using System.Runtime.Remoting.Messaging;
    using System.Security;

    /// <typeparam name="T">The Test Aspect.</typeparam>
    internal class MSTestExtensionsTestProperty<T> : IContextProperty, IContributeObjectSink where T : IMessageSink, IMSTestExtensionsTestAspect, new()
    {
        /// <summary>
        /// Holds the assembly qualified name of the Test Aspect.
        /// </summary>
        private readonly string name = typeof(T).AssemblyQualifiedName;

        public string Name
        {
            [SecurityCritical]
            get { return this.name; }
        }

        /// <summary>
        /// IsNewContextOK Method.
        /// </summary>
        /// <param name="newCtx">The new context.</param>
        /// <returns>Whether the new context is okay.</returns>
        [SecurityCritical]
        public bool IsNewContextOK(Context newCtx)
        {
            return true;
        }

        /// <summary>
        /// Freeze Method.
        /// </summary>
        /// <param name="newContext">The new context.</param>
        [SecurityCritical]
        public void Freeze(Context newContext)
        {
        }

        /// <summary>
        /// GetObjectSink Method.
        /// </summary>
        /// <param name="obj">An <see cref="object"/>.</param>
        /// <param name="nextSink">An <see cref="IMessageSink"/>.</param>
        /// <returns>An <see cref="IMessageSink"/>. The sink.</returns>
        [SecurityCritical]
        public IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink)
        {
            T coreTestAspect = new T();
            coreTestAspect.AddMessageSink(nextSink);
            return coreTestAspect;
        }
    }
}
