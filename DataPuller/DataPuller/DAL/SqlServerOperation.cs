using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataPuller.BAL;
using DataPuller.BO;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace DataPuller.DAL
{
    public class SqlServerOperation
    {
        SqlConnection sqlConnection = null;
        ServerConnection serverConnection = null;
        string tableName;
        string databaseName;
        string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        DataTable table = null;

        private static SqlServerOperation instance = null;
        SqlServerOperation()
        {
            InitializeConnection();
        }
        public static SqlServerOperation Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SqlServerOperation();
                }
                return instance;
            }
        }
        public void SetDataSets(string dbName, string tblName)
        {
            databaseName = dbName;
            tableName = tblName;
            connectionString = ConfigurationManager.AppSettings["ConnectionString"] + "Initial Catalog=" + databaseName;
            InitializeConnection();
        }
        void InitializeConnection()
        {
            sqlConnection = new SqlConnection(connectionString);
            serverConnection = new ServerConnection(sqlConnection);
        }
        void OpenConnection()
        {
            try
            {
                sqlConnection.Open();
                serverConnection.Connect();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        void CloseConnection()
        {
            try
            {
                sqlConnection.Close();
                serverConnection.Disconnect();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        void CreateDataTable(DataModel dataModel)
        {
            if (table == null)
            {
                table = new DataTable();
                table.Columns.Add("Source", typeof(string));
                table.Columns.Add("ProductName", typeof(string));
                table.Columns.Add("ScrappedDate", typeof(DateTime));

                foreach (var field in dataModel.fieldsList)
                {
                    if (field.fieldName.Equals("Date"))
                    {
                        table.Columns.Add(field.fieldName, typeof(DateTime));
                    }
                    else
                    {
                        table.Columns.Add(field.fieldName, typeof(string));
                    }
                }
            }
        }
        public bool CreateTable(DataModel dataModel)
        {
            try
            {
                OpenConnection();
                CreateDataTable(dataModel);
                Server server = new Server(serverConnection);
                Database database = server.Databases[databaseName];

                Table table = null;

                if (!database.Tables.Contains(tableName))
                {
                    table = new Table(database, tableName);
                    Column column = null;
                    column = new Column(table, "DocId", DataType.BigInt);
                    column.Identity = true;
                    column.IdentityIncrement = 1;
                    column.IdentitySeed = 1;
                    table.Columns.Add(column);

                    column = new Column(table, "Source", DataType.VarCharMax);
                    table.Columns.Add(column);

                    column = new Column(table, "ProductName", DataType.VarCharMax);
                    table.Columns.Add(column);

                    column = new Column(table, "ScrapeKey", DataType.VarCharMax);
                    table.Columns.Add(column);

                    column = new Column(table, "ScrappedDate", DataType.DateTime);
                    table.Columns.Add(column);


                    foreach (var field in dataModel.fieldsList)
                    {
                        if (field.fieldName.Equals("Date"))
                        {
                            column = new Column(table, field.fieldName, DataType.DateTime);
                        }
                        else
                        {
                            column = new Column(table, field.fieldName, DataType.VarCharMax);
                        }
                        table.Columns.Add(column);
                    }

                    table.Create();
                }

            }
            catch (SqlServerManagementException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                CloseConnection();
            }
            return true;
        }
        public bool SaveData(List<dynamic> pagesData, DataModel dataModel, string url, string productName)
        {
            DateTime d = DateTime.MinValue;
            try
            {
                if (table == null)
                {
                    CreateDataTable(dataModel);
                }
                else
                {
                    table.Clear();
                }
                foreach (var pageData in pagesData)
                {
                    foreach (var data in pageData)
                    {
                        DataRow row = table.NewRow();
                        row["Source"] = url;
                        row["ProductName"] = productName;
                        row["ScrappedDate"] = DateTime.Today;

                        foreach (var field in dataModel.fieldsList)
                        {
                            if (field.fieldName.Equals("Date"))
                            {
                                d = Utility.ConvertToDateTime(data[field.fieldName] , DateTime.Today);
                                row[field.fieldName] = d;
                            }
                            else
                            {
                                row[field.fieldName] = data[field.fieldName];
                            }
                        }
                        if (d.CompareTo(DateTime.MinValue) != 0)
                        {
                            table.Rows.Add(row);
                        }
                    }
                }
                OpenConnection();
                if (table.Rows.Count > 0)
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connectionString))
                    {
                        bulkCopy.BulkCopyTimeout = 0;
                        bulkCopy.DestinationTableName = tableName;
                        MapColumns(bulkCopy, dataModel);

                        bulkCopy.WriteToServer(table);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return true;
        }
        void MapColumns(SqlBulkCopy bulkCopy, DataModel dataModel)
        {
            bulkCopy.ColumnMappings.Add("Source", "Source");
            bulkCopy.ColumnMappings.Add("ProductName", "ProductName");
            bulkCopy.ColumnMappings.Add("ScrappedDate", "ScrappedDate");

            foreach (var fields in dataModel.fieldsList)
            {
                bulkCopy.ColumnMappings.Add(fields.fieldName, fields.fieldName);
            }
        }
        public Dictionary<string , List<string>> GetDataSets()
        {
            Dictionary<string , List<string>> list = new Dictionary<string, List<string>>();
            List<string> tblNames;
            OpenConnection();
            Server server = new Server(serverConnection);
            DatabaseCollection databases = server.Databases;
            foreach (Database db in databases)
            {
                tblNames = new List<string>();
                foreach(Table tb in db.Tables)
                {
                    tblNames.Add(tb.Name);
                }
                list.Add(db.Name , tblNames);
            }
            CloseConnection();
            return list;
        }

        public bool IsAllAreLatest(dynamic pageData, string url)
        {
            DateTime date = GetLatestDate(url);
            foreach (var data in pageData)
            {
                if (!IsLatestData(data, date))
                {
                    return false;
                }

            }
            return true;
        }
        bool IsLatestData(dynamic data, DateTime date)
        {
            DateTime d = Utility.ConvertToDateTime(data["Date"] , DateTime.Today);
            if (d.CompareTo(DateTime.MinValue) == 0 || d.CompareTo(date) > 0)
            {
                return true;
            }
            return false;
        }
        bool IsLatest(dynamic data, DateTime date)
        {
            DateTime d = Utility.ConvertToDateTime(data["Date"] , DateTime.Today);
            if (d.CompareTo(date) > 0)
            {
                return true;
            }
            return false;
        }
        DateTime GetLatestDate(string url)
        {
            DateTime d = DateTime.MinValue;
            try
            {
                SqlCommand cmd = null;
                string query = "Select MAX(Date) from " + tableName + " where Source = @u";
                cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.Add(new SqlParameter("u", url));
                OpenConnection();
                object dt = cmd.ExecuteScalar();
                if (DBNull.Value != dt)
                {
                    d = (DateTime)dt;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return d;
        }
        public void RemoveInvalid(dynamic pageData, string url)
        {
            DateTime date = GetLatestDate(url);
            for (int i = pageData.Count - 1; i >= 0; i--)
            {
                if (!IsLatest(pageData[i], date))
                {
                    pageData.RemoveAt(i);
                }
            }
        }
    }
}
