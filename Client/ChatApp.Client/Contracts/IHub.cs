using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Client.Contracts
{
    public interface IHub:IAsyncDisposable
    {
        Task Connect();

        Task DisConnect();
    }
}
