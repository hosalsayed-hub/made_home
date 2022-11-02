using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homemade.BLL.Resources;
using Homemade.Model;

namespace Homemade.BLL.ViewModel.Employees
{
    public class EmployeesViewModel : BaseEntity
    {
        public int EntityEmpID { get; set; }
        public Guid EntityEmpGuid { get; set; }
        [Required(ErrorMessageResourceName = "JobRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int JobsID { get; set; }
        [Required(ErrorMessageResourceName = "ArNameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[\u0621-\u064A\u0660-\u0669\0-9]+$", ErrorMessageResourceName = "ArabicOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string FirstNameAR { get; set; }
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[\u0621-\u064A\u0660-\u0669\0-9]+$", ErrorMessageResourceName = "ArabicOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string MidNameAR { get; set; }
        [Required(ErrorMessageResourceName = "ArNameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[\u0621-\u064A\u0660-\u0669\0-9]+$", ErrorMessageResourceName = "ArabicOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string LastNameAR { get; set; }
        [Required(ErrorMessageResourceName = "EnNameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[a-zA-Z\0-9]+$", ErrorMessageResourceName = "EnglishOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string FirstNameEN { get; set; }
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[a-zA-Z\0-9]+$", ErrorMessageResourceName = "EnglishOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string MidNameEN { get; set; }
        [Required(ErrorMessageResourceName = "EnNameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[a-zA-Z\0-9]+$", ErrorMessageResourceName = "EnglishOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string LastNameEN { get; set; }
        public string FileNo { get; set; }
        [Required(ErrorMessageResourceName = "GenderRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int Gender { get; set; }
        [Required(ErrorMessageResourceName = "IDNoRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(10, MinimumLength = 5, ErrorMessageResourceName = nameof(HomemadeErrorMessages.NameMax10), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [RegularExpression("^[0-9]+$", ErrorMessageResourceName = nameof(HomemadeErrorMessages.IDNoValid), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public string IDNo { get; set; }
        public string BirthDateHijri { get; set; }
        public DateTime? BirthDateMilady { get; set; }
        public string Photo { get; set; }
        [Required(ErrorMessageResourceName = "EmailRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [EmailAddress(ErrorMessageResourceName = "Email", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string Email { get; set; }
        public int? BloodTypeId { get; set; } // BloodTypeEnum
        [StringLength(20, MinimumLength = 9, ErrorMessageResourceName = nameof(HomemadeErrorMessages.NameMax20), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [RegularExpression(@"^(5)[0-9]{8}$", ErrorMessageResourceName = nameof(HomemadeErrorMessages.MobileValid), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public string MobileNo { get; set; }
        public string PhoneNumber { get; set; }
        [Required(ErrorMessageResourceName = "NationalityRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int NationalityID { get; set; } // with Type Enum ID = 2
        [Required(ErrorMessageResourceName = "CityRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int CityID { get; set; }
        public string AddressAR { get; set; }
        public string AddressEN { get; set; }
        public string Notes { get; set; }
        #region Map Data
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string Zoom { get; set; }
        #endregion
        [Required(ErrorMessageResourceName = "RegionRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int RegionID { get; set; }
        public string CityName { get; set; }
        public string RegionName { get; set; }
        public string NationalityName { get; set; }
        public string EntityName { get; set; }
        public string BloodTypeName { get; set; }
        public string EntityTypeName { get; set; }
        public int JobTypeId { get; set; }
        public string JobsName { get; set; }
        public string DepartmentsName { get; set; }
        public string FullName { get; set; }
        public string CheckedItems { get; set; }
        public string[] Roles { get; set; }
        public int EntitiesGroupEmployeeID { get;  set; }
        public string BirthDateMiladyString { get; set; }
        public string EntityEmpName { get; set; }
        public string IsEnableString { get; set; }
    }
}
