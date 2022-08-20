using ChatApp.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Entities
{
    public class Conversation:Entity
    {
        public Guid UserId { get; private set; }
        public Guid UserOtherId { get; private set; }
        public List<Message> Messages { get; private set; }
    }
}
