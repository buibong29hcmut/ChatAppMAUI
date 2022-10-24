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
        public string Password { get; private  set; }
        public string ?UrlAvatar { get;  private set; }
        public string ?PhoneNumber { get; private set; }
        public string ?Email { get; private set; }
        public string Salt { get; private set; }
        public string ?Name { get;  private set; }
        public User()
        {

        }
        public User(string UserName)
        {
            this.UserName = UserName;
            
        }

        public Message SendMessage(Guid FromUserId, Guid ConversationId, DateTimeOffset Send,string content)
        {
            return new Message(this.Id, content, Send,ConversationId);
        }
        public void ReadMessage(Message message)
        {
            message.SetMessageRead();
        }
     
  
        public static User CreateUser(string UserName,string Name, string PasswordHash, string Salt)
        {
            return new User()
            {
                Id = Guid.NewGuid(),
                UserName = UserName,
                Password = PasswordHash,
                Name = Name,
                Salt = Salt,

            };
        }
        public void UploadAvatar(string urlImage)
        {
            this.UrlAvatar = urlImage;
        }
      
    }
}
