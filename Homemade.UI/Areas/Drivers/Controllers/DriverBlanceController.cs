using Homemade.BLL;
using Homemade.BLL.Driver;
using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Driver;
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
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.UI.Areas.Drivers.Controllers
{
    public class DriverBlanceController : Controller
    {
        #region Declarations
        private readonly BlDriverBalance _blDriverBlance;
        private readonly BlDriver _blDriver;
        private readonly BLGeneral _bLGeneral;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly BlTransactionType _blTransactionType;

        public DriverBlanceController(BlDriverBalance blDriverBlance, BlDriver blDriver, BLGeneral bLGeneral, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, BlTransactionType blTransactionType)
        {
            _blDriverBlance = blDriverBlance;
            _blDriver = blDriver;
            _bLGeneral = bLGeneral;
            this.webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _blTransactionType = blTransactionType;
        }
        #endregion
        #region Views
        /// <summary>
        /// دالة لعرض صفحة العرض
        /// </summary>
        /// <returns></returns>
        [CustomeAuthoriz((int)ControllerEnum.DriverBlance, (int)ActionEnum.View)]
        public IActionResult Index()
        {
            return View(_blDriverBlance.GetDriverBlanceViewModelForIndex());
        }
        [CustomeAuthoriz((int)ControllerEnum.DriverBlance, (int)ActionEnum.Insert)]
        public IActionResult PayToDrivers()
        {
            return View(_blDriverBlance.GetDriverBlanceViewModelForIndex());
        }
        [CustomeAuthoriz((int)ControllerEnum.DriverBlance, (int)ActionEnum.View)]
        public IActionResult PaymentHistory()
        {
            return View(_blDriverBlance.GetDriverBlanceViewModelForIndex());
        }
        [CustomeAuthoriz((int)ControllerEnum.DriverBlance, (int)ActionEnum.Insert)]
        public IActionResult InquirySTC()
        {
            return View(_blDriverBlance.GetDriverBlanceViewModelForIndex());
        }
        /// <summary>
        /// دالة لعرض صفحة الانشاء
        /// </summary>
        /// <returns></returns>
        [CustomeAuthoriz((int)ControllerEnum.DriverBlance, (int)ActionEnum.Insert)]
        public IActionResult Create()
        {
            return View(_blDriverBlance.GetDriverBlanceViewModelForCreate());
        }
        [CustomeAuthoriz((int)ControllerEnum.DriverBlance, (int)ActionEnum.View)]
        public IActionResult TranactionDetails(Guid? id)
        {
            if (id != null)
            {
                return View();
            }
            else
            {
                return LocalRedirect("/Financial/DriverBlance/Index");
            }
        }
        [CustomeAuthoriz((int)ControllerEnum.DriverBlance, (int)ActionEnum.Insert)]
        public IActionResult CreatedriverGuid(Guid? id)
        {
            if (id != null)
            {
                var DriverBlanceViewModel = new DriverBlanceViewModel();
                var DriverData = _blDriver.GetAllDriver().Where(e => e.DriverGuid == id).FirstOrDefault();
                DriverBlanceViewModel.DriverID = DriverData.DriversID;
                DriverBlanceViewModel.DriverName = InfraStructure.Utility.CurrentLanguageCode == "ar" ? DriverData.NameAr : DriverData.NameEn;
                return View(DriverBlanceViewModel);
            }
            else
            {
                return LocalRedirect("/Financial/DriverBlance/Index");
            }
        }
        /// <summary>
        /// Driver Total Balance
        /// </summary>
        /// <returns></returns>
        [CustomeAuthoriz((int)ControllerEnum.DriverBlance, (int)ActionEnum.View)]
        public IActionResult TotalBalance()
        {
            return View(_blDriverBlance.GetAllBalances());
        }
        #endregion
        #region Actions
        [CustomeAuthoriz((int)ControllerEnum.DriverBlance, (int)ActionEnum.Insert)]
        [HttpPost, ValidateAntiForgeryToken]
        [Obsolete]
        public JsonResult Create(DriverBlanceViewModel viewModel)
        {
            ResultMessage _Result = new ResultMessage();
            DateTime DateOperation = _bLGeneral.GetDateTimeNow();
            DateTime.TryParseExact(viewModel.DateOperationString, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateOperation);
            viewModel.DateOperation = DateOperation;
            ModelState.Remove("DateOperation");
            try
            {
                if (ModelState.IsValid)
                {
                    viewModel.UserId = User.GetUserIdInt();
                    int TypeOperation = 0;
                    if (viewModel.TransactionTypeID == (int)TransactionTypeEnum.Deposit || viewModel.TransactionTypeID == (int)TransactionTypeEnum.Bouns)
                    {
                        TypeOperation = (int)TypeOperationEnum.CR;
                    }
                    else
                    {
                        TypeOperation = (int)TypeOperationEnum.DR;
                    }
                    var Attachment = "";
                    if (!string.IsNullOrEmpty(viewModel.Attachment))
                    {
                        string FileName = Guid.NewGuid() + ".png";
                        Attachment = FileName;
                        string MainPath = _configuration["DriverBalanceImageSave"];
                        _bLGeneral.SaveImage(new BLGeneral.SaveImageModel
                        {
                            base64 = viewModel.Attachment,
                            key = "",
                            fileName = FileName,
                            mainPath = MainPath
                        });
                    }
                    var Dr = _blDriverBlance.InsertBalance(TypeOperation, viewModel.Amount, viewModel.TransactionTypeID, viewModel.DriverID, viewModel.UserId, null, viewModel.DateOperation, viewModel.Discripe, Attachment);

                    var transactionType = _blTransactionType.GetById(viewModel.TransactionTypeID);
                    string notificationDescEN = transactionType.NameEN + " Amount " + viewModel.Amount + " Dated " + _bLGeneral.GetDateTimeNow().ToString("dd/MM/yyyy hh:mm tt");
                    string notificationDescAR = "تم " + transactionType.NameAR + " مبلغ " + viewModel.Amount + " بتاريخ " + _bLGeneral.GetDateTimeNow().ToString("dd/MM/yyyy hh:mm tt");
                    _blDriverBlance.InsertNotification(viewModel.UserId, viewModel.DriverID, transactionType.NameEN, transactionType.NameAR, notificationDescEN, notificationDescAR,null, Dr.DriverBlanceID, (int)NotificationTypeEnum.Driver_Balance);
                    _Result.Status = true;
                    _Result.ResultType = ResultMessageType.success.ToString();
                    _Result.Message = Resources.Homemade.SuccessSaveMessage;
                    return Json(_Result);
                }
                else
                {
                    return Json(new ResultMessage
                    {
                        Status = false,
                        ResultType = ResultMessageType.error.ToString(),
                        Message = Resources.Homemade.InsertValidDataMessage
                    });
                }
            }
            catch (Exception e)
            {
                return Json(new ResultMessage
                {
                    Status = false,
                    ResultType = ResultMessageType.error.ToString(),
                    Message = e.Message
                });
            }
        }
        [CustomeAuthoriz((int)ControllerEnum.DriverBlance, (int)ActionEnum.Insert)]
        [HttpPost, ValidateAntiForgeryToken]
        [Obsolete]
        public JsonResult CreatedriverGuid(DriverBlanceViewModel viewModel)
        {
            ResultMessage _Result = new ResultMessage();
            DateTime DateOperation = _bLGeneral.GetDateTimeNow();
            DateTime.TryParseExact(viewModel.DateOperationString, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateOperation);
            viewModel.DateOperation = DateOperation;
            ModelState.Remove("DateOperation");
            try
            {
                if (ModelState.IsValid)
                {
                    viewModel.UserId = User.GetUserIdInt();
                    var Attachment = "";
                    if (!string.IsNullOrEmpty(viewModel.Attachment))
                    {
                        string FileName = Guid.NewGuid() + ".png";
                        Attachment = FileName;
                        string MainPath = _configuration["DriverImageSave"];
                        _bLGeneral.SaveImage(new BLGeneral.SaveImageModel
                        {
                            base64 = viewModel.Attachment,
                            key = "",
                            fileName = FileName,
                            mainPath = MainPath
                        });
                    }
                    var Dr = _blDriverBlance.InsertBalance((int)TypeOperationEnum.CR, viewModel.Amount,
                        (int)TransactionTypeEnum.Deposit, viewModel.DriverID, viewModel.UserId, null, viewModel.DateOperation, viewModel.Discripe, Attachment);

                    var transactionType = _blTransactionType.GetById((int)TransactionTypeEnum.Deposit);
                    string notificationDescEN = transactionType.NameEN + " Amount " + viewModel.Amount + " Dated " + _bLGeneral.GetDateTimeNow().ToString("dd/MM/yyyy hh:mm tt");
                    string notificationDescAR = "تم " + transactionType.NameAR + " مبلغ " + viewModel.Amount + " بتاريخ " + _bLGeneral.GetDateTimeNow().ToString("dd/MM/yyyy hh:mm tt");
                    _blDriverBlance.InsertNotification(viewModel.UserId, viewModel.DriverID, transactionType.NameEN, transactionType.NameAR, notificationDescEN, notificationDescAR, null, Dr.DriverBlanceID, (int)NotificationTypeEnum.Driver_Balance);
                    _Result.Status = true;
                    _Result.ResultType = ResultMessageType.success.ToString();
                    _Result.Message = Resources.Homemade.SuccessSaveMessage;
                    return Json(_Result);
                }
                else
                {
                    return Json(new ResultMessage
                    {
                        Status = false,
                        ResultType = ResultMessageType.error.ToString(),
                        Message = Resources.Homemade.InsertValidDataMessage
                    });
                }
            }
            catch (Exception e)
            {
                return Json(new ResultMessage
                {
                    Status = false,
                    ResultType = ResultMessageType.error.ToString(),
                    Message = e.Message
                });
            }
        }
        #endregion
        #region Helper
        [HttpPost]
        //عرض البيانات من جدول DriverBlance
        public JsonResult LoadTable(int? driverID, string taxiNumber, string driverMobile, string tripNo, DateTime? fromDate, DateTime? toDate,/* int? modelID, int? brandID,*/ int? isEnable, bool? isActive)
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
            if (string.IsNullOrEmpty(taxiNumber) || taxiNumber == "null")
            {
                taxiNumber = null;
            }
            if (string.IsNullOrEmpty(driverMobile) || driverMobile == "null")
            {
                driverMobile = null;
            }
            if (string.IsNullOrEmpty(tripNo) || tripNo == "null")
            {
                tripNo = null;
            }
            string fileView = _configuration["ClientBalanceImageView"];
            var data = _blDriverBlance.GetAllDriverBlanceViewModel(null, null, null, driverID, taxiNumber, fromDate, toDate
            , null, null, isEnable, isActive, InfraStructure.Utility.CurrentLanguageCode, fileView, driverMobile, tripNo);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x => x.TransactionTypeName.Contains(search) || x.DriverName.Contains(search) || x.After.ToString().Contains(search)
                    || x.Before.ToString().Contains(search) || x.Amount.ToString().Contains(search));
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
        [HttpPost]
        [Obsolete]
        public JsonResult PayDrivers(int? driverID, string taxiNumber, string driverMobile, string tripNo, DateTime? fromDate, DateTime? toDate,/* int? modelID, int? brandID,*/ int? isEnable, bool? isActive, decimal? lowest, int? type)
        {
            if (string.IsNullOrEmpty(taxiNumber) || taxiNumber == "null")
            {
                taxiNumber = null;
            }
            if (string.IsNullOrEmpty(driverMobile) || driverMobile == "null")
            {
                driverMobile = null;
            }
            if (string.IsNullOrEmpty(tripNo) || tripNo == "null")
            {
                tripNo = null;
            }
            if (lowest == null)
            {
                lowest = 0;
            }
            var data = _blDriverBlance.GetAllDriverBlanceViewModelPay(null, null, null, driverID, taxiNumber, fromDate, toDate, null, null, isEnable, isActive, InfraStructure.Utility.CurrentLanguageCode, "", driverMobile, tripNo, (decimal)lowest);
            var Responce = data.AsQueryable();
            var listdrivers = Responce.Where(x => x.VerifyStc == (int)VerifyStcEnum.Verified && !x.OpenTransaction).Select(s => s.DriverID).Distinct().ToList();
            var listmodel = new List<DriverWithTrips>();
            foreach (var item in listdrivers)
            {
                var model = new DriverWithTrips();
                model.driverId = item;
                model.trips = Responce.Where(x => x.DriverID == item).Select(x => (int)x.OrderVendorID).ToList();
                listmodel.Add(model);
            }
            if (type.HasValue)
            {
                var resule = _blDriverBlance.SendDiversToSTCPAY(_configuration["SecertToken"], listmodel, _configuration["LogiSTCLink"], User.GetUserIdInt(), (int)type);
                return Json(resule);
            }
            return Json("");
        }
        [HttpPost]
        [Obsolete]
        public JsonResult RePayDrivers(int? driverID, string taxiNumber, string driverMobile, string tripNo, DateTime? fromDate, DateTime? toDate,int? isEnable, bool? isActive, decimal? lowest, int? type)
        {
            if (string.IsNullOrEmpty(taxiNumber) || taxiNumber == "null")
            {
                taxiNumber = null;
            }
            if (string.IsNullOrEmpty(driverMobile) || driverMobile == "null")
            {
                driverMobile = null;
            }
            if (string.IsNullOrEmpty(tripNo) || tripNo == "null")
            {
                tripNo = null;
            }
            if (lowest == null)
            {
                lowest = 0;
            }
            var data = _blDriverBlance.GetAllDriverBlanceViewModelInquiry(null, null, null, driverID, taxiNumber, fromDate, toDate
            , null, null, isEnable, isActive, InfraStructure.Utility.CurrentLanguageCode, "", driverMobile, tripNo, (decimal)lowest);
            var Responce = data.AsQueryable();
            var listtranactions = Responce.Where(x => x.VerifyStc == (int)VerifyStcEnum.Verified && !x.OpenTransaction).Select(s => (int)s.OrderVendorID).Distinct().ToList();
            if (type.HasValue)
            {
                var resule = _blDriverBlance.ReSendDiversToSTCPAY(_configuration["SecertToken"], listtranactions, _configuration["STCRePayLink"], User.GetUserIdInt(), (int)type);
                return Json(resule);
            }
            return Json("");
        }


        [HttpPost]
        //عرض البيانات من جدول DriverBlance
        public JsonResult LoadTablePay(int? driverID, string taxiNumber, string driverMobile, string tripNo, DateTime? fromDate, DateTime? toDate,/* int? modelID, int? brandID,*/ int? isEnable, bool? isActive, decimal? lowest)
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
            if (string.IsNullOrEmpty(taxiNumber) || taxiNumber == "null")
            {
                taxiNumber = null;
            }
            if (string.IsNullOrEmpty(driverMobile) || driverMobile == "null")
            {
                driverMobile = null;
            }
            if (string.IsNullOrEmpty(tripNo) || tripNo == "null")
            {
                tripNo = null;
            }
            if (lowest == null)
            {
                lowest = 0;
            }
            string fileView = _configuration["ClientBalanceImageView"];
            var data = _blDriverBlance.GetAllDriverBlanceViewModelPay(null, null, null, driverID, taxiNumber, fromDate, toDate, null, null, isEnable, isActive, InfraStructure.Utility.CurrentLanguageCode, fileView, driverMobile, tripNo, (decimal)lowest);
            var Responce = data.AsQueryable();
            Responce = Responce.Skip(Convert.ToInt32(pageno - 1) * displayLength);
            var takenData = Responce.Take(((Convert.ToInt32(pageno) * displayLength)) - (Convert.ToInt32(pageno - 1) * displayLength));
            int totalCount = data.Count();
            return Json(new
            {
                iTotalRecords = totalCount,
                taxiNumber,
                iTotalDisplayRecords = totalCount,
                aaData = takenData
            });
        }
        [HttpPost]
        public JsonResult PayTotals(int? driverID, string taxiNumber, string driverMobile, string tripNo, DateTime? fromDate, DateTime? toDate,/* int? modelID, int? brandID,*/ int? isEnable, bool? isActive, decimal? lowest)
        {
            if (string.IsNullOrEmpty(taxiNumber) || taxiNumber == "null")
            {
                taxiNumber = null;
            }
            if (string.IsNullOrEmpty(driverMobile) || driverMobile == "null")
            {
                driverMobile = null;
            }
            if (string.IsNullOrEmpty(tripNo) || tripNo == "null")
            {
                tripNo = null;
            }
            if (lowest == null)
            {
                lowest = 0;
            }
            string fileView = _configuration["ClientBalanceImageView"];
            var data = _blDriverBlance.GetAllDriverBlanceViewModelPay(null, null, null, driverID, taxiNumber, fromDate, toDate
            , null, null, isEnable, isActive, InfraStructure.Utility.CurrentLanguageCode, fileView, driverMobile, tripNo, (decimal)lowest);
            var Responce = data.AsQueryable();
            var TotalDrivers = Responce.Select(x => x.DriverID).Distinct().Count();
            var TotalTrips = Responce.Select(x => x.OrderVendorID).Count();
            var TotalAmount = Responce.Sum(x => x.Amount);
            return Json(new { TotalDrivers, TotalTrips, TotalAmount });
        }
        [HttpPost]
        public JsonResult InquiryTotals(int? driverID, string taxiNumber, string driverMobile, string tripNo, DateTime? fromDate,DateTime? toDate,/* int? modelID, int? brandID,*/ int? isEnable, bool? isActive, decimal? lowest)
        {
            if (string.IsNullOrEmpty(taxiNumber) || taxiNumber == "null")
            {
                taxiNumber = null;
            }
            if (string.IsNullOrEmpty(driverMobile) || driverMobile == "null")
            {
                driverMobile = null;
            }
            if (string.IsNullOrEmpty(tripNo) || tripNo == "null")
            {
                tripNo = null;
            }
            if (lowest == null)
            {
                lowest = 0;
            }
            string fileView = _configuration["ClientBalanceImageView"];
            var data = _blDriverBlance.GetAllDriverBlanceViewModelInquiry(null, null, null, driverID, taxiNumber, fromDate, toDate
            , null, null, isEnable, isActive, InfraStructure.Utility.CurrentLanguageCode, fileView, driverMobile, tripNo, (decimal)lowest);
            var Responce = data.AsQueryable();
            var TotalDrivers = Responce.Select(x => x.DriverID).Distinct().Count();
            var TotalTrips = Responce.Select(x => x.OrderVendorID).Count();
            var TotalAmount = Responce.Sum(x => x.Amount);
            return Json(new { TotalDrivers, TotalTrips, TotalAmount });
        }
        [HttpPost]
        public JsonResult HistoryTotals(int? driverID, string taxiNumber, string driverMobile, string tripNo, DateTime? fromDate, DateTime? toDate,/* int? modelID, int? brandID,*/ int? isEnable, bool? isActive, decimal? lowest)
        {
            if (string.IsNullOrEmpty(taxiNumber) || taxiNumber == "null")
            {
                taxiNumber = null;
            }
            if (string.IsNullOrEmpty(driverMobile) || driverMobile == "null")
            {
                driverMobile = null;
            }
            if (string.IsNullOrEmpty(tripNo) || tripNo == "null")
            {
                tripNo = null;
            }
            if (lowest == null)
            {
                lowest = 0;
            }
            string fileView = _configuration["ClientBalanceImageView"];
            var data = _blDriverBlance.GetAllDriverBlanceViewModelHistory(null, null, null, driverID, taxiNumber, fromDate, toDate
            , null, null, isEnable, isActive, InfraStructure.Utility.CurrentLanguageCode, fileView, driverMobile, tripNo, (decimal)lowest);
            var Responce = data.AsQueryable();
            var TotalDrivers = Responce.Select(x => x.DriverID).Distinct().Count();
            var TotalTrips = Responce.Select(x => x.OrderVendorID).Count();
            var TotalAmount = Responce.Sum(x => x.Amount);
            return Json(new { TotalDrivers, TotalTrips, TotalAmount });
        }
        [HttpPost]
        //عرض البيانات من جدول DriverBlance
        public JsonResult LoadTableHistory(int? driverID, string taxiNumber, string driverMobile, string tripNo, DateTime? fromDate, DateTime? toDate,/* int? modelID, int? brandID,*/ int? isEnable, bool? isActive, decimal? lowest)
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
            if (string.IsNullOrEmpty(taxiNumber) || taxiNumber == "null")
            {
                taxiNumber = null;
            }
            if (string.IsNullOrEmpty(driverMobile) || driverMobile == "null")
            {
                driverMobile = null;
            }
            if (string.IsNullOrEmpty(tripNo) || tripNo == "null")
            {
                tripNo = null;
            }
            if (lowest == null)
            {
                lowest = 0;
            }
            string fileView = _configuration["ClientBalanceImageView"];
            var data = _blDriverBlance.GetAllDriverBlanceViewModelHistory(null, null, null, driverID, taxiNumber, fromDate, toDate, null, null, isEnable, isActive, InfraStructure.Utility.CurrentLanguageCode, fileView, driverMobile, tripNo, (decimal)lowest);
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
        [HttpPost]
        //عرض البيانات من جدول DriverBlance
        public JsonResult LoadTableInquiry(int? driverID, string taxiNumber, string driverMobile, string tripNo, DateTime? fromDate, DateTime? toDate,/* int? modelID, int? brandID,*/ int? isEnable, bool? isActive, decimal? lowest)
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
            if (string.IsNullOrEmpty(taxiNumber) || taxiNumber == "null")
            {
                taxiNumber = null;
            }
            if (string.IsNullOrEmpty(driverMobile) || driverMobile == "null")
            {
                driverMobile = null;
            }
            if (string.IsNullOrEmpty(tripNo) || tripNo == "null")
            {
                tripNo = null;
            }
            if (lowest == null)
            {
                lowest = 0;
            }
            string fileView = _configuration["ClientBalanceImageView"];
            var data = _blDriverBlance.GetAllDriverBlanceViewModelInquiry(null, null, null, driverID, taxiNumber, fromDate, toDate, null, null, isEnable, isActive, InfraStructure.Utility.CurrentLanguageCode, fileView, driverMobile, tripNo, (decimal)lowest);
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

                // add the content into the Excel file  
                workSheet.Cells["A" + startRowFrom].LoadFromDataTable(dataTable, true);

                // autofit width of cells with small content  
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
        public FileContentResult ExportDriverBlance(int? driverID, string taxiNumber, string driverMobile, string tripNo, DateTime? fromDate, DateTime? toDate,/* int? modelID, int? brandID,*/ int? isEnable, bool? isActive)
        {
            try
            {
                if (string.IsNullOrEmpty(taxiNumber) || taxiNumber == "null")
                {
                    taxiNumber = null;
                }
                string fileView = _configuration["DriverBalanceImageView"];
                var data = _blDriverBlance.GetAllDriverBlanceViewModel(null, null, null, driverID, taxiNumber, fromDate, toDate
            , null, null, isEnable, isActive, InfraStructure.Utility.CurrentLanguageCode, fileView, driverMobile, tripNo);
                int totalCount = data.Count();
                if (totalCount > 0)
                {
                    List<DriverBlanceExcel> list = data.Select(DBListitem => new DriverBlanceExcel()
                    {
                        Driver_Name = DBListitem.DriverName != null ? DBListitem.DriverName.ToString() : "---",
                        Amount = DBListitem.Amount.ToString(),
                        Before = DBListitem.Before.ToString(),
                        After = DBListitem.After.ToString(),
                        Type_Operation = DBListitem.TypeOperationID == (int)TypeOperationEnum.CR ? Homemade.UI.Resources.Homemade.CR : DBListitem.TypeOperationID == (int)TypeOperationEnum.DR ? Homemade.UI.Resources.Homemade.DR : "---",
                        Transaction_Type = DBListitem.TransactionTypeName != null ? DBListitem.TransactionTypeName.ToString() : "---",
                        Date_Operation = DBListitem.DateOperation != null ? DBListitem.DateOperation.ToString("dd/MM/yyyy") : "---",
                    }).ToList<DriverBlanceExcel>();

                    string[] columns = { "Driver", "Amount", "Before", "After", "Type Operation", "Transaction Type", "Date Operation" };
                    byte[] filecontent = ExportExcel(list, "Selected count : " + totalCount, true, columns);
                    var downloadFileName = "DBList_" + _bLGeneral.GetDateTimeNow() + ".xlsx";
                    return File(filecontent, ExcelContentType, downloadFileName);

                }
                else
                {
                    List<DriverBlanceExcel> list = new List<DriverBlanceExcel>();
                    list.Add(new DriverBlanceExcel()
                    {
                        Driver_Name = "---",
                        Amount = "---",
                        Before = "---",
                        After = "---",
                        Type_Operation = "---",
                        Transaction_Type = "---",
                        Date_Operation = "---",
                    });

                    string[] columns = { "Driver", "Amount", "Before", "After", "Type Operation", "TransactionType", "Date Operation" };
                    byte[] filecontent = ExportExcel(list, "Selected count : " + totalCount, true, columns);
                    var downloadFileName = "DBList_" + _bLGeneral.GetDateTimeNow() + ".xlsx";
                    return File(filecontent, ExcelContentType, downloadFileName);
                }

            }
            catch (Exception ex)
            {

                List<DriverBlanceExcel> list = new List<DriverBlanceExcel>();
                list.Add(new DriverBlanceExcel()
                {
                    Driver_Name = "---",
                    Amount = "---",
                    Before = "---",
                    After = "---",
                    Type_Operation = "---",
                    Transaction_Type = "---",
                    Date_Operation = "---",
                });

                string[] columns = { "Driver", "Amount", "Before", "After", "Type Operation", "TransactionType", "Date Operation" };
                byte[] filecontent = ExportExcel(list, "Driver Blance", true, columns);
                var downloadFileName = "DBList_" + _bLGeneral.GetDateTimeNow() + ".xlsx";
                return File(filecontent, ExcelContentType, downloadFileName);
            }
        }
        public FileContentResult ExportTotalBalance()
        {
            try
            {
                var data = _blDriverBlance.GetAllBalances();
                int? totalCount = data.Count();
                if (totalCount > 0)
                {
                    List<DriverTotalBalanceExcel> list = data.Select(DBListitem => new DriverTotalBalanceExcel()
                    {
                        Driver_Name = Request.Cookies.IsArabic() ? DBListitem.DriverNameAr.ToString() : DBListitem.DriverNameEn.ToString(),
                        Total_Credit = DBListitem.TotalCredit.ToString(),
                        Total_Debit = DBListitem.TotalDebit.ToString(),
                        Net_Balance = DBListitem.NetBalance.ToString(),
                    }).ToList<DriverTotalBalanceExcel>();

                    string[] columns = { "Driver", "Total Credit", "Total Debit", "Net Balance" };
                    byte[] filecontent = ExportExcel(list, Homemade.UI.Resources.Homemade.Total_Credit + " : " + data.Sum(m => m.TotalCredit).ToString() + "       " + Homemade.UI.Resources.Homemade.Total_Debit + " : " + data.Sum(m => m.TotalDebit).ToString() + "       " + Homemade.UI.Resources.Homemade.Balance + " : " + data.Sum(m => m.NetBalance).ToString(), true, columns);
                    var downloadFileName = "DriverTotalBalanceList_" + _bLGeneral.GetDateTimeNow() + ".xlsx";
                    return File(filecontent, ExcelContentType, downloadFileName);

                }
                else
                {
                    List<DriverTotalBalanceExcel> list = new List<DriverTotalBalanceExcel>();
                    list.Add(new DriverTotalBalanceExcel()
                    {
                        Driver_Name = "---",
                        Total_Credit = "---",
                        Total_Debit = "---",
                        Net_Balance = "---",
                    });

                    string[] columns = { "Driver", "Total Credit", "Total Debit", "Net Balance" };
                    byte[] filecontent = ExportExcel(list, "Driver Total Balance count : " + totalCount, true, columns);
                    var downloadFileName = "DriverTotalBalanceList_" + _bLGeneral.GetDateTimeNow() + ".xlsx";
                    return File(filecontent, ExcelContentType, downloadFileName);
                }

            }
            catch (Exception ex)
            {

                List<DriverTotalBalanceExcel> list = new List<DriverTotalBalanceExcel>();
                list.Add(new DriverTotalBalanceExcel()
                {
                    Driver_Name = "---",
                    Total_Credit = "---",
                    Total_Debit = "---",
                    Net_Balance = "---",
                });

                string[] columns = { "Driver", "Total Credit", "Total Debit", "Net Balance" };
                byte[] filecontent = ExportExcel(list, "Driver Total Balance", true, columns);
                var downloadFileName = "DriverTotalBalanceList_" + _bLGeneral.GetDateTimeNow() + ".xlsx";
                return File(filecontent, ExcelContentType, downloadFileName);
            }
        }
        #endregion
        #region Print 
        [CustomeAuthoriz((int)ControllerEnum.DriverBlance, (int)ActionEnum.View)]
        public ActionResult Print(int? driverID, string taxiNumber, string driverMobile, string tripNo, DateTime? fromDate, DateTime? toDate,/* int? modelID, int? brandID,*/ int? isEnable, bool? isActive)
        {
            if (string.IsNullOrEmpty(taxiNumber) || taxiNumber == "null")
            {
                taxiNumber = null;
            }
            string fileView = _configuration["DriverBalanceImageView"];
            var data = _blDriverBlance.GetAllDriverBlanceViewModel(null, null, null, driverID, taxiNumber, fromDate, toDate
            , null, null, isEnable, isActive, InfraStructure.Utility.CurrentLanguageCode, fileView, driverMobile, tripNo);
            int totalCount = data.Count();
            return View(data);
        }
        #endregion
        #endregion
    }
}
