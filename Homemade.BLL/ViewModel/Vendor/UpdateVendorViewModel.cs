using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Homemade.BLL.ViewModel.Vendor
{
    public class CompleteRegisterApi
    {
        public string storeNameEn { get; set; }
        public string storeNameAr { get; set; }
        public string aboutStoreEn { get; set; }
        public string aboutStoreAr { get; set; }
        public string cRnumber { get; set; } //السجل التجارى
        public string taxNumber { get; set; }//الرقم الضريبي
        public string maarofNumber { get; set; }
        public int? activityID { get; set; } //fk
        public int? bankID { get; set; } //fk
        public string accountNumber { get; set; }
        public string ibanNumber { get; set; }
        public string swiftCode { get; set; }
        public bool isShowContact { get; set; }
        public string daysWork { get; set; }
        public DateTime? workFrom { get; set; }
        public DateTime? workTo { get; set; }
        public string daysVac { get; set; }
        public DateTime? vacWorkFrom { get; set; }
        public DateTime? vacWorkTo { get; set; }

    }
    public class UpdateStoreViewModel
    {
        public double? lat { get; set; }
        public double? lng { get; set; }
        public string storeNameEn { get; set; }
        public string storeNameAr { get; set; }
        public string aboutStoreEn { get; set; }
        public string aboutStoreAr { get; set; }
        public string logo { get; set; }
        public string banner { get; set; }
        public string cRPic { get; set; } //صورة السجل التجارى
        public int? activityID { get; set; } //fk
        public int? blockID { get; set; } //fk
        public int? cityID { get; set; } //fk
        public string address { get; set; }
        public string cRnumber { get; set; } //السجل التجارى
        public string taxNumber { get; set; }//الرقم الضريبي
        public string maarofNumber { get; set; }//الرقم الضريبي
        public string daysWork { get; set; }
        public string workFrom { get; set; }
        public string workTo { get; set; }
        public string daysVac { get; set; }
        public string vacWorkFrom { get; set; }
        public string vacWorkTo { get; set; }
        public List<int> listDaysWork { get; set; }
        public List<int> listDaysVac { get; set; }
    }
    public class UpdateStoreViewModelApi
    {
        public double? lat { get; set; }
        public double? lng { get; set; }
        public string storeNameEn { get; set; }
        public string storeNameAr { get; set; }
        public string aboutStoreEn { get; set; }
        public string aboutStoreAr { get; set; }
        public int? activityID { get; set; } //fk
        public int? blockID { get; set; } //fk
        public string address { get; set; }
        public string cRnumber { get; set; } //السجل التجارى
        public string taxNumber { get; set; }//الرقم الضريبي
        public string maarofNumber { get; set; }//الرقم الضريبي
        public string daysWork { get; set; }
        public DateTime? workFrom { get; set; }
        public DateTime? workTo { get; set; }
        public string daysVac { get; set; }
        public DateTime? vacWorkFrom { get; set; }
        public DateTime? vacWorkTo { get; set; }
    }
    public class PaymentDetails
    {
        public int? bankID { get; set; } //fk
        public string accountNumber { get; set; }
        public string ibanNumber { get; set; }
        public string swiftCode { get; set; }
        public int? packageID { get; set; } //fk
    }
    public class LocationDetails
    {
        public int? blockID { get; set; } //fk
        public int cityID { get; set; } //fk
        public string address { get; set; }
        public double? lat { get; set; }
        public double? lng { get; set; }
    }
}
