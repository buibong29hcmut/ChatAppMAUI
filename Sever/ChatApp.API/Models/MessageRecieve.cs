using ChatApp.Domain.Entities;

namespace ChatApp.API.Models
{
    public class MessageRecieve
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTimeOffset SendTime { get; set; }
        public Guid FromUserId { get; set; }
    }
    public class ConversationMessageRecieve
    {
        public Guid ConversationId { get; set; }
        public UserConversationRecieve User { get; set; }
        public MessageRecieve LastMessage { get; set; }
    }
    public class UserConversationRecieve
    {
        public Guid Id { get; set; }
        public string UrlProfile { get; set; }
        public string Name { get; set; }
        public bool IsOnline { get; set; }

    }


}
