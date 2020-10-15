using System;
using System.Collections.Generic;
using System.Text;

namespace Tekus.Datos.Filters
{
    public  class Pagination
    {
        public Pagination()
        {
            DefaultValues();
        }
        public Pagination(int page)
        {
            DefaultValues();
            _page = page;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderType">Cómo ordenar la consulta, valores posibles:
        /// asc
        /// desc</param>
        public Pagination(string orderType)
        {
            DefaultValues();
            _orderType = orderType;
        }
        public Pagination(int page, int rowsPerPage)
        {
            DefaultValues();
            _rowsPerPage = rowsPerPage;
            _page = page;
        }

        void DefaultValues()
        {
            _maxRow = 0;
            _minRow = 0;
            _rowsPerPage = 10;
            _page = 1;
            _orderType = "asc";
        }
        private string _orderType;

        public string OrderType => _orderType;

        private int _rowsPerPage;

        public int RowsPerPage
        {
            get { return _rowsPerPage; }
        }
        private int _page;
        public int Page
        {
            get { return _page; }
        }
        private int _minRow;
        public int MinRow
        {
            get { return _minRow; }
        }
        private int _maxRow;
        public int MaxRow
        {
            get { return _maxRow; }
        }

        private int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
        }
        public void SetPagination(int totalRows)
        {
            bool paginated = (_page > 1) ? true : (_rowsPerPage > 0);
            if (paginated)
            {
                decimal estimatedPages = totalRows / _rowsPerPage;
                decimal roundedPages = Math.Truncate(estimatedPages);
                int extraPage = (totalRows % _rowsPerPage > 0) ? 1 : 0;
                _totalPages = (int)(roundedPages + extraPage);
                if(_totalPages >= _page)
                {
                    _maxRow = _page * _rowsPerPage;
                    _minRow = _maxRow - _rowsPerPage + 1;
                }
            }
            else
            {
                _totalPages = 1;
                _maxRow = _rowsPerPage;
                _minRow = 1;
            }
        }

    }
}
