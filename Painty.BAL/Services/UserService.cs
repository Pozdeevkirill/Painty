using Painty.BAL.Interfaces;
using Painty.BAL.MapperProfiles;
using Painty.BAL.ModelsDTO;
using Painty.DAL.Interfaces;
using Painty.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painty.BAL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork db;
        private readonly UserMapper mapper;

        public UserService(IUnitOfWork db, UserMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task AcceptRequest(int uid, int id)
        {
            //Тот, кто отправляет
            var user = await db.UserRepository.Get(id);
            //Тот, кому отправляют
            var _user = await db.UserRepository.Get(uid);

            await db.RequestRepository.Delete(_user.Requests.FirstOrDefault(rq => rq.UserName == user.Login).Id);
            _user.Requests.Remove(_user.Requests.FirstOrDefault(rq => rq.UserName == user.Login));


            user.Friends.Add(new() { FriendId = _user.Id, FriendName = _user.Login});
            _user.Friends.Add(new() { FriendId = user.Id, FriendName = user.Login });

            await db.UserRepository.Update(user);
            await db.UserRepository.Update(_user);
            await db.Save();
        }

        public async Task Create(UserDTO user)
        {
            if (user == null) return;

            await db.UserRepository.Create(mapper.Map(user));
            await db.Save();
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var list = await db.UserRepository.Get();
            return mapper.Map(list.ToList());
        }

        public async Task<UserDTO> GetByLogin(string login)
        {
            if (login == "" || login == null) return new();

            var user = await db.UserRepository.GetByLogin(login);
            return mapper.Map(user);
        }

        public async Task<UserDTO> GetUser(int id)
        {
            if (id <= 0) return new();
            return mapper.Map(await db.UserRepository.Get(id));
        }

        public async Task SendRequest(int uid, int id)
        {
            //Поиск пользователей
            var user = await db.UserRepository.Get(uid);
            var _user = await db.UserRepository.Get(id);
            //Добавление в список запросов
            var friendsRequests = _user.Requests.ToList();
            Request req = new Request() { SenderId = user.Id, UserName = user.Login };

            friendsRequests.Add(req);
            _user.Requests = friendsRequests;

            //Обновление данных в бд
            await db.UserRepository.Update(_user);
            await db.Save();
        }

        public async Task UploadImage(ImageDTO img, int uid)
        {
            if (img == null) return;

            var user = await db.UserRepository.Get(uid);

            if (user.Images == null)
                user.Images = new();

            user.Images.Add(new ()
            {
                Path = img.Path,
                UserId = user.Id,
            });
            await db.UserRepository.Update(user);
            await db.Save();
        }
    }
}
