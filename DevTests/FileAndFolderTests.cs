using System;
using System.IO;
using System.Text;
using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using NUnitAutomationFramework.Framework.IO;

namespace NUnitAutomationFramework.DevTests
{
    [TestFixture]
    [AllureNUnit]
    [AllureDisplayIgnored]
    public class FileAndFolderTests
    {
        [Test(Description = "Testin That Can Copy Files")]
        [AllureTag("Framework Implementation")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureIssue("ISSUE-Reading_From_Folders")]
        [AllureTms("TMS-Reading_From_Folders")]
        [AllureOwner("Behrang Bina")]
        [AllureSuite("Framework")]
        [AllureSubSuite("FileAndFolders")]
        public void TestCanCopyFiles()
        {
            var uat = FileAndFolder.GetExecutionDirectory();
            var filename = "Test.txt";
            var path = Path.Combine(uat, filename);
            try
            {
                Assert.IsFalse(File.Exists(path));
                var newPath = Path.Combine(uat, SolutionFolders.Reports.ToString());
                Assert.IsFalse(File.Exists(Path.Combine(newPath, filename)));
                using (var fs = File.Create(path))
                {
                    var info = new UTF8Encoding(true).GetBytes("This is some text in the file.");
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
                FileAndFolder.CopyFile(filename, uat,filename,newPath);
                Console.WriteLine("Solution Dir: " + uat);
                Assert.IsTrue(File.Exists(Path.Combine(newPath, filename)));
                File.Delete(path);
                newPath = Path.Combine(newPath, filename);
                File.SetAttributes(newPath, FileAttributes.Normal);

                File.Delete(newPath);
                Assert.IsFalse(File.Exists(path));
                Assert.IsFalse(File.Exists(newPath));
            }
            finally
            {
               if( File.Exists(path)) File.Delete(path);
            }
        }

  
    }
}