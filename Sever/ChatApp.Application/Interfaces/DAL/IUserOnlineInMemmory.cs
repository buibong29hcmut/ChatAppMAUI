using ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Interfaces.DAL
{
    public interface IUserOperationInMemmory
    {
         void AddUserConnection(string userName, string connectionId);


         List<UserConnection> GetListConnection(string userName);


         void RemoveConnection(string userName, string connectionId);


         int CountConnection(string userName);


          Task<bool> IsUserOnline(string userName);
      
    }
}
