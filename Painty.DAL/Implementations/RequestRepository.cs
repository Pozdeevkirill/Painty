﻿using Microsoft.EntityFrameworkCore;
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
    public class RequestRepository : IRequestRepository
    {
        private readonly PaintyDbContext db;

        public RequestRepository(PaintyDbContext db)
        {
            this.db = db;
        }

        public async Task Delete(int id)
        {
            db.Requests.Remove(await Get(id));
        }

        public async Task<Request> Get(int id)
        {
            return await db.Requests.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
