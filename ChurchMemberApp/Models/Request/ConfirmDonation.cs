using System;
using System.Collections.Generic;
using System.Text;

namespace ChurchMemberApp.Models.Request
{
    public class ConfirmDonation
    {
        public string paymentFormId { get; set; }
        public string churchLogoUrl { get; set; }
        public string churchName { get; set; }
        public string tenantID { get; set; }
        public bool isAnonymous { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string userID { get; set; }
        public string merchantID { get; set; }
        public string currencyId { get; set; }
        public string superMerchantID { get; set; }
        public string orderID { get; set; }
        public string paymentGatewayId { get; set; } = "00000000-0000-0000-0000-000000000000";
        public List<Contribution> contributionItems { get; set; }
    }

}
