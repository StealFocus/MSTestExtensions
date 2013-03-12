// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="TestTimerAttribute.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the TestTimerAttribute type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSTestExtensions
{
    using System;

    /// <summary>
    /// TestTimerAttribute Class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestTimerAttribute : Attribute
    {   
        /// <summary>
        /// Initializes a new instance of the <see cref="TestTimerAttribute"/> class.
        /// </summary>
        /// <param name="hours">An <see cref="int"/>. The hours.</param>
        /// <param name="minutes">An <see cref="int"/>. The minutes.</param>
        /// <param name="seconds">An <see cref="int"/>. The seconds.</param>
        /// <param name="milliseconds">An <see cref="int"/>. The milliseconds.</param>
        public TestTimerAttribute(int hours, int minutes, int seconds, int milliseconds)
        {
            this.TestLength = new TimeSpan(0, hours, minutes, seconds, milliseconds);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestTimerAttribute"/> class.
        /// </summary>
        /// <param name="minutes">An <see cref="int"/>. The minutes.</param>
        /// <param name="seconds">An <see cref="int"/>. The seconds.</param>
        /// <param name="milliseconds">An <see cref="int"/>. The milliseconds.</param>
        public TestTimerAttribute(int minutes, int seconds, int milliseconds)
        {
            this.TestLength = new TimeSpan(0, 0, minutes, seconds, milliseconds);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestTimerAttribute"/> class.
        /// </summary>
        /// <param name="seconds">An <see cref="int"/>. The seconds.</param>
        /// <param name="milliseconds">An <see cref="int"/>. The milliseconds.</param>
        public TestTimerAttribute(int seconds, int milliseconds)
        {
            this.TestLength = new TimeSpan(0, 0, 0, seconds, milliseconds);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestTimerAttribute"/> class.
        /// </summary>
        /// <param name="milliseconds">An <see cref="int"/>. The milliseconds.</param>
        public TestTimerAttribute(int milliseconds)
        {
            this.TestLength = new TimeSpan(0, 0, 0, 0, milliseconds);
        }
        
        public TimeSpan TestLength
        {
            get; private set;
        }
    }
}
