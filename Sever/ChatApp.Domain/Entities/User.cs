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
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string UrlAvatar { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public byte[] Salt { get; private set; }
        public string Name { get; private set; }
        protected User()
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
        public static User CreateRegister(string UserName, string Password, byte[] Salt)
        {
            return new User()
            {
                UserName = UserName,
                Password = Password,
                Salt = Salt,

            };
        }
        public void UploadAvatar(string urlImage)
        {
            this.UrlAvatar = urlImage;
        }
      
    }
}
