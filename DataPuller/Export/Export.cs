using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataPullerHelper;
using Logger;


namespace Export
{
    abstract class Export
    {
        public abstract void InitializeConnection();
        public abstract void OpenConnection();
        public abstract void CreateTable(dynamic propList);
        public abstract void PopulateTable(dynamic list, dynamic propList);
        public abstract void CloseConnection(string path);
        
    }
}
