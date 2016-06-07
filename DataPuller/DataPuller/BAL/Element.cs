using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataPuller.BAL
{
    public class Element
    {
        public HtmlSelector htmlSelector { get; set; }
        public AttributeType attributeType { get; set; }
        public string attributeValue { get; set; }
       
        public Element() { 
        
        }
        public Element(HtmlSelector selector)
        {
            this.htmlSelector = selector;
        }
        public Element(HtmlSelector selector,AttributeType valueAttributeType)
        {
            this.htmlSelector = selector;
            this.attributeType = valueAttributeType;
        }
        public Element(HtmlSelector selector, string value)
        {
            this.htmlSelector = selector;
            attributeValue = value;
        }
    }
}
