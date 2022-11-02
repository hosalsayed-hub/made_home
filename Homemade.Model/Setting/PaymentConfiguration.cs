using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homemade.Model.Setting
{
    [Table("PaymentConfiguration", Schema = "Setting")]
    public  class PaymentConfiguration : BaseEntity
    {
        public PaymentConfiguration()
        {
            PaymentConfigurationGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentConfigurationID { get; set; }
        public Guid PaymentConfigurationGuid { get; set; }
        public string IBANnumber { get; set; }
        public string AccountNumber { get; set; }
        public string AccountImage { get; set; }
        public string Attachment { get; set; }
        public int BanksID { get; set; }

        public string UserAnrdoid { get; set; }
        public string UserIOS { get; set; }
        public string VendorAndorid { get; set; }
        public string VendorIOS { get; set; }

        public string DriverAndorid { get; set; }
        public string DriverIOS { get; set; }

        public virtual Banks Banks { get; set; }
        public virtual User User { get; set; }

    }
}
