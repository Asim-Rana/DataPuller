using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataPuller.BO;

namespace DataPuller.DAL
{
    public class ExcelFileReader
    {

        private string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        private string connString;
        private string sheetName;
        DataTable dt = new DataTable();
        public IList<ProductInfo> GetData(string fileName , string sheet) 
        {
            IList<ProductInfo> productInfo = new List<ProductInfo>();
            try
            {
                PopulateConnectionString(fileName);
                sheetName = sheet;
                ReadDataFromFirstSheet();
                UpdateInfoList(productInfo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return productInfo;
        }
        void PopulateConnectionString(string filename)
        {
            string extension = Path.GetExtension(filename);
            switch (extension)
            {

                case ".xls": //Excel 97-03
                    connString = string.Format(Excel03ConString, filename, "YES");
                    break;

                case ".xlsx": //Excel 07
                    connString = string.Format(Excel07ConString, filename, "YES");
                    break;
            }
        }
        void ReadDataFromFirstSheet()
        {
            using (OleDbConnection con = new OleDbConnection(connString))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    using (OleDbDataAdapter oda = new OleDbDataAdapter())
                    {
                        cmd.CommandText = "SELECT * From [" + sheetName + "]";
                        cmd.Connection = con;
                        con.Open();
                        oda.SelectCommand = cmd;
                        oda.Fill(dt);
                        con.Close();
                    }
                }
            }
        }
        void UpdateInfoList(IList<ProductInfo> productInfo)
        {
            foreach (DataRow row in dt.Rows)
            {
                if(row[0].ToString() == "" || row[1].ToString() == "" || row[2].ToString() == "" || row[3].ToString() == "")
                {
                    continue;
                }
                try
                {
                    ProductInfo info = new ProductInfo();
                    info.Name = row[0].ToString();
                    info.Url = row[1].ToString();
                    info.LastPageNo = row[2].ToString();
                    info.Template = row[3].ToString();

                    productInfo.Add(info);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            dt.Clear();
        }
        public IList<string> GetSheetNames(string filename)
        {
            IList<string> sheetNames = new List<string>();
            PopulateConnectionString(filename);
            using (OleDbConnection con = new OleDbConnection(connString))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = con;
                    con.Open();
                    DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    for(int i = 0; i < dtExcelSchema.Rows.Count; i++)
                    {
                        sheetNames.Add(dtExcelSchema.Rows[i]["TABLE_NAME"].ToString());
                    }
                    con.Close();
                }
            }
            return sheetNames;
        }
    }
}
