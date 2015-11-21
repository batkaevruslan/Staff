using System;
using System.Collections.Generic;
using System.Linq;

namespace RB.Staff.Common.Pub.Types
{
    public class PagedSearchResult< T >: List<T>
    {
        public List<T> Data { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PagedSearchResult(
            IQueryable<T> query,
            int pageNumber,
            int pageSize )
        {
            const int firstPage = 1;
            if( pageNumber < firstPage ) {
                throw new Exception( string.Format("Page number must be greater or equal to {0}", firstPage) );
            }

            PageNumber = pageNumber;
            PageSize = pageSize;
            var totalCount = query.Count();
            TotalPages = ( int ) Math.Ceiling( totalCount/( double ) pageSize );

            Data = query.Skip( ( pageNumber - 1 )*pageSize ).Take( pageSize ).ToList();
        }
    }
}