using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Response
{
    public class BoxChatResponse
    {
        public Guid ConversationId { get; set; }
        public string Message { get; set; }
        public string UrlProfile { get; set; }
        public string Name { get; set; }
        public bool IsOnline { get; set; }
        public DateTimeOffset SeenMessage { get; set; }
        public DateTimeOffset TimeMessage { get; set; }
    }

    public class BoxChatRawQuery
    {
        public Guid ConversationId { get; set; }
        public Guid UserId { get; set; }
        public Guid OtherUserId { get; set; }
        public Guid LastMessageId { get; set; }
        public string Content { get; set; }
        public DateTimeOffset SendTime { get; set; }
        public string Message { get; set; }
        public DateTimeOffset Read { get; set; }
    }
    public class UserProfileByConversation
    {
        public string UserName { get;  set; }
        public string UrlAvatar { get;  set; }
        public string Name { get;  set; }
      
    }
}
