using System;
using System.Collections.Generic;
using System.Text;

namespace Tekus.Datos.Filters
{
    public abstract class FilterBase
    {
        public abstract string OrderByColumnName { get; }
        
    }
}
