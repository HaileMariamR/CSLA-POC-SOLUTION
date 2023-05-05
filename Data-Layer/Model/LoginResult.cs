using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Model
{
    public class LoginResult
    {
        public string? Message { get; set; }
        public string? UserName { get; set; }
        public string? JwtToken { get; set; }   
        public string? Role { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }

    }
}
