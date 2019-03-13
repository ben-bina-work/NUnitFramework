using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using log4net;
using NUnitAutomationFramework.Framework.Configuration;
using NUnitAutomationFramework.Framework.IO;
using OfficeOpenXml;

namespace NUnitAutomationFramework.Framework.Spreadsheet
{
    public class SpreadsheetUtility
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(SpreadsheetUtility));
        private readonly ConfigurationReader _configHandler;
        public SpreadsheetUtility()
        {
            _configHandler = new ConfigurationReader();
        }
        /// <summary>
        ///  Gets the objects in the Excelfile
        /// </summary>
        /// <param name="filePath">File Path location</param>
        /// <returns>Array of data in the excell file</returns>
        public object[,] GetExcelFileObjects(string filePath)
        {


            var existingFile = new FileInfo(filePath);
            // Open and read the XlSX file.
            Log.Info("Trying to load all abjedts from the file");
            using (var package = new ExcelPackage(existingFile))
            {
                object[,] allObjects = { };
                // Get the work book in the file
                var workBook = package.Workbook;
                if (workBook?.Worksheets.Count > 0)
                {
                    // Get the first worksheet
                    var currentWorksheet = workBook.Worksheets.First();
                    Log.Info($"Current Sheet: {currentWorksheet}");
                    // read some data
                    allObjects = (object[,])currentWorksheet.Cells.Value;
                }
                else
                {
                    Log.Error("Could not find any worksheet in the excelfile: "+ filePath);
                }
                
                return allObjects;
            }
        }

        /// <summary>
        /// Get objects in a row by row id
        /// </summary>
        /// <param name="filePath">Full Excel File Path</param>
        /// <param name="rowId">Row number /ID</param>
        /// <returns></returns>
        public List<string> GetRow(string filePath, int rowId)
        {
            var allObjects = GetExcelFileObjects(filePath);
            Log.Info("Geting the data in the row " + rowId);
            var row = new List<string>();
            for (var i = 0; i < allObjects.GetLength(1); i++)
            {
                var tmp = allObjects[rowId, i] ?? string.Empty;
                row.Add(tmp.ToString());
                Log.Info("Row Added to the collection: " +tmp);
            }
            return row;
        }
        /// <summary>
        /// Write an object to an excel file
        /// </summary>
        /// <param name="fileName">File Name</param>
        /// <param name="className">Class Name i.e. Applicant</param>
        public void WriteToExistingExcel(string fileName, object className)
        {
            try
            {
                var allObject = GetExcelFileObjects(fileName);
                var rows = allObject.GetLength(0);
                var cols = allObject.GetLength(0);
                Log.Info($"current rows: {rows} - current columns: {cols}");
                Log.Info("Trying to append to row" + rows + 1);
                if (allObject[rows - 1, 1] != null)
                {
                    rows = rows + 1;
                }
                //
                var folderPath = _configHandler.ReadFolderPathFromConfigurationFile(SolutionFolders.Resources);
                var filePath = Path.Combine(folderPath, fileName);

                Log.Info($"Trying to open the File: {fileName} in the path: {filePath}");
                // Get the file we are going to process
                var existingFile = new FileInfo(filePath);
                var package = new ExcelPackage(existingFile);

                var ws = package.Workbook.Worksheets[1];
                var prop = className.GetType().GetProperties().ToList();
                var values = prop.Select(p => p.GetValue(className)).ToList();

                for (var c = 0; c < cols; c++)
                {
                    for (var f = 0; f < values.Count; f++)
                    {
                        ws.Cells[rows, f + 1].Value = values.ElementAt(f);
                    }
                }
                Log.Info("Saving the excel file " + filePath);
                package.Save();
            }
            catch (Exception e)
            {
                Log.Error(e.Message + "\n" + e.StackTrace);
                
            }
        }
        /// <summary>
        /// Get data from the excel with the sheet number
        /// </summary>
        /// <param name="fileName">file name </param>
        /// <param name="worksheetNumber">worksheet number</param>
        /// <returns></returns>
        public object[,] GetExcelFileObjectsInAsheeet(string fileName, int worksheetNumber)
        {

            var folderPath = _configHandler.ReadFolderPathFromConfigurationFile(SolutionFolders.Resources);
            var filePath = Path.Combine(folderPath, fileName);
            Log.Info($"Trying to open the File: {fileName} in the path: {filePath}");
            // Get the file we are going to process
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"{fileName} could not found in {filePath}");
            }
            var existingFile = new FileInfo(filePath);
            // Open and read the XlSX file.
            using (var package = new ExcelPackage(existingFile))
            {
                object[,] allObjects = { };
                // Get the work book in the file
                var workBook = package.Workbook;
                if (workBook?.Worksheets.Count > 0)
                {
                    // Get the first worksheet
                    var currentWorksheet = workBook.Worksheets[worksheetNumber];
                    // read some data
                    allObjects = (object[,])currentWorksheet.Cells.Value;

                }
                return allObjects;
            }
        }
        /// <summary>
        /// Get data from the excel with the sheet number
        /// </summary>
        /// <param name="fileName">file name </param>
        /// <param name="worksheetName">worksheet Name</param>
        /// <returns></returns>
        public object[,] GetExcelFileObjectsInAsheeet(string fileName, string worksheetName)
        {

            var folderPath = _configHandler.ReadFolderPathFromConfigurationFile(SolutionFolders.Resources)
;
            var filePath = Path.Combine(folderPath, fileName);
            Log.Info($"Trying to open the File: {fileName} in the path: {filePath}");
            // Get the file we are going to process
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"{fileName} could not found in {filePath}");
            }
            var existingFile = new FileInfo(filePath);
            // Open and read the XlSX file.
            using (var package = new ExcelPackage(existingFile))
            {
                object[,] allObjects = { };
                // Get the work book in the file
                var workBook = package.Workbook;
                if (workBook?.Worksheets.Count > 0)
                {
                    // Get the first worksheet
                    var currentWorksheet = workBook.Worksheets[worksheetName];
                    // read some data
                    allObjects = (object[,])currentWorksheet.Cells.Value;

                }
                return allObjects;
            }
        }
        public string GetExcelSheetName(string fileName, int worksheetNumber)
        {
            var folderPath = _configHandler.ReadFolderPathFromConfigurationFile(SolutionFolders.Resources);
            var filePath = Path.Combine(folderPath, fileName);
            Log.Info($"Trying to open the File: {fileName} in the path: {filePath}");
            // Get the file we are going to process
            if (!File.Exists(filePath))
            {
                Log.Error($"{ fileName} could not found in { filePath}" );
                throw new FileNotFoundException($"{fileName} could not found in {filePath}");
            }
            var existingFile = new FileInfo(filePath);
            var package = new ExcelPackage(existingFile);
            var workBook = package.Workbook;
            var currentWorksheet = workBook.Worksheets[worksheetNumber];
            Log.Info("Returning the current sheet name: " + currentWorksheet.Name);
            return currentWorksheet.Name;
        }
    }
}