using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.MyFatoora
{
    public class RequestPayTap
    {
        public double amount { get; set; }
        public string currency { get; set; }
        public bool threeDSecure { get; set; }
        public bool save_card { get; set; }
        public string description { get; set; }
        public string statement_descriptor { get; set; }
        public metadataClass metadata { get; set; }
        public referenceClass reference { get; set; }
        public receiptClass receipt { get; set; }
        public customerClass customer { get; set; }
        public merchantClass merchant { get; set; }
        public sourceClass source { get; set; }
        public postClass post { get; set; }
        public redirectClass redirect { get; set; }
        public string language { get; set; }
    }
    public class metadataClass
    {
        public string udf1 { get; set; }
        public string udf2 { get; set; }
    }

    public class referenceClass
    {
        public string transaction { get; set; }
        public string order { get; set; }
    }

    public class receiptClass
    {
        public bool email { get; set; }
        public bool sms { get; set; }
    }

    public class customerClass
    {
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        
        public phoneClass phone { get; set; }
    }

    public class phoneClass
    {
        public string country_code { get; set; }
        public string number { get; set; }
 
    }

    public class merchantClass
    {
        public string id { get; set; }
    }
    
    public class sourceClass
    {
        public string id { get; set; }
    }

    public class postClass
    {
        public string url { get; set; }
    }

    public class redirectClass
    {
        public string url { get; set; }
    }
    
}
