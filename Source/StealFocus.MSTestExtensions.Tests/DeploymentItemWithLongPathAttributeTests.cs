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
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ZetaLongPaths;

    [TestClass]
    public class DeploymentItemWithLongPathAttributeTests : MSTestExtensionsTestClass
    {
        private const int MaximumPathLength = 248 - 1 /* null terminating character */;
        private const int MaximumFilenameLength = 260;
        private const string PathSeparator = @"\";
        private const string ProjectPath = "StealFocus.MSTestExtensions.Tests";
        private const string ShortTestFile1DotTxt = "TestFile1.txt";
        private const string ShortTestFile1DotTxtPath = ProjectPath + PathSeparator + ShortTestFile1DotTxt;
        private const string ShortOutputDirectoryName = "ShortDirectoryName";
        private const string TestFileADotTxt = "TestFileA.txt";
        private const string ShortDirectoryPath = ProjectPath + PathSeparator + "TestData";
        private const string LongTestFile1DotTxt = "a very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very long file name 1.txt";
        private const string LongTestFile1DotTxtPath = ProjectPath + PathSeparator + LongTestFile1DotTxt;
        private const string LongDirectoryPath = ProjectPath + PathSeparator + "a very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very long directory name";
        private const string LongOutputDirectoryName = "a very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very long output directory name";
        private const string TestFileContents = "This file is used for testing";

        private static readonly List<string> FilesToDelete = new List<string>();
        private static readonly List<string> DirectoriesToDelete = new List<string>();

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            string solutionPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string testResultsOutDirectoryPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Out");
            DirectoriesToDelete.Add(Path.Combine(testResultsOutDirectoryPath, ShortOutputDirectoryName));
            DirectoriesToDelete.Add(Path.Combine(testResultsOutDirectoryPath, LongOutputDirectoryName));
            FilesToDelete.Add(Path.Combine(testResultsOutDirectoryPath, ShortTestFile1DotTxt));
            FilesToDelete.Add(Path.Combine(testResultsOutDirectoryPath, ShortDirectoryPath));
            FilesToDelete.Add(Path.Combine(testResultsOutDirectoryPath, LongTestFile1DotTxt));
            FilesToDelete.Add(Path.Combine(testResultsOutDirectoryPath, TestFileADotTxt));

            // create TestFile1.txt
            string shortTestFile1DotTxtFullPath = Path.Combine(solutionPath, ShortTestFile1DotTxtPath);
            File.WriteAllText(shortTestFile1DotTxtFullPath, TestFileContents);
            FilesToDelete.Add(shortTestFile1DotTxtFullPath);
            
            // create TestData
            string shortDirectoryFullPath = Path.Combine(solutionPath, ShortDirectoryPath);
            Directory.CreateDirectory(shortDirectoryFullPath);
            DirectoriesToDelete.Add(shortDirectoryFullPath);
            
            // create TestData\TestFileA.txt
            string shortTestFileADotTxtFilePath = Path.Combine(shortDirectoryFullPath, TestFileADotTxt);
            ZlpIOHelper.WriteAllText(shortTestFileADotTxtFilePath, TestFileContents);
            FilesToDelete.Add(shortTestFileADotTxtFilePath);

            // create "a very ... long directory name"
            string longDirectoryFullPath = Path.Combine(solutionPath, LongDirectoryPath);
            ZetaLongPaths.ZlpIOHelper.CreateDirectory(longDirectoryFullPath);
            DirectoriesToDelete.Add(longDirectoryFullPath);

            // create "a very ... long directory name\TestFileA.txt"
            string longTestFileADotTxtFilePath = Path.Combine(longDirectoryFullPath, TestFileADotTxt);
            ZlpIOHelper.WriteAllText(longTestFileADotTxtFilePath, TestFileContents);
            FilesToDelete.Add(longTestFileADotTxtFilePath);

            // create "a very ... long file name 1.txt"
            string longTestFile1DotTxtFilePath = Path.Combine(solutionPath, LongTestFile1DotTxtPath);
            ZlpIOHelper.WriteAllText(longTestFile1DotTxtFilePath, TestFileContents);
            FilesToDelete.Add(longTestFile1DotTxtFilePath);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            foreach (string path in DirectoriesToDelete)
            {
                if (ZetaLongPaths.ZlpIOHelper.DirectoryExists(path))
                {
                    ZetaLongPaths.ZlpIOHelper.DeleteDirectory(path, true);
                }
            }

            foreach (string filePath in FilesToDelete)
            {
                if (ZetaLongPaths.ZlpIOHelper.FileExists(filePath))
                {
                    ZetaLongPaths.ZlpIOHelper.DeleteFile(filePath);
                }
            }
        }

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
