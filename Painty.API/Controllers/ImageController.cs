using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Painty.API.Common;
using Painty.BAL.Interfaces;

namespace Painty.API.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ImageController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IUserService userServices;
        private readonly IWebHostEnvironment appEnvironment;
        private readonly ILogger<AuthControllers> logger;

        public ImageController(IConfiguration configuration, 
                                ILogger<AuthControllers> logger, 
                                IUserService userServices, 
                                IWebHostEnvironment appEnvironment)
        {
            this.configuration = configuration;
            this.logger = logger;
            this.userServices = userServices;
            this.appEnvironment = appEnvironment;
        }

        [HttpPost,Authorize, Route("image/upload")]
        public async Task<IActionResult> UplodaImage(IFormFile file)
        {
            var user = await userServices.GetByLogin(User.Identity.Name);

            var path = await SaveFile.SaveImage(appEnvironment, file, configuration["FileSetting:SaveFilePath"]);

            await userServices.UploadImage(new() { Path = path, UserId = user.Id }, user.Id);

            return Ok();
        }
    }
}
