namespace Homemade.UI.InfraStructure
{
    public static class AppConst
    {

        public static int NumberOfObjectsPerPage { get; set; } = 10;

        public static string DomainUrl { get; set; } = "https://made-home.com";

        public const string ExcelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public const string PdfContentType = "application/pdf";

        public const string ResetPasswordTemplatePath = "/App/Operators/Operators/EmailTemplate/EmailTemplate.html";

    }
}
