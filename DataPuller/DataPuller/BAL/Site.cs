using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Export;
using DataPuller.DAL;
using DataPullerHelper;
using System.Threading;
using Logger;
using System.Configuration;

namespace DataPuller.BAL
{
    public abstract class Site
    {
        public string name { get; set; }
        public string productName { get; set; }
        public int firstPage { get; set; }
        public int lastPage { get; set; }
        public string url { get; set; }
        public DataModel dataModel { get; set; }
        public IDictionary<Element, DomAction> siteActionList { get; set; }
        public IDictionary<Element, DomAction> pageActionList { get; set; }

        public dynamic data = null;

        public int dataCount = 0;

        public string status = SiteStatus.CLOSED;
        public Browser browser { get; set; }
        string lastPageSelector;
        public int SiteTimeout { get; set; }
        public int PageTimeout { get; set; }

        private IDataAccessLayer dataAccessLayer = DataAccessFactory.Create(0);
        public Site(Browser browser, string name, string url, DataModel dataModel, int firstPage, string lastPage , string prodName , int siteTimeout , int pageTimeout)
        {
            this.browser = browser;
            this.name = name;
            this.firstPage = firstPage;
            if(lastPage.All(char.IsDigit))
            {
                this.lastPage = int.Parse(lastPage);
            }
            else
            {
                this.lastPage = -1;
                lastPageSelector = lastPage;
            }
            this.url = url;
            this.dataModel = dataModel;
            productName = prodName;
            SiteTimeout = siteTimeout;
            PageTimeout = pageTimeout;
        }
        int GetLastPageFromSiteAction()
        {
            try
            {
                HtmlSelector selector = new HtmlSelector(lastPageSelector, SelectBy.CSS_SELECTOR);
                Element element = new Element(selector, AttributeType.TEXT);
                string value = browser.GetHTMLElementText(element);
                return int.Parse(value);
            }
            catch (Exception)
            {
                return 1;
            }
        }
        internal abstract void NavigateToPage(int pageNo);
        internal virtual dynamic GetData(bool updateFlag)
        {
            SqlServerOperation sqlOp = SqlServerOperation.Instance;

            int threshold = int.Parse(ConfigurationManager.AppSettings["DBThreshold"]);
            data = new List<dynamic>();

            // navigate to site url
            browser.NavigateTo(url);
            Thread.Sleep(SiteTimeout * 1000);
            // process all actions on site main url
            ProcessActions(siteActionList , PageTimeout);

            if(lastPage == -1)
            {
                lastPage = GetLastPageFromSiteAction();
            }

            for (int pageNo = firstPage ; pageNo <= lastPage; pageNo++)
            {
                // wait between navigation
                //Thread.Sleep(Helper.GetNavigationWait());

                // process actions on page
                

                dynamic pageData = this.GetPageData(pageNo);

                if (pageData != null)
                {
                    // add page data count to site data count
                    dataCount += pageData.Count;
                    // add page data to site data
                    data.Add(pageData);

                    //for updating scrapped data
                    if(updateFlag)
                    {
                        if(!sqlOp.IsAllAreLatest(pageData , url))
                        {
                            sqlOp.RemoveInvalid(pageData , url); 
                            sqlOp.SaveData(data, dataModel, url, productName);
                            data.Clear();
                            break;
                        }
                    }

                    //if data exceeds from threshold then store in DB
                    else if (pageNo % threshold == 0)
                    {
                        sqlOp.SaveData(data, dataModel, url, productName);
                        data.Clear();
                    }
                }
                else
                {
                    Console.WriteLine("No Data Found on Site:{0} \n PageNumber:{1}", name, pageNo);
                    string customMessage = string.Format("Site:{0} \n PageNumber:{1} \n Method:{2} \n ", name, pageNo, "GetData returned Empty Parent Elements");
                    Log.Exception(customMessage);
                }

                Console.WriteLine("Page:" + pageNo);
                ProcessActions(pageActionList , PageTimeout);
            }
            if(data.Count != 0)
            {
                sqlOp.SaveData(data, dataModel, url, productName);
                data.Clear();
            }
            // return null if no data fetched
            if (dataCount == 0)
            {
                Console.WriteLine("No Data Found on Site:{0}", name);
                return null;
            }

            return data;
        }
        internal virtual dynamic GetPageData(int pageNo)
        {
            try
            {
                dynamic parentElementCollection = browser.GetHTMLElementCollection(this.dataModel.parentElement.htmlSelector);

                if (parentElementCollection != null)
                {
                    dynamic pageData = PopulatePageData(parentElementCollection);

                    return pageData;
                }
                else
                {
                    return parentElementCollection;
                }
            }
            catch (Exception ex)
            {
                string customMessage = string.Format("Site:{0} \n PageNumber:{1} \n Method:{2} \n ", name, pageNo, "GetPageData: Returned null ParentElementCollection");
                Log.Exception(customMessage, ex);
                throw;
            }
        }
        private dynamic PopulatePageData(IEnumerable<dynamic> parentElementCollection)
        {
            dynamic datalist = new List<dynamic>();

            foreach (dynamic parentElement in parentElementCollection)
            {
                IDictionary<string, string> row = new Dictionary<string, string>();

                foreach (Field field in dataModel.fieldsList)
                {
                    string fieldName = field.fieldName;

                    try
                    {
                        string value = browser.GetHTMLElementTextFromParent(parentElement, field);

                        row.Add(field.fieldName, Helper.RemoveNewLine(value));
                    }
                    catch (Exception ex)
                    {
                        string customMessage = string.Format("Site:{0}\n Method:{1} \n Message{2} ", name, "PopulatePageData", field.htmlElement.htmlSelector.selectorValue + " Returned Empty Text");
                        Log.Exception(customMessage, ex);
                        row.Add(field.fieldName , "");
                    }
                }

                datalist.Add(row);
            }

            return datalist;

        }
        private void Export(int exportType, string path = "", string fileName = "")
        {
            if (data != null)
            {
                path = path == string.Empty ?
                string.Concat(string.Format("{0}\\{1}_{2}", Helper.GetDefaultExportPath(), this.name, DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"))) : string.Concat(path);
                ExportBusinessLogic exportBusinessLogic = new ExportBusinessLogic();
                exportBusinessLogic.Export(exportType, path, fileName, data, dataModel.fieldsList);
            }
            else
            {
                Console.WriteLine("No Data to Export on Site:{0}", name);
            }
        }
        public void ExportToCSV(string path = "", string fileName = "")
        {
            Export(0, path, fileName);
        }
        public void ExportToExcel(string path = "", string fileName = "")
        {
            Export(1, path, fileName);
        }
        public void ExportToPDF(string path = "", string fileName = "")
        {
            Export(2, path, fileName);
        }
        public void SaveSite(DataModel dataModel)
        {
            dataAccessLayer.Save(this.name, dataModel);
        }
        public Site GetSite(string name)
        {
            Site dataModel = dataAccessLayer.Get<Site>(name);

            return dataModel;
        }
        private void ProcessActions(dynamic actionList , int timeout)
        {
            if (actionList != null)
            {
                if(browser.isDocumentReady())
                browser.ProcessActionList(actionList , timeout);
            }
        }
       
        internal static class SiteStatus { 
        
            public  const string RUNNING="Running";
            public  const string COMPLETED = "Completed";
            public  const string CLOSED="Closed";
        }
    }
}