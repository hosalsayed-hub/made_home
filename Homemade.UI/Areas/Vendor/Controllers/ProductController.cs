using Homemade.BLL;
using Homemade.BLL.General;
using Homemade.BLL.Vendor;
using Homemade.BLL.ViewModel.Vendor;
using Homemade.UI.Attributes;
using Homemade.UI.InfraStructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Homemade.UI.Areas.Vendor.Controllers
{
    public class ProductController : Controller
    {
        #region Declarations
        private readonly ResultMessage result;
        private readonly BlProduct _blProduct;
        private readonly IConfiguration _configuration;
        private readonly BlVendor _blVendor;
        private readonly BLPermission _bLPermission;
        private readonly BLUser _bLUser;
        private readonly BLGeneral _BLGeneral;

        public ProductController(BLGeneral BLGeneral, ResultMessage result, IConfiguration configuration, BlProduct blProduct, BlVendor blVendor, BLPermission bLPermission, BLUser bLUser)
        {
            this.result = result;
            _configuration = configuration;
            _blProduct = blProduct;
            _blVendor = blVendor;
            _bLPermission = bLPermission;
            _bLUser = bLUser;
            _BLGeneral = BLGeneral;
        }
        #endregion
        #region View     
        [CustomeAuthoriz((int)ControllerEnum.ProductOperation, (int)ActionEnum.Update)]
        public IActionResult FavouriteProducts()
        {
            if (GetPermissionUser((int)ActionEnum.View))
            {
                return View();
            }
            else
            {
                return Redirect("/AccessDenied/Unauthorized");
            }
        }
        [CustomeAuthoriz((int)ControllerEnum.ProductOperation, (int)ActionEnum.View)]
        public IActionResult Index()
        {
            if (GetPermissionUser((int)ActionEnum.View))
            {
                return View();
            }
            else
            {
                return Redirect("/AccessDenied/Unauthorized");
            }
        }
        [CustomeAuthoriz((int)ControllerEnum.Product, (int)ActionEnum.View)]
        public IActionResult IndexVendor()
        {
            if (GetPermissionUser((int)ActionEnum.View))
            {
                return View();
            }
            else
            {
                return Redirect("/AccessDenied/Unauthorized");
            }
        }
        public IActionResult Create()
        {
            if (GetPermissionUser((int)ActionEnum.View))
            {
                var model = new ProductViewModel();
                var user = _blVendor.GetVendorByUserId(User.GetUserIdInt());
                if (user != null)
                {
                    model.VendorsID = user.VendorsID;
                    return View(model);
                }
                else
                {
                    return View(model);
                }
            }
            else
            {
                return Redirect("/AccessDenied/Unauthorized");
            }

        }
        public IActionResult Details(Guid? id)
        {
            if (GetPermissionUser((int)ActionEnum.View))
            {
                if (id.HasValue)
                {
                    ProductViewModel ProductViewModel = _blProduct.GetProductViewModelByGuid(id.Value, InfraStructure.Utility.CurrentLanguageCode, _configuration["ProductImageView"]);
                    if (ProductViewModel != null)
                    {
                        return View(ProductViewModel);
                    }
                }
                return Redirect(Url.Action("Index", "Vendors", new { Area = "Vendor" }));
            }
            else
            {
                return Redirect("/AccessDenied/Unauthorized");
            }
        }
        public IActionResult Edit(Guid? id)
        {
            if (GetPermissionUser((int)ActionEnum.View))
            {
                if (id.HasValue)
                {
                    ProductViewModel ProductViewModel = _blProduct.GetProductViewModelByGuid(id.Value, InfraStructure.Utility.CurrentLanguageCode, _configuration["ProductImageView"]);
                    if (ProductViewModel != null)
                    {
                        return View(ProductViewModel);
                    }
                }
                return Redirect(Url.Action("Index", "Product", new { Area = "Vendor" }));
            }
            else
            {
                return Redirect("/AccessDenied/Unauthorized");
            }
        }
        #endregion
        #region Action
        [HttpPost]
        public JsonResult RemoveFromFavouriteProducts(int id)
        {
            var result = false;
            try
            {
                result = _blProduct.RemoveFromFavouriteProducts(id, User.GetUserIdInt());
            }
            catch (Exception ex)
            {
            }
            ResultMessage rResultMessage = result ?
                new ResultMessage { Message = Resources.Homemade.SuccessChangeMessage, ResultType = ResultMessageType.success.ToString() }
                :
                new ResultMessage { Message = Resources.Homemade.FailSaveMessage, ResultType = ResultMessageType.error.ToString() };
            return Json(rResultMessage);
        }
        [HttpPost]
        public JsonResult AddToFavouriteProducts(int id)
        {
            var result = false;
            try
            {
                result = _blProduct.AddToFavouriteProducts(id, User.GetUserIdInt());
            }
            catch (Exception ex)
            {
            }
            ResultMessage rResultMessage = result ?
                new ResultMessage { Message = Resources.Homemade.SuccessChangeMessage, ResultType = ResultMessageType.success.ToString() }
                :
                new ResultMessage { Message = Resources.Homemade.FailSaveMessage, ResultType = ResultMessageType.error.ToString() };
            return Json(rResultMessage);
        }
        [HttpPost, ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<JsonResult> Create(ProductViewModel viewModel)
        {
            try
            {
                #region Validation

                JsonResult ValidateData = ValidateModel(viewModel.ProductID, viewModel.NameAr, viewModel.NameEn);

                if (ValidateData != null) return ValidateData;

                if (viewModel.ProductTypeId == (int)ProductTypeEnum.Product_Ready_Shipping && (string.IsNullOrEmpty(viewModel.Weight) || viewModel.Weight == "0"))
                {
                    result.Message = BLL.Resources.HomemadeErrorMessages.WeightRequired;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }

                int UserId = User.GetUserIdInt();
                #endregion
                viewModel.Discount = !string.IsNullOrEmpty(viewModel.Discount) ? viewModel.Discount : "0";
                decimal discount = decimal.Parse(viewModel.Discount, CultureInfo.InvariantCulture);
                if (discount > 0)
                {
                    if (string.IsNullOrEmpty(viewModel.StartDiscountDate) || string.IsNullOrEmpty(viewModel.StartDiscountDate))
                    {
                        result.Message = BLL.Resources.HomemadeErrorMessages.Discount_Date_Required;
                        result.ResultType = ResultMessageType.error.ToString();
                        result.Status = false;
                        return Json(result);
                    }
                }
                if (viewModel.ProductID == 0)
                {
                    var Photo = "";
                    if (!string.IsNullOrEmpty(viewModel.Logo))
                    {
                        string FileName = Guid.NewGuid() + ".png";
                        Photo = FileName;
                        string MainPath = _configuration["ProductImageSave"];
                        _BLGeneral.SaveImage(new BLGeneral.SaveImageModel
                        {
                            base64 = viewModel.Logo,
                            key = "",
                            fileName = FileName,
                            mainPath = MainPath
                        });
                        viewModel.Logo = Photo;
                    }
                    #region Adding
                    int ID = 0;
                    if (_blProduct.Insert(viewModel, UserId, out ID))
                    {
                        result.Message = Homemade.UI.Resources.Homemade.SuccessSaveMessage;
                        result.ResultType = ResultMessageType.success.ToString();
                        result.Status = true;
                        result.ID = ID;
                        return Json(result);
                    }
                    else
                    {
                        result.Message = Homemade.UI.Resources.Homemade.FailSaveMessage;
                        result.ResultType = ResultMessageType.error.ToString();
                        result.Status = false;
                        return Json(result);
                    }
                }
                else
                {
                    #region Update
                    if (_blProduct.Update(viewModel, UserId, _configuration["ProductImageSave"], _configuration["ProductImageView"]))
                    {
                        result.Message = Homemade.UI.Resources.Homemade.SuccessSaveMessage;
                        result.ResultType = ResultMessageType.success.ToString();
                        result.Status = true;
                        return Json(result);
                    }
                    else
                    {
                        result.Message = Homemade.UI.Resources.Homemade.FailSaveMessage;
                        result.ResultType = ResultMessageType.error.ToString();
                        result.Status = false;
                        return Json(result);
                    }
                    #endregion
                }

                #endregion
            }
            catch (System.Exception ex)
            {
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = ex.Message.ToString();
                result.Status = false;
                return Json(result);
            }
        }
        [HttpPost, ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<JsonResult> Edit(ProductViewModel viewModel)
        {
            try
            {
                #region Validation

                JsonResult ValidateData = ValidateModel(viewModel.ProductID, viewModel.NameAr, viewModel.NameEn);

                if (ValidateData != null) return ValidateData;
                if (viewModel.ProductTypeId == (int)ProductTypeEnum.Product_Ready_Shipping && (string.IsNullOrEmpty(viewModel.Weight) || viewModel.Weight == "0"))
                {
                    result.Message = BLL.Resources.HomemadeErrorMessages.WeightRequired;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
                #endregion
                #region Update
                decimal discount = decimal.Parse(viewModel.Discount, CultureInfo.InvariantCulture);
                if (discount > 0)
                {
                    if (string.IsNullOrEmpty(viewModel.StartDiscountDate) || string.IsNullOrEmpty(viewModel.StartDiscountDate))
                    {
                        result.Message = BLL.Resources.HomemadeErrorMessages.Discount_Date_Required;
                        result.ResultType = ResultMessageType.error.ToString();
                        result.Status = false;
                        return Json(result);
                    }
                }
                int UserId = User.GetUserIdInt();
                if (_blProduct.Update(viewModel, UserId, _configuration["ProductImageSave"], _configuration["ProductImageView"]))
                {
                    result.Message = Homemade.UI.Resources.Homemade.SuccessSaveMessage;
                    result.ResultType = ResultMessageType.success.ToString();
                    result.Status = true;
                    return Json(result);
                }
                else
                {
                    result.Message = Homemade.UI.Resources.Homemade.FailSaveMessage;
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Status = false;
                    return Json(result);
                }
                #endregion

            }
            catch (System.Exception ex)
            {

                result.ResultType = ResultMessageType.error.ToString();
                result.Message = ex.Message.ToString();
                result.Status = false;
                return Json(result);
            }
        }
        [HttpPost]
        public JsonResult CreateProductQuestion(ProductQuestionViewModel model)
        {
            try
            {
                #region Vaildation
                if (string.IsNullOrWhiteSpace(model.QuestionAr))
                {
                    result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.ArNameRequierd;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
                if (string.IsNullOrWhiteSpace(model.QuestionEn))
                {
                    result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.EnNameRequierd;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
                #endregion
                #region SaveData
                int ProductQuestionID = 0;
                string OperationType = "add";

                bool IsSuccess;
                if (model.ProductQuestionID == 0)
                {
                    #region AddProductQuestion
                    model.UserId = User.GetUserIdInt();
                    IsSuccess = _blProduct.InsertProductQuestion(model, out ProductQuestionID);
                    #endregion
                }
                else
                {
                    #region UpdateProductQuestion
                    model.UserUpdate = User.GetUserIdInt();
                    IsSuccess = _blProduct.UpdateProductQuestion(model);
                    OperationType = "update";
                    #endregion
                }
                if (IsSuccess)
                {
                    result.ResultType = ResultMessageType.success.ToString();
                    result.Message = Resources.Homemade.SuccessSaveMessage;
                }
                else
                {
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = Resources.Homemade.FailSaveMessage;
                }
                #endregion
                return Json(new { result, ProductQuestionID, OperationType });
            }
            catch (System.Exception ex)
            {
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = ex.Message.ToString();
                return Json(result);
            }
        }
        [HttpPost]
        public async Task<JsonResult> UploadProductImages(int ProductID, List<IFormFile> ProductImgFile)
        {
            List<string> ListProductImgFileName = new List<string>();
            if (ProductImgFile != null)
            {
                foreach (var item in ProductImgFile)
                {
                    if (item.Length > 0)
                    {

                        var ProductImgFileName = string.Concat(Guid.NewGuid().ToString()) + (ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"').Substring(ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"').IndexOf(".")));
                        var FilePath = Path.Combine(_configuration["ProductFileImageSave"], ProductImgFileName);

                        _BLGeneral.SaveIFromFile(new BLGeneral.SaveImageFormFileModel
                        {
                            file = item,
                            filename = ProductImgFileName,
                            key = "",
                            mainPath = FilePath
                        });
                        ListProductImgFileName.Add(ProductImgFileName);
                    }
                }

            }
            if (ListProductImgFileName.Count() > 0)
            {
                var IsSuccess = _blProduct.InsertProductImage(ProductID, ListProductImgFileName, User.GetUserIdInt());
                if (IsSuccess)
                {
                    result.Status = true;
                    result.ResultType = ResultMessageType.success.ToString();
                    result.Message = Resources.Homemade.SuccessSaveMessage;
                }
                else
                {
                    result.Status = false;
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = Resources.Homemade.FailSaveMessage;
                }
            }
            else
            {
                result.Status = false;
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = Resources.Homemade.FailSaveMessage;
            }
            return Json(result);
        }
        [HttpPost]
        public async Task<JsonResult> UploadProductMainImage(int ProductID, IFormFile ProductImgFile)
        {
            string Logo = string.Empty;
            if (ProductImgFile != null)
            {
                if (ProductImgFile.Length > 0)
                {
                    var ProductImgFileName = string.Concat(Guid.NewGuid().ToString()) + (ContentDispositionHeaderValue.Parse(ProductImgFile.ContentDisposition).FileName.Trim('"').Substring(ContentDispositionHeaderValue.Parse(ProductImgFile.ContentDisposition).FileName.Trim('"').IndexOf(".")));
                    var FilePath = Path.Combine(_configuration["ProductFileImageSave"], ProductImgFileName);
                    _BLGeneral.SaveIFromFile(new BLGeneral.SaveImageFormFileModel
                    {
                        file = ProductImgFile,
                        filename = ProductImgFileName,
                        key = "",
                        mainPath = FilePath
                    });
                    Logo = ProductImgFileName;
                }
            }
            if (!string.IsNullOrEmpty(Logo))
            {
                var IsSuccess = _blProduct.UpdateProductLogo(ProductID, Logo, User.GetUserIdInt());
                if (IsSuccess)
                {
                    result.Status = true;
                    result.ResultType = ResultMessageType.success.ToString();
                    result.Message = Resources.Homemade.SuccessSaveMessage;
                    result.ObjResult = Path.Combine(_configuration["ProductImageView"], Logo);
                }
                else
                {
                    result.Status = false;
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = Resources.Homemade.FailSaveMessage;
                }
            }
            else
            {
                result.Status = false;
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = Resources.Homemade.FailSaveMessage;
            }
            return Json(result);
        }
        private JsonResult ValidateModel(int ProductID, string NameAr, string NameEn)
        {
            if (string.IsNullOrWhiteSpace(NameAr))
            {
                result.Message = BLL.Resources.HomemadeErrorMessages.ArNameRequierd;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
            return null;
        }
        [HttpPost]
        public JsonResult DeleteAllProduct(int id)
        {
            var result = false;
            try
            {
                result = _blProduct.DeleteAllProduct(id, User.GetUserIdInt());
            }
            catch (Exception ex)
            {
            }
            ResultMessage rResultMessage = result ?
                new ResultMessage { Message = Resources.Homemade.SuccessDeleteMessage, ResultType = ResultMessageType.success.ToString() }
                :
                new ResultMessage { Message = Resources.Homemade.FailDeleteMessage, ResultType = ResultMessageType.error.ToString() };
            return Json(rResultMessage);
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = false;
            try
            {
                result = _blProduct.DeleteProduct(id, User.GetUserIdInt());
            }
            catch (Exception ex)
            {
            }
            ResultMessage rResultMessage = result ?
                new ResultMessage { Message = Resources.Homemade.SuccessDeleteMessage, ResultType = ResultMessageType.success.ToString() }
                :
                new ResultMessage { Message = Resources.Homemade.FailDeleteMessage, ResultType = ResultMessageType.error.ToString() };
            return Json(rResultMessage);
        }
        [HttpPost]
        public JsonResult DeleteFile(int productImageID)
        {
            var result = false;
            try
            {

                result = _blProduct.DeleteProductImage(productImageID, User.GetUserIdInt());
            }
            catch (Exception ex)
            {
            }
            ResultMessage rResultMessage = result ?
                new ResultMessage { Message = Resources.Homemade.SuccessDeleteMessage, ResultType = ResultMessageType.success.ToString() }
                :
                new ResultMessage { Message = Resources.Homemade.FailDeleteMessage, ResultType = ResultMessageType.error.ToString() };
            return Json(rResultMessage);

        }
        [HttpPost]
        public JsonResult DeleteQuestions(int id)
        {
            var result = false;
            try
            {
                result = _blProduct.DeleteProductQuestion(id, User.GetUserIdInt());
            }
            catch (Exception ex)
            {
            }
            ResultMessage rResultMessage = result ?
                new ResultMessage { Message = Resources.Homemade.SuccessDeleteMessage, ResultType = ResultMessageType.success.ToString() }
                :
                new ResultMessage { Message = Resources.Homemade.FailDeleteMessage, ResultType = ResultMessageType.error.ToString() };
            return Json(rResultMessage);
        }
        [HttpPost]
        public JsonResult ChangeStatue(int id, int type)
        {
            var result = false;
            try
            {
                if (type == 3)
                {
                    result = _blProduct.ChangeStatus(id, User.GetUserIdInt());
                }
                else
                {
                    result = _blProduct.UpdateProdFixed(id, type, User.GetUserIdInt());
                }

            }
            catch (Exception ex)
            {
            }
            ResultMessage rResultMessage = result ?
                new ResultMessage { Message = Resources.Homemade.SuccessChangeMessage, ResultType = ResultMessageType.success.ToString() }
                :
                new ResultMessage { Message = Resources.Homemade.FailChangeStatueMessage, ResultType = ResultMessageType.error.ToString() };
            return Json(rResultMessage);

        }
        #endregion
        #region Helper
        public JsonResult LoadTableFavouriteProducts()
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
            var data = _blProduct.GetAllProductViewModelByFavourite(InfraStructure.Utility.CurrentLanguageCode, _configuration["ProductImageView"]);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                    x.Name.ToLower().Contains(search.ToLower()));
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
        public JsonResult LoadTableNotFavouriteProducts()
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
            var data = _blProduct.GetAllProductViewModelByNotFavourite(InfraStructure.Utility.CurrentLanguageCode, _configuration["ProductImageView"]);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                    x.Name.ToLower().Contains(search.ToLower()));
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
        public JsonResult LoadTable(string name, string listDepartmentID, string listKeyWordsID, string listVendorID)
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
            string[] department = null;
            if (!string.IsNullOrEmpty(listDepartmentID) && listDepartmentID != "null")
            {
                department = listDepartmentID.Split(',');
            }
            string[] keywords = null;
            if (!string.IsNullOrEmpty(listKeyWordsID) && listKeyWordsID != "null")
            {
                keywords = listKeyWordsID.Split(',');
            }
            string[] vendor = null;
            if (!string.IsNullOrEmpty(listVendorID) && listVendorID != "null")
            {
                vendor = listVendorID.Split(',');
            }
            var user = _blVendor.GetVendorByUserId(User.GetUserIdInt());
            if (user != null)
            {
                vendor = new string[1];
                vendor[0] = user.VendorsID.ToString();
            }
            var data = _blProduct.GetAllProductViewModelBySearch(vendor, department, keywords, name, InfraStructure.Utility.CurrentLanguageCode, _configuration["ProductImageView"]);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                    x.NameAr.ToLower().Contains(search.ToLower()) || x.NameEn.ToLower().Contains(search.ToLower()) || x.DepartmentName.ToLower().Contains(search.ToLower()) ||
                    x.VendorsName.ToLower().Contains(search.ToLower()) || x.Size.ToLower().Contains(search.ToLower()));
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
        public JsonResult LoadTableQuestions(int? productID)
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
            var data = _blProduct.GetAllProductQuestionViewModelByProduct(productID, InfraStructure.Utility.CurrentLanguageCode);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                    x.ProductID.ToString().Contains(search) || x.ProductQuestionID.ToString().Contains(search) || x.QuestionAr.Contains(search) || x.QuestionEn.ToString().Contains(search) ||
                    x.AnswerAr.ToString().Contains(search) || x.AnswerEn.ToString().Contains(search));
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
        public ActionResult ProductImgPartial(int productID)
        {
            return PartialView("_ProductImgPartial", _blProduct.GetAllProductImageViewModelByProductID(productID, _configuration["ProductImageView"]));
        }
        public JsonResult GetAllKeyWords()
        {
            var obj = _blProduct.GetAllKeyWords().Select(p => new { p.KeyWordsID, KeyWordsName = Homemade.UI.InfraStructure.Utility.CurrentLanguageCode == "ar" ? p.NameAR : p.NameEN }).ToList();
            return Json(obj);
        }
        public JsonResult GetAllProductType()
        {
            var obj = _blProduct.GetProductTypeEnum(Homemade.UI.InfraStructure.Utility.CurrentLanguageCode).ToList();
            return Json(obj);
        }
        public bool GetPermissionUser(int action)
        {
            var user = _bLUser.GetUserInfo(User.GetUserIdInt());
            int UserTypeId = user.UserType;
            int controller = (int)ControllerEnum.ProductOperation;
            if (UserTypeId == (int)UserTypeEnum.Vendor)
            {
                controller = (int)ControllerEnum.Product;
            }
            return _bLPermission.GetPermissionByUserAndController(user.UserName, controller, action);
        }
        #endregion

    }
}
