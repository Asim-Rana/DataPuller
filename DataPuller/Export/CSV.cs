using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Logger;

namespace Export
{
    internal class CSV:Export
    {
        //Variables for build CSV string
        StringBuilder sb = null;

        List<string> propNames;
        List<string> propValues;
        public override void InitializeConnection()
        {
            sb = new StringBuilder();
        }
        public override void OpenConnection()
        {
            
        }
        private string GetFileExtension()
        {
            return ".csv";
        }
        public override void CreateTable(dynamic propList)
        {
            propNames = new List<string>();

            foreach (var prop in propList)
            {
                //Construct property name string if not done in sb
                propNames.Add(prop.fieldName);
            }

            sb.AppendLine(string.Join(",", propNames));
        }
        public override void PopulateTable(dynamic list, dynamic propList)
        {
            string line = string.Empty;

            foreach (var item in list)
            {
                propValues = new List<string>();

                //Iterate through property collection
                foreach (var prop in propList)
                {
                    //Construct property value string with double quotes for issue of any comma in string type data
                    var val = item[prop.fieldName].GetType() == typeof(string) ? "\"{0}\"" : "{0}";
                    propValues.Add(string.Format(val, item[prop.fieldName]));
                }
              
                sb.Append(string.Join(",", propValues));
                sb.AppendLine();
            }
        }
        public override void CloseConnection(string path)
        {
            path = string.Concat(path, GetFileExtension());
            Log.WriteFile(path, sb.ToString());
        }
    }
}
