using System;
using System.ComponentModel.DataAnnotations;

namespace Homemade.BLL.ViewModel.Setting
{
    public class CompanyWorkingHoursViewModel
    {
        public int CompanyWorkingHoursID { get; set; }
        public Guid CompanyWorkingHoursGuid { get; set; }
        public string DaysWork { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public DateTime FirstShiftWorkFrom { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public DateTime FirstShiftWorkTo { get; set; }
        public DateTime? SecondShiftWorkFrom { get; set; }
        public DateTime? SecondShiftWorkTo { get; set; }
        public string DaysVac { get; set; }
        public DateTime? FirstShiftVacWorkFrom { get; set; }
        public DateTime? FirstShiftVacWorkTo { get; set; }
        public DateTime? SecondShiftVacWorkFrom { get; set; }
        public DateTime? SecondShiftVacWorkTo { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string[] ListDaysWork { get; set; }
        public string FirstShiftWorkFromString { get; set; }
        public string FirstShiftWorkToString { get; set; }
        public string SecondShiftWorkFromString { get; set; }
        public string SecondShiftWorkToString { get; set; }
        public string[] ListDaysVac { get; set; }
        public string FirstShiftVacWorkFromString { get; set; }
        public string FirstShiftVacWorkToString { get; set; }
        public string SecondShiftVacWorkFromString { get; set; }
        public string SecondShiftVacWorkToString { get; set; }
    }
}
