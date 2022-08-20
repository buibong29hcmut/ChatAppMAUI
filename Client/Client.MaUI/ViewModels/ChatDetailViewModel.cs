using Client.MaUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MaUI.ViewModels
{
    public class ChatDetailViewModel:BaseViewModel
    {
        public ObservableCollection<MessageModel> Messages { get; } = new ObservableCollection<MessageModel>();
        public int FromUserId { get; set; } = 1;
        public int ToUserId { get; set; } = 2;
        public ChatDetailViewModel()
        {
            GetChatMessage();
        }
        public void GetChatMessage()
        {
            if (Messages.Count() ==0)
            {
                var items = MessageModel.Messages();
                foreach (var item in items)
                {
                    Messages.Add(item);
                }
            }
        }
        
    }
}
