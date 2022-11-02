using Homemade.BLL;
using Homemade.BLL.General;
using Homemade.BLL.Setting;
using Homemade.BLL.Vendor;
using Homemade.BLL.ViewModel.Vendor;
using Homemade.UI.Attributes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Homemade.UI.Areas.Vendor.Controllers
{
    public class InvoiceController : Controller
    {
        #region Declerations
        private readonly BlVendor _BlVendor;
        private readonly IConfiguration _configuration;
        private readonly ResultMessage result;
        private readonly BlInvoice _blInvoice;
        private readonly BlDiscount _blDiscount;
        private readonly BLGeneral _bLGeneral;
        private readonly BLPermission _bLPermission;
        private readonly BlBanks _blBank;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly BlListTransfer _blListTransfer;
        private readonly BlConfiguration _blConfiguration;
        public InvoiceController(BlVendor BlVendor, IConfiguration configuration,
            ResultMessage result, BlInvoice blInvoice, BlDiscount blDiscount, BLGeneral bLGeneral,
            BLPermission bLPermission, BlBanks blBank, IWebHostEnvironment hostingEnvironment, BlListTransfer blListTransfer, BlConfiguration blConfiguration)
        {
            _BlVendor = BlVendor;
            this.result = result;
            _configuration = configuration;
            _blInvoice = blInvoice;
            _blDiscount = blDiscount;
            _bLGeneral = bLGeneral;
            _bLPermission = bLPermission;
            _blBank = blBank;
            _hostingEnvironment = hostingEnvironment;
            _blListTransfer = blListTransfer;
            _blConfiguration = blConfiguration;
        }
        #endregion
        #region View_Operation
        [CustomeAuthoriz((int)ControllerEnum.Invoice, (int)ActionEnum.Insert)]
        public IActionResult GenerateInvoice()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.Invoice, (int)ActionEnum.Insert)]
        public IActionResult PaymentReports()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.Invoice, (int)ActionEnum.Insert)]
        public IActionResult GenerateInvoiceBulk()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.Invoice, (int)ActionEnum.View)]
        public IActionResult InvoiceReport()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.Invoice, (int)ActionEnum.View)]
        public IActionResult PendingAction()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.Invoice, (int)ActionEnum.View)]
        public IActionResult Details(Guid Id)
        {
            if (Guid.Empty == Id)
                return RedirectToAction(nameof(InvoiceReport));
            var Invoice = _blInvoice.GetInvoiceMasterByGuid(Id);
            if (Invoice == null)
                return RedirectToAction(nameof(InvoiceReport));
            var invoiceMasterViewModel = _blInvoice.GetInvoiceMasterViewModelById(Invoice.InvoiceMasterID, InfraStructure.Utility.CurrentLanguageCode);
            if (invoiceMasterViewModel == null)
                return RedirectToAction(nameof(InvoiceReport));
            return View(invoiceMasterViewModel);
        }
        [CustomeAuthoriz((int)ControllerEnum.Invoice, (int)ActionEnum.View)]
        public IActionResult PendingActionDetails(Guid Id)
        {
            if (Guid.Empty == Id)
                return RedirectToAction(nameof(PendingAction));
            var Invoice = _blInvoice.GetInvoiceMasterByGuid(Id);
            if (Invoice == null)
                return RedirectToAction(nameof(PendingAction));
            var invoiceMasterViewModel = _blInvoice.GetInvoiceMasterViewModelById(Invoice.InvoiceMasterID, InfraStructure.Utility.CurrentLanguageCode);
            if (invoiceMasterViewModel == null)
                return RedirectToAction(nameof(PendingAction));
            return View(invoiceMasterViewModel);
        }
        [CustomeAuthoriz((int)ControllerEnum.Invoice, (int)ActionEnum.View)]
        public IActionResult CollectInvoice()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.Invoice, (int)ActionEnum.View)]
        public IActionResult ListCollected()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.Invoice, (int)ActionEnum.View)]
        public IActionResult ListCollectedDetails(Guid Id)
        {
            if (Guid.Empty == Id)
                return RedirectToAction(nameof(ListCollected));
            var listTransfer = _blListTransfer.GetListTransferByGuid(Id);
            if (listTransfer == null)
                return RedirectToAction(nameof(ListCollected));
            var listTransferViewModel = _blListTransfer.GetListTransferViewModelById(listTransfer.ListTransferID, InfraStructure.Utility.CurrentLanguageCode, _configuration["InvoiceImageView"]);
            if (listTransferViewModel == null)
                return RedirectToAction(nameof(ListCollected));
            return View(listTransferViewModel);
        }
        #endregion
        #region Store_Views
        [CustomeAuthoriz((int)ControllerEnum.Client_Invoice, (int)ActionEnum.View)]
        public IActionResult StoreInvoices()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.Client_Invoice, (int)ActionEnum.View)]
        public IActionResult ReportPayment()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.Client_Invoice, (int)ActionEnum.View)]
        public IActionResult StorePending()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.Client_Invoice, (int)ActionEnum.View)]
        public IActionResult StoreCollected()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.Client_Invoice, (int)ActionEnum.View)]
        public IActionResult StoreDetails(Guid Id)
        {
            if (Guid.Empty == Id)
                return RedirectToAction(nameof(InvoiceReport));
            var Invoice = _blInvoice.GetInvoiceMasterByGuid(Id);
            if (Invoice == null)
                return RedirectToAction(nameof(InvoiceReport));
            var invoiceMasterViewModel = _blInvoice.GetInvoiceMasterViewModelById(Invoice.InvoiceMasterID, InfraStructure.Utility.CurrentLanguageCode);
            if (invoiceMasterViewModel == null)
                return RedirectToAction(nameof(InvoiceReport));
            return View(invoiceMasterViewModel);
        }
        [CustomeAuthoriz((int)ControllerEnum.Client_Invoice, (int)ActionEnum.View)]
        public IActionResult StoreActionDetails(Guid Id)
        {
            if (Guid.Empty == Id)
                return RedirectToAction(nameof(PendingAction));
            var Invoice = _blInvoice.GetInvoiceMasterByGuid(Id);
            if (Invoice == null)
                return RedirectToAction(nameof(PendingAction));
            var invoiceMasterViewModel = _blInvoice.GetInvoiceMasterViewModelById(Invoice.InvoiceMasterID, InfraStructure.Utility.CurrentLanguageCode);
            if (invoiceMasterViewModel == null)
                return RedirectToAction(nameof(PendingAction));
            return View(invoiceMasterViewModel);
        }
        [CustomeAuthoriz((int)ControllerEnum.Client_Invoice, (int)ActionEnum.View)]
        public IActionResult StoreCollectedDetails(Guid Id)
        {
            if (Guid.Empty == Id)
                return RedirectToAction(nameof(ListCollected));
            var listTransfer = _blListTransfer.GetListTransferByGuid(Id);
            if (listTransfer == null)
                return RedirectToAction(nameof(ListCollected));
            var listTransferViewModel = _blListTransfer.GetListTransferViewModelById(listTransfer.ListTransferID, InfraStructure.Utility.CurrentLanguageCode, _configuration["InvoiceImageView"]);
            if (listTransferViewModel == null)
                return RedirectToAction(nameof(ListCollected));
            return View(listTransferViewModel);
        }
        [HttpPost]
        public JsonResult StoreInvoiceMasterForInvoiceReport(DateTime? fromDate, DateTime? toDate, int? typeId, int? StatusId)
        {
            try
            {
                #region DataTableParameters
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
                #endregion
                var user = _BlVendor.GetVendorByUserId(User.GetUserIdInt());
                var listClientID = user.VendorsID.ToString() + ",";
                string[] client = null;
                if (!string.IsNullOrEmpty(listClientID) && listClientID != "null" && listClientID != "undefined")
                {
                    client = listClientID.Split(",");
                }
                var data = new List<InvoiceMasterViewModel>();
                data = _blInvoice.GetAllInvoiceMasterViewModelForInvoiceReport(client, fromDate, toDate, InfraStructure.Utility.CurrentLanguageCode, StatusId).ToList();
                if (Request.Form.ContainsKey("sSearch"))
                {
                    string search = Request.Form["sSearch"];
                    if (!string.IsNullOrEmpty(search))
                    {
                        data = data.Where(x =>
                            x.VendorName.Contains(search) || x.DebitAmount.ToString().Contains(search) || x.Discount.ToString().Contains(search) ||
                            x.CreateDate.Contains(search)).ToList();
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
                    aaData = takenData,
                });
            }
            catch (Exception ex)
            {
                string path = Directory.GetCurrentDirectory() + "\\wwwroot\\Log.txt";
                string text = " \n" +
           "Message = " + ex.Message + " \n, InnerException = " + (ex.InnerException != null ? ex.InnerException.Message : "No InnerException ..!") + "";
                string[] lines = { string.Concat(_bLGeneral.GetDateTimeNow().ToString(), " : ", text) };

                System.IO.File.AppendAllLines(path, lines);

                return Json(new { Message = ex.Message, InnerException = (ex.InnerException != null ? ex.InnerException.Message : "No InnerException ..!") });

            }


        }
        [HttpPost]
        public JsonResult StoreInvoiceMasterForPendingAction(DateTime? fromDate, DateTime? toDate)
        {
            #region DataTableParameters
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
            #endregion
            var user = _BlVendor.GetVendorByUserId(User.GetUserIdInt());
            var data = new List<InvoiceMasterViewModel>();
            data = _blInvoice.GetAllInvoiceMasterViewModelForPendingAction(user.VendorsID, fromDate, toDate, InfraStructure.Utility.CurrentLanguageCode).ToList();
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                        x.VendorName.Contains(search) || x.DebitAmount.ToString().Contains(search) || x.Discount.ToString().Contains(search) ||
                        x.CreateDate.Contains(search)).ToList();
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
                aaData = takenData,
            });

        }
        [HttpPost]
        public JsonResult LoadTableTripPaymentVendor(DateTime? fromDate, DateTime? toDate)
        {
            #region DataTableParameters
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
            #endregion
            var user = _BlVendor.GetVendorByUserId(User.GetUserIdInt());
            var listClientID = user.VendorsID.ToString() + ",";
            string[] client = null;
            if (!string.IsNullOrEmpty(listClientID) && listClientID != "null" && listClientID != "undefined")
            {
                client = listClientID.Split(',');
            }
            var data = new List<OrderVendorViewModelInvoice>();
            data = _BlVendor.GetAllTripHistoryQueryResultViewModelForPayment(client, fromDate, toDate, InfraStructure.Utility.CurrentLanguageCode).ToList();
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                        x.VendorName.Contains(search) || x.OrderVendorID.ToString().Contains(search) || x.Total.ToString().Contains(search) ||
                        x.CreateDate.Contains(search) ).ToList();
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
                aaData = takenData,
            });

        }
        [HttpPost]
        public JsonResult StoreListCollected(DateTime? fromDate, DateTime? toDate)
        {
            #region DataTableParameters
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
            #endregion
            var user = _BlVendor.GetVendorByUserId(User.GetUserIdInt());
            var listClientID = user.VendorsID.ToString() + ",";
            string[] client = null;
            if (!string.IsNullOrEmpty(listClientID) && listClientID != "null" && listClientID != "undefined")
            {
                client = listClientID.Split(',');
            }
            var data = new List<ListTransferViewModel>();
            data = _blListTransfer.GetAllListTransferViewModelForListCollected(client, fromDate, toDate, InfraStructure.Utility.CurrentLanguageCode, _configuration["InvoiceImageView"]).ToList();
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                        x.VendorName.Contains(search) || x.BankName.Contains(search) || x.Total.ToString().Contains(search) ||
                        x.CreateDate.Contains(search) || x.ReferenceNo.Contains(search)).ToList();
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
                aaData = takenData,
            });

        }
        #endregion
        #region Action
        [HttpPost]
        public JsonResult GetDiscount()
        {
            decimal discount = _blDiscount.GetFirstDiscount() != null ? _blDiscount.GetFirstDiscount().DiscountValue : 0;
            return Json(discount);
        }
        [HttpPost]
        public JsonResult Generate(int clientID, DateTime? fromDate, DateTime? toDate, decimal? discount)
        {
            try
            {
                decimal vat = _blConfiguration.GetDeliveryPriceVatPercent();
                int UserId = User.GetUserIdInt();
                var data = _BlVendor.GetAllTripHistoryQueryResultViewModelForInvoiceGenerate(clientID, fromDate, toDate,
                        InfraStructure.Utility.CurrentLanguageCode).Select(m => m.OrderVendorID).ToList();
                TripItemsProc items = new TripItemsProc();
                decimal total = 0;
                decimal Amount = 0;
                if (data.Count() > 0)
                {
                    foreach (var OrderVendorID in data)
                    {
                        items.Add(new InvoiceForProc()
                        {
                            OrderVendorID = OrderVendorID,
                        });
                        Amount = Amount + _BlVendor.GetAmountOrder(OrderVendorID);
                    }
                    vat = vat > 0 ? (total * vat) / 100 : 0;
                    total = (Amount - (decimal)discount) + vat;
                    string connectionString = _configuration.GetConnectionString("HomemadeDBConnection");
                    var SaveStored = _blInvoice.GenerateInvoice(items, UserId, clientID, (decimal)discount, vat,
                         "GenerateInvoice", connectionString, total, Amount);
                    if (SaveStored.flag == "100")
                    {
                        result.Status = true;
                        result.Message = Resources.Homemade.SuccessSaveMessage;
                        result.ResultType = ResultMessageType.success.ToString();
                    }
                    else
                    {
                        result.Status = false;
                        result.Message = Resources.Homemade.FailSaveMessage + "</br>" + SaveStored.ErrorMessage;
                        result.ResultType = ResultMessageType.error.ToString();
                    }
                }
                else
                {
                    result.Status = false;
                    result.Message = Resources.Homemade.FailSaveMessage;
                    result.ResultType = ResultMessageType.error.ToString();
                }
                return Json(result);
            }
            catch (Exception e)
            {

                result.Message = Resources.Homemade.FailSaveMessage;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
        }
        [HttpPost]
        public JsonResult GenerateBulk(string listVendorID, DateTime? fromDate, DateTime? toDate, decimal? discount)
        {
            try
            {
                string[] vendor = null;
                if (!string.IsNullOrEmpty(listVendorID) && listVendorID != "null")
                {
                    vendor = listVendorID.Split(',');
                }
                decimal vat = _blConfiguration.GetDeliveryPriceVatPercent();
                int UserId = User.GetUserIdInt();
                var data = _BlVendor.GetAllTripHistoryQueryResultViewModelForInvoiceGenerateBulk(vendor, fromDate, toDate,
                        InfraStructure.Utility.CurrentLanguageCode).Select(m => m.OrderVendorID).ToList();
                var dataVendors = _BlVendor.GetAllTripHistoryQueryResultViewModelForInvoiceGenerateBulk(vendor, fromDate, toDate,
                       InfraStructure.Utility.CurrentLanguageCode).GroupBy(x => x.VendorsID).Select(m => m.Key).ToList();
                TripItemsProc items = new TripItemsProc();
                VendorItemsProc Vendoritems = new VendorItemsProc();
                if (data.Count() > 0)
                {
                    foreach (var OrderVendorID in data)
                    {
                        items.Add(new InvoiceForProc()
                        {
                            OrderVendorID = OrderVendorID,
                        });
                    }
                    foreach (var VendorsID in dataVendors)
                    {
                        Vendoritems.Add(new VendorForProc()
                        {
                            VendorsID = VendorsID,
                        });
                    }
                    string connectionString = _configuration.GetConnectionString("HomemadeDBConnection");
                    var SaveStored = _blInvoice.GenerateInvoiceBulk(items, Vendoritems, UserId, (decimal)discount, vat,
                         "GenerateInvoiceBulk", connectionString);
                    if (SaveStored.flag == "100")
                    {
                        result.Status = true;
                        result.Message = Resources.Homemade.SuccessSaveMessage;
                        result.ResultType = ResultMessageType.success.ToString();
                    }
                    else
                    {
                        result.Status = false;
                        result.Message = Resources.Homemade.FailSaveMessage + "</br>" + SaveStored.ErrorMessage;
                        result.ResultType = ResultMessageType.error.ToString();
                    }
                }
                else
                {
                    result.Status = false;
                    result.Message = Resources.Homemade.FailSaveMessage;
                    result.ResultType = ResultMessageType.error.ToString();
                }
                return Json(result);
            }
            catch (Exception e)
            {

                result.Message = Resources.Homemade.FailSaveMessage;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
        }
        [HttpPost]
        public JsonResult ChangeInvoiceType(int invoiceMasterID, int invoiceTypeId)
        {
            try
            {
                if ((_bLPermission.GetPermissionByUserAndController(User.Identity.Name, (int)ControllerEnum.Invoice,
                        (int)ActionEnum.Update) && invoiceTypeId == (int)InvoiceTypeEnum.Pending_Admin)
                    || (_bLPermission.GetPermissionByUserAndController(User.Identity.Name, (int)ControllerEnum.Invoice,
                        (int)ActionEnum.Delete) && invoiceTypeId == (int)InvoiceTypeEnum.Pending_Paid))
                {

                    if (_blInvoice.ChangeInvoiceType(invoiceMasterID, User.GetUserIdInt(), invoiceTypeId))
                    {
                        result.Message = Resources.Homemade.SuccessChangeMessage;
                        result.Status = true;
                        result.ResultType = ResultMessageType.success.ToString();
                        return Json(result);
                    }
                    result.Message = Resources.Homemade.FailChangeStatueMessage;
                    result.Status = false;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
                result.Message = Resources.Homemade.No_Permissions;
                result.Status = false;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);

            }
            catch (Exception ex)
            {

                result.Message = ex.Message;
                result.Status = false;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
        }
        [HttpPost]
        [Obsolete]
        public JsonResult PaidInvoiceMaster(int InvoiceMasterID, int? BankID, string ReferenceNo, string TransferDate, string Image)
        {
            try
            {
                DateTime TransDate = _bLGeneral.GetDateTimeNow();
                if (!string.IsNullOrEmpty(TransferDate))
                {
                    TransDate = Convert.ToDateTime(TransferDate);
                }
                var Attachment = string.Empty;
                if (!string.IsNullOrEmpty(Image))
                {
                    string FileName = Guid.NewGuid() + ".png";
                    Attachment = FileName;
                    string MainPath = _configuration["InvoiceImageSave"];
                    _bLGeneral.SaveImage(new BLGeneral.SaveImageModel
                    {
                        base64 = Image,
                        key = "",
                        fileName = FileName,
                        mainPath = MainPath
                    });
                }
                if (_blInvoice.CollectInvoice(InvoiceMasterID, User.GetUserIdInt(), (int)InvoiceTypeEnum.Paid, BankID, Attachment, ReferenceNo, TransDate))
                {
                    result.Message = Resources.Homemade.SuccessChangeMessage;
                    result.Status = true;
                    result.ResultType = ResultMessageType.success.ToString();
                    return Json(result);
                }
                result.Message = Resources.Homemade.FailChangeStatueMessage;
                result.Status = false;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Status = false;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
        }

        #endregion
        #region Helper
        [HttpPost]
        public JsonResult LoadTableTripForInvoiceGenerate(int? ClientID, DateTime? fromDate, DateTime? toDate)
        {
            #region DataTableParameters
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
            var data = new List<OrderVendorViewModelInvoice>();
            #endregion
            if (ClientID.HasValue)
            {
                data = _BlVendor.GetAllTripHistoryQueryResultViewModelForInvoiceGenerate(ClientID, fromDate, toDate, InfraStructure.Utility.CurrentLanguageCode);
            }
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                    x.VendorName.Contains(search) || x.OrderStatusName.Contains(search) || x.CustomerMobileNo.Contains(search) ||
                    x.CustomerName.Contains(search) || x.DriverName.Contains(search)).ToList();
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
                aaData = takenData,
            });

        }
        [HttpPost]
        public JsonResult LoadTableTripForInvoiceGenerateBulk(string listVendorID, DateTime? fromDate, DateTime? toDate)
        {
            #region DataTableParameters
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
            #endregion
            string[] vendor = null;
            if (!string.IsNullOrEmpty(listVendorID) && listVendorID != "null")
            {
                vendor = listVendorID.Split(',');
            }
            var data = _BlVendor.GetAllTripHistoryQueryResultViewModelForInvoiceGenerateBulk(vendor, fromDate, toDate, InfraStructure.Utility.CurrentLanguageCode);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                    x.VendorName.Contains(search) || x.OrderStatusName.Contains(search) || x.CustomerMobileNo.Contains(search) ||
                    x.CustomerName.Contains(search) || x.DriverName.Contains(search)).ToList();
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
                aaData = takenData,
            });

        }
        [HttpPost]
        public JsonResult LoadTableTripPayment(string listVendorID, DateTime? fromDate, DateTime? toDate)
        {
            #region DataTableParameters
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
            #endregion
            string[] vendor = null;
            if (!string.IsNullOrEmpty(listVendorID) && listVendorID != "null")
            {
                vendor = listVendorID.Split(',');
            }
            var data = _BlVendor.GetAllTripHistoryQueryResultViewModelForPayment(vendor, fromDate, toDate, InfraStructure.Utility.CurrentLanguageCode);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                    x.VendorName.Contains(search) || x.OrderStatusName.Contains(search) || x.CustomerMobileNo.Contains(search) ||
                    x.CustomerName.Contains(search) || x.DriverName.Contains(search)).ToList();
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
                aaData = takenData,
            });

        }
        [HttpPost]
        public JsonResult LoadTableInvoiceMasterForInvoiceReport(string listClientID, DateTime? fromDate, DateTime? toDate, int? typeId, int? StatusId)
        {
            try
            {
                #region DataTableParameters
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
                #endregion
                string[] client = null;
                if (!string.IsNullOrEmpty(listClientID) && listClientID != "null" && listClientID != "undefined")
                {
                    client = listClientID.Split(',');
                }
                var data = new List<InvoiceMasterViewModel>();
                data = _blInvoice.GetAllInvoiceMasterViewModelForInvoiceReport(client, fromDate, toDate, InfraStructure.Utility.CurrentLanguageCode, StatusId).ToList();
                if (Request.Form.ContainsKey("sSearch"))
                {
                    string search = Request.Form["sSearch"];
                    if (!string.IsNullOrEmpty(search))
                    {
                        data = data.Where(x =>
                            x.VendorName.Contains(search) || x.DebitAmount.ToString().Contains(search) || x.Discount.ToString().Contains(search) ||
                            x.CreateDate.Contains(search)).ToList();
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
                    aaData = takenData,
                });
            }
            catch (Exception ex)
            {
                string path = Directory.GetCurrentDirectory() + "\\wwwroot\\Log.txt";
                string text = " \n" +
           "Message = " + ex.Message + " \n, InnerException = " + (ex.InnerException != null ? ex.InnerException.Message : "No InnerException ..!") + "";
                string[] lines = { string.Concat(_bLGeneral.GetDateTimeNow().ToString(), " : ", text) };

                System.IO.File.AppendAllLines(path, lines);

                return Json(new { Message = ex.Message, InnerException = (ex.InnerException != null ? ex.InnerException.Message : "No InnerException ..!") });

            }


        }
        [HttpPost]
        public JsonResult LoadTableInvoiceDetails(int? InvoiceMasterID)
        {
            #region DataTableParameters
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
            #endregion
            var data = _blInvoice.GetAllInvoiceDeatilsViewModelByInvoiceMaster(InvoiceMasterID, InfraStructure.Utility.CurrentLanguageCode);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                        x.CustomerMobile.Contains(search) || x.VendorName.Contains(search) || x.CustomerName.Contains(search) ||
                        x.DebitAmount.ToString().Contains(search) || x.CreateDate.Contains(search));
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
                aaData = takenData,
            });

        }
        [HttpPost]
        public JsonResult LoadTablePendingActionDetails(int? InvoiceMasterID)
        {
            #region DataTableParameters
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
            #endregion
            var data = _blInvoice.GetAllInvoiceDeatilsViewModelByInvoiceMaster(InvoiceMasterID, InfraStructure.Utility.CurrentLanguageCode);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                        x.CustomerName.Contains(search) || x.VendorName.Contains(search) || x.CustomerMobile.Contains(search) ||
                        x.DebitAmount.ToString().Contains(search) || x.CreateDate.Contains(search));
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
                aaData = takenData,
            });

        }
        [HttpPost]
        public JsonResult LoadTableInvoiceMasterForPendingAction(string listClientID, DateTime? fromDate, DateTime? toDate)
        {
            #region DataTableParameters
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
            #endregion
            string[] client = null;
            if (!string.IsNullOrEmpty(listClientID) && listClientID != "null" && listClientID != "undefined")
            {
                client = listClientID.Split(',');
            }
            var data = new List<InvoiceMasterViewModel>();
            data = _blInvoice.GetAllInvoiceMasterViewModelForPendingAction(client, fromDate, toDate, InfraStructure.Utility.CurrentLanguageCode).ToList();
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                        x.VendorName.Contains(search) || x.DebitAmount.ToString().Contains(search) || x.Discount.ToString().Contains(search) ||
                        x.CreateDate.Contains(search)).ToList();
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
                aaData = takenData,
            });

        }
        [HttpPost]
        public JsonResult LoadTableInvoiceMasterForCollectInvoice(string listClientID, DateTime? fromDate, DateTime? toDate)
        {
            #region DataTableParameters
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
            #endregion
            string[] client = null;
            if (!string.IsNullOrEmpty(listClientID) && listClientID != "null" && listClientID != "undefined")
            {
                client = listClientID.Split(',');
            }
            var data = new List<InvoiceMasterViewModel>();
            data = _blInvoice.GetAllInvoiceMasterViewModelForCollectInvoice(client, fromDate, toDate, InfraStructure.Utility.CurrentLanguageCode).ToList();
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                        x.VendorName.Contains(search) || x.DebitAmount.ToString().Contains(search) || x.Discount.ToString().Contains(search) ||
                        x.CreateDate.Contains(search) || x.InvoiceTypeName.Contains(search)).ToList();
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
                aaData = takenData,
            });

        }
        [HttpPost]
        public JsonResult LoadTableListCollected(string listClientID, DateTime? fromDate, DateTime? toDate)
        {
            #region DataTableParameters
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
            #endregion
            string[] client = null;
            if (!string.IsNullOrEmpty(listClientID) && listClientID != "null" && listClientID != "undefined")
            {
                client = listClientID.Split(',');
            }
            var data = new List<ListTransferViewModel>();
            data = _blListTransfer.GetAllListTransferViewModelForListCollected(client, fromDate, toDate, InfraStructure.Utility.CurrentLanguageCode, _configuration["InvoiceImageView"]).ToList();
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                        x.VendorName.Contains(search) || x.BankName.Contains(search) || x.Total.ToString().Contains(search) ||
                        x.CreateDate.Contains(search) || x.ReferenceNo.Contains(search)).ToList();
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
                aaData = takenData,
            });

        }
        public JsonResult GetBanks()
        {
            return Json(_blBank.GetBanks(InfraStructure.Utility.CurrentLanguageCode));
        }
        #endregion
        #region Print
        public ActionResult SearchInvoiceDetailsReport(int? InvoiceMasterID, int type)
        {
            TempData["SearchInvoiceDetailsReportByMaster"] = Newtonsoft.Json.JsonConvert.SerializeObject(InvoiceMasterID);
            if (type == 2)
            {
                return RedirectToAction("ExcelInvoiceDetailsReport");
            }
            else
            {
                return RedirectToAction("PrintInvoiceDetailsReport");
            }
        }
        public ActionResult PrintInvoiceDetailsReport()
        {
            int InvoiceMasterID = 0;
            object o;
            TempData.TryGetValue("SearchInvoiceDetailsReportByMaster", out o);
            if (o != null)
            {
                InvoiceMasterID = JsonSerializer.Deserialize<int>(o.ToString());
            }
            var data = _blInvoice.GetAllInvoiceDeatilsViewModelByInvoiceMaster(InvoiceMasterID, InfraStructure.Utility.CurrentLanguageCode);
            return View(data);
        }
        #endregion
        #region Excel
        #region Basic Excel
        public static byte[] ExportExcel(DataTable dataTable, string heading = "", bool showSrNo = false, params string[] columnsToTake)
        {
            byte[] result = null;
            using (ExcelPackage package = new ExcelPackage())
            {

                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(String.Format("{0} Data", heading));
                int startRowFrom = String.IsNullOrEmpty(heading) ? 1 : 3;

                if (showSrNo)
                {
                    DataColumn dataColumn = dataTable.Columns.Add("SR No", typeof(int));
                    dataColumn.SetOrdinal(0);
                    int index = 1;
                    foreach (DataRow item in dataTable.Rows)
                    {
                        item[0] = index;
                        index++;
                    }
                }

                workSheet.Cells["A" + startRowFrom].LoadFromDataTable(dataTable, true);
                int columnIndex = 1;
                foreach (DataColumn column in dataTable.Columns)
                {
                    ExcelRange columnCells = workSheet.Cells[workSheet.Dimension.Start.Row, columnIndex, workSheet.Dimension.End.Row, columnIndex];

                    if (!string.IsNullOrWhiteSpace(columnCells.Max(cell => cell.Value.ToString())))
                    {
                        int maxLength = columnCells.Max(cell => cell.Value.ToString().Count());
                        if (maxLength < 150)
                        {
                            workSheet.Column(columnIndex).AutoFit();

                        }
                    }

                    columnIndex++;
                }
                // format header - bold, yellow on black  
                using (ExcelRange r = workSheet.Cells[startRowFrom, 1, startRowFrom, dataTable.Columns.Count])
                {
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.Font.Bold = true;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));
                }
                // format cells - add borders  
                using (ExcelRange r = workSheet.Cells[startRowFrom + 1, 1, startRowFrom + dataTable.Rows.Count, dataTable.Columns.Count])
                {
                    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                }

                if (!String.IsNullOrEmpty(heading))
                {
                    workSheet.Cells["A1"].Value = heading;
                    workSheet.Cells["A1"].Style.Font.Size = 20;

                    workSheet.InsertColumn(1, 1);
                    workSheet.InsertRow(1, 1);
                    workSheet.Column(1).Width = 5;
                }

                result = package.GetAsByteArray();
            }
            return result;
        }
        public static byte[] ExportExcel<T>(List<T> data, string Heading = "", bool showSlno = false, params string[] ColumnsToTake)
        {
            return ExportExcel(ListToDataTable<T>(data), Heading, showSlno, ColumnsToTake);
        }
        public static DataTable ListToDataTable<T>(List<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dataTable = new DataTable();

            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }

            object[] values = new object[properties.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = properties[i].GetValue(item);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
        public static string ExcelContentType
        {
            get
            { return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; }
        }
        #endregion
        public FileContentResult ExcelInvoiceDetailsReport()
        {
            byte[] filecontent = null;
            string downloadFileName = "";
            try
            {
                int InvoiceMasterID = 0;
                object o;
                TempData.TryGetValue("SearchInvoiceDetailsReportByMaster", out o);
                if (o != null)
                {
                    InvoiceMasterID = JsonSerializer.Deserialize<int>(o.ToString());
                }
                var data = _blInvoice.GetAllInvoiceDeatilsViewModelByInvoiceMaster(InvoiceMasterID, InfraStructure.Utility.CurrentLanguageCode);
                var Invoicemaster = _blInvoice.GetInvoiceMasterViewModelById(InvoiceMasterID, InfraStructure.Utility.CurrentLanguageCode);

                if (data != null)
                {
                    int totalCount = data.Count();
                    var listexcel = data.Select(x => new
                    {
                        OrderNo = x.OrderVendorID,
                        Customer = x.CustomerName,
                        SendDate = x.FinishDate,
                        Mobile = x.CustomerMobile,
                        Amount = x.DebitAmount
                    }).ToList();
                    string[] column = { "Order", "Customer", "Date", "Mobile", "Amount" };
                    filecontent = ExportExcel(listexcel, "Selected count : " + totalCount + " Invoice_No : " + Invoicemaster.InvoiceNo + " Total : " + Invoicemaster.Total
                        + " Vendor : " + Invoicemaster.VendorName + " Date : " + Invoicemaster.CreateDate, true, column);
                    downloadFileName = "OrderList_" + _bLGeneral.GetDateTimeNow() + ".xlsx";
                    return File(filecontent, ExcelContentType, downloadFileName);
                }
                else
                {
                    string[] columns = { "Order", "Customer", "Date", "Mobile", "Amount" };
                    filecontent = ExportExcel(data.ToList(), "Trip List ", true, columns);
                    downloadFileName = "OrderList_" + _bLGeneral.GetDateTimeNow() + ".xlsx";
                    return File(filecontent, ExcelContentType, downloadFileName);
                }
            }
            catch (Exception e)
            {
                List<InvoiceDetailsViewModel> list = new List<InvoiceDetailsViewModel>();
                list.Add(new InvoiceDetailsViewModel()
                {
                    OrderVendorID = 0,
                    VendorName = "---",
                    CreateDate = "---",
                    CustomerMobile = "---",
                    DebitAmount = 0,
                    CustomerName = "------"
                });

                string[] columns = { "Order", "Customer", "Send Date", "Mobile", "Debit Amount" };
                filecontent = ExportExcel(list, "Trip List ", true, columns);
                downloadFileName = "OrderList_" + _bLGeneral.GetDateTimeNow() + ".xlsx";
                return File(filecontent, ExcelContentType, downloadFileName);
            }
        }
        public FileContentResult ExcelTripForInvoiceGenerate(int? ClientID, DateTime? fromDate, DateTime? toDate)
        {

            byte[] filecontent = null;
            string downloadFileName = "";
            try
            {
                var data = new List<OrderVendorViewModelInvoice>();
                if (ClientID.HasValue)
                {
                    data = _BlVendor.GetAllTripHistoryQueryResultViewModelForInvoiceGenerate(ClientID, fromDate, toDate, InfraStructure.Utility.CurrentLanguageCode);
                }

                if (data != null)
                {
                    int totalCount = data.Count();
                    var listexcel = data.Select(x => new
                    {
                        OrderNo = x.OrderVendorID,
                        Status = x.OrderStatusName,
                        CreateDate = x.CreateDate,
                        Vendor = x.VendorName,
                        PerHome = x.PerHome,
                        DeliveryPrice = x.DeliveryPrice,
                        PerStore = x.PerStore,
                    }).ToList();
                    string[] column = { "Order", "Status", "Date", "Vendor", "PerHome", "DeliveryPrice", "PerStore" };
                    filecontent = ExportExcel(listexcel, "Selected count : " + totalCount, true, column);
                    downloadFileName = "OrderList_" + _bLGeneral.GetDateTimeNow() + ".xlsx";
                    return File(filecontent, ExcelContentType, downloadFileName);
                }
                else
                {
                    string[] columns = { "Order", "Status", "Date", "Vendor", "PerHome", "DeliveryPrice", "PerStore" };
                    filecontent = ExportExcel(data.ToList(), "Trip List ", true, columns);
                    downloadFileName = "OrderList_" + _bLGeneral.GetDateTimeNow() + ".xlsx";
                    return File(filecontent, ExcelContentType, downloadFileName);
                }
            }
            catch (Exception e)
            {
                List<OrderVendorViewModelInvoice> list = new List<OrderVendorViewModelInvoice>();
                list.Add(new OrderVendorViewModelInvoice()
                {
                    OrderVendorID = 0,
                    OrderStatusName = "",
                    CreateDate = "",
                    VendorName = "",
                    PerHome = 0,
                    DeliveryPrice = 0,
                    PerStore = 0,
                });

                string[] columns = { "Order", "Status", "Date", "Vendor", "PerHome", "DeliveryPrice", "PerStore" };
                filecontent = ExportExcel(list, "Trip List ", true, columns);
                downloadFileName = "OrderList_" + _bLGeneral.GetDateTimeNow() + ".xlsx";
                return File(filecontent, ExcelContentType, downloadFileName);
            }
        }
        public FileContentResult ExcelTripForInvoiceGenerateBulk(string listVendorID, DateTime? fromDate, DateTime? toDate)
        {

            byte[] filecontent = null;
            string downloadFileName = "";
            try
            {
                string[] vendor = null;
                if (!string.IsNullOrEmpty(listVendorID) && listVendorID != "null")
                {
                    vendor = listVendorID.Split(',');
                }
                var data = _BlVendor.GetAllTripHistoryQueryResultViewModelForInvoiceGenerateBulk(vendor, fromDate, toDate, InfraStructure.Utility.CurrentLanguageCode);

                if (data != null)
                {
                    int totalCount = data.Count();
                    var listexcel = data.Select(x => new
                    {
                        OrderNo = x.OrderVendorID,
                        Status = x.OrderStatusName,
                        CreateDate = x.CreateDate,
                        Vendor = x.VendorName,
                        PerHome = x.PerHome,
                        DeliveryPrice = x.DeliveryPrice,
                        PerStore = x.PerStore,
                    }).ToList();
                    string[] column = { "Order", "Status", "Date", "Vendor", "PerHome", "DeliveryPrice", "PerStore" };
                    filecontent = ExportExcel(listexcel, "Selected count : " + totalCount, true, column);
                    downloadFileName = "OrderList_" + _bLGeneral.GetDateTimeNow() + ".xlsx";
                    return File(filecontent, ExcelContentType, downloadFileName);
                }
                else
                {
                    string[] columns = { "Order", "Status", "Date", "Vendor", "PerHome", "DeliveryPrice", "PerStore" };
                    filecontent = ExportExcel(data.ToList(), "Trip List ", true, columns);
                    downloadFileName = "OrderList_" + _bLGeneral.GetDateTimeNow() + ".xlsx";
                    return File(filecontent, ExcelContentType, downloadFileName);
                }
            }
            catch (Exception e)
            {
                List<OrderVendorViewModelInvoice> list = new List<OrderVendorViewModelInvoice>();
                list.Add(new OrderVendorViewModelInvoice()
                {
                    OrderVendorID = 0,
                    OrderStatusName = "",
                    CreateDate = "",
                    VendorName = "",
                    PerHome = 0,
                    DeliveryPrice = 0,
                    PerStore = 0,
                });

                string[] columns = { "Order", "Status", "Date", "Vendor", "PerHome", "DeliveryPrice", "PerStore" };
                filecontent = ExportExcel(list, "Trip List ", true, columns);
                downloadFileName = "OrderList_" + _bLGeneral.GetDateTimeNow() + ".xlsx";
                return File(filecontent, ExcelContentType, downloadFileName);
            }
        }
        public FileContentResult ExcelTripForPayment(string listVendorID, DateTime? fromDate, DateTime? toDate)
        {

            byte[] filecontent = null;
            string downloadFileName = "";
            try
            {
                string[] vendor = null;
                if (!string.IsNullOrEmpty(listVendorID) && listVendorID != "null")
                {
                    vendor = listVendorID.Split(',');
                }
                var data = _BlVendor.GetAllTripHistoryQueryResultViewModelForPayment(vendor, fromDate, toDate, InfraStructure.Utility.CurrentLanguageCode);

                if (data != null)
                {
                    int totalCount = data.Count();
                    var listexcel = data.Select(x => new
                    {
                        OrderNo = x.OrderVendorID,
                        Status = x.OrderStatusName,
                        CreateDate = x.CreateDate,
                        Vendor = x.VendorName,
                        PerHome = x.PerHome,
                        DeliveryPrice = x.DeliveryPrice,
                        PerStore = x.PerStore,
                    }).ToList();
                    string[] column = { "Order", "Status", "Date", "Vendor", "PerHome", "DeliveryPrice", "PerStore" };
                    filecontent = ExportExcel(listexcel, "Selected count : " + totalCount, true, column);
                    downloadFileName = "OrderList_" + _bLGeneral.GetDateTimeNow() + ".xlsx";
                    return File(filecontent, ExcelContentType, downloadFileName);
                }
                else
                {
                    string[] columns = { "Order", "Status", "Date", "Vendor", "PerHome", "DeliveryPrice", "PerStore" };
                    filecontent = ExportExcel(data.ToList(), "Trip List ", true, columns);
                    downloadFileName = "OrderList_" + _bLGeneral.GetDateTimeNow() + ".xlsx";
                    return File(filecontent, ExcelContentType, downloadFileName);
                }
            }
            catch (Exception e)
            {
                List<OrderVendorViewModelInvoice> list = new List<OrderVendorViewModelInvoice>();
                list.Add(new OrderVendorViewModelInvoice()
                {
                    OrderVendorID = 0,
                    OrderStatusName = "",
                    CreateDate = "",
                    VendorName = "",
                    PerHome = 0,
                    DeliveryPrice = 0,
                    PerStore = 0,
                });

                string[] columns = { "Order", "Status", "Date", "Vendor", "PerHome", "DeliveryPrice", "PerStore" };
                filecontent = ExportExcel(list, "Trip List ", true, columns);
                downloadFileName = "OrderList_" + _bLGeneral.GetDateTimeNow() + ".xlsx";
                return File(filecontent, ExcelContentType, downloadFileName);
            }
        }
        public FileContentResult ExcelTripForPaymentVendor(DateTime? fromDate, DateTime? toDate)
        {

            byte[] filecontent = null;
            string downloadFileName = "";
            try
            {
                var user = _BlVendor.GetVendorByUserId(User.GetUserIdInt());
                var listClientID = user.VendorsID.ToString() + ",";
                string[] client = null;
                if (!string.IsNullOrEmpty(listClientID) && listClientID != "null" && listClientID != "undefined")
                {
                    client = listClientID.Split(',');
                }
                var data = _BlVendor.GetAllTripHistoryQueryResultViewModelForPayment(client, fromDate, toDate, InfraStructure.Utility.CurrentLanguageCode);

                if (data != null)
                {
                    int totalCount = data.Count();
                    var listexcel = data.Select(x => new
                    {
                        OrderNo = x.OrderVendorID,
                        Status = x.OrderStatusName,
                        CreateDate = x.CreateDate,
                        Vendor = x.VendorName,
                        PerHome = x.PerHome,
                        DeliveryPrice = x.DeliveryPrice,
                        PerStore = x.PerStore,
                    }).ToList();
                    string[] column = { "Order", "Status", "Date", "Vendor", "PerHome", "DeliveryPrice", "PerStore" };
                    filecontent = ExportExcel(listexcel, "Selected count : " + totalCount, true, column);
                    downloadFileName = "OrderList_" + _bLGeneral.GetDateTimeNow() + ".xlsx";
                    return File(filecontent, ExcelContentType, downloadFileName);
                }
                else
                {
                    string[] columns = { "Order", "Status", "Date", "Vendor", "PerHome", "DeliveryPrice", "PerStore" };
                    filecontent = ExportExcel(data.ToList(), "Trip List ", true, columns);
                    downloadFileName = "OrderList_" + _bLGeneral.GetDateTimeNow() + ".xlsx";
                    return File(filecontent, ExcelContentType, downloadFileName);
                }
            }
            catch (Exception e)
            {
                List<OrderVendorViewModelInvoice> list = new List<OrderVendorViewModelInvoice>();
                list.Add(new OrderVendorViewModelInvoice()
                {
                    OrderVendorID = 0,
                    OrderStatusName = "",
                    CreateDate = "",
                    VendorName = "",
                    PerHome = 0,
                    DeliveryPrice = 0,
                    PerStore = 0,
                });

                string[] columns = { "Order", "Status", "Date", "Vendor", "PerHome", "DeliveryPrice", "PerStore" };
                filecontent = ExportExcel(list, "Trip List ", true, columns);
                downloadFileName = "OrderList_" + _bLGeneral.GetDateTimeNow() + ".xlsx";
                return File(filecontent, ExcelContentType, downloadFileName);
            }
        }

        #endregion
    }
}
