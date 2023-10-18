using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painty.DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public List<Friend> Friends { get; set; }
        public List<Request> Requests { get; set; } = new();
        public List<Image> Images { get; set; }
    }
}

