using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Driver
{
   public class DriverViewModelApi
    {
        public int cityID { get; set; }
        public int gender { get; set; }
        public string mobileNo { get; set; }
        public string email { get; set; }
        public string iDNo { get; set; }
        public string name { get; set; }
        public int nationalityID { get; set; }
        public string address { get; set; }
        public string cityName { get; set; }
        public string nationalityName { get; set; }
        public string personalPic { get; set; }
        public string carPic { get; set; }
        public string iDPic { get; set; }
        public string licensePic { get; set; }
        public int driverID { get; set; }
        public string wallet { get;  set; }
        public string carLicensePicture { get; set; }
        public int notificationsCount { get;  set; }
        public decimal pendingPay { get;  set; }
        public decimal paid { get;  set; }
    }
}
