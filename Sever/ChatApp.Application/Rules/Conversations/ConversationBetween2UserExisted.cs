using ChatApp.Application.Interfaces.DAL;
using ChatApp.Application.Specifications.Contracts;
using ChatApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Rules.Conversations
{
    public class ConversationBetween2UserExisted: IBusinessChecker
    { 
        private readonly IBussinessRule<Conversation> _rule;
        private  Guid UserId { get; set; }
        private  Guid OtherUserId { get; set; }
        public ConversationBetween2UserExisted(IBussinessRule<Conversation> rule,
            Guid userId, Guid otherUserId)
        {
            _rule = rule;  
            UserId= userId;
            OtherUserId = otherUserId;
        }

        public bool IsSatisfied() 
        {
            Func<Conversation, bool> func = p => p.UserId == UserId && p.OtherUserId==OtherUserId;  
            return _rule.IsSatisfied(func); 
        }
    }
}
