using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace Homemade.Model.Setting
{
    [Table("Configuration", Schema = "Setting")]
    public  class Configuration : BaseEntity
    {
        public Configuration()
        {
            ConfigurationGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConfigurationID { get; set; }
        public Guid ConfigurationGuid { get; set; }
        public decimal DeliveryPrice { get; set; }
        public decimal MinDeliveryPrice { get; set; }
        public decimal MaxDeliveryPrice { get; set; }
        public string CRNumber { get; set; }
        public string CompanNameAr { get; set; }
        public string CompanNameEn { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Fax { get; set; }
        public string WebSite { get; set; }
        public string Address { get; set; }
        public string StreetNo { get; set; }
        public string Email { get; set; }
        public string SeconedEmail { get; set; }
        public string TaxNumber { get; set; }
        public string CRImage { get; set; }
        public string Logo { get; set; }
        public string Banner { get; set; }
        public bool IsSmsSend { get; set; }
        public bool IsCloseCompletePurchase { get; set; }
        public decimal LastSmsBalance { get; set; }

        public decimal MinKM { get; set; }
        public decimal OverKmFare { get; set; }

        public decimal DeliveryPriceWithoutVat { get; set; }
        public decimal DeliveryPriceVatPercent { get; set; }

        public string WhatsappLink { get; set; }
        public string TwitterLink { get; set; }
        public string InstagramLink { get; set; }
        public string SnapchatLink { get; set; }

        public virtual User User { get; set; }

    }
}
