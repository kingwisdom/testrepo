using System;
using System.Collections.Generic;
using System.Text;

namespace ChurchMemberApp.Models.Request
{
    public class CheckDataRequest
    {
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string tenantId { get; set; }
    }
}
