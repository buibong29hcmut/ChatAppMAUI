using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Cores.Commands
{
    public interface ICommandBus
    {
        Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default(CancellationToken));

        Task Send(ICommand command, CancellationToken cancellationToken = default(CancellationToken));
    }
}
