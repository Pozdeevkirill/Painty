using Painty.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painty.DAL.Interfaces
{
    public interface IImageRepository : IBaseRepository<Image>
    {
        public Task<List<Image>> GetByUser(int userId);
    }
}
