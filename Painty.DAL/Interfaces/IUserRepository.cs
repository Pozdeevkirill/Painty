using Painty.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painty.DAL.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<User> GetByLogin(string login);
    }
}
