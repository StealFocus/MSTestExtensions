namespace StealFocus.MSTestExtensions
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.Remoting.Messaging;
    using System.Security;
    using ZetaLongPaths;

    internal class DeploymentItemWithLongPathAspect : MSTestExtensionsTestAspect<DeploymentItemWithLongPathAttribute>, IMessageSink, IMSTestExtensionsTestAspect
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

            DeploymentItemWithLongPathAttribute deploymentItemWithLongPathAttribute = GetAttribute(msg);
            if (deploymentItemWithLongPathAttribute != null)
            {
                // by default, mstest sets the test results folder to format: %current directory%\TestResults\%username%_%computername% %datetime%\Out
                // TODO: get the value of the RelativePathRoot property setting in the current .testsettings file
                string solutionPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

                string source = ZlpPathHelper.GetAbsolutePath(deploymentItemWithLongPathAttribute.Path, solutionPath);
                string destinationPath = ZlpPathHelper.GetAbsolutePath(deploymentItemWithLongPathAttribute.OutputDirectory, Directory.GetCurrentDirectory());

                bool fileExists = ZlpIOHelper.FileExists(source);
                bool directoryExists = ZlpIOHelper.DirectoryExists(source);
                if ((!fileExists) && (!directoryExists))
                {
                    throw new MSTestExtensionsException("\"" + source + "\" does not exist.");
                }

                if (fileExists)
                {
                    CopyFile(source, ZlpPathHelper.Combine(destinationPath, ZlpPathHelper.GetFileNameFromFilePath(source)));
                }

                if (directoryExists)
                {
                    this.CopyDirectory(source, destinationPath);
                }
            }

            return this.NextSink.SyncProcessMessage(msg);
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

        private static void CopyFile(string source, string destination)
        {
            if (ZlpIOHelper.FileExists(destination))
            {
                // TODO: add warning to test results
            }
            else
            {
                string destinationPath = ZlpPathHelper.GetDirectoryPathNameFromFilePath(destination);

                if (!ZlpIOHelper.DirectoryExists(destinationPath))
                {
                    ZlpIOHelper.CreateDirectory(destinationPath);
                }

                ZlpIOHelper.CopyFile(source, destination, false);
            }
        }

        private void CopyDirectory(string source, string destination)
        {
            if (!ZlpIOHelper.DirectoryExists(destination))
            {
                ZlpIOHelper.CreateDirectory(destination);
            }

            ZlpFileInfo[] files = ZlpIOHelper.GetFiles(source, SearchOption.TopDirectoryOnly);
            foreach (ZlpFileInfo file in files)
            {
                string destinationFile = ZlpPathHelper.Combine(destination, ZlpPathHelper.GetFileNameFromFilePath(file.FullName));

                CopyFile(file.FullName, destinationFile);
            }

            ZlpDirectoryInfo[] sourceSubDirectories = ZlpIOHelper.GetDirectories(source);
            foreach (ZlpDirectoryInfo sourceSubDirectory in sourceSubDirectories)
            {
                string sourceSubDirectoryName = sourceSubDirectory.FullName.Substring(source.Length + 1);

                string destinationSubDirectoryName = ZlpPathHelper.Combine(destination, sourceSubDirectoryName);

                this.CopyDirectory(
                    ZlpPathHelper.Combine(source, sourceSubDirectoryName),
                    destinationSubDirectoryName);
            }
        }
    }
}
