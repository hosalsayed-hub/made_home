using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Notifications
{
    public class ResponsListNoti
    {
        public bool isNextPage { get; set; }
        public List<NotificationListApiViewModel> notifications { get; set; }
    }
    public class NotificationListApiViewModel
    {
        public List<NotificationDetailsApiViewModel> dayNotifications { get; set; }
        public string date { get; set; }
        public int notificationCount { get;  set; }
        public DateTime datekey { get; set; }
    }

    public class NotificationDetailsApiViewModel
    {
        public int notificationID { get;  set; }
        public bool isSeen { get;  set; }
        public string time { get;  set; }
        public string desc { get;  set; }
        public string title { get;  set; }
        public int notificationType { get;  set; }
        public int? orderId { get;  set; }
        public int? rateId { get; set; }
        public int? questionId { get; set; }
    }
}
