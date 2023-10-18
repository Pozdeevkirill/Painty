using Microsoft.EntityFrameworkCore;
using Painty.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painty.DAL.Data
{
    public class PaintyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Request> Requests{ get; set; }
        public DbSet<Friend> Friends{ get; set; }
        public DbSet<Image> Images { get; set; }
        public PaintyDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
