using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Homemade.Model;

namespace Homemade.BLL.ViewModel.Setting
{
    public class MainPageDetailsViewModel : BaseEntity
    {
        public int MainPageDetailsID { get; set; }
        public Guid MainPageDetailsGuid { get; set; }
        [Required(ErrorMessageResourceName = "TitleArRequired", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string TitleAr { get; set; }
        [Required(ErrorMessageResourceName = "TitleEnRequired", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string TitleEn { get; set; }
         
        public string DescAr { get; set; }
        
        public string DescEn { get; set; }
        public string Image { get; set; }
        public string VedioLink { get; set; }
        [Required(ErrorMessageResourceName = "MainPageRequired", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int MainPageID { get; set; }
        public string MainPageName { set; get; }
        public string MainPageDetailsTitle { get; set; }
        public string Desc { get; set; }
        public Guid MainPageGuid { set; get; }
        public string IdeaTitleAr { get; set; }
        public string IdeaTitleEn { get; set; }
        public string IdeaDescAr { get; set; }
        public string IdeaDescEn { get; set; }
        public string HomeTitleAr { get; set; }
        public string HomeTitleEn { get; set; }
        public string HomeDescAr { get; set; }
        public string HomeDescEn { get; set; }
        public string HomeImage { get; set; }
        public string IdeaTitle { get; set; }
        public string IdeaDesc { get; set; }
        public string HomeTitle { get; set; }
        public string HomeDesc { get; set; }
    }
}