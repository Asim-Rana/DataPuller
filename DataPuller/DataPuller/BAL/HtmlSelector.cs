using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataPuller.BAL
{
    public class HtmlSelector
    {
        public string selectorValue { get; set; }
        public SelectBy selectBy { get; set; }

        public HtmlSelector(string selectorValue,SelectBy selectBy)
        {
            this.selectorValue = selectorValue;
            this.selectBy = selectBy;
        }
    }
}
