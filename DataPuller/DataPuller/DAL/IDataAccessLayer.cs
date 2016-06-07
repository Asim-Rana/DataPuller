using DataPuller.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataPuller.DAL
{
    public interface IDataAccessLayer
    {
       void Save(dynamic name,dynamic obj);
       T Get<T>(string name);
       T GetTemplate<T>(string name);
    }
}
