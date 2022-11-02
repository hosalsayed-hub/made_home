using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Cart;
using Homemade.BLL.ViewModel.MyFatoora.FatoraPayment;
using Homemade.BLL.ViewModel.Order;
using Homemade.DAL.Contract;
using Homemade.Model.BankTransaction;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.Purchase
{
    public class BLTransaction
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BLTransaction(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region TransactionCard
        public TransactionCard InsertTransactionCard(TransactionCard tripChargeLog)
        {
            tripChargeLog = Uow.TransactionCard.Insert(tripChargeLog);
            Uow.Commit();
            return tripChargeLog;
        }
        public TransactionCard UpdateTransactionCard(TransactionCard tripChargeLog)
        {
            tripChargeLog = Uow.TransactionCard.Update(tripChargeLog);
            Uow.Commit();
            return tripChargeLog;
        }
        public TransactionCard GetTransactionCard(string PaymentID, string InvoiceId, string CustomerReference)
        {
            return Uow.TransactionCard.GetAll(e => e.PaymentID == PaymentID || e.InvoiceId == InvoiceId || e.CustomerReference == CustomerReference, "Orders").OrderByDescending(e=> e.TransactionID).FirstOrDefault();
        }
        public TransactionCardLog InsertTransactionCardLog(TransactionCardLog tripChargeLog)
        {
            tripChargeLog = Uow.TransactionCardLog.Insert(tripChargeLog);
            Uow.Commit();
            return tripChargeLog;
        }
        public async Task<string> SendPayMyfatoraLink_Drafts(int orderid,string _sessionID, string paymentMethod, string cookieLingua, string PaymyFatoraSecertToken, string PaymyFatoraUrl, int CustomersID, string PaymyFatoraRedirectUrl, decimal totalWithDelivery )
        {
            PayMyfatora payMyfatoraResult = new PayMyfatora();

            try
            {

                var RequestDesc = "";

                //var Data = Uow.CartMaster.GetAll(e => e.CartMasterID == SessionID && !e.IsDeleted, "CartDetails.Product,Customers,CartDetails.Product.Vendors,CartDetails").Select(e => new CartDetailsVM()
                //{
                //    IsDeleted = e.IsDeleted,
                //    CreateDate = e.CreateDate,
                //    UpdateDate = e.UpdateDate,
                //    DeleteDate = e.DeleteDate,
                //    CartMasterID = e.CartMasterID,
                //    Customername = e.Customers.FirstName + e.Customers.SeconedName,
                //    ReceiverMobile = e.Customers.MobileNo,
                //    receiverAddress = e.Customers.Address,
                //    Promocode = e.Promocode
                //}).FirstOrDefault();
                //var needToPay = 0.01;
                var needToPay = totalWithDelivery;


                var orderdata = Uow.Orders.GetAll(e => e.OrdersID == orderid).FirstOrDefault().Customers;



                if (cookieLingua == "ar")
                {
                    RequestDesc = "دفع  طلب من ( منصه شغل بيت ) ";
                }
                else
                {
                    RequestDesc = "Collecting an order Cost from ( HomeMade )";
                }

                var RequestPayfatoraData = new RequestPayFatora()
                {
                    NotificationOption = "EML",
                    CallBackUrl = string.Concat(PaymyFatoraRedirectUrl, "site/home/Checking_PayingReditect"),
                    ErrorUrl = string.Concat(PaymyFatoraRedirectUrl, "site/home/Checking_PayingReditect"),
                    CustomerAddress = new CustomerAddress
                    {
                        Address = orderdata.Address != null ? orderdata.Address.Length > 50 ? orderdata.Address.Substring(0, 40) : orderdata.Address : "No Address ..",
                        AddressInstructions = "",
                        Block = "",
                        HouseBuildingNo = "",
                        Street = orderdata.Address != null ? orderdata.Address.Length > 50 ? orderdata.Address.Substring(0, 40) : orderdata.Address : "No Address .."
                    },
                    CustomerCivilId = 0,
                    CustomerEmail = "a.elaf@hotmail.com",
                    CustomerMobile = orderdata.MobileNo,
                    CustomerName = orderdata.FirstName,
                    CustomerReference = orderid.ToString(),
                    DisplayCurrencyIso = "SAR",
                    ExpireDate = "",
                    InvoiceValue = (double)needToPay,

                    Language = cookieLingua,
                    MobileCountryCode = "+966",
                    PaymentMethodId = paymentMethod,
                    UserDefinedField = _sessionID ,
                    InvoiceItems = new System.Collections.Generic.List<InvoiceItem>() {
                    new InvoiceItem{ ItemName = RequestDesc ,Quantity = 1,UnitPrice =(double)needToPay}
                    //new InvoiceItem{ ItemName = RequestDesc ,Quantity = 1,UnitPrice =1}
                    }
                };
                var jsonRequestPayTapData = System.Text.Json.JsonSerializer.Serialize(RequestPayfatoraData);


                string SecertToken = PaymyFatoraSecertToken;
                string baseURL = PaymyFatoraUrl;
                var url = baseURL + "/v2/SendPayment";

                var paymentLog = (new TransactionCard()
                {
                    Amount = RequestPayfatoraData.InvoiceValue,
                    IsRedirect = false,
                    IsSMSSentToUser = false,
                    LastStatusUpdateFrom = (int)LastStatusUpdateFromEnum.Web,
                    Message = "",
                    PaymentID = string.Concat("order_", orderid),
                    PaymentUrl = "",
                    RequestDate = _blGeneral.GetDateTimeNow(),
                    Status = "Pending",
                    TransactionStatus = (int)TransactionEnum.SendPayment_Process_Begin,
                    OrdersID = orderid,
                    UpdateDate = null,
                    CustomersID = CustomersID

                });
                paymentLog.TransactionCardLog.Add(new TransactionCardLog()
                {
                    TransactionID = paymentLog.TransactionID,
                    OrdersID = orderid,
                    DateAdded = _blGeneral.GetDateTimeNow(),
                    LastStatusUpdateFrom = (int)LastStatusUpdateFromEnum.Web,
                    Message = "",
                    PaymentID = string.Concat("order_", orderid),
                    Status = "Pending",
                    TransactionStatus = (int)TransactionEnum.SendPayment_Process_Begin,
                    CustomersID = CustomersID

                });

                InsertTransactionCard(paymentLog);

                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", "Bearer " + SecertToken);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", jsonRequestPayTapData, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                InvoiceResponce deserializedProduct = JsonConvert.DeserializeObject<InvoiceResponce>(response.Content);


                if (response.StatusCode == System.Net.HttpStatusCode.OK && deserializedProduct.IsSuccess)
                {
                    #region Log

                    try
                    {
                        paymentLog.InvoiceId = deserializedProduct.Data.InvoiceId.ToString();
                        paymentLog.PaymentID = deserializedProduct.Data.InvoiceId.ToString();
                        paymentLog.PaymentUrl = deserializedProduct.Data.InvoiceURL;
                        paymentLog.Message = deserializedProduct.Message;
                        paymentLog.Status = deserializedProduct.IsSuccess.ToString();
                        paymentLog.TransactionStatus = (int)TransactionEnum.SendPayment_Process_Completed;

                        paymentLog = UpdateTransactionCard(paymentLog);

                        InsertTransactionCardLog(new TransactionCardLog()
                        {
                            TransactionID = paymentLog.TransactionID,
                            OrdersID = orderid,
                            DateAdded = _blGeneral.GetDateTimeNow(),
                            LastStatusUpdateFrom = (int)LastStatusUpdateFromEnum.Web,
                            Message = deserializedProduct.Message,
                            InvoiceId = deserializedProduct.Data.InvoiceId.ToString(),
                            Status = deserializedProduct.IsSuccess.ToString(),
                            TransactionStatus = (int)TransactionEnum.SendPayment_Process_Completed,
                            CustomersID = CustomersID

                        });

                    }
                    catch (Exception)
                    {
                    }

                    #endregion

                    DataResponce dataResponce = new DataResponce()
                    {
                        CustomerReference = deserializedProduct.Data.CustomerReference,
                        InvoiceId = deserializedProduct.Data.InvoiceId.ToString(),
                        IsDirectPayment = false,
                        PaymentURL = deserializedProduct.Data.InvoiceURL,
                        UserDefinedField = deserializedProduct.Data.UserDefinedField,
                    };

                    payMyfatoraResult.IsSuccess = deserializedProduct.IsSuccess;
                    payMyfatoraResult.Message = deserializedProduct.Message;
                    payMyfatoraResult.Data = dataResponce;
                    payMyfatoraResult.ValidationErrors = deserializedProduct.ValidationErrors;

                    return dataResponce.PaymentURL;
                }
                else
                {
                    payMyfatoraResult.IsSuccess = deserializedProduct.IsSuccess;
                    payMyfatoraResult.Message = deserializedProduct.Message;
                    payMyfatoraResult.Data = null;
                    payMyfatoraResult.ValidationErrors = deserializedProduct.ValidationErrors;

                    #region Log
                    paymentLog.PaymentID = "Invalid";
                    paymentLog.PaymentUrl = "Invalid";
                    paymentLog.Message = deserializedProduct.Message;
                    paymentLog.Status = deserializedProduct.IsSuccess.ToString();
                    paymentLog.TransactionStatus = (int)TransactionEnum.SendPayment_Process_Failed;

                    UpdateTransactionCard(paymentLog);

                    InsertTransactionCardLog(new TransactionCardLog()
                    {
                        TransactionID = paymentLog.TransactionID,

                        OrdersID = orderid,
                        DateAdded = _blGeneral.GetDateTimeNow(),
                        LastStatusUpdateFrom = (int)LastStatusUpdateFromEnum.Web,
                        Message = deserializedProduct.Message,
                        PaymentID = "Invalid",
                        Status = deserializedProduct.IsSuccess.ToString(),
                        TransactionStatus = (int)TransactionEnum.SendPayment_Process_Failed,
                        CustomersID = CustomersID

                    });

                    #endregion

                    return "Invalid";
                }

            }
            catch (Exception ex)
            {
                payMyfatoraResult.IsSuccess = false;
                payMyfatoraResult.Message = ex.Message + (ex.InnerException != null ? ex.InnerException.Message : "");
                payMyfatoraResult.Data = null;
                payMyfatoraResult.ValidationErrors = null;

                // send To Admin
                var message = "error When ExecutePayment : " + Environment.NewLine + (ex.Message + (ex.InnerException != null ? ex.InnerException.Message : ""));
                _blGeneral.SendEmail("a.elaf@hotmail.com", "Urgent Paying Issues", message);
                //_blGeneral.SendEmail("mostafatfs@hotmail.com", "Urgent Paying Issues", message);
                //_blGeneral.SendEmail("mostafasayed_2014@hotmail.com", "Urgent Paying Issues", message);

                // send urgent Whatsapp To Admin 
                //await _blTripMaster.SendWhatsApp("1211487713", message, _configuration["WhatsAppToken"], _configuration["WhatsAppURL"]);
                //await _blTripMaster.SendWhatsApp("1016632100", message, _configuration["WhatsAppToken"], _configuration["WhatsAppURL"]);
                //await _blTripMaster.SendWhatsApp("595489308", message, _configuration["WhatsAppToken"], _configuration["WhatsAppURL"]);
                //await _blTripMaster.SendWhatsApp("1201196763", message, _configuration["WhatsAppToken"], _configuration["WhatsAppURL"]);



                return "Invalid";

            }
        }
        #endregion
    }
}
