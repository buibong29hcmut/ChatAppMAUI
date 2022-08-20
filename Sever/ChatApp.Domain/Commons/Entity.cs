using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Commons
{
    public class Entity:IEntity<Guid>
    {
        public Guid Id { get; set; }
    }
}
