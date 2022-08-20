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
        public Message(Guid fromUserId, Guid toUserId, string content, DateTimeOffset sendTime)
        {
            FromUserId = fromUserId;
            ToUserId = toUserId;
            Content = content;
            SendTime = sendTime;
        }
        public void SetMessageRead()
        {
            SetMessageRead(DateTimeOffset.Now);
        }
        public void SetMessageRead(DateTimeOffset time)
        {
            Read = time;
        }

        public Guid FromUserId { get; private set; }
        public Guid ToUserId { get; private set; }
        public string Content { get; private set; }
        public DateTimeOffset? Read { get; private set; }
        public DateTimeOffset? SendTime { get; private set; }
        public bool IsRead
        {
            get
            {
                return Read != null;
            }
        }

    }
}
