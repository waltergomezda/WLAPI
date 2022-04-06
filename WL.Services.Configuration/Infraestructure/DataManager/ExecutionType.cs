using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.DataManager
{
    public enum ExecutionType
    {
        Procedure, 
        NonQuery,
        Scalar,
        Query
    }
}
