using System;
using System.Collections.Generic;
using System.Text;

namespace ChurchMemberApp.Models.Request
{
    public class ResetPassword
    {
        public string email { get; set; }
        public string password { get; set; }
        public string resetToken { get; set; }
    }
}
