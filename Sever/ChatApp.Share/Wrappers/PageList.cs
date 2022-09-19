using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Share.Wrappers
{
    public class PageList<T>:List<T>
    {
        public PageList(List<T> data, int count = 0, int pageNumer = 1, int pageSize = 6)
        {
            this.PageSize = pageSize;
            this.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.CurrentPage = pageNumer;

            AddRange(data);
        }
        public PageList(int count = 0, int pageNumer = 1, int pageSize = 6)
        {
            this.PageSize = pageSize;
            this.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.CurrentPage = pageNumer;
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public List<string> Messages { get; set; }

        public bool HasNextPage => CurrentPage < TotalPages;


    }
    public static class PageListHelper
    {

        public static async Task<PageList<T>> ToPageListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        {
            int count = source.Count();
            var items =  source.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            return new PageList<T>(items, count, pageNumber, pageSize);

        }
        public static PageList<T> ToPageList<T>(this IEnumerable<T> source, int pageNumber, int pageSize)
        {
            return new PageList<T>(source.ToList(), 0, pageNumber, pageSize);
        }
    }
}

