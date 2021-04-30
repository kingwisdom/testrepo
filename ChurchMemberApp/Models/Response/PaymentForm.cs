using System;
using System.Collections.Generic;
using System.Text;

namespace ChurchMemberApp.Models.Response
{
    public class PaymentForm
    {

        public string id { get; set; }
        public string name { get; set; }
        public object uniqueName { get; set; }
        public DateTime date { get; set; }
        public string bankID { get; set; }
        public string accountName { get; set; }
        public string accountNumber { get; set; }
        public string orderId { get; set; }
        public bool isActive { get; set; }
        public string currencyId { get; set; } ="00000000-0000-0000-0000-000000000000";
        public string tenantID { get; set; }
        public List<ContributionItem> contributionItems { get; set; }
        public List<GateWay> gateWays { get; set; }
    }

    public class ContributionItem
    {
        public decimal Amount { get; set; }

        public string id { get; set; }
        public string paymentFormID { get; set; }
        public string financialContributionID { get; set; } = "00000000-0000-0000-0000-000000000000";
        public string contributionName { get; set; }
        public bool isContributionPublic { get; set; }
        public string incomeAccountID { get; set; }
        public string cashAccountID { get; set; }

    }

    public class GateWay
    {

        public string id { get; set; }
        public string paymentGateWayID { get; set; } = "00000000-0000-0000-0000-000000000000";
        public string paymentFormID { get; set; }
        //
        public string subAccountID { get; set; }
        public string paymentGateName { get; set; }
        public string countryCoverageArea { get; set; }
        public bool isActive { get; set; }
        public object isSubAccountSupported { get; set; }
    }

}
