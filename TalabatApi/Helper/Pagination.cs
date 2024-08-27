using Talabat.Core.Entities;
using TalabatApi.DTOs;

namespace TalabatApi.Helper
{
    public class Pagination <T> 
    {
        public Pagination(int pageSize, int pageIndex, IReadOnlyList<T> items, int count)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            Items = items;
            TotalCount = count;
        }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int PageIndex { get; set; }
            
        public IReadOnlyList<T> Items { get; set; }
    }
}
