using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataPuller.BAL
{
    public class DynamicPage:Page
    {
        public string pageUrl { get; set; }
        public string urlSplitter { get; set; }
        public int pageIncremental { get; set; }
        public DynamicPage(string pageUrl, string urlSplitter, int pageIncremental)
        {
            this.pageUrl = pageUrl;
            this.urlSplitter = urlSplitter;
            this.pageIncremental = pageIncremental;
        }
        public string GetPageUrl()
        {
            if (pageNumber == 1)
            {
                string toBeReplaced1 = pageNumber == 1 ? urlSplitter + (pageNumber) : urlSplitter + ((pageNumber - 1) * pageIncremental);
                string replaceWith1 = urlSplitter + (pageNumber * pageIncremental);
                pageUrl = pageUrl.Replace(toBeReplaced1, replaceWith1);
                return pageUrl;
            }
            string toBeReplaced = pageNumber == 1 ? urlSplitter + (pageNumber * pageIncremental) : urlSplitter + ((pageNumber - 1) * pageIncremental);
            string replaceWith = urlSplitter + (pageNumber * pageIncremental);
            pageUrl = pageUrl.Replace(toBeReplaced, replaceWith);

            return pageUrl;
        }
    }
}
