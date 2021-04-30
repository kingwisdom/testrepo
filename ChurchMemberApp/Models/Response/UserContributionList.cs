using System;
using System.Collections.Generic;
using System.Text;

namespace ChurchMemberApp.Models.Response
{
    public class UserContributionList
    {
        public string memo { get; set; }
        public string date { get; set; }
        public string paymentGatewayName { get; set; }
        public Guid paymentGatewayId { get; set; }
        public string amount { get; set; }
    }
}
