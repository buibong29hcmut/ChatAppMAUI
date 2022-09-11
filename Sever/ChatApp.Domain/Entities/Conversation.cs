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
        public Guid OtherUserId { get; private set; }
        public DateTime Created { get; private set; } = DateTime.Now;
        public User User { get; private set; }
        public User OtherUser { get; private set; }
        public virtual ICollection<Message> Messages { get; private set; }
        public Guid LastMessageId { get; private set; }
        public Conversation(Guid userId, Guid otherUserId, DateTime created)
        {
            UserId = userId;
            OtherUserId = otherUserId;
            Created = created;
      
        }
        public void SetLastMessage(Guid LastMessageId)
        {
            this.LastMessageId = LastMessageId;
        }
 
    }
}
