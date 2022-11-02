using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Model.Setting
{
    [Table("LogTextMessage", Schema = "Setting")]
    public class LogTextMessage
    {
        public LogTextMessage()
        {
            LogTextMessageGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogTextMessageID { get; set; }
        public Guid LogTextMessageGuid { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Response { get; set; }
        public DateTime SendTime { get; set; }
        public DateTime DateAdded { get; set; }
        public int MessageTypeId { get; set; }  // MessageTypeEnum
        public int MessageReasonId { get; set; }  // MessageReasonEnum
        public int UserTypeId { get; set; } // UserTypeEnum
        public bool IsSend { get; set; } // UserTypeEnum
    }
}
