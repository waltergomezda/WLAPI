using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.DataManager
{
    public class DbParameter
    {
        public string Name { get; set; }
        public ParameterDirection Direction { get; set; }
        public object Value { get; set; }
        public int Size { get; set; }
        public DbParameter(string name, ParameterDirection direction, object value): this(name, direction, value, 0)
        { }
        public DbParameter(string name, ParameterDirection direction, object value, int size)
        {
            Name = name;
            Direction = direction;
            Value = value;
            Size = size;
        }
    }
}
