namespace Painty.API.Common
{
    public static class SaveFile
    {
        public static async Task<string> SaveImage(IWebHostEnvironment appEnvironment, IFormFile file, string path)
        {
            //TODO: Изменить название , что бы оно было уникальным
            string _path = $"{path}/{file.FileName}";
            using (var fs = new FileStream(appEnvironment.WebRootPath + _path, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }

            return _path;
        }
    }
}
