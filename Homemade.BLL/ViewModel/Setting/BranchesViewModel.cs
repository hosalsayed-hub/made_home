using Homemade.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Homemade.BLL.ViewModel.Setting
{
    public class BranchesViewModel : BaseEntity
    {
        public int BranchesID { get; set; }
        public Guid BranchesGuid { get; set; }

        [Required(ErrorMessageResourceName = "ArNameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[\u0621-\u064A\u0660-\u0669\0-9]+$", ErrorMessageResourceName = "ArabicOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string BranchesNameAR { get; set; }

        [Required(ErrorMessageResourceName = "EnNameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[a-zA-Z\0-9]+$", ErrorMessageResourceName = "EnglishOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string BranchesNameEN { get; set; }
        [Required(ErrorMessageResourceName = "CityRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int CountryID { get; set; }
        public string CountryName { get; set; }


        [Required(ErrorMessageResourceName = "EnNameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string Address { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string FaxNumber { get; set; }

        [Required(ErrorMessageResourceName = "MobileRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax10", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[a-zA-Z\0-9]+$", ErrorMessageResourceName = "NumberOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string MobileNo { get; set; }

        [Required(ErrorMessageResourceName = "EmailRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [EmailAddress(ErrorMessageResourceName = "Email", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string FirstEamil { get; set; }
        public string SeconedEmail { get; set; }
        public string WebSite { get; set; }
        public string BranchesManger { get; set; }
        [Required(ErrorMessageResourceName = "ZoneRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int? ZoneID { get; set; }

        [Required(ErrorMessageResourceName = "DeliveryPriceRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public double DeliveryPrice { get; set; }
        public double ExtraoneKilo { get; set; }
        public double FixedFilo { get; set; }
        public string ClientName { get; set; }
        public string ZoneName { get; set; }
        public int ClientID { get; set; }
        public bool IsMain { get; set; }
        public string Name { set; get; }
        public string CityNameEn { set; get; }
        public string CityNameAr { set; get; }
    }
}
