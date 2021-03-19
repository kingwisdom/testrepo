using System;
using System.Collections.Generic;
using System.Text;

namespace ChurchMemberApp.Models.Request
{
    public class Contribution
    {
        public string contributionItemId { get; set; } = "00000000-0000-0000-0000-000000000000";
        public string description { get; set; }
        public string contributionCurrencyId { get; set; } = "00000000-0000-0000-0000-000000000000";
        public string contributionItemName { get; set; }
        public decimal amount { get; set; }

    }

    public class DonationRequest
    {
        public string paymentFormId { get; set; } = "00000000-0000-0000-0000-000000000000";
        public string churchLogoUrl { get; set; }
        public string churchName { get; set; }
        public string tenantID { get; set; }
        public bool isAnonymous { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string userID { get; set; } = "00000000-0000-0000-0000-000000000000";
        public string merchantID { get; set; } = "00000000-0000-0000-0000-000000000000";
        public string currencyId { get; set; } = "00000000-0000-0000-0000-000000000000";
        public string superMerchantID { get; set; } = "00000000-0000-0000-0000-000000000000";
        public string orderID { get; set; }
        public string paymentGatewayId { get; set; } = "00000000-0000-0000-0000-000000000000";
        public List<Contribution> contributionItems { get; set; }
    }

}
