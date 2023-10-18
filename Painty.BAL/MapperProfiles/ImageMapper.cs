using Painty.BAL.ModelsDTO;
using Painty.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painty.BAL.MapperProfiles
{
    public class ImageMapper
    {

        #region Image => ImageDTO

        public ImageDTO Map(Image img)
        {
            return new()
            {
                Id = img.Id,
                Path = img.Path,
                UserId = img.UserId,
            };
        }

        public List<ImageDTO> Map(List<Image> imgs)
        {
            List<ImageDTO> mapped = new();

            foreach (var item in imgs)
            {
                mapped.Add(Map(item));
            }

            return mapped;
        }

        #endregion

        #region ImageDTO => Image

        public Image Map(ImageDTO img)
        {
            return new()
            {
                Id = img.Id,
                UserId = img.UserId,
                Path = img.Path
            };
        }

        public List<Image> Map(List<ImageDTO> imgs)
        {
            List<Image> mapped = new();

            foreach (var item in imgs)
            {
                mapped.Add(Map(item));
            }

            return mapped;
        }

        #endregion

    }
}
