using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Exceptions
{
    public class BussinessException:Exception
    {
        public BussinessException(string message)
            : base(message)
        {

        }
    }
}
