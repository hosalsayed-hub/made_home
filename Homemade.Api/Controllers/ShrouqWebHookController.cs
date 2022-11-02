using Homemade.BLL.General;
 using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
  using Microsoft.AspNetCore.Mvc;
using System.Text;
using Homemade.BLL;
using Homemade.BLL.Vendor;
using System.Web;

namespace Homemade.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //Shrouq Delivery Company WeebHook
    public class ShrouqWebHookController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly BLGeneral _blGeneral;
        private readonly BlVendor _BlVendor;

        public ShrouqWebHookController(BlVendor BlVendor,BLGeneral blGeneral, IConfiguration configuration)
        {
            _blGeneral = blGeneral;
            _configuration = configuration;
            _BlVendor = BlVendor;
        }
        #region VMs

        public class Driver
        {
            public int id { get; set; }
            public string name { get; set; }
            public string phone { get; set; }
            public string photo { get; set; }
            public string rate { get; set; }
        }
        public class GetOrderRoot
        {
            public int order_id { get; set; }
            public int customer_id { get; set; }
            public int client_order_id { get; set; }
            public string shop { get; set; }
            public string branch { get; set; }
            public int branch_id { get; set; }
            public string branch_area { get; set; }
            public string dropoff_area { get; set; }
            public string tracking_code { get; set; }
            public string tracking_url { get; set; }
            public string expected_pickup { get; set; }
            public string expected_delivery { get; set; }
            public string at_pickup { get; set; }
            public string pickup { get; set; }
            public object at_dropoff_at { get; set; }
            public object dropoff_at { get; set; }
            public string fees { get; set; }
            public string status { get; set; }
            public int status_id { get; set; }
            public string value { get; set; }
            public string payment_type { get; set; }
            public string currency { get; set; }
            public string details { get; set; }
            public string created_at { get; set; }
            public string pickup_poa { get; set; }
            public string pickup_poa_qrcode { get; set; }
            public string dropoff_poa { get; set; }
            public string dropoff_poa_qrcode { get; set; }
            public Driver driver { get; set; }
        }


        // uodate order POST
        public class UpdateOrderRoot
        {
            public string details { get; set; }
            public string dropoff_instructions { get; set; }
            public decimal value { get; set; }
            public int payment_type { get; set; }
            public int preparation_time { get; set; }
            public decimal lat { get; set; }
            public decimal lng { get; set; }
        }


        // WebHook >> order Updated
        public class HookOrderUpdatedRoot
        {
            public string client_order_id { get; set; }
            public string order_id { get; set; }
            public string status_id { get; set; }
            public Driver driver { get; set; }
        }


        // WebHook >> order Cancelled
        public class HookOrderCancelledRoot
        {
            public string client_order_id { get; set; }
            public string order_id { get; set; }
        }


        public class shrouqStatus
        {
            public string statusId { get; set; }
            public string statusName { get; set; }
            public int madeStatusId { get; set; }
        }
        #endregion
        /// <summary>
        /// ارجاع ىتفاصيل طلب الشروق برقم الطلب
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public GetOrderRoot GetOrderById(Int64 orderId)
        {

            string apiPath = _configuration["AlshroukCompanyAPI"] + "/order/get/"+ orderId + "";


            var client = new RestClient(apiPath);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
           
            IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);

            GetOrderRoot myDeserializedClass = JsonConvert.DeserializeObject<GetOrderRoot>(response.Content);
            return myDeserializedClass;
        }
        /// <summary>
        ///  details (Optional) to add note to order
        ///  dropoff_instructions(Optional) to add dropoff instructions
        ///  value(Optional) Order value
        ///  payment_type(Optional) Payment type[1 = CASH / 2 = CREDIT]
        ///  preparation_time(Optional) Preparation time, default 0
        ///  lat(Optional) Customer address latitude, this will update the current address
        ///  lng(Optional) Customer address longitude, this will update the current address
        /// </summary>
        /// <param name="details"></param>
        /// <param name="dropoff_instructions"></param>
        /// <param name="value"></param>
        /// <param name="payment_type"></param>
        /// <param name="preparation_time"></param>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public GetOrderRoot UpdateOrder(string details,string dropoff_instructions,decimal value,int payment_type,int preparation_time,decimal lat,decimal lng, int orderid)
        {

            UpdateOrderRoot updateOrderRoot = new UpdateOrderRoot
            {
                details = details,
                dropoff_instructions = dropoff_instructions,
                lat = lat,
                lng = lng,
                payment_type = payment_type,
                preparation_time = preparation_time,
                value = value
            };
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(updateOrderRoot);
            string apiPath = _configuration["AlshroukCompanyAPI"] + "/order/update/" + orderid + "";
            var client = new RestClient(apiPath);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            GetOrderRoot myDeserializedClass = JsonConvert.DeserializeObject<GetOrderRoot>(response.Content);
            return myDeserializedClass;
        }
        #region Hook
        /// <summary>
        /// Status ID    Status description
        /// 1         :  Order created
        /// 2         :  Pending driver acceptance
        /// 4         :  Pending order preparation
        /// 6         :  Order picked up
        /// 8         :  Arrived to dropoff
        /// 9         :  Order delivered
        /// 10        :  Order cancelled
        /// 13        :  Driver acceptance timeout
        /// 16        :  Arrived to pickup
        /// 18        :  Driver rejected the order
        /// 19        :  Order Unassigned
        /// 20        :  Order failed
        /// </summary>
        /// <returns></returns>
        /// 
        ///{
        ///  "branch": "Home Made",
        ///  "branch_area": "-",
        ///  "branch_id": "8219",
        ///  "client_order_id": "102030",
        ///  "created_at": "2021-10-26 16:00:11",
        ///  "currency": "SAR",
        ///  "customer_id": "1980026",
        ///  "details": "",
        ///  "distance": "1558.89",
        ///  "dropoff_area": "Port Said City",
        ///  "expected_delivery": "2021-10-28 21:06:10",
        ///  "expected_pickup": "2021-10-26 16:51:10",
        ///  "fees": "15.00",
        ///  "order_id": "12829929",
        ///  "shop": "TEST INT",
        ///  "status": "Pending driver acceptance",
        ///  "status_id": "2",
        ///  "tracking_code": "8219128299291635253211",
        ///  "tracking_url": "https://line.yallow.com/o/t/8219128299291635253211"
        /// }
        [HttpPost]
        [Route("orderUpdated")]
        public async Task<bool> orderUpdated()
        {
            string json = "";
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    json = await reader.ReadToEndAsync();
                }
                var collection = HttpUtility.ParseQueryString(json);
                json = Newtonsoft.Json.JsonConvert.SerializeObject(collection.AllKeys.ToDictionary(y => y, y => collection[y]));
                HookOrderUpdatedRoot mySallaWebhook = JsonConvert.DeserializeObject<HookOrderUpdatedRoot>(json);
                var IntegrationOrderID = mySallaWebhook.order_id;
                var orderID = mySallaWebhook.client_order_id;
                var orderStatus = _BlVendor.GetStatusCompany((int)ShippingCompanyEnum.Shourq).Where(e => e.StatusCoId.ToString() == mySallaWebhook.status_id).FirstOrDefault();
                var ShrouqRes = json;
                var DriverName = "";
                var DriverMobile = "";
                if (mySallaWebhook.driver != null)
                {
                    DriverName = mySallaWebhook.driver.name;
                    DriverMobile = mySallaWebhook.driver.phone;
                }
                var Response = _BlVendor.UpdateShrouqOrder(orderStatus.StatusHomeMadeId, orderStatus.NameEN, orderStatus.StatusCoId.ToString(), IntegrationOrderID, ShrouqRes,_configuration["FireBaseKey"], DriverName, DriverMobile);
            }
            catch (Exception ex)
            {

            }

            return true;
        }
        /// <summary>
        /// {
        ///  "branch": "Home Made",
        ///  "branch_area": "-",
        ///  "branch_id": "8219",
        ///  "client_order_id": "102030",
        ///  "created_at": "2021-10-26 16:00:11",
        ///  "currency": "SAR",
        ///  "customer_id": "1980026",
        ///  "details": "",
        ///  "distance": "1558.89",
        ///  "dropoff_area": "Port Said City",
        ///  "expected_delivery": "2021-10-28 21:06:10",
        ///  "expected_pickup": "2021-10-26 16:51:10",
        ///  "fees": "15.00",
        ///  "order_id": "12829929",
        ///  "shop": "TEST INT",
        ///  "status": "Pending driver acceptance",
        ///  "status_id": "2",
        ///  "tracking_code": "8219128299291635253211",
        ///  "tracking_url": "https://line.yallow.com/o/t/8219128299291635253211"
        ///}
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("orderCancelled")]
        public async Task<bool> orderCancelled()
        {
            string json = "";
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    json = await reader.ReadToEndAsync();
                }
                var collection = HttpUtility.ParseQueryString(json);
                json = Newtonsoft.Json.JsonConvert.SerializeObject(collection.AllKeys.ToDictionary(y => y, y => collection[y]));
                HookOrderCancelledRoot mySallaWebhook = JsonConvert.DeserializeObject<HookOrderCancelledRoot>(json);
                var IntegrationOrderID = mySallaWebhook.order_id;
                var orderID = mySallaWebhook.client_order_id;
                var ShrouqRes = json;
                var Response = _BlVendor.CancelShrouqOrder(IntegrationOrderID, ShrouqRes, _configuration["FireBaseKey"]);
            }
            catch (Exception ex)
            {
              
            }
            return true;
        }

        #endregion
    }
}
