using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataPullerHelper;

namespace Export
{
    public class ExportBusinessLogic
    {
        public void Export(int exportType,string path,string fileName,dynamic datatList, dynamic fieldsList)
        {
            Export export = ExportFactory.Create(exportType);

            export.InitializeConnection();

            export.OpenConnection();

            export.CreateTable(fieldsList);

            // iterate through each page
            foreach (var pageItems in datatList)
            {
                export.PopulateTable(pageItems, fieldsList);
            }

            export.CloseConnection(path);
        }
    }
}
