using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painty.DAL.Models
{
    public class Request
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string UserName { get; set; }
    }
}
