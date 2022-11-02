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
    //Lavander Delivery Company WeebHook
    public class LavenderController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly BLGeneral _blGeneral;
        private readonly BlVendor _BlVendor;
        private FixedResultMessage _Result = new FixedResultMessage();

        public LavenderController(BlVendor BlVendor, BLGeneral blGeneral, IConfiguration configuration)
        {
            _blGeneral = blGeneral;
            _configuration = configuration;
            _BlVendor = BlVendor;
        }

        #region Classes
        public class DriverDetail
        {
            public string name { get; set; }
            public string phone { get; set; }
            //public string image { get; set; }
            //public int rating { get; set; }
            //public string driverLat { get; set; }
            //public string driverLong { get; set; }
        }

        public class UpdateStatus
        {
            public string refOrderNo { get; set; }
            public string lavender_order_id { get; set; }
            public int status_code { get; set; }
            public string Reason { get; set; }
            public string status { get; set; }
            public DriverDetail driverDetail { get; set; }
        }
        #endregion
        public string GetSecretId()
        {
            try
            {
                var browserLang = Request.Headers["Authorization"].ToString().Split(";").FirstOrDefault()?.Split(",").FirstOrDefault();

                var headers = Request.Headers;

                if (headers.ContainsKey("Authorization"))
                {
                    string token = Request.Headers["Authorization"].FirstOrDefault().ToString().Replace("bearer ", "").Replace("Bearer ", "");

                    if (!string.IsNullOrWhiteSpace(token))
                    {
                        #region Update JWT Token
                        string SecertToken = _configuration["LavenderToken"];
                        var JWTTokenValid = token == SecertToken ? true : false;
                        #endregion
                        if (JWTTokenValid)
                        {
                            return "true";
                        }
                        else // JWT Token Not Valid
                        {
                            return "incorrect Authorization Value";
                        }
                    }
                    else // JWT Token Not Valid
                    {
                        return "incorrect Authorization Value";
                    }
                }
                else
                {
                    return "incorrect Authorization Value";
                }
            }
            catch (Exception ex)
            {
                return "incorrect Authorization Value";
            }
        }
        [HttpPost]
        [Route("OrderUpdate")]
        public IActionResult orderUpdated(UpdateStatus model)
        {
            try
            {
                var UserLogin = GetSecretId();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                // you can find This Id in Table VendorOrder in Col (VendorOrderId)
                if (!string.IsNullOrWhiteSpace(model.refOrderNo))
                {
                    if (!string.IsNullOrWhiteSpace(model.lavender_order_id))
                    {
                        if (model.status_code != 0)
                        {
                            var orderStatus = _BlVendor.GetStatusCompany((int)ShippingCompanyEnum.Lavender).Where(e => e.StatusCoId == model.status_code).FirstOrDefault();
                            var ModelJson = JsonConvert.SerializeObject(model);
                            var DriverName = "";
                            var DriverMobile = "";
                            if (model.driverDetail != null)
                            {
                                DriverName = model.driverDetail.name;
                                DriverMobile = model.driverDetail.phone;
                            }
                            var Response = _BlVendor.UpdateLavenderOrder(orderStatus.StatusHomeMadeId, orderStatus.NameEN, orderStatus.StatusCoId, model.lavender_order_id, ModelJson, _configuration["FireBaseKey"], DriverName, DriverMobile, model.Reason);
                            _Result.message = "Updated Successfully";
                            _Result.status = true;
                            _Result.isAuthorize = true;
                            _Result.data = new { order =  model };
                        }
                        else
                        {
                            _Result.message = "status_code not found";
                            _Result.status = false;
                            _Result.isAuthorize = true;
                        }
                    }
                    else
                    {
                        _Result.message = "lavender_order_id not found";
                        _Result.status = false;
                        _Result.isAuthorize = true;
                    }
                }
                else
                {
                    _Result.message = "refOrderNo not found";
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
