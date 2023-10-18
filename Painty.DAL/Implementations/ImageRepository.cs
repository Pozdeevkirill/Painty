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
    public class ImageRepository : IImageRepository
    {
        private readonly PaintyDbContext db;

        public ImageRepository(PaintyDbContext db)
        {
            this.db = db;
        }

        public async Task Create(Image entity)
        {
            if (entity == null) return;
            
            await db.Images.AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            if (id <= 0) return;
            var img = await Get(id);
            if (img.Id <= 0) return;

            db.Images.Remove(img);
        }

        public async Task<IEnumerable<Image>> Get()
        {
            return await db.Images.ToListAsync();
        }

        public async Task<Image> Get(int id)
        {
            if (id <= 0) return new();
            return await db.Images.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Image>> GetByUser(int userId)
        {
            if (userId <= 0) return new();

            return await db.Images.Where(i => i.UserId == userId).ToListAsync();
        }

        public async Task Update(Image entity)
        {
            if (entity == null) return;

            var img = await Get(entity.Id);
            if (img.Id <= 0) return;

            img.UserId = entity.UserId;
            img.Path = entity.Path;

            db.Images.Update(img);
        }
    }
}
