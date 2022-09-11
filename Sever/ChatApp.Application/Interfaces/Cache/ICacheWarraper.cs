using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Interfaces.Cache
{
    public interface ICacheWarraper
    {
        T Get<T>(string key);
        void CacheObject();
    }
}
