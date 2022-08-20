using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Commons
{
    public interface IEntity
    {
    }
    public interface IEntity<Tid> : IEntity
    {

    }
}
