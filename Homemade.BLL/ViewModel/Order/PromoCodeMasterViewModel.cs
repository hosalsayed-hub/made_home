using Homemade.BLL.Resources;
using Homemade.Model.Setting;
using Homemade.Model.Vendor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Order
{
    public class PromoCodeMasterViewModel
    {
        public int PromoCodeMasterID { get; set; }

        public Guid PromoCodeGuid { get; set; }
        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.Required), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.Required), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public DateTime ExpiryDate { get; set; }
        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.Required), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = nameof(HomemadeErrorMessages.NameMax0), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [RegularExpression(@"^-?[0-9]*\.?[0-9]+$", ErrorMessageResourceName = nameof(HomemadeErrorMessages.NumberOnly), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public double LimitCount { get; set; }
        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.Required), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public string PromoCode { get; set; }
        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.Required), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public string Subject { get; set; }
        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.Required), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public string Content { get; set; }
        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.Required), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = nameof(HomemadeErrorMessages.NameMax0), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [RegularExpression(@"^-?[0-9]*\.?[0-9]+$", ErrorMessageResourceName = nameof(HomemadeErrorMessages.NumberOnly), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public double DiscountValue { get; set; }
        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.Required), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public int DiscountType { get; set; }  // DiscountTypeEnum

        public List<int> lstCityID { get; set; }

        public List<string> lstCityName { get; set; }

        public int PromoType { get; set; }
        public int UserId { get; set; }
        public int UsedCount { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public int Enabled { get; set; }

        public int? UserUpdate { get; set; }

        public int? UserDelete { get; set; }

        public int? TripPromoCodeCount { get; set; }
        public string StartDateString { get; set; }
        public string ExpiryDateString { get; set; }
        public List<Vendors> Cities { get; set; }
    }
}
