using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Smo;
using System.Security;
using Microsoft.SqlServer.Management.Common;
namespace Export
{
    internal class SqlServer : Export
    {
        SqlConnection sqlConnection = null;
        SqlCommand sqlCommand = null;
        ServerConnection serverConnection = null;
        string tableName = string.Empty;
        const string connectionString = "data source=.;Integrated Security=True"; //"data source=.;initial catalog=Metrics_Integration;user id=sa;password=12345";
        public override void InitializeConnection()
        {
            sqlConnection = new SqlConnection(connectionString);
        }
        public override void OpenConnection()
        {
            try
            {
                sqlConnection.Open();
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
        public override void CreateTable(dynamic propList)
        {
            try
            {
                serverConnection = new ServerConnection(sqlConnection);
                serverConnection.Connect();
               
                Server server = new Server(serverConnection);
                Database database = server.Databases["Cars"];

                Table table = null;

                if (database.Tables.Contains("Kbb2"))
                {
                    table = database.Tables["Kbb2"];
                }
                else {

                    table = new Table(database, "Kbb2");
                    Column column = null;

                    for (int j = 0; j <= propList.Count - 1; j++)
                    {
                        column = new Column(table, propList[j].fieldName, DataType.VarCharMax);
                        table.Columns.Add(column);
                    }
                    table.Create();
                }

                tableName = table.Name;
            }
            catch (SqlServerManagementException ex)
            {
 
            }
        }
        public override void PopulateTable(dynamic dataList, dynamic propList)
        {
            foreach (var pageItems in dataList)
            {
                string query = GetInsertQuery(propList, pageItems);

                try
                {
                    sqlCommand = new SqlCommand(query, sqlConnection);

                    for (int j = 0; j <= propList.Count - 1; j++)
                    {
                        sqlCommand.Parameters.AddWithValue(string.Concat("@", propList[j].fieldName), pageItems[propList[j].fieldName]);
                    }
                    sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
 
                }
            }
        }
        public override void CloseConnection(string path)
        {
            try
            {
                sqlConnection.Close();
                serverConnection.Disconnect();
            }
            catch (SqlException ex)
            {
                throw;
            }
            finally {

                sqlConnection.Dispose();
            }
        }
        private SecureString convertToSecureString(string strPassword)
        {
            var secureStr = new SecureString();
            if (strPassword.Length > 0)
            {
                foreach (var c in strPassword.ToCharArray()) secureStr.AppendChar(c);
            }
            return secureStr;
        }
        private string GetInsertQuery(dynamic propList, dynamic pageItems)
        {
            StringBuilder insertQuery = new StringBuilder();
            insertQuery.Append("INSERT INTO ");
            insertQuery.Append(tableName);
            insertQuery.Append(" ( ");

            for (int j = 0; j <= propList.Count - 1; j++)
            {

                insertQuery.Append(propList[j].fieldName);
                insertQuery.Append(",");

            }

            insertQuery.Remove(insertQuery.ToString().LastIndexOf(","), 1);
            insertQuery.Append(" ) ");
            insertQuery.Append(" VALUES ");
            insertQuery.Append(" ( ");

            for (int j = 0; j <= propList.Count - 1; j++)
            {
                insertQuery.Append(" @");
                insertQuery.Append(propList[j].fieldName);
                insertQuery.Append(" , ");
            }

            insertQuery.Remove(insertQuery.ToString().LastIndexOf(","), 1);
            insertQuery.Append(" ) ");

            return insertQuery.ToString();
        }
    }
}
