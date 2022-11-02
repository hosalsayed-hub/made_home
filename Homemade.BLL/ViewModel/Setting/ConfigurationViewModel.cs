using Homemade.BLL.Resources;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Setting
{
    public class ConfigurationViewModel
    {
        public int ConfigurationID { get; set; }
        [Required(ErrorMessageResourceName = "ArNameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[\u0621-\u064A\u0660-\u0669\0-9]+$", ErrorMessageResourceName = "ArabicOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string CompanNameAr { get; set; }
        [Required(ErrorMessageResourceName = "EnNameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[a-zA-Z\0-9]+$", ErrorMessageResourceName = "EnglishOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string CompanNameEn { get; set; }

        [StringLength(20, MinimumLength = 9, ErrorMessageResourceName = nameof(HomemadeErrorMessages.NameMax20), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [RegularExpression(@"^(5)[0-9]{8}$", ErrorMessageResourceName = nameof(HomemadeErrorMessages.MobileValid), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Fax { get; set; }
        [Required(ErrorMessageResourceName = "AddressRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string Address { get; set; }
        public string StreetNo { get; set; }
        public string CRNumber { get; set; }
        public string CRImage { get; set; }
        [Required(ErrorMessageResourceName = "ArNameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string Logo { get; set; }
        public string Banner { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public int UserId { get; set; }
        public int? UserUpdate { get; set; }
        public bool IsEnable { get; set; }

        public string SeconedEmail { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessageResourceName = "DeliveryPriceRequired", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string DeliveryPrice { get; set; }
        [Required(ErrorMessageResourceName = "MinDeliveryPriceRequired", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string MinDeliveryPrice { get; set; }
        [Required(ErrorMessageResourceName = "MaxDeliveryPriceRequired", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string MaxDeliveryPrice { get; set; }
        public decimal DeliveryPriceVatPercent { get;   set; }
        public double DeliveryPriceWithoutVat { get;   set; }
        public float DeliveryPriceWithoutVat_new { get;   set; }
        public string DeliveryPriceVatPercentstring { get; set; }
        [Required(ErrorMessageResourceName = "MinKMRequired", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string MinKM { get; set; }
        [Required(ErrorMessageResourceName = "OverKmFareRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string OverKmFare { get; set; }
        public string WhatsappLink { get; set; }
        public string TwitterLink { get; set; }
        public string InstagramLink { get; set; }
        public string SnapchatLink { get; set; }
    }
}
