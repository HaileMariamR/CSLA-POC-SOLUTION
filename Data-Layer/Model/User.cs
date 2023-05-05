﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Model
{
    public class User
    {
        public int Id { get; set; }
        public string?  UserName { get; set; }    
        public string? Password { get; set; }

        public string? Role { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
}
}
