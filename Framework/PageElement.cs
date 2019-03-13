using System;
using System.Collections.Generic;
using System.IO;
using log4net;
using NUnit.Framework;
using NUnitAutomationFramework.Framework.IO;
using NUnitAutomationFramework.Framework.Spreadsheet;

namespace NUnitAutomationFramework.Framework
{
    public class PageElement
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(PageElement));
        public string Name { get; set; }
        public string By { get; set; }
        public string Query { get; set; }
        public override string ToString()
        {
            return $"{nameof(Name)}:{Name} | {nameof(By)}:{By} | {nameof(Query)}:Query ";
        }

        public List<PageElement> GetPageElementsFromExccelFile(string filename, string[] folderStructure)
        {
            var exeDir = FileAndFolder.GetExecutionDirectory();
            Log.Info("Execution directory is: " + exeDir);
            var fileLocation = exeDir;
            foreach (var folder in folderStructure)
            {
                fileLocation = Path.Combine(fileLocation, folder);
                Log.Info("New Path: "+fileLocation);
            }
            fileLocation = Path.Combine(fileLocation, filename);
            if (!File.Exists(fileLocation))
            {
                var message = $"Trying to Open {filename} from {fileLocation}";
                Log.Error(message);
                throw new FileNotFoundException(message);
            }
            var spreadsheet = new SpreadsheetUtility();
            var excelObjects = spreadsheet.GetExcelFileObjects(fileLocation);
            var pageElements = new List<PageElement>();

            var rows = excelObjects.GetLength(0);
            //var columns = allObjects.GetLength(1);
            for (var r = 1; r < rows; r++)
            {
                var pageElement = new PageElement()
                {
                    Name = excelObjects.GetValue(r, 0).ToString(),
                    By = excelObjects.GetValue(r, 1).ToString(),
                    Query = excelObjects.GetValue(r, 2).ToString(),
                };
                Log.Info(pageElement + " Added To the Collection");
                Console.WriteLine(pageElement);
                Assert.NotNull(pageElement);
                pageElements.Add(pageElement);
            }
            return pageElements;
        }
    }
}
