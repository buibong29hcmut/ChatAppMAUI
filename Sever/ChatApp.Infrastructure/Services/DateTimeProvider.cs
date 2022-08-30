using ChatApp.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure.Services
{
    public class DateTimeProvider:IDateTimeProvider
    {
        public DateTime UtcNow { get; private set; } = DateTime.UtcNow;
    }
}
