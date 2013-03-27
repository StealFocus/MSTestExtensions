namespace StealFocus.MSTestExtensions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// DeploymentItemWithLongPath, much like DeploymentItem, but supports long file paths.
    /// It allows the source and destination deployment items to have long file paths (greater than 256 characters).
    /// See the unresolved Microsoft connect bug here:
    /// http://connect.microsoft.com/VisualStudio/feedback/details/753341/unit-test-deploymentitem-attribute-is-limits-file-paths-with-a-maximum-file-path-length-of-about-256-characters
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class DeploymentItemWithLongPathAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeploymentItemWithLongPathAttribute"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public DeploymentItemWithLongPathAttribute(string path)
        {
            this.Path = path;
            this.OutputDirectory = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeploymentItemWithLongPathAttribute"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="outputDirectory">The output directory.</param>
        public DeploymentItemWithLongPathAttribute(string path, string outputDirectory)
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
