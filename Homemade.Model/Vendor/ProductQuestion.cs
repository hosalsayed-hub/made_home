using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Model.Vendor
{
    [Table("ProductQuestion", Schema = "Vendor")]
    public class ProductQuestion  : BaseEntity
    {
        public ProductQuestion()
        {
            ProductQuestionGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductQuestionID { get; set; }
        public Guid ProductQuestionGuid { get; set; }
        public string QuestionAr { get; set; }
        public string QuestionEn { get; set; }
        public string AnswerAr { get; set; }
        public string AnswerEn { get; set; }
        #region FK
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
        #endregion
    }
}
