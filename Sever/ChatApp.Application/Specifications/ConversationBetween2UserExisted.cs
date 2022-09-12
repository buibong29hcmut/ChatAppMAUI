using ChatApp.Application.Specifications.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Specifications
{
    public class ConversationBetween2UserExisted : ISpecification
    {   private Guid UserId { get; set; }
        private Guid OtherUserId { get; set; }
        public ConversationBetween2UserExisted(Guid userId, Guid otherUserId)
        {
            UserId = userId;
            OtherUserId = otherUserId;
        }
    
        public bool IsSatisfied()
        {
            throw new NotImplementedException();
        }
    }
}
 