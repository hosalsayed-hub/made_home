using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.MyFatoora
{
    public class ResponcePayTap
    {
        public string id { get; set; }
        public bool live_mode { get; set; }
        public string api_version { get; set; }
        public string method { get; set; }
        public string status { get; set; }
        public float amount { get; set; }
        public string currency { get; set; }
        public bool threeDSecure { get; set; }
        public bool save_card { get; set; }
        public string merchant_id { get; set; }
        public string product { get; set; }
        public string statement_descriptor { get; set; }
        public string description { get; set; }
        public R_metadataClass metadata { get; set; }
        public R_transactionClass transaction { get; set; }
        public R_responseClass response { get; set; }
        public R_receiptClass receipt { get; set; }
        public R_customerClass customer { get; set; }
        public R_sourceClass source { get; set; }
        public R_redirectClass redirect { get; set; }
        public R_postClass post { get; set; }
        public R_card card { get; set; }
    }
    public class R_metadataClass
    {
        public string udf1 { get; set; }
        public string udf2 { get; set; }
    }

    public class R_transactionClass
    {
        public string timezone { get; set; }
        public string created { get; set; }
        public string url { get; set; }
        public R_expiryClass expiry { get; set; }
        public bool asynchronous { get; set; }
        public float amount { get; set; }
        public string currency { get; set; }
    }

    public class R_expiryClass
    {
        public string period { get; set; }
        public string type { get; set; }
    }
    public class R_responseClass
    {
        public string code { get; set; }
        public string message { get; set; }
    }

    public class R_receiptClass
    {
        public bool email { get; set; }
        public bool sms { get; set; }
    }

    public class R_customerClass
    {
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }

        public R_phoneClass phone { get; set; }
    }

    public class R_phoneClass
    {
        public string country_code { get; set; }
        public string number { get; set; }

    }

    public class R_sourceClass
    {

        // public string object { get; set; }
        public string id { get; set; }
    }

    public class R_postClass
    {
        public string status { get; set; }
        public string url { get; set; }
    }

    public class R_redirectClass
    {
        public string status { get; set; }
        public string url { get; set; }
    }
    public class R_card
    {
        public string @object { get; set; }
        public string first_six { get; set; }
        public string brand { get; set; }
        public string last_four { get; set; }
    }
}
