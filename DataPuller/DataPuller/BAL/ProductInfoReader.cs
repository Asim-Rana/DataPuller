using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataPuller.BO;
using DataPuller.DAL;

namespace DataPuller.BAL
{
    public class ProductInfoReader
    {
        ExcelFileReader excelReader;
        public ProductInfoReader()
        {
            excelReader = new ExcelFileReader();
        }
        public IList<ProductInfo> GetData(string filename , string sheet)
        {
            
            return excelReader.GetData(filename , sheet);
        }
        public IList<string> GetSheetNames(string filename)
        {
            return excelReader.GetSheetNames(filename);
        }
    }
}
