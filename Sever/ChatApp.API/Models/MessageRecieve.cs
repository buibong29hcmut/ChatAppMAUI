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
}
