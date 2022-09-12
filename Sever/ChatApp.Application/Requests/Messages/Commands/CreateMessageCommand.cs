using ChatApp.Application.Cores.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Requests.Messages.Commands
{
    public class CreateMessageCommand : ICommand<Unit>
    {
        public Guid ConversationId { get; set; }
        public string Content { get; set; }
        public DateTime SendTime { get; set; } = DateTime.Now;

    }
}
