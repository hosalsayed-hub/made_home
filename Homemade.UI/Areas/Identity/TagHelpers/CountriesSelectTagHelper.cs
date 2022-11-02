using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;

namespace Homemade.UI.Areas.Identity.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("countries-select")]
    public class CountriesSelectTagHelper : TagHelper
    {
        [HtmlAttributeName("cityid")]
        public int CityID { get; set; }


        public CountriesSelectTagHelper()
        {
        }
        public override void Process(TagHelperContext context , TagHelperOutput output)
        {
            bool IsAr = InfraStructure.Utility.CurrentLanguageCode == "ar";

            output.TagName = "select";
            output.Attributes.Clear();
            output.Attributes.Add("class" , "form-control js-select2");
            output.Attributes.Add("name" , "CountryID");
            output.Attributes.Add("onchange" , "changeCity(this)");
            output.Attributes.Add("data-val" , "true");
            output.Attributes.Add("data-val-required" , Homemade.BLL.Resources.HomemadeErrorMessages.Required);

           
            output.TagMode = TagMode.StartTagAndEndTag;
            

        }
    }
}
