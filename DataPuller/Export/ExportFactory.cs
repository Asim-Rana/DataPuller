using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Export
{
    internal static class ExportFactory
    {
        internal static Export Create(int driverId)
        {
            switch (driverId)
            {
                case 0:
                    return new CSV();
                case 1:
                    return new Excel();
                case 2:
                    return new PDF();
                case 3:
                    return new SqlServer();
                default:
                    return new CSV();
            }
        }
    }
}
        
    

