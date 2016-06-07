using DataPullerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataPuller.BAL
{
    public class Browser
    {
        private Type type { get; set; }
        private bool enableProxy { get; set; }
        public string runningTime{ get; set; }
        public Browser(Type type,bool enableProxy)
        {
            this.type = type;
            this.enableProxy = enableProxy;
        }
        public IEnumerable<dynamic> GetHTMLElementCollection(HtmlSelector selector)
        {
            string parentSelectorValue = selector.selectorValue;
            string parentSelectorType = Convert.ToString(selector.selectBy);

           return DomService.GetHTMLElementCollection(parentSelectorValue, parentSelectorType);
        }
        internal IEnumerable<dynamic> GetCQHTMLElementCollection(HtmlSelector selector)
        {
            string parentSelectorValue = selector.selectorValue;
            string parentSelectorType = Convert.ToString(selector.selectBy);

           return DomService.GetCQHTMLElementCollection(parentSelectorValue, parentSelectorType);
        }
        internal string GetHTMLElementTextFromParent(dynamic parentElement, Field field)
        {
            string fieldSelectorValue = field.htmlElement.htmlSelector.selectorValue;
            string fieldSelectorType = Convert.ToString(field.htmlElement.htmlSelector.selectBy);
            string fieldAttributeType;
            if (field.htmlElement.attributeValue == null )
            {
                fieldAttributeType = Convert.ToString(field.htmlElement.attributeType);
            }
            else
            {
                fieldAttributeType = field.htmlElement.attributeValue;
            }
            

            return DomService.GetHTMLElementTextFromParent(parentElement, fieldSelectorValue, fieldSelectorType, fieldAttributeType);
        }
        internal string GetHTMLElementText(Element element)
        {
            string selectorValue = element.htmlSelector.selectorValue;
            string selectorType = Convert.ToString(element.htmlSelector.selectBy);

            return DomService.GetHTMLElementText(selectorValue, selectorType);
        }
        public void PerformAction(Element element, DomAction action , int timeout)
        {
            try
            {
                string selectorValue = element.htmlSelector.selectorValue;
                string selectorType = Convert.ToString(element.htmlSelector.selectBy);
                string actionType = Convert.ToString(action.actionType);
                string actionSelect = Convert.ToString(action.actionSelectyBy);
                string actionValue = Convert.ToString(action.value);

                DomService.PerformAction(selectorValue, selectorType, actionValue, actionType, actionSelect);
            }
            catch (Exception)
            {

            }
        }
        public void EnterText(Element element, string value, bool clearFirst = true)
        {
            string selectorValue = element.htmlSelector.selectorValue;
            string selectorType = Convert.ToString(element.htmlSelector.selectBy);

            DomService.EnterText(selectorValue, selectorType, value, clearFirst);
        }
        public bool HasElement(Element element)
        {
            string selectorValue = element.htmlSelector.selectorValue;
            string selectorType = Convert.ToString(element.htmlSelector.selectBy);

            return DomService.HasElement(selectorValue, selectorType);
        }
        public void ProcessActionList(dynamic actionList , int timeout)
        {
            foreach (dynamic action in actionList)
            {
               PerformAction(action.Key, action.Value , timeout);

               Thread.Sleep(timeout*1000);
            }
        }
        public void Open()
        {
            if (enableProxy)
            {
                ProxyService.StartProxyServer();
            }

            dynamic browser = BrowserFactory.Create((int)type, enableProxy);
            DomService.InitializeService(browser);
        }
        public void NavigateTo(string url)
        {
            DomService.NavigateToPage(url);
        }
        public void MaximizeWindow()
        {
            DomService.MaximizeWindow();
        }
        public void Close()
        {
            DomService.Quit();
            ProxyService.CloseProxyServer();
        }
        public void SwitchToWindow(int windowNumber)
        {
            DomService.SwitchToWindow(windowNumber);
        }
        public void TakeScreenShot(string fileName)
        {
            DomService.TakeScreenShot(fileName);
        }
        public void CloseWindow(int windowNumber)
        {
            DomService.CloseWindow(windowNumber);
        }
        public bool isNumberOfWindowsEqual(int no)
        {
            try
            {
                return DomService.isNumberOfWindowsEqual(no);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void ScrollBottom()
        {
            DomService.ScrollBottom();
        }
        public void ScrollTop()
        {
            DomService.ScrollTop();
        }
        public string GetTitle()
        {
            return DomService.GetPageTitle();
        }
        public string GetUrl()
        {
            return DomService.GetPageUrl();
        }
        public bool isPageTitleEqual(string pageTitle)
        {
            return DomService.isTitleEqual(pageTitle);
        }
        public void ExecuteJavaScript(string script)
        {
            DomService.ExecuteJavaScript(script);
        }
        public void Refresh()
        {
            DomService.Refresh();
        }
        public bool isDocumentReady()
        {
            return DomService.isDocumentReady();
        }
        public enum Type { 
        
            PHANTOM_JS=0,
            CHROME=1,
            FIREFOX=2
        }
    }
}
