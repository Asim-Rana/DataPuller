using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataPuller.BAL
{
    public class StaticSite : Site
    {
        internal StaticPage page { get; set; }
        public StaticSite(Browser browser, string name, string url, DataModel dataModel, int firstPage, string lastPage, string prodName, Element nextPageElement, int siteTimeout , int pageTimeout ,IDictionary<Element, DomAction> siteActions = null, IDictionary<Element, DomAction> pageActions = null)
            : base(browser, name, url, dataModel, firstPage, lastPage , prodName , siteTimeout , pageTimeout)
        {
            page = new StaticPage(nextPageElement);

            siteActionList = siteActions;
            pageActionList = pageActions;
        }

        internal override dynamic GetPageData(int pageNo)
        {
            dynamic pageData = base.GetPageData(pageNo);

            //if(pageNo != lastPage)
            //{ 
            //    NavigateToPage(pageNo);
            //}

            return pageData;

        }
        internal override void NavigateToPage(int pageNo)
        {
            page.pageNumber = pageNo;

            DomAction nextClickAction = new DomAction(DomAction.ActionType.CLICK,"");
            browser.PerformAction(page.nextPageElement, nextClickAction , PageTimeout);
        }
    }
}
