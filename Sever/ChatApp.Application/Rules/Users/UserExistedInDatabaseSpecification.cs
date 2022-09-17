using ChatApp.Application.Specifications.Contracts;
using ChatApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Rules.Users
{
    public class UserExistedInDatabaseSpecification : IBussinessRule
    {
        private string UserName { get; set; }
        private readonly IBussinessRule<User> _rule;
        public UserExistedInDatabaseSpecification(IBussinessRule<User> rule,string userName)
        {
            UserName = userName;
            _rule = rule;   
        }

        public bool IsSatisfied()
        {
           return  _rule.IsSatisfied(p=>p.UserName==UserName);
        }
    }
}
