using Painty.DAL.Data;
using Painty.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painty.DAL.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PaintyDbContext db;

        public UnitOfWork(PaintyDbContext db)
        {
            this.db = db;
        }

        private UserRepository userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                    userRepository = new(db);
                return userRepository;
            }
        }

        private FriendRepository friendRepository;
        public IFriendRepository FriendRepository
        {
            get
            {
                if(friendRepository == null)
                    friendRepository = new(db);
                return friendRepository;
            }
        }

        private RequestRepository requestRepository;
        public IRequestRepository RequestRepository
        {
            get
            {
                if (requestRepository == null)
                    requestRepository = new(db);
                return requestRepository;
            }
        }

        private ImageRepository imageRepository;
        public IImageRepository ImageRepository
        {
            get
            {
                if(imageRepository == null) 
                    imageRepository = new(db);
                return imageRepository;
            }
        }

        public async Task Save()
        {
            await db.SaveChangesAsync();
        }


        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
