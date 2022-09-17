using ChatApp.Application.Interfaces.DAL;
using ChatApp.Application.Specifications.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Rules.Contracts
{
    public class BussinessRuleExistChecker<T> : IBussinessRule<T> where T : class
    {
        private readonly IChatDbContext _db;
        public BussinessRuleExistChecker(IChatDbContext db)
        {
            _db = db;
        }

        public bool IsSatisfied(Func<T, bool> funcCheck)
        {
            return _db.Set<T>().Any(funcCheck);
        }
    }
}
