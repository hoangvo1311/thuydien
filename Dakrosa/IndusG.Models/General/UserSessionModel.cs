using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IndusG.Models.Enums;

namespace IndusG.Models
{
    public class UserSessionModel
    {
        public string Username { get; set; }
        public UserType UserType { get; set; }
    }

}
