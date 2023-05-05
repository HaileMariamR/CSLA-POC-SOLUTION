using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Model
{
    public class AuthModel
    {
        public bool isAuthenticated { get; set; }
        public User? CurrentUser { get; set; }      
    }
}
