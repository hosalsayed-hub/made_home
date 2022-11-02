

namespace Homemade.BLL.ViewModel.Identity
{
    public class ResetPasswordViewModel
    {
        public string UserName { get; set; }

        public string Code { get; set; }
    }
    public class SiteResetPasswordViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Code { get; set; }
        public int Type { get; set; }
    }
}
