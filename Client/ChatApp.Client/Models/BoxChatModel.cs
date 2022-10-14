using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Client.Models
{
    public class BoxChatModel
    {
        public Guid ConversationId { get; set; }
        public UserBoxChatResponse User { get; set; }
        public string Message { get; set; }
        public DateTimeOffset SeenMessage { get; set; }
        public DateTimeOffset TimeMessage { get; set; }
    }
    public class UserBoxChatResponse
    {  
        public Guid Id { get; set; }
        public string UrlProfile { get; set; }
        public string Name { get; set; }
        public bool IsOnline { get; set; }
    }
    public class FactoryBoxChatProperty
    {
        public static string[] Names = { "Bùi Bổng", "Huy Bùi", "Ngọc Bảo", "Đoàn Thế Đạt", "Thu Hà", "Thu Trà", "Ánh Ngọc", "Lê Hồng Phúc" };
       
    }
}
