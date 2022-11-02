using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace Homemade.Model.Setting
{
    [Table("Inqueries", Schema = "Setting")]
    public class Inqueries
    {
        public Inqueries()
        {
            InqueriesGuid = Guid.NewGuid();
            InqueriesReply = new HashSet<InqueriesReply>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InqueriesID { get; set; }
        public Guid InqueriesGuid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string MobileNo { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual ICollection<InqueriesReply> InqueriesReply { get; set; }

    }
}
