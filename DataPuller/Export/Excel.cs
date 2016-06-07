using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace Export
{
    internal class Excel : Export
    {
        Microsoft.Office.Interop.Excel.Application xlApp;
        Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
        Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;

        private int row=0;
     
        public override void InitializeConnection()
        {
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
        }
        public override void OpenConnection()
        {
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Cells.NumberFormat = "@";
           
        }
        private string GetFileExtension()
        {
            return ".xls";
        }
        public override void CreateTable(dynamic propList)
        {
            for (int j = 0; j <= propList.Count - 1; j++)
            {
                xlWorkSheet.Cells[row + 1, j + 1] = propList[j].fieldName;
            }

            row++;
        }
        public override void PopulateTable(dynamic dataList, dynamic propList)
        {
            foreach (var pageItems in dataList)
            {
                for (int j = 0; j <= propList.Count - 1; j++)
                {
                    xlWorkSheet.Cells[row + 1, j + 1] = pageItems[propList[j].fieldName];
                }

                row++;
            }
        }
        public override void CloseConnection(string path)
        {
            path = string.Concat(path, GetFileExtension());
            xlWorkSheet.Columns.AutoFit();

            xlWorkBook.SaveAs(path, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, null, null, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared, false, false, null, null, null);
            xlWorkBook.Saved = true;
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
        }
    }
}
