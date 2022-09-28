using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Client.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public int FromUserId { get; set; } = 1;
        public int ToUserId { get; set; } = 2;
        public DateTime SendDateTime { get; set; }
        public bool IsRead { get; set; }
        public string Content { get; set; } = "Hello, Message"+" " + new Random().Next(0, 100);
        public string DateTime { get; private set; } = "12:30 AM";
        public string Status { get; set; } = "Đã xem";
        public bool IsUser { get; set; } = new Random().Next(0, 2) == 1 ? true : false;
  
        public static List<MessageModel> Messages()
        {
            var result = new List<MessageModel>();
            for(int i = 0; i < 20; i++)
            {
                result.Add(new MessageModel());
            }
            return result;
        }
    }
}
