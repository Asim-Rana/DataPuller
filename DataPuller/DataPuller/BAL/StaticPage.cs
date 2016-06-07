using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataPuller.BAL
{
    public class StaticPage:Page
    {
        public Element nextPageElement { get; set; }
        public StaticPage(Element NexPageElement)
        {
            this.nextPageElement = NexPageElement;
        }
    }
}
