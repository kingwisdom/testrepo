//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace ChurchMemberApp.Models.Response
//{
//    public class OfferingPayment
//    {
//        public string id { get; set; }
//        public string name { get; set; }
//        public object uniqueName { get; set; }
//        public DateTime date { get; set; }
//        public string bankID { get; set; }
//        public string accountName { get; set; }
//        public string accountNumber { get; set; }
//        public bool isActive { get; set; }
//        public string tenantID { get; set; }
//        public object bank { get; set; }
//        public object tenant { get; set; }
//        public List<ContributionItems> contributionItems { get; set; }
//        public List<PaymentGateWay> paymentGateWays { get; set; }
//    }

//    public class FinancialContribution
//    {
//        public string id { get; set; }
//        public string name { get; set; }
//        public double Amount { get; set; }
//        public bool isPublic { get; set; }
//        public object incomeAccount { get; set; }
//        public string incomeAccountID { get; set; }
//        public object cashAccount { get; set; }
//        public string cashAccountID { get; set; }
//        public string tenantID { get; set; }
//        public object contributionTransactions { get; set; }
//        public object incomeRemittance { get; set; }
//    }

//    public class ContributionItems
//    {
//        public string id { get; set; }
//        public string paymentFormID { get; set; }
//        public string financialContributionID { get; set; }
//        public FinancialContribution financialContribution { get; set; }
//    }

//    public class PaymentGateways
//    {
//        public string id { get; set; }
//        public string name { get; set; }
//        public string countryCoverageArea { get; set; }
//        public bool isActive { get; set; }
//        public object isSubAccountSupported { get; set; }
//    }

//    public class PaymentGateWay
//    {
//        public string id { get; set; }
//        public string paymentGateWayID { get; set; }
//        public string paymentFormID { get; set; }
//        public string subAccountID { get; set; }
//        public PaymentGateways paymentGateway { get; set; }
//        public object financialContribution { get; set; }
//    }

//}
