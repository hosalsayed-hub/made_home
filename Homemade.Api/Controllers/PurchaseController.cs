using Homemade.Api.Model;
using Homemade.BLL;
using Homemade.BLL.Customer;
using Homemade.BLL.General;
using Homemade.BLL.Purchase;
using Homemade.BLL.Setting;
using Homemade.BLL.SMS;
using Homemade.BLL.ViewModel.MyFatoora.FatoraPayment;
using Homemade.Model;
using Homemade.Model.BankTransaction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {

        #region Declarations
        private readonly BLGeneral _blGeneral;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly BLTransaction _BLTransaction;
        private readonly OTPService _OTPService;
        private FixedResultMessage _Result = new FixedResultMessage();
        private readonly BlTokens _blTokens;
        private readonly BlCustomer _blCustomer;
        private readonly BlCustomerBalance _blCustomerBalance;
        public PurchaseController(BlCustomerBalance BlCustomerBalance,BlCustomer blCustomer, BlTokens blTokens, BLGeneral blGeneral, UserManager<User> userManager, IConfiguration configuration, BLTransaction bLTransaction, FixedResultMessage fixedResultMessage, OTPService oTPService)
        {
            _blGeneral = blGeneral;
            _Result = fixedResultMessage;
            _userManager = userManager;
            _configuration = configuration;
            _OTPService = oTPService;
            _BLTransaction = bLTransaction;
            _blTokens = blTokens;
            _blCustomer = blCustomer;
            _blCustomerBalance = BlCustomerBalance;
        }
        #endregion
        #region Get User Lang
        private string GetUserLang()
        {
            // In your action method.
            var languages = Request.GetTypedHeaders()
                                   .AcceptLanguage
                                   ?.OrderByDescending(x => x.Quality ?? 1) // Quality defines priority from 0 to 1, where 1 is the highest.
                                   .Select(x => x.Value.ToString())
                                   .ToArray() ?? Array.Empty<string>();

            var browserLang = Request.Headers["Accept-Language"].ToString().Split(";").FirstOrDefault()?.Split(",").FirstOrDefault();
            return languages.FirstOrDefault();
        }
        private string GetUserId()
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
                        int JWTTokenValid = _blTokens.IsValidUserJWTTokenCustomer(token);
                        #endregion
                        if (JWTTokenValid != 0)
                        {
                            return JWTTokenValid.ToString();
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
        #endregion
        #region Transaction_Wallet
        public TransactionCard Insert_TRANSACTION_CARD(TransactionCard tripChargeLog)
        {
            return _BLTransaction.InsertTransactionCard(tripChargeLog);
        }
        public bool Insert_TRANSACTION_CARD_lOG(TransactionCardLog tripChargeLog)
        {
            _BLTransaction.InsertTransactionCardLog(tripChargeLog);
            return true;
        }
        public TransactionCard Get_TRANSACTION_CARD(string PaymentID, string InvoiceID, string CustomerReference)
        {
            return _BLTransaction.GetTransactionCard(PaymentID, InvoiceID, CustomerReference);
        }

        #region Update GenderRequest Driver

        [Route("/api/Purchase/GenerateCustomerReference")]
        [HttpPost]
        public async Task<IActionResult> GenerateCustomerReference(float InvoiceValue)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserId();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var Driverobj = _blCustomer.GetCustomerByUser(UserId);
                if (Driverobj != null)
                {
                    string token = RandomString(50);
                    // insert in Transaction With Log
                    var paymentLog = (new TransactionCard()
                    {
                        Amount = InvoiceValue,
                        IsRedirect = false,
                        IsSMSSentToUser = false,
                        LastStatusUpdateFrom = (int)LastStatusUpdateFromEnum.Mobile,
                        Message = "",
                        CustomerReference = token,
                        PaymentUrl = "",
                        RequestDate = _blGeneral.GetDateTimeNow(),
                        Status = "Pending",
                        TransactionStatus = (int)TransactionEnum.Execute_Process_Begin,
                        OrdersID = null,
                        UpdateDate = null,
                        CustomersID = Driverobj.CustomersID
                    });

                    paymentLog.TransactionCardLog.Add(new TransactionCardLog()
                    {
                        TransactionID = paymentLog.TransactionID,
                        OrdersID = 0,
                        CustomersID = Driverobj.CustomersID,
                        DateAdded = _blGeneral.GetDateTimeNow(),
                        LastStatusUpdateFrom = (int)LastStatusUpdateFromEnum.Web,
                        Message = "",
                        CustomerReference = token,
                        Status = "Pending",
                        TransactionStatus = (int)TransactionEnum.Execute_Process_Begin,
                    });
                    Insert_TRANSACTION_CARD(paymentLog);


                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new
                    {
                        CustomerReference = token
                    };
                    return Ok(_Result);
                }
                else
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Sorry_Not_Allow_Update : Api.Resources.Response.Sorry_Not_Allow_Update;
                    _Result.status = false;
                    _Result.isAuthorize = true;
                    return Ok(_Result);
                }
            }
            catch (System.Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.Message.ToString() : Api.Resources.Response.Error + e.Message.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }


        [HttpPost]
        [Obsolete]
        [Route("/api/Purchase/UpdateTransaction")]
        public async Task<IActionResult> UpdateTransaction()
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserId();
                string json;
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    json = await reader.ReadToEndAsync();
                }
                var deserializedDirectError = JsonConvert.DeserializeObject<RooterrorMobile>(json);
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var Driverobj = _blCustomer.GetCustomerByUser(UserId);
                if (Driverobj != null)
                {
                        var paymentLog = Get_TRANSACTION_CARD(deserializedDirectError.invoiceId.ToString(), deserializedDirectError.invoiceId.ToString(), deserializedDirectError.customerReference.ToString());
                        paymentLog.Status = deserializedDirectError.statusCode.ToString();
                        paymentLog.UpdateDate = _blGeneral.GetDateTimeNow();
                        paymentLog.LastStatusUpdateFrom = (int)LastStatusUpdateFromEnum.Mobile;
                        paymentLog.TransactionStatus = (int)TransactionEnum.Execute_Process_Failed;
                        paymentLog.TransactionCardLog.Add(new TransactionCardLog()
                        {
                            TransactionID = paymentLog.TransactionID,
                            CustomersID = Driverobj.CustomersID,
                            OrdersID = 0,
                            DateAdded = _blGeneral.GetDateTimeNow(),
                            LastStatusUpdateFrom = (int)LastStatusUpdateFromEnum.Mobile,
                            Message = deserializedDirectError.message,
                            CustomerReference = deserializedDirectError.customerReference,
                            InvoiceId = deserializedDirectError.invoiceId.ToString(),
                            Status = deserializedDirectError.statusCode.ToString(),
                            TransactionStatus = (int)TransactionEnum.Execute_Process_Completed,
                        });
                        paymentLog = _BLTransaction.UpdateTransactionCard(paymentLog);
                        _Result.message = deserializedDirectError.message;
                        _Result.status = true;
                        _Result.isAuthorize = true;
                        return Ok(_Result);
                }
                else
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Sorry_Not_Allow_Update : Api.Resources.Response.Sorry_Not_Allow_Update;
                    _Result.status = false;
                    _Result.isAuthorize = true;
                    return Ok(_Result);
                }
            }
            catch (System.Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.Message.ToString() : Api.Resources.Response.Error + e.Message.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        [HttpPost]
        [Obsolete]
        [Route("/api/Purchase/UpdateTransactionWallet")]
        public async Task<IActionResult> UpdateTransactionWallet()
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserId();
                string json;
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    json = await reader.ReadToEndAsync();
                }
                var deserializedDirectError = JsonConvert.DeserializeObject<RooterrorMobileWallet>(json);
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var Driverobj = _blCustomer.GetCustomerByUser(UserId);
                if (Driverobj != null)
                {
                    var paymentLog = Get_TRANSACTION_CARD(deserializedDirectError.invoiceId.ToString(), deserializedDirectError.invoiceId.ToString(), deserializedDirectError.customerReference.ToString());
                    paymentLog.Status = deserializedDirectError.statusCode.ToString();
                    paymentLog.UpdateDate = _blGeneral.GetDateTimeNow();
                    paymentLog.LastStatusUpdateFrom = (int)LastStatusUpdateFromEnum.Mobile;
                    paymentLog.TransactionStatus = (int)TransactionEnum.Execute_Process_Failed;
                    paymentLog.TransactionCardLog.Add(new TransactionCardLog()
                    {
                        TransactionID = paymentLog.TransactionID,
                        CustomersID = Driverobj.CustomersID,
                        OrdersID = 0,
                        DateAdded = _blGeneral.GetDateTimeNow(),
                        LastStatusUpdateFrom = (int)LastStatusUpdateFromEnum.Mobile,
                        Message = deserializedDirectError.message,
                        CustomerReference = deserializedDirectError.customerReference,
                        InvoiceId = deserializedDirectError.invoiceId.ToString(),
                        Status = deserializedDirectError.statusCode.ToString(),
                        TransactionStatus = (int)TransactionEnum.Execute_Process_Completed,
                    });
                    paymentLog = _BLTransaction.UpdateTransactionCard(paymentLog);
                    if (deserializedDirectError.statusCode == "000")
                    {
                     _blCustomerBalance.InsertBalance((int)TypeOperationEnum.CR, decimal.Parse(deserializedDirectError.amount), (int)TransactionTypeEnum.Charge_Wallet, Driverobj.CustomersID, UserId, null, _blGeneral.GetDateTimeNow(), "", "");
                    }
                    _Result.message = deserializedDirectError.message;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    return Ok(_Result);
                }
                else
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Sorry_Not_Allow_Update : Api.Resources.Response.Sorry_Not_Allow_Update;
                    _Result.status = false;
                    _Result.isAuthorize = true;
                    return Ok(_Result);
                }
            }
            catch (System.Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.Message.ToString() : Api.Resources.Response.Error + e.Message.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        public IActionResult GenerateToken()
        {
            int month = _blGeneral.GetDateTimeNow().Month;
            int day = _blGeneral.GetDateTimeNow().Day;
            string token = ((day * 100 + month) * 700 + day * 13).ToString();
            return Ok(token);
        }
        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz!@#$&()_×";
            var tok = new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            return tok;
        }
        public bool AddAmountToDriverWallet(int DriverID, double InvoiceValue, int UserId, string NameAr, string NameEn)
        {
            return true;
        }
        #endregion
        #endregion
    }
}
