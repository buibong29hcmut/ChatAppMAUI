using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Client.Models
{
    public class MessageModel
    {

        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTimeOffset SendTime { get; set; }
        public Guid FromUserId { get; set; }
        public bool IsThisUser { get; set; }
        public static List<MessageModel> MessagesCreate()
        { var list = new List<MessageModel>();
            for (int i = 0; i < 40; i++)
            {
                MessageModel message = new MessageModel()
                {
                    Content = "Abc",
                    SendTime = DateTimeOffset.Now,
                    FromUserId = Guid.NewGuid(),
                    IsThisUser = new Random().Next(0, 2) == 0 ? true : false,
                };
                list.Add(message);
             }
            return list;
            }
           
        }

    }

