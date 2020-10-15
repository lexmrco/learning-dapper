using System;
using System.Collections.Generic;
using System.Text;

namespace Tekus.Datos.Filters
{
    public class ServicioFilter: FilterBase
    {
        public int ClienteID { get; set; }
        public override string OrderByColumnName => "Nombre";

    }
}
