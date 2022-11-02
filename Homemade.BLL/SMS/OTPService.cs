using Homemade.BLL.Customer;
using Homemade.BLL.Driver;
using Homemade.BLL.Emp;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.SMS
{
    public class OTPService
    {
        #region Declaretion
        private readonly IConfiguration _configuration;
        private readonly BlCustomer _BlCustomer;
        private readonly BlDriver _blDriver;
        private readonly BlEmployee _BlEmployee;
        private readonly BlLogTextMessage _blLogTextMessage;
        public OTPService(IConfiguration configuration, BlCustomer BlCustomer, BlDriver BlDriver, BlEmployee BlEmployee, BlLogTextMessage BlLogTextMessage)
        {
            _configuration = configuration;
            _BlCustomer = BlCustomer;
            _blDriver = BlDriver;
            _BlEmployee = BlEmployee;
            _blLogTextMessage = BlLogTextMessage;
        }
        #endregion
        #region Helpers
        public class UniFoinicRespons
        {
            public bool success { get; set; }
            public string message { get; set; }
            public string errorCode { get; set; }
            public DataUnifonicResponse data { get; set; }
        }
        public class DataUnifonicResponse
        {
            public string MessageID { get; set; }
            public string CorrelationID { get; set; }
            public string Status { get; set; }
            public int NumberOfUnits { get; set; }
            public decimal Cost { get; set; }
            public decimal Balance { get; set; }
            public string Recipient { get; set; }
            public string TimeCreated { get; set; }
            public string CurrencyCode { get; set; }
        }
        #endregion
        #region SMS
        [Obsolete]
        public async Task<int> ThankMessageAfterRegister(string MobileNumber, int UserTypeId)
        {
            var message =
                   "شكرا لتسجيلكم في شغل بيت، سيتم التواصل معكم في أقرب وقت لتفعيل حسابكم.";
            if (UserTypeId == (int)UserTypeEnum.Customer)
            {
                message = "شكرا لتسجيلكم في شغل بيت، يمكنك ان تحظي بمتعة التسوق و البدء في طلب المنتجات .";
            }
            else if (UserTypeId == (int)UserTypeEnum.Vendor)
            {
                message = "شكرا لتسجيلكم في شغل بيت، رجاء إكمال بيانات متجرك والبدء في إضافة منتجاتك .";
            }
            int MessageReasonId = (int)MessageReasonEnum.Registration;
            var valueResult = await SendMessageToUser(MobileNumber, message, UserTypeId, (int)BLL.MessageTypeEnum.SMS, MessageReasonId);
            return valueResult;

        }
        [Obsolete]
        public async Task<int>  MessageAfterRegister_Employee(string MobileNumber, int UserTypeId,string username,string Password)
        {
            var message = "شكرا لانضامكم معنا موظف بشغل بيت.";
            message += "اسم المستخدم : " + username;
            message += "رمز المرور : "+ Password;
            message += "https://made-home.com/identity/account/login";

            int MessageReasonId = (int)MessageReasonEnum.Registration;
            var valueResult = await SendMessageToUser(MobileNumber, message, UserTypeId, (int)BLL.MessageTypeEnum.SMS, MessageReasonId);
            return valueResult;

        }
        public async Task<int> SendSMSForVerify(string MobileNumber, string ActivationCode, int UserTypeId,int MessageReasonId)
        {
            var message = "رمز التحقق" + Environment.NewLine + "Verification Code" + Environment.NewLine + ActivationCode;
            var valueResult = await SendMessageToUser(MobileNumber, message, UserTypeId, (int)MessageTypeEnum.SMS, MessageReasonId);
            return valueResult;
        }
        public async Task<int> SendSMSForOrder(string MobileNumber, string message, int UserTypeId, int MessageReasonId)
        {
            var valueResult = await SendMessageToUser(MobileNumber, message, UserTypeId, (int)MessageTypeEnum.SMS, MessageReasonId);
            return valueResult;
        }
        public async Task<int> SendMessageToUser(string MobileNumber, string Message, int UserTypeId, int MessageTypeId, int MessageReasonId)
        {
            #region Mobile
            int County = 1;
            if (UserTypeId == (int)UserTypeEnum.Customer)
            {
                var Client = _BlCustomer.GetCustomersByMobileNo(MobileNumber, "City.Region");
                if (Client != null)
                {
                    County = (int)Client.City.Region.CountryID;
                }
            }
            else if (UserTypeId == (int)UserTypeEnum.Driver)
            {
                var driver = _blDriver.GetByMobileNo(MobileNumber);
                if (driver != null)
                {
                    County = driver.City.Region.CountryID;
                }
            }
            else if (UserTypeId == (int)UserTypeEnum.Employee || UserTypeId == (int)UserTypeEnum.Admin)
            {
                var operators = _BlEmployee.GetByMobile(MobileNumber);
                if (operators != null)
                {
                    County = operators.City.Region.CountryID;
                }
            }
            var message = string.Format(Message);
            var mobilenumber = "";
            if (MobileNumber.StartsWith("0"))
            {
                MobileNumber = MobileNumber.Substring(1);
            }
            if (County == 1)
            {
                mobilenumber = "966" + MobileNumber;
            }
            else
            {
                mobilenumber = "0020" + MobileNumber;
            }
            #endregion
            int StatusCode = 0;
            var smsBalance = _BlCustomer.LastSmsBalance();
                try
                {
                if (_BlCustomer.IsSmsSend())
                {
                    #region Send message
                    var appsid = _configuration["AppSid"];
                    var senderId = _configuration["SenderID"];
                    var fulllink = "https://basic.unifonic.com/rest/SMS/messages?AppSid=" + appsid + "&SenderID=" + senderId + "&Recipient=" + mobilenumber + "&Body=" + Message;
                    var client = new RestClient(fulllink);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json;charset=utf-8");
                    IRestResponse response = client.Execute(request);
                    var resultContent = response.Content.ToString();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        UniFoinicRespons jsons_success_Inquiry = JsonConvert.DeserializeObject<UniFoinicRespons>(resultContent);
                        if (jsons_success_Inquiry.success == true)
                        {
                            var ResponseUnifonic = "Balance: " + jsons_success_Inquiry.data.Balance + " "+
                                "CorrelationID: " + jsons_success_Inquiry.data.CorrelationID + " " +
                                "Cost: " + jsons_success_Inquiry.data.Cost + " " +
                                "CurrencyCode: " + jsons_success_Inquiry.data.CurrencyCode + " "+
                                "NumberOfUnits: " + jsons_success_Inquiry.data.NumberOfUnits + " "+
                                "MessageID: " + jsons_success_Inquiry.data.MessageID + " "+
                                "Recipient: " + jsons_success_Inquiry.data.Recipient + " "+
                                "Status: " + jsons_success_Inquiry.data.Status + " "+
                                "TimeCreated: " + jsons_success_Inquiry.data.TimeCreated + " ";
                            _blLogTextMessage.Insert(MobileNumber, message, UserTypeId, MessageReasonId, MessageTypeId, "", ResponseUnifonic, true);
                            StatusCode = 0;
                        }
                        else
                        {
                            var sendmessage = message + " SMS Result : " + jsons_success_Inquiry.data.Status;
                            _blLogTextMessage.Insert(MobileNumber, Message, UserTypeId, MessageReasonId, MessageTypeId, "", sendmessage, false);
                            StatusCode = -1;
                        }
                    }
                    else
                    {
                        var sendmessage = message + " SMS Result : Not Success " + response;
                        _blLogTextMessage.Insert(MobileNumber, Message, UserTypeId, MessageReasonId, MessageTypeId, "", sendmessage, false);

                        StatusCode = -2;
                    }
                    #endregion
                }
                else
                {
                    var sendmessage = "Is Send False";
                    _blLogTextMessage.Insert(MobileNumber, Message, UserTypeId, MessageReasonId, MessageTypeId, "", sendmessage, false);
                }
                return StatusCode;
                }    
                catch (Exception ex)
                {
                    var sendmessage = "Exception SMS Result : " + ex.Message.ToString();
                    _blLogTextMessage.Insert(MobileNumber, Message, UserTypeId, MessageReasonId, MessageTypeId, "", sendmessage, false);
                    return StatusCode;
                }
        }
        #endregion
    }
}
