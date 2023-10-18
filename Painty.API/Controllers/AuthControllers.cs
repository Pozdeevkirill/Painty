using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Painty.API.Common;
using Painty.API.ViewModels;
using Painty.BAL.Interfaces;
using Painty.BAL.ModelsDTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Painty.API.Controllers
{
    [Route("api/")]
    public class AuthControllers : ControllerBase
    {
        private readonly IUserService userServices;
        private readonly ILogger<AuthControllers> logger;
        private readonly IConfiguration _configuration;

        public AuthControllers(IUserService userServices, ILogger<AuthControllers> logger, IConfiguration configuration)
        {
            this.userServices = userServices;
            this.logger = logger;
            _configuration = configuration;
        }

        [HttpPost, AllowAnonymous, Route("register")]
        public async Task<IActionResult> Register([FromForm] RegisterVM registerVM)
        {
            var isExist = await userServices.GetByLogin(registerVM.Login);
            if (isExist.Id > 0) return BadRequest(new Response<RegisterVM>() { 
                StatusCode=400, 
                Message="Пользователь с таким логином уже зарегестрирован", 
                Data=registerVM 
            });

            if (registerVM.Password != registerVM.ConfirmPassword) 
                return BadRequest(new Response<RegisterVM>()
                {
                    StatusCode = 400,
                    Message = "Пароли не совпадают",
                    Data = registerVM
                });

            UserDTO user = new()
            {
                Login = registerVM.Login,
                Password = registerVM.Password,
            };

            await userServices.Create(user);
            logger.LogInformation($"[{DateTime.Now}] - AuthControllers.Register: Регистриция прошла успешно!");
                return Ok(new Response<RegisterVM>()
                {
                    StatusCode = 200,
                    Message = "Пользователь успешно зарегестрирован",
                    Data = registerVM,
                });
        }

        [HttpPost, AllowAnonymous, Route("login")]
        public async Task<IActionResult> Login ([FromForm]LoginVM loginVM)
        {
            var user = await userServices.GetByLogin(loginVM.Login);

            if (user.Id <= 0) return BadRequest(new Response<LoginVM>
            {
                StatusCode = 400,
                Message = "Пользователь с такми логином небыл найден",
                Data = loginVM
            });

            if (user.Password != loginVM.Password) return BadRequest(new Response<LoginVM>
            {
                StatusCode = 400,
                Message = "Неверно указан логин и/или пароль",
                Data = loginVM
            });

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JsonWebTokenKeys:IssuerSigningKey"]));
            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(3),
                claims: authClaims, 
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            logger.LogInformation($"[{DateTime.Now}] - AuthControllers.Login: Пользователь успешно авторизован!");
            return Ok(new
            {
                api_key = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                user = user,
                status = "Пользователь успешно авторизован"
            });

        }
    }
}
