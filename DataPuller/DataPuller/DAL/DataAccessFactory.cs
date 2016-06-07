using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataPuller.DAL
{
    public static class DataAccessFactory
    {
        public static IDataAccessLayer Create(int id)
        {
            switch (id)
            {
                case 0:
                    return new JsonFile();
              
                default:
                    return new JsonFile();
            }
        }
    }
}
