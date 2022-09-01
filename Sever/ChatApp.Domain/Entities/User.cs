using ChatApp.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Entities
{
    public class User : Entity
    {
        public string UserName { get;  set; }
        public string Password { get;  set; }
        public string ?UrlAvatar { get;  set; }
        public string ?PhoneNumber { get;  set; }
        public string ?Email { get;  set; }
        public string Salt { get;  set; }
        public string ?Name { get;  set; }
        public User()
        {

        }
        public User(string UserName)
        {
            this.UserName = UserName;
            
        }

        public Message SendMessage(Guid FromUserId, Guid ToUserId, DateTimeOffset Send,string content)
        {
            return new Message(this.Id, ToUserId, content, Send);
        }
        public void ReadMessage(Message message)
        {
            message.SetMessageRead();
        }
        public void UpdateInfo(string Name, string UrlAvatar)
        {
            this.Name = Name;
            this.UrlAvatar = UrlAvatar;
        }
        public static User CreateRegister(string UserName, string Password, string Salt,string Name)
        {
            return new User()
            {
                Id = Guid.NewGuid(),
                UserName = UserName,
                Password = Password,
                Salt = Salt,

                Name=Name,

            };
        }
        public void UploadAvatar(string urlImage)
        {
            this.UrlAvatar = urlImage;
        }
      
    }
}
