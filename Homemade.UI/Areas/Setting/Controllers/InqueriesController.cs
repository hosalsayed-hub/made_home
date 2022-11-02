using Homemade.BLL;
using Homemade.BLL.General;
using Homemade.BLL.Setting;
using Homemade.BLL.SMS;
using Homemade.BLL.ViewModel.Setting;
using Homemade.UI.Attributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Homemade.UI.Areas.Setting.Controllers
{
    public class InqueriesController : Controller
    {
        #region Declarations
        private readonly BlInqueries blInqueries;
        private readonly ResultMessage result;
        private readonly BLGeneral bLGeneral;
        private readonly OTPService _OTPService;

        public InqueriesController(BlInqueries blInqueries, ResultMessage result, BLGeneral bLGeneral, OTPService oTPService)
        {
            this.bLGeneral = bLGeneral;
            this.blInqueries = blInqueries;
            this.result = result;
            _OTPService = oTPService;
        }
        #endregion
        #region View
        [CustomeAuthoriz((int)ControllerEnum.Inqueries, (int)ActionEnum.View)]
        public IActionResult Index() => View(new InqueriesViewModel());
        #endregion
        #region Helpers
        [HttpPost]
        public JsonResult LoadTable()
        {
            int displayLength = int.Parse(Request.Form["iDisplayLength"]);
            int displayStart = int.Parse(Request.Form["iDisplayStart"]);
            var isfirst = false;
            if (displayStart == 0)
            {
                displayStart = displayLength;
                isfirst = true;
            }
            int pageno = displayStart / displayLength;
            if (!isfirst)
            {
                pageno = pageno + 1;
            }

            var data = blInqueries.GetInqueries().Select(p => new
            {
                id = p.InqueriesID,
                name = p.Name,
                email = !string.IsNullOrWhiteSpace(p.Email) ? p.Email : "---",
                message = p.Message,
                mobileNo = p.MobileNo,
                createDate = p.CreateDate.ToString("yyyy-MM-dd hh:mm tt"),
            }).ToList();
            if (Request.Form.ContainsKey("sSearch"))
            {
                string sea = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(sea))
                {
                    data = data.Where(x => x.name.ToLower().Contains(sea.ToLower()) || x.message.ToLower().Contains(sea.ToLower()) || (x.email != null ? x.email.ToLower().Contains(sea.ToLower()) : true)).ToList();
                }
            }
            var Responce = data.AsQueryable();
            Responce = Responce.Skip(Convert.ToInt32(pageno - 1) * displayLength);
            var takenData = Responce.Take(((Convert.ToInt32(pageno) * displayLength)) - (Convert.ToInt32(pageno - 1) * displayLength));
            int totalCount = data.Count();
            return Json(new
            {
                iTotalRecords = totalCount,
                iTotalDisplayRecords = totalCount,
                aaData = takenData
            });
        }
        #endregion
        #region Action
        [HttpPost]
        public async Task<JsonResult> ReplyInqueries(int inqueriesID, int[] messageTypeId, string message, string emailContent, string subject)
        {
            var result = false;
            var IsSendEmail = true;
            var IsSendSMS = true;
            try
            {
                var inqueries = blInqueries.GetByID(inqueriesID);
                if (inqueries != null)
                {
                    bool checkvalidatemobile = false;
                    bool checkvalidateEmail = false;
                    if (!string.IsNullOrEmpty(inqueries.MobileNo))
                    {
                        string MatchPhoneNumberPattern = @"^(009665|9665|\+9665|05|5)(5|0|3|6|4|9|1|8|7)([0-9]{7})$";
                        var MobileNo = inqueries.MobileNo.Trim();
                        string oustput = new string(MobileNo.Where(c => char.IsLetter(c) || char.IsDigit(c)).ToArray());
                        checkvalidatemobile = Regex.IsMatch(oustput, MatchPhoneNumberPattern);
                    }
                    if (!string.IsNullOrEmpty(inqueries.Email))
                    {
                        string MatchEmailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                        string outputEmail = inqueries.Email;
                        checkvalidateEmail = Regex.IsMatch(outputEmail, MatchEmailPattern);
                    }

                    if ((!string.IsNullOrEmpty(message) && messageTypeId.Any(x => x == (int)MessageTypeEnum.SMS) && checkvalidatemobile && !string.IsNullOrEmpty(inqueries.MobileNo)) || (!string.IsNullOrEmpty(emailContent) && !string.IsNullOrEmpty(subject) && messageTypeId.Any(x => x == (int)MessageTypeEnum.Email) && !string.IsNullOrEmpty(inqueries.Email) && checkvalidateEmail))
                    {
                        if (messageTypeId != null)
                        {
                            var IsSuccess = blInqueries.ReplyInqueries(inqueriesID, message, emailContent, subject, messageTypeId.Any(x => x == (int)MessageTypeEnum.Email), messageTypeId.Any(x => x == (int)MessageTypeEnum.SMS), User.GetUserIdInt());
                            if (IsSuccess)
                            {
                                if (messageTypeId.Any(x => x == (int)MessageTypeEnum.SMS))
                                {

                                    if (!string.IsNullOrEmpty(inqueries.MobileNo))
                                    {
                                        IsSendSMS = false;
                                        var SMSStatus = await _OTPService.SendMessageToUser(inqueries.MobileNo, message, (int)UserTypeEnum.Customer, (int)MessageTypeEnum.SMS, (int)MessageReasonEnum.ReplyInqueries);
                                        IsSendSMS = true;
                                    }
                                    result = true;
                                }
                                if (messageTypeId.Any(x => x == (int)MessageTypeEnum.Email))
                                {
                                    if (!string.IsNullOrEmpty(inqueries.Email))
                                    {

                                        IsSendEmail = false;
                                        bLGeneral.SendEmail(inqueries.Email, subject, emailContent);
                                        IsSendEmail = true;
                                    }
                                    result = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (messageTypeId.Any(x => x == (int)MessageTypeEnum.SMS))
                        {
                            if (string.IsNullOrEmpty(message))
                            {
                                ResultMessage resultMessage = new ResultMessage { Message = Resources.Homemade.Please_Insert_Message, ResultType = ResultMessageType.error.ToString() };
                                return Json(resultMessage);
                            }
                            if (string.IsNullOrEmpty(inqueries.MobileNo))
                            {
                                ResultMessage resultMessage = new ResultMessage { Message = Resources.Homemade.Not_Found_Mobile, ResultType = ResultMessageType.error.ToString() };
                                return Json(resultMessage);
                            }
                            if (checkvalidatemobile)
                            {
                                ResultMessage resultMessage = new ResultMessage { Message = Resources.Homemade.Mobile_Not_Valid, ResultType = ResultMessageType.error.ToString() };
                                return Json(resultMessage);
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(subject))
                            {
                                ResultMessage resultMessage = new ResultMessage { Message = Resources.Homemade.Please_Insert_Subject, ResultType = ResultMessageType.error.ToString() };
                                return Json(resultMessage);
                            }
                            if (string.IsNullOrEmpty(emailContent))
                            {
                                ResultMessage resultMessage = new ResultMessage { Message = Resources.Homemade.Please_Insert_Email_Content, ResultType = ResultMessageType.error.ToString() };
                                return Json(resultMessage);
                            }
                            if (string.IsNullOrEmpty(inqueries.Email))
                            {
                                ResultMessage resultMessage = new ResultMessage { Message = Resources.Homemade.Not_Found_Email, ResultType = ResultMessageType.error.ToString() };
                                return Json(resultMessage);
                            }
                            if (checkvalidateEmail)
                            {
                                ResultMessage resultMessage = new ResultMessage { Message = Resources.Homemade.Email_Not_Valid, ResultType = ResultMessageType.error.ToString() };
                                return Json(resultMessage);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            ResultMessage rResultMessage = result ?
              new ResultMessage { Message = Resources.Homemade.SuccessSendMessage, ResultType = ResultMessageType.success.ToString() }
              :
              (!IsSendEmail ? new ResultMessage { Message = Resources.Homemade.FailSendEmail, ResultType = ResultMessageType.error.ToString() } :
              !IsSendSMS ? new ResultMessage { Message = Resources.Homemade.FailSendSMS, ResultType = ResultMessageType.error.ToString() } :
              new ResultMessage { Message = Resources.Homemade.FailSendMessage, ResultType = ResultMessageType.error.ToString() });
            return Json(rResultMessage);
        }
        #endregion

    }
}
