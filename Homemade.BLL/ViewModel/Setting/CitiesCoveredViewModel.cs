using Homemade.Model;
using System;

namespace Homemade.BLL.ViewModel.Setting
{
    public class CitiesCoveredViewModel : BaseEntity
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
        public bool IsCovered { get; set; }
    }
}
