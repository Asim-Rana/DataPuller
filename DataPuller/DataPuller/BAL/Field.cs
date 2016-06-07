using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataPuller.BAL
{
    public class Field
    {
        public string fieldName { get; set; }
        public Element htmlElement { get; set; }

        public Field(string name,Element element)
        {
            this.fieldName = name;
            this.htmlElement = element;
        }
    }
}
