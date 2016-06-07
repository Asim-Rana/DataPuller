using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataPuller.BAL
{
    public class DynamicSite:Site
    {
       internal DynamicPage page { get; set; }
       public DynamicSite(Browser browser, string name, string url, DataModel dataModel, string pageUrl, string urlSplitter, int pageIncremental, string prodName, int siteTimeout, int pageTimeout, IDictionary<Element, DomAction> siteActions = null, IDictionary<Element, DomAction> pageActions = null, int firstPage = 1, string lastPage = "")
           : base(browser, name, url, dataModel, firstPage, lastPage , prodName, siteTimeout, pageTimeout)
       {
           page = new DynamicPage(pageUrl, urlSplitter, pageIncremental);

           siteActionList = siteActions;
           pageActionList = pageActions;
       }

        internal override dynamic GetPageData(int pageNo)
        {
            dynamic pageData = base.GetPageData(pageNo);

            if (pageNo != lastPage)
            {
                NavigateToPage(pageNo);
            }

            return pageData;
        }
        internal override void NavigateToPage(int pageNo)
        {
            page.pageNumber = pageNo;

            string url = page.GetPageUrl();

            browser.NavigateTo(url);
            Thread.Sleep(SiteTimeout*1000);
        }
    }
}
