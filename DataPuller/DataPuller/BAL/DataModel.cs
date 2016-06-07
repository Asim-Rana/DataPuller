using DataPuller.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using DataPuller.BO;
using Newtonsoft.Json;

namespace DataPuller.BAL
{
    public class DataModel
    {
        public string name { get; set; }
        public Element parentElement { get; set; }
        public List<Field> fieldsList { get; set; }
        public Element nextPageSelectorElement { get; set; }        
        public IDictionary<Element, DomAction> siteActions { get; set; }
        public IDictionary<Element, DomAction> pageActions { get; set; }
        public List<KeyValuePair<Element, DomAction>> siteActionsList = new List<KeyValuePair<Element, DomAction>>();
        public List<KeyValuePair<Element, DomAction>> pageActionsList = new List<KeyValuePair<Element, DomAction>>();

        private IDataAccessLayer dataAccessLayer =  DataAccessFactory.Create(0);

        public DataModel() { 
        
        }
        public DataModel(string name,Element parentElement,List<Field> fields, Element nextPage , IDictionary<Element, DomAction> pageAction = null, IDictionary<Element, DomAction> siteAction = null)
        {
            this.name = name;
            this.parentElement = parentElement;
            fieldsList = fields;
            nextPageSelectorElement = nextPage;
            siteActions = siteAction;
            pageActions = pageAction;
        }

        public void SaveDataModel(DataModel dataModel)
        {
            dataAccessLayer.Save(this.name, dataModel);
        }

        public DataModel GetDataModel(string name)
        {
            DataModel dataModel = dataAccessLayer.Get<DataModel>(name);
            return dataModel;

        }
    }
}
