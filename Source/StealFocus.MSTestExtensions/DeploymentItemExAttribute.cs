namespace StealFocus.MSTestExtensions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// DeploymentItemEx, much like DeploymentItem, but supports long file paths
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class DeploymentItemExAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeploymentItemExAttribute"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public DeploymentItemExAttribute(string path)
        {
            this.Path = path;
            this.OutputDirectory = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeploymentItemExAttribute"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="outputDirectory">The output directory.</param>
        public DeploymentItemExAttribute(string path, string outputDirectory)
        {
            this.Path = path;
            this.OutputDirectory = outputDirectory;
        }

        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public string Path { get; private set; }

        /// <summary>
        /// Gets the output directory.
        /// </summary>
        /// <value>
        /// The output directory.
        /// </value>
        public string OutputDirectory { get; private set; }
    }
}
