using ChatApp.Application.Specifications.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Specifications
{
    public class UserExistedInDatabaseSpecification:ISpecification
    {   private string UserName { get; set; }
        public UserExistedInDatabaseSpecification(string userName)
        {
             this.UserName= userName;   
        }
   
         public bool IsSatisfied()
        {
            string query = "";
            return true;
        }
    }
}
