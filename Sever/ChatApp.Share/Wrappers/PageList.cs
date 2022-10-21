using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JsonConstructorAttribute = Newtonsoft.Json.JsonConstructorAttribute;
namespace ChatApp.Share.Wrappers
{
    public class PageList<T>
    {
        [JsonConstructor]   
        public PageList(List<T> data, int count = 0, int pageNumer = 1, int pageSize = 6)
        {
            this.PageSize = pageSize;
            this.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.CurrentPage = pageNumer;

            Items = data;
        }
        public List<T> Items { get;  set; }
        public PageList(int count = 0, int pageNumer = 1, int pageSize = 6)
        {
            this.PageSize = pageSize;
            this.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.CurrentPage = pageNumer;
            Items = new List<T>();
        }
        public static PageList<T> Empty => new PageList<T>(0, 0, 0);
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public void Add(T item)
        {
            Items.Add(item);
        }
        public void AddRange(IEnumerable<T> items)
        {
            Items.AddRange(items);
        }


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

