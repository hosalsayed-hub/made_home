using System.ComponentModel.DataAnnotations;

namespace Homemade.BLL.ViewModel.Setting
{
    public class DeliverySettingViewModel
    {
        public int DeliverySettingID { get; set; }
        [Required(ErrorMessageResourceName = "DriverCommisionRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string DriverCommision { get; set; } // percent
        [Required(ErrorMessageResourceName = "MinKMRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string MinKM { get; set; }
        [Required(ErrorMessageResourceName = "OverKmFareRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string OverKmFare { get; set; }
    }
}
