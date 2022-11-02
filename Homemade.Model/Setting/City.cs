using System;
using Homemade.Model.Emp;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;
using Homemade.Model.Vendor;
using Homemade.Model.Customer;
using Homemade.Model.Driver;

namespace Homemade.Model.Setting
{
    [Table("City", Schema = "Setting")]

    public class City : BaseEntity
    {
        public City()
        {
            CityGuid = Guid.NewGuid();
            Employees = new HashSet<Employees>();
            Block = new HashSet<Block>();
            Vendors = new HashSet<Vendors>();
            Customers = new HashSet<Customers>();
            ShippingCompany = new HashSet<ShippingCompany>();
            Branches = new HashSet<Branches>();
            Drivers = new HashSet<Drivers>();
            CitiesCovered = new HashSet<CitiesCovered>();
           
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int CityID { get; set; }
        public Guid CityGuid { get; set; }

        [ForeignKey(nameof(Region))]
        public int RegionID { get; set; }

        public string Code { get; set; }
        [StringLength(75)]
        public string NameAR { get; set; }
        [StringLength(75)]
        public string NameEN { get; set; }

        //------------------- ��� ����� -------------------  
        #region Map Data
        public string Place { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string Zoom { get; set; }
        #endregion

        //--------------------------------------------------------
        public virtual Region Region { get; set; }
     
        public virtual User User { get; set; }
        public virtual ICollection<ShippingCompany> ShippingCompany { get; set; }
        public virtual ICollection<Drivers> Drivers { get; set; }
        public virtual ICollection<Employees> Employees { get; set; }
        public virtual ICollection<Block> Block { get; set; }
        public virtual ICollection<Vendors> Vendors { get; set; }
        public virtual ICollection<Customers> Customers { get; set; }
        public virtual ICollection<Branches> Branches { get; set; }
        public virtual ICollection<CitiesCovered> CitiesCovered { get; set; }
       
    }
    
}
