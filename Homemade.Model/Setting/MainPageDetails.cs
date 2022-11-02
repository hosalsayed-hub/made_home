using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homemade.Model.Setting
{
    [Table("MainPageDetails", Schema = "Setting")]
    public class MainPageDetails : BaseEntity
    {
        public MainPageDetails()
        {
            MainPageDetailsGuid = Guid.NewGuid();
            MainPageImages = new HashSet<MainPageImages>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MainPageDetailsID { get; set; }
        public Guid MainPageDetailsGuid { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public string DescAr { get; set; }
        public string DescEn { get; set; }
        public string Image { get; set; }
        public string VedioLink { get; set; }
        public string IdeaTitleAr { get; set; }
        public string IdeaTitleEn { get; set; }
        public string IdeaDescAr { get; set; }
        public string IdeaDescEn { get; set; }
        public string HomeTitleAr { get; set; }
        public string HomeTitleEn { get; set; }
        public string HomeDescAr { get; set; }
        public string HomeDescEn { get; set; }
        public string HomeImage { get; set; }

        public int MainPageID { get; set; }
        public virtual MainPage MainPage { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<MainPageImages> MainPageImages { get; set; }
    }
}