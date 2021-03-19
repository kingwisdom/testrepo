using System;
using System.Collections.Generic;
using System.Text;

namespace ChurchMemberApp.Models.Request
{
    public class Register
    {
        public string name { get; set; }
        public string email { get; set; }
        public string tenantID { get; set; }
        public string password { get; set; }
        public string phoneNumber { get; set; }
    }
}
