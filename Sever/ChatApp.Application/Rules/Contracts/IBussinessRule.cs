using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Specifications.Contracts
{   
    public interface IBussinessRule
    {
        bool IsSatisfied();
    }
    public interface IBussinessRule<T> where T:class
    {
        bool IsSatisfied(Func<T,bool> item);   
    }
}
