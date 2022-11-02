using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;

namespace Homemade.UI.Areas.Identity.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("cities-select")]
    public class CitiesSelectTagHelper : TagHelper
    {
        [HtmlAttributeName("value")]
        public int SelectedValue { get; set; }

        //[HtmlAttributeName("name")]
        //public string Name { get; set; }

        public CitiesSelectTagHelper( )
        {
        }
        public override void Process(TagHelperContext context , TagHelperOutput output)
        {
            bool IsAr = InfraStructure.Utility.CurrentLanguageCode == "ar";

            output.TagName = "select";
            output.Attributes.Clear();
            output.Attributes.Add("class" , "form-control js-select2");
            output.Attributes.Add("name" , "CityID");
            output.Attributes.Add("data-val" , "true");
            output.Attributes.Add("data-val-required" , Homemade.BLL.Resources.HomemadeErrorMessages.Required);

            output.TagMode = TagMode.StartTagAndEndTag;
            

        }
    }
}
