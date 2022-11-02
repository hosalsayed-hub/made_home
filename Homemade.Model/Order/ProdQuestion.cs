using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Homemade.Model.Customer;
using Homemade.Model.Vendor;

namespace Homemade.Model.Order
{
    [Table("OrderQuestion", Schema = "Order")]
    public class ProdQuestion : BaseEntity
    {
        public ProdQuestion()
        {
            ProdQuestionGuid = Guid.NewGuid();
            Notification = new HashSet<Notification>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProdQuestionID { get; set; }
        public Guid ProdQuestionGuid { get; set; }
        public int CustomersID { get; set; }
        public int ProductID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public bool IsRepley { get; set; }
        public virtual User User { get; set; }
        public virtual Customers Customers { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<Notification> Notification { get;  set; }

    }
}
