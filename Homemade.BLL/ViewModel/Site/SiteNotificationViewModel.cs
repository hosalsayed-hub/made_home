using System;
using System.Collections.Generic;

namespace Homemade.BLL.ViewModel.Site
{
    public class SiteNotificationViewModel
    {
        public string NotificationDate { get; set; }
        public List<SiteListNotificationViewModel> List { get; set; }
       
    }
    public class SiteListNotificationViewModel
    {
        public int NotificationID { get; set; }
        public bool IsSeen { get; set; }
        public string NotificationDate { get; set; }
        public string NotificationTime { get; set; }
        public string NotificationDesc { get; set; }
        public string NotificationTitle { get; set; }
        public int NotificationTypeID { get; set; }
        public int? OrderStatusID { get; set; }
        public Guid? RedirectGuid { get; set; }
    }
}
