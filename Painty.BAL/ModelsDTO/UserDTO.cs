using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painty.BAL.ModelsDTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public IEnumerable<UserDTO>? Friends{ get; set; }
        public IEnumerable<UserDTO>? FriendsRequest{ get; set; }
        public IEnumerable<ImageDTO>? Images { get; set; }
    }
}
