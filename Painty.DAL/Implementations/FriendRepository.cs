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
    public class FriendRepository : IFriendRepository
    {
        private readonly PaintyDbContext db;

        public FriendRepository(PaintyDbContext db)
        {
            this.db = db;
        }

        public async Task Delete(int id)
        {
            db.Friends.Remove(await Get(id));
        }

        public async Task<Friend> Get(int id)
        {
            return await db.Friends.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
