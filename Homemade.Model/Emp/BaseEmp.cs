using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homemade.Model.Emp
{

    public class BaseEmp : BaseEntity
    {

        [StringLength(75)]
        public string FirstNameAR { get; set; }
        [StringLength(75)]
        public string MidNameAR { get; set; }
        [StringLength(75)]
        public string LastNameAR { get; set; }
        [StringLength(75)]
        public string FirstNameEN { get; set; }
        [StringLength(75)]
        public string MidNameEN { get; set; }
        [StringLength(75)]
        public string LastNameEN { get; set; }
        public string FileNo { get; set; }
        public int Gender { get; set; }
        public string IDNo { get; set; }
        public string BirthDateHijri { get; set; }
        public DateTime? BirthDateMilady { get; set; }
        public string Photo { get; set; }
        public string Email { get; set; }
        public int? BloodTypeId { get; set; } // BloodTypeEnum
        public string MobileNo { get; set; }
        public string PhoneNumber { get; set; }
        public int NationalityID { get; set; } // with Type Enum ID = 2
        public int CityID { get; set; }
        public string AddressAR { get; set; }
        public string AddressEN { get; set; }
        public string Notes { get; set; }
        #region Map Data
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string Zoom { get; set; }
        #endregion



    }
}
