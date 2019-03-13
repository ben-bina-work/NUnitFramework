using System;
using System.IO;
using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using NUnitAutomationFramework.Framework.Configuration;
using NUnitAutomationFramework.Framework.IO;

namespace NUnitAutomationFramework.DevTests
{
    [TestFixture]
    [AllureNUnit]
    [AllureDisplayIgnored]
    public class ConfigurationReaderTester
    {
        private readonly ConfigurationReader _configurationReader = new ConfigurationReader();


        [Test(Description = "Testing Logs Folder")]
        [AllureTag("Framework Implementation")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureIssue("ISSUE-Configuration")]
        [AllureTms("TMS-Reading_Configuration")]
        [AllureOwner("Behrang Bina")]
        [AllureSuite("Framework")]
        [AllureSubSuite("Configuration")]
        public void TestLogFolder()
        {
            var uat = _configurationReader.ReadFolderPathFromConfigurationFile(SolutionFolders.Logs);
            Console.WriteLine(SolutionFolders.Logs+" Log Folder is located in: "+uat);
            Assert.IsTrue(Directory.Exists(uat));
        }

        [Test(Description = "Testing Reports Folder")]
        [AllureTag("Framework Implementation")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureIssue("ISSUE-Configuration")]
        [AllureTms("TMS-Reading_Configuration")]
        [AllureOwner("Behrang Bina")]
        [AllureSuite("Framework")]
        [AllureSubSuite("Configuration")]
        public void TestReportsFolder()
        {
            var uat = _configurationReader.ReadFolderPathFromConfigurationFile(SolutionFolders.Reports);
            Console.WriteLine(SolutionFolders.Reports+" Log Folder is located in: "+uat);
            Assert.IsTrue(Directory.Exists(uat));
        }
        [Test(Description = "Testing Resources Folder")]
        [AllureTag("Framework Implementation")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureIssue("ISSUE-Configuration")]
        [AllureTms("TMS-Configuration")]
        [AllureOwner("Behrang Bina")]
        [AllureSuite("Framework")]
        [AllureSubSuite("Configuration")]
        public void TestGetSolutionDirectory()
        {
            var uat = _configurationReader.ReadFolderPathFromConfigurationFile(SolutionFolders.Resources);
            Console.WriteLine(SolutionFolders.Resources+" Log Folder is located in: "+uat);
            Assert.IsTrue(Directory.Exists(uat));
        }
        
    }
}