using System;
using System.Collections.Generic;
using System.Text;

namespace ChurchMemberApp.Models.Request
{
    public class ContributionsReq
    {
        public DateTime startDate { get; set; } = DateTime.Parse("01/01/0001 00:00:00");
        public DateTime endDate { get; set; } = DateTime.Parse("12/31/9999 11:59:59");
        public string userId { get; set; }
        public string tenantId { get; set; }
    }
}
