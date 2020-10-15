using System;
using System.Collections.Generic;
using System.Text;

namespace Tekus.Datos.Repositories
{
    public class QueryResult
    {
        public int TotalRows { get; set; }
        public int RowsPerPage { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }

        public IEnumerable<Object> Data { get; set; }
}
}
