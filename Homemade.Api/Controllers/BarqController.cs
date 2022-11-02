using Homemade.Api.Model;
using Homemade.BLL;
using Homemade.BLL.General;
using Homemade.BLL.Vendor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //Barq Delivery Company WeebHook
    public class BarqController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly BLGeneral _blGeneral;
        private readonly BlVendor _BlVendor;
        private FixedResultMessage _Result = new FixedResultMessage();

        public BarqController(BlVendor BlVendor, BLGeneral blGeneral, IConfiguration configuration)
        {
            _blGeneral = blGeneral;
            _configuration = configuration;
            _BlVendor = BlVendor;
        }
        #region Classes
        public class Courier
        {
            public string name { get; set; }
            public string mobile { get; set; }
            public string id_number { get; set; }
            public string dob { get; set; }
            public string nationality { get; set; }
        }
        public class UpdateStatus
        {
            public int id { get; set; }
            public string return_otp { get; set; }
            public string pickup_otp { get; set; }
            public string order_no { get; set; }
            public string merchant_order_id { get; set; }
            public string status { get; set; }
            public bool is_completed { get; set; }
            public int hub_id { get; set; }
            public double delivery_fee { get; set; }
            public Courier courier { get; set; }
        }
        public class shrouqStatus
        {
            public int statusId { get; set; }
            public string statusName { get; set; }
            public int madeStatusId { get; set; }
        }
        #endregion
        [HttpPost]
        [Route("OrderUpdate")]
        public IActionResult orderUpdated(UpdateStatus model)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(model.id.ToString()))
                {
                            var orderStatus = _BlVendor.GetStatusCompany((int)ShippingCompanyEnum.Barq).Where(e => e.NameEN == model.status).FirstOrDefault();
                            var ModelJson = JsonConvert.SerializeObject(model);
                            var DriverName = "";
                            var DriverMobile = "";
                            if (model.courier != null)
                            {
                                DriverName = model.courier.name;
                                DriverMobile = model.courier.mobile;
                            }
                            var Response = _BlVendor.UpdateBarqOrder(orderStatus.StatusHomeMadeId, orderStatus.NameEN, orderStatus.StatusCoId, model.id.ToString(), ModelJson, _configuration["FireBaseKey"], DriverName, DriverMobile);
                            _Result.message = "Updated Successfully";
                            _Result.status = true;
                            _Result.isAuthorize = true;
                            _Result.data = new { order = model };
                }
                else
                {
                    _Result.message = "id not found";
                    _Result.status = false;
                    _Result.isAuthorize = true;
                }
            }
            catch (Exception ex)
            {
                _Result.message = "";
                _Result.status = false;
                _Result.isAuthorize = true;
            }
            return Ok(_Result);

        }
    }
}
