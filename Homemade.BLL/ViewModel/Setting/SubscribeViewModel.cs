using System;

namespace Homemade.BLL.ViewModel.Setting
{
    public class SubscribeViewModel
    {
        public int SubscribeID { get; set; }
        public Guid SubscribeGuid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
