using System.IO;
using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using NUnitAutomationFramework.Framework;
using NUnitAutomationFramework.Framework.IO;

namespace NUnitAutomationFramework.DevTests
{
    [TestFixture]
    [AllureNUnit]
    [AllureDisplayIgnored]
    public class TestCanGetPageElementCollection
    {
         private const string FileName = "DevLocators.xlsx";
        [Test(Description = "Can Get Collection Of Page Objject Elements From a Excel File")]
        [AllureTag("Framework Implementation")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureIssue("ISSUE-Spreadsheeet-3")]
        [AllureTms("TMS-Spreadsheet-3")]
        [AllureOwner("Behrang Bina")]
        [AllureSuite("Framework")]
        [AllureSubSuite("SpreadsheetUtility")]
        public void CanGetPageElementCollection()
        {
            //Setup
            var destFolderName = "PageObject";
            var destFolder = Path.Combine(
               FileAndFolder.GetExecutionDirectory(),
               SolutionFolders.Resources.ToString(),
               SolutionFolders.Resources.ToString(),
               destFolderName
               );
            if (!Directory.Exists(destFolder)) Directory.CreateDirectory(destFolder);

            
            var exePath = FileAndFolder.GetProjectPath();
            var filePath = Path.Combine(exePath, "DevTests", SolutionFolders.Resources.ToString());
            FileAndFolder.CopyFile(FileName, filePath, FileName, destFolder);
            // instanciate
            var pageElement = new PageElement();
            var folderStructure = new string[]{ SolutionFolders.Resources.ToString() , SolutionFolders.Resources.ToString() , destFolderName };
            var pageElementCollection = pageElement.GetPageElementsFromExccelFile(FileName, folderStructure);
            // Assert 
            Assert.IsTrue(pageElementCollection.Count==3);
            // Tear Down
            File.Delete(Path.Combine(destFolder,FileName));
            Directory.Delete(destFolder);
        }
    }
}
