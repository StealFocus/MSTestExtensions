// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="DeploymentItemWithLongPathAttributeTests.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the DeploymentItemWithLongPathAttributeTests type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSTestExtensions.Tests
{
    using System.Globalization;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ZetaLongPaths;

    [TestClass]
    public class DeploymentItemWithLongPathAttributeTests : MSTestExtensionsTestClass
    {
        private const int MaximumPathLength = 248 - 1 /* null terminating character */;
        private const int MaximumFilenameLength = 260;

        private const string ProjectPath = "StealFocus.MSTestExtensions.Tests\\";

        private const string ShortTestFile1DotTxt = "TestFile1.txt";
        private const string ShortTestFile1DotTxtPath = ProjectPath + ShortTestFile1DotTxt;
        private const string ShortOutputDirectoryName = "ShortDirectoryName";

        private const string TestFileADotTxt = "TestFileA.txt";

        private const string ShortDirectoryPath = ProjectPath + "TestData";

        private const string LongTestFile1DotTxt = "a very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very long file name 1.txt";
        private const string LongTestFile1DotTxtPath = ProjectPath + LongTestFile1DotTxt;
        private const string LongDirectoryPath = ProjectPath + "a very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very long directory name";
        private const string LongOutputDirectoryName = "a very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very long output directory name";

        [TestMethod]
        [DeploymentItemWithLongPath(ShortTestFile1DotTxtPath)]
        public void DeploymentItemWithLongPath_UsingShortFileNameAsSource_Succeeds()
        {
            string shortFilePath = Path.Combine(Directory.GetCurrentDirectory(), ShortTestFile1DotTxt);

            Assert.IsTrue(ZlpIOHelper.FileExists(shortFilePath), string.Format(CultureInfo.CurrentCulture, "File doesn't exist: '{0}'", shortFilePath));
        }

        [TestMethod]
        [DeploymentItemWithLongPath(ShortDirectoryPath)]
        public void DeploymentItemWithLongPath_UsingShortDirectoryNameAsSource_Succeeds()
        {
            string shortFilePath = Path.Combine(Directory.GetCurrentDirectory(), TestFileADotTxt);

            Assert.IsTrue(ZlpIOHelper.FileExists(shortFilePath), string.Format(CultureInfo.CurrentCulture, "File doesn't exist: '{0}'", shortFilePath));
        }

        [TestMethod]
        [DeploymentItemWithLongPath(ShortTestFile1DotTxtPath, ShortOutputDirectoryName)]
        public void DeploymentItemWithLongPath_UsingShortFileNameAsSourceAndShortOutputDirectoryName_Succeeds()
        {
            string shortFilePath = Path.Combine(Directory.GetCurrentDirectory(), ShortOutputDirectoryName, ShortTestFile1DotTxt);

            Assert.IsTrue(ZlpIOHelper.FileExists(shortFilePath), string.Format(CultureInfo.CurrentCulture, "File doesn't exist: '{0}'", shortFilePath));
        }

        [TestMethod]
        [DeploymentItemWithLongPath(ShortDirectoryPath, ShortOutputDirectoryName)]
        public void DeploymentItemWithLongPath_UsingShortDirectoryNameAsSourceAndShortOutputDirectoryName_Succeeds()
        {
            string shortFilePath = Path.Combine(Directory.GetCurrentDirectory(), ShortOutputDirectoryName, TestFileADotTxt);

            Assert.IsTrue(ZlpIOHelper.FileExists(shortFilePath), string.Format(CultureInfo.CurrentCulture, "File doesn't exist: '{0}'", shortFilePath));
        }

        [TestMethod]
        [DeploymentItemWithLongPath(LongTestFile1DotTxtPath)]
        public void DeploymentItemWithLongPath_UsingLongFileNameAsSource_Succeeds()
        {
            string longFilePath = Path.Combine(Directory.GetCurrentDirectory(), LongTestFile1DotTxt);

            Assert.IsTrue(longFilePath.Length > MaximumFilenameLength);

            Assert.IsTrue(ZlpIOHelper.FileExists(longFilePath), string.Format(CultureInfo.CurrentCulture, "File doesn't exist: '{0}'", longFilePath));
        }

        [TestMethod]
        [DeploymentItemWithLongPath(LongDirectoryPath)]
        public void DeploymentItemWithLongPath_UsingLongDirectoryNameAsSource_Succeeds()
        {
            string longFilePath = Path.Combine(Directory.GetCurrentDirectory(), TestFileADotTxt);

            string solutionPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            string sourcePath = ZlpPathHelper.GetAbsolutePath(LongDirectoryPath, solutionPath);

            Assert.IsTrue(sourcePath.Length >= MaximumPathLength, sourcePath.Length.ToString(CultureInfo.CurrentCulture));

            Assert.IsTrue(ZlpIOHelper.FileExists(longFilePath), string.Format(CultureInfo.CurrentCulture, "File doesn't exist: '{0}'", longFilePath));
        }

        [TestMethod]
        [DeploymentItemWithLongPath(LongTestFile1DotTxtPath, LongOutputDirectoryName)]
        public void DeploymentItemWithLongPath_UsingLongFileNameAsSourceAndLongOutputDirectoryName_Succeeds()
        {
            string longFilePath = Path.Combine(Directory.GetCurrentDirectory(), LongOutputDirectoryName, LongTestFile1DotTxt);

            Assert.IsTrue(longFilePath.Length > MaximumFilenameLength);

            Assert.IsTrue(ZlpIOHelper.FileExists(longFilePath), string.Format(CultureInfo.CurrentCulture, "File doesn't exist: '{0}'", longFilePath));
        }

        [TestMethod]
        [DeploymentItemWithLongPath(LongDirectoryPath, LongOutputDirectoryName)]
        public void DeploymentItemWithLongPath_UsingLongDirectoryNameAsSourceAndLongOutputDirectoryName_Succeeds()
        {
            string longFilePath = Path.Combine(Directory.GetCurrentDirectory(), LongOutputDirectoryName, TestFileADotTxt);

            Assert.IsTrue(longFilePath.Length > MaximumFilenameLength, longFilePath.Length.ToString(CultureInfo.CurrentCulture));

            Assert.IsTrue(ZlpIOHelper.FileExists(longFilePath), string.Format(CultureInfo.CurrentCulture, "File doesn't exist: '{0}'", longFilePath));
        }
    }
}
