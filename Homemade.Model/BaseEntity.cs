using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homemade.Model
{

    public class BaseEntity
    {
        public BaseEntity()
        {
            CreateDate = DateTime.UtcNow.AddHours(3);
        }
        public bool IsDeleted { get; set; }
        public bool IsEnable { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? EnableDate { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public int? UserUpdate { get; set; }
        public int? UserDelete { get; set; }
        public int? UserEnable { get; set; }

    }
}
