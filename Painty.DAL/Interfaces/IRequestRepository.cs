using Painty.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painty.DAL.Interfaces
{
    public interface IRequestRepository
    {
        public Task<Request> Get(int id);
        public Task Delete(int id);
    }
}
