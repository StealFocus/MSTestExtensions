// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="ExpectedExceptionMessageAttribute.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the ExpectedExceptionMessageAttribute type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSTestExtensions
{
    using System;

    /// <summary>
    /// ExpectedExceptionMessageAttribute Class.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use to declare an expected Exception with a specific message.
    /// </para>
    /// <para>
    /// The MSTest attribute of "ExpectedExceptionAttribute" has the 
    /// property "ExpectedExceptionAttribute.Message" but this does not
    /// specify the expected Exception message but the message to be 
    /// logged to the test output.
    /// </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ExpectedExceptionMessageAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpectedExceptionMessageAttribute"/> class.
        /// </summary>
        /// <param name="exceptionType">The exception type.</param>
        /// <param name="exceptionMessage">The exception message.</param>
        public ExpectedExceptionMessageAttribute(Type exceptionType, string exceptionMessage)
        {
            this.ExceptionType = exceptionType;
            this.ExceptionMessage = exceptionMessage;
        }

        public Type ExceptionType
        {
            get; private set;
        }

        public string ExceptionMessage
        {
            get; private set;
        }
    }
}