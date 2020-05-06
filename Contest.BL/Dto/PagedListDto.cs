using System;
using System.Collections.Generic;

namespace Contest.BL.Dto
{
    public class PagedListDto<T>
    {
        public PagedListDto(int pageNumber, int pageSize, int totalCount, List<T> items)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            Items = items;
        }

        public List<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public bool HasNextPage
        {
            get
            {
                return PageNumber >= 1 && PageNumber < TotalPages;
            }
        }
    }
}
