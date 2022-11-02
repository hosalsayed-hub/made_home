using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Homemade.Model.Setting;
using Homemade.Model.Order;
using Homemade.Model.Site;

namespace Homemade.Model.Customer
{
    [Table("Customers", Schema = "Customer")]
    public class Customers : BaseEntity
    {
        public Customers()
        {
            CustomersGuid = Guid.NewGuid();
            CustomerLocation = new HashSet<CustomerLocation>();
            CustomerFavourites = new HashSet<CustomerFavourites>();
            Orders = new HashSet<Orders>();
            ProdQuestion = new HashSet<ProdQuestion>();
            CartMaster = new HashSet<CartMaster>();
            CustomerBalance = new HashSet<CustomerBalance>();
            Notification = new HashSet<Notification>();
            TabCharge = new HashSet<TabCharge>();
            TabChargeExLog = new HashSet<TabChargeExLog>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomersID { get; set; }
        public Guid CustomersGuid { get; set; }
        public string FirstName { get; set; }
        public string SeconedName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ProfilePic { get; set; }
        public int Gender { get; set; } //GenderEnum
        public int CityID { get; set; }
        public int NationalityID { get; set; }
        public virtual ICollection<CustomerBalance> CustomerBalance { get; set; }
        public virtual ICollection<CustomerLocation> CustomerLocation { get; set; }
        public virtual ICollection<CustomerFavourites> CustomerFavourites { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<ProdQuestion> ProdQuestion { get; set; }
        public virtual ICollection<Notification> Notification { get; set; }
        public virtual ICollection<CartMaster> CartMaster { get; set; }
        public virtual ICollection<TabCharge> TabCharge { get; set; }
        public virtual ICollection<TabChargeExLog> TabChargeExLog { get; set; }
        
        public virtual User User { get; set; }
        public virtual City City { get; set; }
        public virtual Nationality Nationality { get; set; }

    }
}
