using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painty.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        public Task Create(T entity);
        public Task Update(T entity);
        public Task Delete(int id);
        public Task<IEnumerable<T>> Get();
        public Task<T> Get(int id);
    }
}
