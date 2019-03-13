using System;
using System.IO;
using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using NUnitAutomationFramework.Framework;
using NUnitAutomationFramework.Framework.IO;
using NUnitAutomationFramework.Framework.Spreadsheet;

namespace NUnitAutomationFramework.DevTests
{
    [TestFixture]
    [AllureNUnit]
    [AllureDisplayIgnored]
    [Parallelizable]
    public class TestSpreadsheetUtility
    {
        private SpreadsheetUtility _excel;
        private string _exePath;
        private const string FileName = "DevLocators.xlsx";
        private string _filePath;
        private string _destFile;
        private string _destFolder;

        [SetUp]
        public void Setup()
        {
            _excel = new SpreadsheetUtility();
            _exePath = FileAndFolder.GetProjectPath();
            _filePath = Path.Combine(_exePath, "DevTests", SolutionFolders.Resources.ToString());

        }
        [Test(Description = "Can Get Excel Data Row By Row")]
        [AllureTag("Framework Implementation")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureIssue("ISSUE-Spreadsheeet-1")]
        [AllureTms("TMS-Spreadsheet-1")]
        [AllureOwner("Behrang Bina")]
        [AllureSuite("Framework")]
        [AllureSubSuite("SpreadsheetUtility")]
        public void GetRowByRowData()
        {
             Assert.IsTrue(File.Exists(Path.Combine(_filePath,FileName)));
            // copy file 
             _destFolder = Path.Combine(
                FileAndFolder.GetExecutionDirectory(),
                SolutionFolders.Resources.ToString(),
                SolutionFolders.Resources.ToString(),
                "HomePageLocator"
                );
            if (!Directory.Exists(_destFolder)) Directory.CreateDirectory(_destFolder);
            FileAndFolder.CopyFile(FileName, _filePath, FileName, _destFolder);

            _destFile = Path.Combine(_destFolder, FileName);
            Assert.IsTrue(File.Exists(_destFile));
            var allObjects = _excel.GetExcelFileObjects(_destFile);

            var rows = allObjects.GetLength(0);
            //var columns = allObjects.GetLength(1);
            for (var  r = 1; r < rows;r++)
            {
                var pageObject = new PageElement
                {
                    Name = allObjects.GetValue(r, 0).ToString(),
                    By = allObjects.GetValue(r, 1).ToString(),
                    Query = allObjects.GetValue(r, 2).ToString(),
                };
                Console.WriteLine(pageObject);
                Assert.NotNull(pageObject);
               
            }
        }

        [Test(Description = "Can Data of Given Row")]
        [AllureTag("Framework Implementation")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureIssue("ISSUE-Spreadsheeet-2")]
        [AllureTms("TMS-Spreadsheet-2")]
        [AllureOwner("Behrang Bina")]
        [AllureSuite("Framework")]
        [AllureSubSuite("SpreadsheetUtility")]
        public void TestGetARowData()
        {
            var uatRowId = 2;
            Assert.IsTrue(File.Exists(Path.Combine(_filePath, FileName)));
            // copy file 
            _destFolder = Path.Combine(
               FileAndFolder.GetExecutionDirectory(),
               SolutionFolders.Resources.ToString(),
               SolutionFolders.Resources.ToString(),
               "HomePageLocator"
               );
            if (!Directory.Exists(_destFolder)) Directory.CreateDirectory(_destFolder);
            FileAndFolder.CopyFile(FileName, _filePath, FileName, _destFolder);

            _destFile = Path.Combine(_destFolder, FileName);
            Assert.IsTrue(File.Exists(_destFile));
            
            var row = _excel.GetRow(_destFile, uatRowId);
            Console.WriteLine(row);
            Assert.NotNull(row,"Row Should not be null");
            Assert.IsTrue(row.Count == 3 , "Expecting 3 Columns in the row id: "+ uatRowId);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            File.Delete(_destFile);
            Directory.Delete(_destFolder);
        }
    }
}
