using Microsoft.EntityFrameworkCore;
using Painty.DAL.Data;
using Painty.DAL.Interfaces;
using Painty.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painty.DAL.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly PaintyDbContext db;
        public UserRepository(PaintyDbContext db)
        {
            this.db = db;
        }

        public async Task Create(User entity)
        {
            if (entity == null) return;

            await db.Users.AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            if (id <= 0) return;

            var _user = await Get(id);
            if (_user == null) return;

            db.Users.Remove(_user);
        }

        public async Task<IEnumerable<User>> Get()
        {
            return await db.Users
                .Include(u => u.Friends).Include(u => u.Images)
                .ToListAsync();
        }

        public async Task<User> Get(int id)
        {
            if (id <= 0) return new();

            return await db.Users.Include(u => u.Friends).Include(u=>u.Requests).Include(u=>u.Images).FirstOrDefaultAsync(u => u.Id == id) ?? new();
        }

        public async Task<User> GetByLogin(string login)
        {
            if (login == "" || login == null) return new();

            return await db.Users.Include(u => u.Friends).Include(u => u.Requests).Include(u => u.Images).FirstOrDefaultAsync(u => u.Login == login) ?? new();
        }

        public async Task Update(User entity)
        {
            if (entity == null) return;

            var _user = await Get(entity.Id);
            if (_user == null) return;
            
            _user.Login = entity.Login;
            _user.Password = entity.Password;
            _user.Friends = entity.Friends;
            _user.Requests = entity.Requests;

            db.Users.Update(_user);
        }
    }
}
