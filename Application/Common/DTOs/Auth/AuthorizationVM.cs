using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.DTOs.Auth
{
    public class AuthorizationVM
    {
        public string UserId { get; set; }
        public string AccessToken { get; set; } 
        public string RefreshToken { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }

        public AuthorizationVM() 
        { 
            Roles= new List<string>();
        }
    }
}
