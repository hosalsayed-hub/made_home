using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Vendor
{
    public class VendorSiteRequestVM
    {
        public string Txt_FnameAr { get; set; }
        public string Txt_FnameEn { get; set; }
        public string Txt_SnameAr { get; set; }
        public string Txt_SnameEn { get; set; }
        public int cities { get; set; }
        public int WizzardNationalityID { get; set; }
        public string Gender { get; set; }
        public string Txt_password { get; set; }
        public string Txt_confirmPassword { get; set; }
        public string Txt_Mobile { get; set; }

        public string Txt_Email { get; set; }
        public string Txt_storenameAr { get; set; }
        public string Txt_storenameEn { get; set; }
        public int Activities { get; set; }
        public string Txt_CommercialNumber { get; set; }
        public string Txt_TaxNumber { get; set; }
        public string Txt_knownnumber { get; set; }
        public string Txt_Address { get; set; }
        public string Txt_MoreaboutservicesAr { get; set; }
        public string Txt_MoreaboutservicesEn { get; set; }
        public int Banks { get; set; }
        public int blocks { get; set; }
        public string Txt_AccountNo { get; set; }
        public string Txt_IBAN { get; set; }
        public string Txt_SwiftCode { get; set; }
        public string __RequestVerificationToken { get; set; }
        public IFormFile fileowner { get; set; }
        public IFormFile filestore { get; set; }
        public IFormFile filecommerc { get; set; }
        public string gender { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string imageownerlogo { get; set; }
        public string imagestorelogo { get; set; }
        public string imagecommercialcumber { get; set; }
    }
}
