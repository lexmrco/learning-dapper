using System;
using System.Collections.Generic;
using System.Text;

namespace Tekus.Datos.Filters
{
    public class ClienteFilter: FilterBase
    {
        public int PaisID { get; set; }

        public override string OrderByColumnName => "Nombre";
    }
}
