using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Setting
{
    public class PaymentConfigurationViewModel
    {
        public int PaymentConfigurationID { get; set; }
        [Required(ErrorMessageResourceName = "IBANnumberRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
       // [StringLength(15, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
       
        public string IBANnumber { get; set; }
        [Required(ErrorMessageResourceName = "AccountNumberRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(15, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]

        public string AccountNumber { get; set; }

        public string AccountImage { get; set; }
        public string Attachment { get; set; }

        [Display(Name = "Bank")]
        [Required(ErrorMessageResourceName = "BankRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = "BankRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int BanksID { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public int UserId { get; set; }
        public int? UserUpdate { get; set; }
        public bool IsEnable { get; set; }

    }
}
