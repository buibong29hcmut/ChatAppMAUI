﻿using ChatApp.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Client.Contracts
{
    public interface IChatHub:IHub
    {
        Task SendMessageToConversation(MessageForSendConversation message);
        void AddMessageHandler(Action<MessageModel> action);
    }
}
