using ChatApp.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Entities
{
    public class Message : Entity
    {
        public Message(Guid fromUserId, string content, DateTimeOffset sendTime,Guid  conversationId)
        {
            Id = Guid.NewGuid();
            FromUserId = fromUserId;
            ConversationId = conversationId;
            Content = content;
            SendTime = sendTime;
        }
        public Guid FromUserId { get; private set; }
        public string Content { get; private set; }
        public DateTimeOffset? Read { get; private set; }
        public DateTimeOffset SendTime { get; private set; }
        public bool IsRead
        {
            get
            {
                return Read != null;
            }
        }
        public User FromUser { get;  private set; }
        public Guid ConversationId { get; private set; }
        public  virtual Conversation Conversation { get; private set; } 

        public void SetMessageRead()
        {
            SetMessageRead(DateTimeOffset.Now);
        }
        public void SetMessageRead(DateTimeOffset time)
        {
            Read = time;
        }

    }
}
