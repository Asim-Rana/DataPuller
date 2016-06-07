using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataPuller.BAL
{
    public class DomAction
    {
        public ActionType actionType { get; set; }
        public string value { get; set; }
        public ActionSelectBy actionSelectyBy { get; set; }
        public DomAction(ActionType actionType, string values=null)
        {
            this.actionType = actionType;
            this.value = values;
        }
        public DomAction()
        {

        }
        public DomAction(ActionType actionType, string values,ActionSelectBy selectBy)
        {
            this.actionType = actionType;
            this.value = values;
            this.actionSelectyBy = selectBy;
        }
        public enum ActionType
        {
            CLICK,
            MOUSE_OVER,
            SCROLL,
            SELECT,
            ENTER_TEXT
        }

        public enum ActionSelectBy
        {
            VISIBLE_TEXT,
            INDEX,
            VALUE
        }
    }
}

   
