using Homemade.BLL.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Setting
{
    public class DiscountViewModel
    {
        public int DiscountID { get; set; }
        public Guid DiscountGuid { get; set; }
        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.Required), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = nameof(HomemadeErrorMessages.NameMax0), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [RegularExpression(@"^-?[0-9]*\.?[0-9]+$", ErrorMessageResourceName = nameof(HomemadeErrorMessages.NumberOnly), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public decimal DiscountValue { get; set; }
        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.Required), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public int DiscountTypeID { get; set; }   // DiscountSettingEnum
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public int UserId { get; set; }
        public int? UserUpdate { get; set; }
        public int? UserDelete { get; set; }
    }
}
