using DataPuller.BAL;
using Logger;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataPuller.BO;
using System.Configuration;

namespace DataPuller.DAL
{
    internal class JsonFile:IDataAccessLayer
    {
        public readonly string path = DataPullerHelper.Helper.GetDataModelsPath();
        public readonly string extension = ".json";
        readonly string templatePath = ConfigurationManager.AppSettings["TemplatePath"];
        public void Save(dynamic name,dynamic obj)
        {
            foreach (var action in obj.siteActions)
            {
                obj.siteActionsList.Add(action);
            }
            foreach (var action in obj.pageActions)
            {
                obj.pageActionsList.Add(action);
            }
            var siteActionsCopy = obj.siteActions;
            obj.siteActions = null;
            var pageActionsCopy = obj.pageActions;
            obj.pageActions = null;
            string filePath = string.Format("{0}{1}",Path.Combine(path, name),extension);
            string jsonString = JsonConvert.SerializeObject(obj , Formatting.Indented /*, new CustomTypeConverter(typeof(DataModel))*/);
            Log.WriteFile(filePath, jsonString);
            obj.siteActions = siteActionsCopy;
            obj.pageActions = pageActionsCopy;
        }
        public T Get<T>(string name)
        {
            //string filePath = string.Format("{0}{1}", Path.Combine(path, name), extension);
            string jsonFileContents = Log.ReadTextFile(name);

            T obj = JsonConvert.DeserializeObject<T>(jsonFileContents /*, new CustomTypeConverter(typeof(DataModel))*/); 

            return obj;
        }
        public T GetTemplate<T>(string filename)
        {
            filename = templatePath + "\\" + filename + extension;
            return Get<T>(filename); 
        }

    }
}
