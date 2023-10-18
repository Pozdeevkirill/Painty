using Painty.BAL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painty.BAL.Interfaces
{
    public interface IUserService
    {
        public Task AcceptRequest(int uid, int id);
        public Task Create(UserDTO user);
        public Task<IEnumerable<UserDTO>> GetAll();
        public Task<UserDTO> GetByLogin(string login);
        public Task<UserDTO> GetUser(int id);
        public Task SendRequest(int uid, int id);
        public Task UploadImage(ImageDTO img, int uid);
    }
}
