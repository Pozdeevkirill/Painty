using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Painty.API.Common;
using Painty.API.ViewModels;
using Painty.BAL.Interfaces;
using Painty.BAL.ModelsDTO;

namespace Painty.API.Controllers
{

    [ApiController]
    [Route("api/")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userServices;
        private readonly ILogger<UserController> logger;
        public UserController(IUserService userServices, ILogger<UserController> logger) 
        {
            this.logger = logger;
            this.userServices = userServices;
        }

        [HttpGet , AllowAnonymous, Route("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await userServices.GetAll());
        }

        [HttpPost, Authorize, Route("sendRequest")]
        public async Task<IActionResult> SendRequest(int id)
        {
            if (id <= 0) return BadRequest(new Response<int>() { StatusCode = 400, Message = "Данного пользователя не существует"});
            //Тот, кто отправляет запрос
            var user = await userServices.GetByLogin(User.Identity.Name);

            if (id == user.Id) return BadRequest(new Response<int> { StatusCode = 400, Message = "Вы не можете отправить сами себе запрос" });

            //Тот, кому отправляют запрос
            var _user = await userServices.GetUser(id);
            if (_user.FriendsRequest.FirstOrDefault(u => u.Id == user.Id) != null) 
                return BadRequest(new Response<int> { StatusCode = 200, Message = "Ранее вы уже отправляли запрос данном человеку" });

            if (_user.Id <= 0) return NotFound(new Response<int> { StatusCode = 404, Message = "Пользователь с таким id небыл найден" });

            await userServices.SendRequest(user.Id, id);
            logger.LogInformation($"[{DateTime.Now}] - UserController.SendRequest: Запрос от пользователя {user.Login} успешно отправлен {_user.Login}");
            return Ok(new Response<string> { StatusCode = 200, Message = "Запрос успешно отправлен" });
        }

        [HttpGet, Authorize, Route("requests")]
        public async Task<IActionResult> GetRequests()
        {
            var user = await userServices.GetByLogin(User.Identity.Name);
            List<UserVM> result = new();
            foreach (var item in user.FriendsRequest)
            {
                UserVM _user = new()
                {
                    Id = item.Id,
                    Login = item.Login,
                };
                result.Add(_user);
            }
            return Ok(new Response<List<UserVM>> { StatusCode = 200, Data = result});
        }

        [HttpPost, Authorize, Route("acceptRequest")]
        public async Task<IActionResult> AccetRequest(int id)
        {
            var user = await userServices.GetByLogin(User.Identity.Name);
            if (user.FriendsRequest.Count() == 0) 
                return NotFound(new Response<string> { StatusCode = 404, Message = "У вас нету запросов \"в друзья\"" });

            var request = user.FriendsRequest.FirstOrDefault(rq => rq.Id == id);
            if (request == null) 
                return NotFound(new Response<string> { StatusCode = 404, Message = "Не удалось найти запрос с таким id" });

            await userServices.AcceptRequest(user.Id, id);
            return Ok(new Response<string> { StatusCode = 200, Message = "Пользователь успешно добавлен в \"Друзья\"" });
        }

        [HttpPost, Route("getUser")]
        public async Task<IActionResult> GetUser(int id)
        {

            var user = await userServices.GetUser(id);
            if (user.Id <= 0) return NotFound(new Response<UserDTO>() { StatusCode = 404, Message = "Пользователь с таким id не найден" });

            user.FriendsRequest = null;
            if (User.Identity.Name != user.Login)
            {
                if (User.Identity.IsAuthenticated && user.Friends.FirstOrDefault(x => x.Login == User.Identity.Name) == null)
                    user.Images = null;
                if (!User.Identity.IsAuthenticated)
                    user.Images = null;
            } 

            return Ok(new Response<UserDTO>() { StatusCode = 200, Data = user });
        }
    }
}
