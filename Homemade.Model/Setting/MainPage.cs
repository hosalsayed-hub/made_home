using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Model.Setting
{
    [Table("MainPage", Schema = "Setting")]
    public class MainPage : BaseEntity
    {
        public MainPage()
        {
            MainPageGuid = Guid.NewGuid();
            MainPageDetails = new HashSet<MainPageDetails>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MainPageID { get; set; }
        public Guid MainPageGuid { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public int MainPageTypeId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<MainPageDetails> MainPageDetails { get; set; }
    }
}
