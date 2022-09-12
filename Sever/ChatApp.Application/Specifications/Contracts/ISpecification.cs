using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Specifications.Contracts
{   
    public interface ISpecification
    {
        bool IsSatisfied();
    }
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T item);   
    }
}
