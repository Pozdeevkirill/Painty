using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painty.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository UserRepository { get; }
        public IFriendRepository FriendRepository { get; }
        public IRequestRepository RequestRepository { get; }
        public IImageRepository ImageRepository { get; }

        public Task Save();
    }
}
