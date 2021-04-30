using System;
using System.Collections.Generic;
using System.Text;

namespace ChurchMemberApp.Models.Response
{
    public class SyncDataResponse
    {
        public string userID { get; set; }
        public string tenantID { get; set; }
        public string password { get; set; }
    }
}
