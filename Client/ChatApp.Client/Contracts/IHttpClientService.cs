using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Client.Contracts
{
    public interface IHttpClientService
    {
        Task<T> GetAsync<T>(string uri);
        Task<T> PostAsync<T>(string uri,object val);
    }
}
