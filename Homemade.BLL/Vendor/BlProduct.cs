using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homemade.BLL.General;
using Homemade.BLL.Setting;
using Homemade.BLL.ViewModel.Customer;
using Homemade.BLL.ViewModel.Driver;
using Homemade.BLL.ViewModel.Order;
using Homemade.BLL.ViewModel.Setting;
using Homemade.BLL.ViewModel.Site;
using Homemade.BLL.ViewModel.Vendor;
using Homemade.DAL.Contract;
using Homemade.Model.Customer;
using Homemade.Model.Order;
using Homemade.Model.Vendor;

namespace Homemade.BLL.Vendor
{
    public class BlProduct
    {
        #region Declartion
        private readonly IUOW Uow;
        private readonly BLGeneral _bLGeneral;
        private readonly BlTokens _blTokens;
        private readonly BlConfiguration _blConfiguration;
        public BlProduct(IUOW _uow, BLGeneral bLGeneral, BlTokens blTokens, BlConfiguration blConfiguration)
        {
            this.Uow = _uow;
            _bLGeneral = bLGeneral;
            _blTokens = blTokens;
            _blConfiguration = blConfiguration;
        }
        #endregion
        #region Helper
        public IQueryable<ProductViewModel> GetAllProductViewModelByFavourite(string lang, string MainPathView)
        {
            var getData = Uow.Product.GetAll(x => !x.IsDeleted && !x.Vendors.IsDeleted && x.Vendors.IsEnable && !x.Vendors.IsVac && x.IsEnable && !x.IsHidden && x.IsFavourite, "Departments").OrderByDescending(p => p.CreateDate)
            .Select(p => new ProductViewModel()
            {
                NameAr = p.NameAr,
                NameEn = p.NameEn,
                IsFixed = p.IsFixed,
                IsAvailable = p.IsEnable,
                IsHidden = p.IsHidden,
                Pieces = lang == "ar" ? p.PiecesAr : p.PiecesEn,
                PiecesAr = p.PiecesAr,
                PiecesEn = p.PiecesEn,
                Price = p.Price.ToString(),
                Quantity = p.Quantity.ToString(),
                Size = p.Size,
                SKU = p.SKU,
                StartDiscountDate = p.StartDiscountDate != null ? p.StartDiscountDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                EndDiscountDate = p.EndDiscountDate != null ? p.EndDiscountDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                DepartmentsID = p.DepartmentsID,
                DescAr = p.DescAr,
                DescEn = p.DescEn,
                Discount = p.Discount.ToString(),
                VendorsID = p.VendorsID,
                Weight = p.Weight.ToString(),
                ProductID = p.ProductID,
                DepartmentName = lang == "ar" ? p.Departments.NameAR : p.Departments.NameEN,
                Name = lang == "ar" ? p.NameAr : p.NameEn,
                ProductGuid = p.ProductGuid,
                KeyWordsListString = p.KeyWords,
                IsAvailableString = lang == "ar" ? (p.IsEnable ? "نعم" : "لا") : (p.IsEnable ? "Yes" : "No"),
                IsHiddenString = lang == "ar" ? (p.IsHidden ? "نعم" : "لا") : (p.IsHidden ? "Yes" : "No"),
                IsFixedString = lang == "ar" ? (p.IsFixed ? "نعم" : "لا") : (p.IsFixed ? "Yes" : "No"),
                Logo = !string.IsNullOrEmpty(p.Logo) ? (MainPathView + p.Logo) : "/Images/noImage.png",
                VendorsName = lang == "ar" ? (p.Vendors.StoreNameAr) : (p.Vendors.StoreNameEn),
            });
            return getData;

        }
        public IQueryable<ProductViewModel> GetAllProductViewModelByNotFavourite(string lang, string MainPathView)
        {
            var getData = Uow.Product.GetAll(x => !x.IsDeleted && !x.Vendors.IsDeleted && x.Vendors.IsEnable && !x.Vendors.IsVac && x.IsEnable && !x.IsHidden && !x.IsFavourite, "Departments").OrderByDescending(p => p.CreateDate)
            .Select(p => new ProductViewModel()
            {
                NameAr = p.NameAr,
                NameEn = p.NameEn,
                IsFixed = p.IsFixed,
                IsAvailable = p.IsEnable,
                IsHidden = p.IsHidden,
                Pieces = lang == "ar" ? p.PiecesAr : p.PiecesEn,
                PiecesAr = p.PiecesAr,
                PiecesEn = p.PiecesEn,
                Price = p.Price.ToString(),
                Quantity = p.Quantity.ToString(),
                Size = p.Size,
                SKU = p.SKU,
                StartDiscountDate = p.StartDiscountDate != null ? p.StartDiscountDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                EndDiscountDate = p.EndDiscountDate != null ? p.EndDiscountDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                DepartmentsID = p.DepartmentsID,
                DescAr = p.DescAr,
                DescEn = p.DescEn,
                Discount = p.Discount.ToString(),
                VendorsID = p.VendorsID,
                Weight = p.Weight.ToString(),
                ProductID = p.ProductID,
                DepartmentName = lang == "ar" ? p.Departments.NameAR : p.Departments.NameEN,
                Name = lang == "ar" ? p.NameAr : p.NameEn,
                ProductGuid = p.ProductGuid,
                KeyWordsListString = p.KeyWords,
                IsAvailableString = lang == "ar" ? (p.IsEnable ? "نعم" : "لا") : (p.IsEnable ? "Yes" : "No"),
                IsHiddenString = lang == "ar" ? (p.IsHidden ? "نعم" : "لا") : (p.IsHidden ? "Yes" : "No"),
                IsFixedString = lang == "ar" ? (p.IsFixed ? "نعم" : "لا") : (p.IsFixed ? "Yes" : "No"),
                Logo = !string.IsNullOrEmpty(p.Logo) ? (MainPathView + p.Logo) : "/Images/noImage.png",
                VendorsName = lang == "ar" ? (p.Vendors.StoreNameAr) : (p.Vendors.StoreNameEn),
            });
            return getData;

        }
        public Product GetProductById(int ProductID)
        {
            return Uow.Product.GetAll(x => x.ProductID == ProductID && !x.IsDeleted).FirstOrDefault();
        }
        public IQueryable<Model.Setting.KeyWords> GetAllKeyWords()
        {
            return Uow.KeyWords.GetAll(p => !p.IsDeleted).OrderByDescending(p => p.CreateDate);
        }
        public bool IsVendorWorking(string DaysWork, DateTime? WorkFrom, DateTime? WorkTo, string DaysVac, DateTime? VacWorkFrom, DateTime? VacWorkTo)
        {
            if (!string.IsNullOrEmpty(DaysWork) || !string.IsNullOrEmpty(DaysVac))
            {
                var datetimenow = _bLGeneral.GetDateTimeNow().TimeOfDay;
                var ListDaysWork = !string.IsNullOrEmpty(DaysWork) ? DaysWork.Split(',') : null;
                var ListDaysVac = !string.IsNullOrEmpty(DaysVac) ? DaysVac.Split(',') : null;
                if (ListDaysWork != null)
                {
                    if (ListDaysWork.Where(x => !string.IsNullOrEmpty(x)).Any(x => Convert.ToInt32(x) == (int)DateTime.Now.DayOfWeek))
                    {

                        TimeSpan WorkFromSpan = (WorkFrom != null ? (DateTime.ParseExact(WorkFrom.Value.ToString("HH:mm"), "HH:mm", System.Globalization.CultureInfo.InvariantCulture)) : new DateTime()).TimeOfDay;
                        TimeSpan WorkToSpan = (WorkTo != null ? (DateTime.ParseExact(WorkTo.Value.ToString("HH:mm"), "HH:mm", System.Globalization.CultureInfo.InvariantCulture)) : new DateTime()).TimeOfDay;

                        if (WorkFromSpan != new TimeSpan() && WorkToSpan != new TimeSpan())
                        {
                            if (WorkFromSpan < WorkToSpan)
                            {
                                if (WorkFromSpan < datetimenow && WorkToSpan > datetimenow)
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                if (WorkFromSpan < datetimenow || WorkToSpan > datetimenow)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                if (ListDaysVac != null)
                {
                    if (ListDaysVac.Where(x => !string.IsNullOrEmpty(x)).Any(x => Convert.ToInt32(x) == (int)DateTime.Now.DayOfWeek))
                    {

                        TimeSpan VacWorkFromSpan = (VacWorkFrom != null ? (DateTime.ParseExact(VacWorkFrom.Value.ToString("HH:mm"), "HH:mm", System.Globalization.CultureInfo.InvariantCulture)) : new DateTime()).TimeOfDay;
                        TimeSpan VacWorkToSpan = (VacWorkTo != null ? (DateTime.ParseExact(VacWorkTo.Value.ToString("HH:mm"), "HH:mm", System.Globalization.CultureInfo.InvariantCulture)) : new DateTime()).TimeOfDay;

                        if (VacWorkFromSpan != new TimeSpan() && VacWorkToSpan != new TimeSpan())
                        {
                            if (VacWorkFromSpan < VacWorkToSpan)
                            {
                                if (VacWorkFromSpan < datetimenow && VacWorkToSpan > datetimenow)
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                if (VacWorkFromSpan < datetimenow || VacWorkToSpan > datetimenow)
                                {
                                    return true;
                                }
                            }
                        }

                    }
                }
                return false;
            }
            {
                return true;
            }
        }
        public IEnumerable<_Enum> GetProductTypeEnum(string lang)
        {
            if (lang == "ar")
            {
                List<_Enum> enumData = new List<_Enum>();
                enumData.Add(new _Enum()
                {
                    Value = (int)ProductTypeEnum.Food,
                    Text = "اكل",
                });
                enumData.Add(new _Enum()
                {
                    Value = (int)ProductTypeEnum.Product_Ready_Shipping,
                    Text = "منتج جاهز للشحن",
                });
                return enumData;
            }
            else
            {
                List<_Enum> enumData = new List<_Enum>();
                enumData.Add(new _Enum()
                {
                    Value = (int)ProductTypeEnum.Food,
                    Text = "Food",
                });
                enumData.Add(new _Enum()
                {
                    Value = (int)ProductTypeEnum.Product_Ready_Shipping,
                    Text = "Product Ready Shipping",
                });
                return enumData;
            }
        }
        public bool IsExistName(string ArName, string EnName, int Id) =>
           Uow.Product.GetAll().Any(query => query.ProductID != Id && (query.NameAr.Trim() == ArName.Trim() || query.NameEn.Trim() == EnName.Trim()) && !query.IsDeleted);
        public ProductViewModel GetProductViewModelByGuid(Guid ProductGuid, string lang, string MainPathView)
        {
            var getData = Uow.Product.GetAll(x => !x.IsDeleted && x.ProductGuid == ProductGuid, "Departments,Vendors")
            .Select(p => new ProductViewModel()
            {
                NameAr = p.NameAr,
                NameEn = p.NameEn,
                IsFixed = p.IsFixed,
                IsAvailable = p.IsEnable,
                IsHidden = p.IsHidden,
                Pieces = lang == "ar" ? p.PiecesAr : p.PiecesEn,
                PiecesAr = p.PiecesAr,
                PiecesEn = p.PiecesEn,
                Price = p.Price.ToString(),
                Quantity = p.Quantity.ToString(),
                Size = p.Size,
                SKU = p.SKU,
                StartDiscountDate = p.StartDiscountDate != null ? p.StartDiscountDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                EndDiscountDate = p.EndDiscountDate != null ? p.EndDiscountDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                DepartmentsID = p.DepartmentsID,
                DescAr = p.DescAr,
                DescEn = p.DescEn,
                Discount = p.Discount.ToString(),
                VendorsID = p.VendorsID,
                Weight = p.Weight.ToString(),
                ProductID = p.ProductID,
                DepartmentName = lang == "ar" ? p.Departments.NameAR : p.Departments.NameEN,
                Name = lang == "ar" ? p.NameAr : p.NameEn,
                ProductGuid = p.ProductGuid,
                KeyWordsListString = p.KeyWords,
                IsAvailableString = lang == "ar" ? (p.IsEnable ? "نعم" : "لا") : (p.IsEnable ? "Yes" : "No"),
                IsHiddenString = lang == "ar" ? (p.IsHidden ? "نعم" : "لا") : (p.IsHidden ? "Yes" : "No"),
                IsFixedString = lang == "ar" ? (p.IsFixed ? "نعم" : "لا") : (p.IsFixed ? "Yes" : "No"),
                Logo = !string.IsNullOrEmpty(p.Logo) ? (MainPathView + p.Logo) : "/Images/noImage.png",
                ProductTypeId = p.ProductTypeId,
                ProductOrder = p.ProductOrder,
                ProductQuantity = p.ProductQuantity,
                ProductQuantityType = p.ProductQuantityType,
                ProductQuantityTypeName = lang == "ar" ? (p.ProductQuantityType ==0 ? "غير محدد" : p.ProductQuantityType == 1 ? "قطع":"أشخاص") : (p.ProductQuantityType == 0 ? "Undefiened" : p.ProductQuantityType == 1 ? "Pieces" : "Persons"),
                ProductTypeName = lang == "ar" ? (p.ProductTypeId == (int)ProductTypeEnum.Food ? "اكل" : "منتج جاهز للشحن") 
                : (p.ProductTypeId == (int)ProductTypeEnum.Food ? "Food" : "Product Ready Shipping"),
                TimeTakenProcess = p.TimeTakenProcess > 60 ? ((int)(p.TimeTakenProcess % 60)).ToString() : p.TimeTakenProcess.ToString(),
                TimeTakenProcessHours = p.TimeTakenProcess > 60 ? ((int)(p.TimeTakenProcess / 60)).ToString() : "0",
                MeasurementId = p.MeasurementId,
                IsLimited = p.IsLimited,
            }).FirstOrDefault();
            if (getData != null)
            {
                if (!string.IsNullOrEmpty(getData.KeyWordsListString))
                {
                    getData.KeyWordsString = GetKeyWordStringFromInt(getData.KeyWordsListString, lang);
                }
            }
            return getData;
        }
        public string GetKeyWordStringFromInt(string KeyWordsListString, string lang)
        {
            var list = KeyWordsListString.Split(',').ToList();
            var KeyWordsString = Uow.KeyWords.GetAll(x => !x.IsDeleted && list.Any(y => y == x.KeyWordsID.ToString())).Select(m => lang == "ar" ? m.NameAR : m.NameEN).ToList();
            var KeyWords = "";
            foreach (var item in KeyWordsString)
            {
                if (!string.IsNullOrWhiteSpace(KeyWords))
                {
                    KeyWords = KeyWords + "," + item.ToString();
                }
                else
                {
                    KeyWords = item.ToString();
                }
            }
            return KeyWords;
        }
        public IQueryable<ProductViewModel> GetAllProductViewModelBySearch(string[] ListVendorID, string[] ListDepartmentID, string[] ListKeyWordsID, string name, string lang, string MainPathView)
        {
            if (ListDepartmentID != null)
            {
                if (ListDepartmentID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListDepartmentID = null;
                }
            }
            if (ListVendorID != null)
            {
                if (ListVendorID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListVendorID = null;
                }
            }
            var getData = Uow.Product.GetAll(x => !x.IsDeleted
            && (ListVendorID != null ? ListVendorID.Any(y => x.VendorsID.ToString() == y) : true)
            && (!string.IsNullOrEmpty(name) ? (x.NameAr.Contains(name) || x.NameEn.Contains(name)) : true)
            && (ListDepartmentID != null ? ListDepartmentID.Any(y => x.DepartmentsID.ToString() == y) : true),"Departments").OrderByDescending(p => p.CreateDate)
            .Select(p => new ProductViewModel()
            {
                NameAr = p.NameAr,
                NameEn = p.NameEn,
                IsFixed = p.IsFixed,
                IsAvailable = p.IsEnable,
                IsHidden = p.IsHidden,
                Pieces = lang == "ar" ? p.PiecesAr : p.PiecesEn,
                PiecesAr = p.PiecesAr,
                PiecesEn = p.PiecesEn,
                Price = p.Price.ToString(),
                Quantity = p.Quantity.ToString(),
                Size = p.Size,
                SKU = p.SKU,
                StartDiscountDate = p.StartDiscountDate != null ? p.StartDiscountDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                EndDiscountDate = p.EndDiscountDate != null ? p.EndDiscountDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                DepartmentsID = p.DepartmentsID,
                DescAr = p.DescAr,
                DescEn = p.DescEn,
                Discount = p.Discount.ToString(),
                VendorsID = p.VendorsID,
                Weight = p.Weight.ToString(),
                ProductID = p.ProductID,
                DepartmentName = lang == "ar" ? p.Departments.NameAR : p.Departments.NameEN,
                Name = lang == "ar" ? p.NameAr : p.NameEn,
                ProductGuid = p.ProductGuid,
                KeyWordsListString = p.KeyWords,
                IsAvailableString = lang == "ar" ? (p.IsEnable ? "نعم" : "لا") : (p.IsEnable ? "Yes" : "No"),
                IsHiddenString = lang == "ar" ? (p.IsHidden ? "نعم" : "لا") : (p.IsHidden ? "Yes" : "No"),
                IsFixedString = lang == "ar" ? (p.IsFixed ? "نعم" : "لا") : (p.IsFixed ? "Yes" : "No"),
                Logo = !string.IsNullOrEmpty(p.Logo) ? (MainPathView + p.Logo) : "/Images/noImage.png",
                VendorsName = lang == "ar" ? (p.Vendors.StoreNameAr) : (p.Vendors.StoreNameEn),
            });
            if (ListKeyWordsID != null)
            {
                if (ListKeyWordsID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListKeyWordsID = null;
                }
                getData = getData.ToList().Where(x => (ListKeyWordsID != null ? ListKeyWordsID.Any(y => x.KeyWordsListString.Split(',').ToList().Any(z => z == y)) : true)).AsQueryable();
            }
            return getData;

        }
        public ProdResonse GetProductListWithDept(int DeptID, string ProdPath, string lang, int page, string search, string vendorpath, string deptList, decimal? from, decimal? to)
        {
            var DeliveryPriceVatPercent = _blConfiguration.GetDeliveryPriceVatPercent();
            var Data = Uow.Product.GetAll(x => !x.IsDeleted && !x.Vendors.IsDeleted && x.Vendors.IsEnable && !x.Vendors.IsVac && x.IsEnable && !x.IsHidden && x.DepartmentsID == DeptID, "Vendors")
                .ToList().OrderByDescending(x => x.CreateDate).OrderByDescending(p => IsVendorWorking(p.Vendors.DaysWork, p.Vendors.WorkFrom, p.Vendors.WorkTo, p.Vendors.DaysVac, p.Vendors.VacWorkFrom, p.Vendors.VacWorkTo)).Select(s => new ProductList()
            {
                image = !string.IsNullOrWhiteSpace(s.Logo) ? ProdPath + s.Logo : "",
                descripe = lang == "ar" ? s.DescAr : s.DescEn,
                price = Math.Round(_blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price - _bLGeneral.CalcProductNet(s.Discount, s.StartDiscountDate, s.EndDiscountDate, s.Price)).PriceWithVat, 2),
                discount = s.StartDiscountDate <= DateTime.Now && s.EndDiscountDate >= DateTime.Now ? Math.Round(s.Discount, 2) : 0,
                priceBefor = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price).PriceWithVat,
                discountAmount = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price).PriceWithVat - Math.Round(_blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price - _bLGeneral.CalcProductNet(s.Discount, s.StartDiscountDate, s.EndDiscountDate, s.Price)).PriceWithVat, 2),
                productId = s.ProductID,
                productName = lang == "ar" ? s.NameAr : s.NameEn,
                vendorId = s.VendorsID,
                vendorName = lang == "ar" ? s.Vendors.StoreNameAr : s.Vendors.StoreNameEn,
                isFixed = s.IsFixed,
                isHidden = s.IsHidden,
                vendorLogo = !string.IsNullOrWhiteSpace(s.Vendors.Logo) ? vendorpath + s.Vendors.Logo : "",
                weight = s.Weight,
                quantity = s.DailyQuantity,
                isLimited = s.IsLimited,
                deptName = s.DepartmentsID.ToString(),
                isVendorWorking = IsVendorWorking(s.Vendors.DaysWork, s.Vendors.WorkFrom, s.Vendors.WorkTo, s.Vendors.DaysVac, s.Vendors.VacWorkFrom, s.Vendors.VacWorkTo),
                isVendorWorkingString = lang == "ar" ? (IsVendorWorking(s.Vendors.DaysWork, s.Vendors.WorkFrom, s.Vendors.WorkTo, s.Vendors.DaysVac, s.Vendors.VacWorkFrom, s.Vendors.VacWorkTo) ? "المتجر مفتوح" : "المتجر مغلق") : (IsVendorWorking(s.Vendors.DaysWork, s.Vendors.WorkFrom, s.Vendors.WorkTo, s.Vendors.DaysVac, s.Vendors.VacWorkFrom, s.Vendors.VacWorkTo) ? "Vendor Open" : "Vendor Close"),

            }).ToList();
            if (!string.IsNullOrWhiteSpace(search))
            {
                Data = Data.Where(s => s.vendorName.ToLower().Contains(search.ToLower()) || s.productName.ToLower().Contains(search.ToLower())).ToList();
            }
            if (!string.IsNullOrWhiteSpace(deptList))
            {
                string[] listdept = deptList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                Data = Data.Where(s => listdept.Any(z => z == s.deptName)).ToList();
            }
            if (from.HasValue)
            {
                Data = Data.Where(s => s.price >= from).ToList();
            }
            if (to.HasValue)
            {
                Data = Data.Where(s => s.price <= to).ToList();
            }
            Data = Data.Skip(Convert.ToInt32(page - 1) * 10).ToList();
            var Datatake = Data.Take(((Convert.ToInt32(page) * 10)) - (Convert.ToInt32(page - 1) * 10)).ToList();
            bool NextPage = false;
            if (Data.Count() > 10)
            {
                NextPage = true;
            }
            var model = new ProdResonse();
            model.products = Datatake;
            model.isNextPage = NextPage;
            return model;
        }
        public ProdResonse GetProductListWithFavourite(string ProdPath, string lang, int page, string search, string vendorpath, int CustoemrID)
        {
            var DeliveryPriceVatPercent = _blConfiguration.GetDeliveryPriceVatPercent();
            var Data = Uow.Product.GetAll(x => !x.IsDeleted && !x.Vendors.IsDeleted && x.Vendors.IsEnable && !x.Vendors.IsVac && x.IsEnable && !x.IsHidden && x.CustomerFavourites.Any(x => x.CustomersID == CustoemrID && !x.IsDeleted), "Vendors").Select(s => new ProductList()
            {
                image = !string.IsNullOrWhiteSpace(s.Logo) ? ProdPath + s.Logo : "",
                descripe = lang == "ar" ? s.DescAr : s.DescEn,
                price = Math.Round(_blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price - _bLGeneral.CalcProductNet(s.Discount, s.StartDiscountDate, s.EndDiscountDate, s.Price)).PriceWithVat, 2),
                discount = s.StartDiscountDate <= DateTime.Now && s.EndDiscountDate >= DateTime.Now ? Math.Round(s.Discount, 2) : 0,
                priceBefor = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price).PriceWithVat,
                discountAmount = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price).PriceWithVat - Math.Round(_blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price - _bLGeneral.CalcProductNet(s.Discount, s.StartDiscountDate, s.EndDiscountDate, s.Price)).PriceWithVat, 2),
                productId = s.ProductID,
                productName = lang == "ar" ? s.NameAr : s.NameEn,
                vendorId = s.VendorsID,
                vendorName = lang == "ar" ? s.Vendors.StoreNameAr : s.Vendors.StoreNameEn,
                isFixed = s.IsFixed,
                isHidden = s.IsHidden,
                vendorLogo = !string.IsNullOrWhiteSpace(s.Vendors.Logo) ? vendorpath + s.Vendors.Logo : "",
                weight = s.Weight,
                quantity = s.DailyQuantity,
                isLimited = s.IsLimited,
                deptName = s.DepartmentsID.ToString()
            }).ToList();
            if (!string.IsNullOrWhiteSpace(search))
            {
                Data = Data.Where(s => s.vendorName.ToLower().Contains(search.ToLower()) || s.productName.ToLower().Contains(search.ToLower())).ToList();
            }
            Data = Data.Skip(Convert.ToInt32(page - 1) * 10).ToList();
            var DataTake = Data.Take(((Convert.ToInt32(page) * 10)) - (Convert.ToInt32(page - 1) * 10)).ToList();
            bool NextPage = false;
            if (Data.Count() > 10)
            {
                NextPage = true;
            }
            var model = new ProdResonse();
            model.products = DataTake;
            model.isNextPage = NextPage;
            return model;
        }
        public AllProductViewModel GetAllProducts(int pagepop, int otherpage, string search, string lang, string ProdPath, string Vendorpath, string deptList, decimal? from, decimal? to)
        {
            var DeliveryPriceVatPercent = _blConfiguration.GetDeliveryPriceVatPercent();
            var model = new AllProductViewModel();
            var Data = Uow.Product.GetAll(x => !x.IsDeleted && !x.Vendors.IsDeleted && x.Vendors.IsEnable && !x.Vendors.IsVac && !x.IsHidden && x.IsEnable, "Vendors,Departments")
                .ToList().OrderByDescending(x => x.CreateDate).OrderByDescending(p => IsVendorWorking(p.Vendors.DaysWork, p.Vendors.WorkFrom, p.Vendors.WorkTo, p.Vendors.DaysVac, p.Vendors.VacWorkFrom, p.Vendors.VacWorkTo)).Take(11).Select(s => new ProductList()
            {
                image = !string.IsNullOrWhiteSpace(s.Logo) ? ProdPath + s.Logo : "",
                descripe = lang == "ar" ? s.DescAr : s.DescEn,
                price = Math.Round(_blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price - _bLGeneral.CalcProductNet(s.Discount, s.StartDiscountDate, s.EndDiscountDate, s.Price)).PriceWithVat, 2),
                discount = s.StartDiscountDate <= DateTime.Now && s.EndDiscountDate >= DateTime.Now ? Math.Round(s.Discount, 2) : 0,
                priceBefor = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price).PriceWithVat,
                discountAmount = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price).PriceWithVat - Math.Round(_blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price - _bLGeneral.CalcProductNet(s.Discount, s.StartDiscountDate, s.EndDiscountDate, s.Price)).PriceWithVat, 2),
                productId = s.ProductID,
                productName = lang == "ar" ? s.NameAr : s.NameEn,
                vendorId = s.VendorsID,
                vendorName = lang == "ar" ? s.Vendors.StoreNameAr : s.Vendors.StoreNameEn,
                vendorLogo = !string.IsNullOrWhiteSpace(s.Vendors.Logo) ? Vendorpath + s.Vendors.Logo : "",
                isFixed = false,
                isHidden = false,
                weight = s.Weight,
                quantity = s.DailyQuantity,
                isLimited = s.IsLimited,
                deptName = s.DepartmentsID.ToString(),
                isVendorWorking = IsVendorWorking(s.Vendors.DaysWork, s.Vendors.WorkFrom, s.Vendors.WorkTo, s.Vendors.DaysVac, s.Vendors.VacWorkFrom, s.Vendors.VacWorkTo),
                isVendorWorkingString = lang == "ar" ? (IsVendorWorking(s.Vendors.DaysWork, s.Vendors.WorkFrom, s.Vendors.WorkTo, s.Vendors.DaysVac, s.Vendors.VacWorkFrom, s.Vendors.VacWorkTo) ? "المتجر مفتوح" : "المتجر مغلق") : (IsVendorWorking(s.Vendors.DaysWork, s.Vendors.WorkFrom, s.Vendors.WorkTo, s.Vendors.DaysVac, s.Vendors.VacWorkFrom, s.Vendors.VacWorkTo) ? "Vendor Open" : "Vendor Close"),

            }).ToList();
            if (!string.IsNullOrWhiteSpace(search))
            {
                Data = Data.Where(s => s.vendorName.ToLower().Contains(search.ToLower()) || s.productName.ToLower().Contains(search.ToLower())).ToList();
            }
            if (!string.IsNullOrWhiteSpace(deptList))
            {
                string[] listdept = deptList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                Data = Data.Where(s => listdept.Any(z => z == s.deptName)).ToList();
            }
            if (from.HasValue)
            {
                Data = Data.Where(s => s.price >= from).ToList();
            }
            if (to.HasValue)
            {
                Data = Data.Where(s => s.price <= to).ToList();
            }
            Data = Data.Skip(Convert.ToInt32(pagepop - 1) * 10).ToList();
            var counts = Data.Count();
            Data = Data.Take(((Convert.ToInt32(pagepop) * 10)) - (Convert.ToInt32(pagepop - 1) * 10)).ToList();
            bool NextPage = false;
            if (counts > 10)
            {
                NextPage = true;
            }
            var DataOther = Uow.Product.GetAll(x => !x.IsDeleted && !x.Vendors.IsDeleted && x.Vendors.IsEnable && !x.Vendors.IsVac && !x.IsHidden && x.IsEnable, "Vendors")
                .ToList().OrderByDescending(x => x.CreateDate).OrderByDescending(p => IsVendorWorking(p.Vendors.DaysWork, p.Vendors.WorkFrom, p.Vendors.WorkTo, p.Vendors.DaysVac, p.Vendors.VacWorkFrom, p.Vendors.VacWorkTo)).Select(s => new ProductList()
            {
                image = !string.IsNullOrWhiteSpace(s.Logo) ? ProdPath + s.Logo : "",
                descripe = lang == "ar" ? s.DescAr : s.DescEn,
                price = Math.Round(_blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price - _bLGeneral.CalcProductNet(s.Discount, s.StartDiscountDate, s.EndDiscountDate, s.Price)).PriceWithVat, 2),
                discount = s.StartDiscountDate <= DateTime.Now && s.EndDiscountDate >= DateTime.Now ? Math.Round(s.Discount, 2) : 0,
                priceBefor = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price).PriceWithVat,
                discountAmount = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price).PriceWithVat - Math.Round(_blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price - _bLGeneral.CalcProductNet(s.Discount, s.StartDiscountDate, s.EndDiscountDate, s.Price)).PriceWithVat, 2),
                productId = s.ProductID,
                productName = lang == "ar" ? s.NameAr : s.NameEn,
                vendorId = s.VendorsID,
                vendorName = lang == "ar" ? s.Vendors.StoreNameAr : s.Vendors.StoreNameEn,
                vendorLogo = !string.IsNullOrWhiteSpace(s.Vendors.Logo) ? Vendorpath + s.Vendors.Logo : "",
                isFixed = false,
                isHidden = false,
                weight = s.Weight,
                quantity = s.DailyQuantity,
                isLimited = s.IsLimited,
                deptName = s.DepartmentsID.ToString(),
                isVendorWorking = IsVendorWorking(s.Vendors.DaysWork, s.Vendors.WorkFrom, s.Vendors.WorkTo, s.Vendors.DaysVac, s.Vendors.VacWorkFrom, s.Vendors.VacWorkTo),
                isVendorWorkingString = lang == "ar" ? (IsVendorWorking(s.Vendors.DaysWork, s.Vendors.WorkFrom, s.Vendors.WorkTo, s.Vendors.DaysVac, s.Vendors.VacWorkFrom, s.Vendors.VacWorkTo) ? "المتجر مفتوح" : "المتجر مغلق") : (IsVendorWorking(s.Vendors.DaysWork, s.Vendors.WorkFrom, s.Vendors.WorkTo, s.Vendors.DaysVac, s.Vendors.VacWorkFrom, s.Vendors.VacWorkTo) ? "Vendor Open" : "Vendor Close"),
            }).ToList();
            if (!string.IsNullOrWhiteSpace(search))
            {
                DataOther = DataOther.Where(s => s.vendorName.ToLower().Contains(search.ToLower()) || s.productName.ToLower().Contains(search.ToLower())).ToList();
            }
            if (!string.IsNullOrWhiteSpace(deptList))
            {
                string[] listdept = deptList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                DataOther = DataOther.Where(s => listdept.Any(z => z == s.deptName)).ToList();
            }
            if (from.HasValue)
            {
                DataOther = DataOther.Where(s => s.price >= from).ToList();
            }
            if (to.HasValue)
            {
                DataOther = DataOther.Where(s => s.price <= to).ToList();
            }
            DataOther = DataOther.Skip(Convert.ToInt32(otherpage - 1) * 10).ToList();
            var count = DataOther.Count();
            DataOther = DataOther.Take(((Convert.ToInt32(otherpage) * 10)) - (Convert.ToInt32(otherpage - 1) * 10)).ToList();
            bool NextPageOther = false;
            if (count > 10)
            {
                NextPageOther = true;
            }
            model.listPopular = Data;
            model.isNextlistPopular = NextPage;
            model.listProducts = DataOther;
            model.isNextlistProducts = NextPageOther;
            return model;
        }
        public bool DeleteProduct(int ProductId, int userdelete)
        {
            var ProdData = Uow.Product.GetAll(s => s.ProductID == ProductId && !s.IsDeleted).FirstOrDefault();
            if (ProdData != null)
            {
                ProdData.IsDeleted = true;
                ProdData.DeleteDate = _bLGeneral.GetDateTimeNow();
                ProdData.UserDelete = userdelete;
                Uow.Product.Update(ProdData);
                Uow.Commit();
            }
            return true;
        }
        public bool UpdateProdFixed(int ProductId, int type, int userid)
        {
            var ProdData = Uow.Product.GetAll(s => s.ProductID == ProductId).FirstOrDefault();
            if (ProdData != null)
            {
                if (type == 1) //fixed
                {
                    ProdData.IsFixed = !ProdData.IsFixed;
                    ProdData.UpdateDate = _bLGeneral.GetDateTimeNow();
                    ProdData.UserUpdate = userid;

                }//hidden
                else
                {
                    ProdData.IsHidden = !ProdData.IsHidden;
                    ProdData.UpdateDate = _bLGeneral.GetDateTimeNow();
                    ProdData.UserUpdate = userid;
                }
                Uow.Product.Update(ProdData);
                Uow.Commit();
            }
            return true;
        }
        public bool ChangeStatus(int id, int userId)
        {
            var data = GetProductById(id);

            if (data != null)
            {
                data.UserEnable = userId;
                data.IsEnable = data.IsEnable == Convert.ToBoolean(StatusEnable.Enable) ? Convert.ToBoolean(StatusEnable.Disable) : Convert.ToBoolean(StatusEnable.Enable);
                data.EnableDate = _bLGeneral.GetDateTimeNow();
                data = Uow.Product.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        public bool DeleteAllProduct(int VendorID, int userdelete)
        {
            var ProdData = Uow.Product.GetAll(s => s.VendorsID == VendorID && !s.IsDeleted);
            if (ProdData != null)
            {
                foreach (var item in ProdData)
                {
                    item.IsDeleted = true;
                    item.DeleteDate = _bLGeneral.GetDateTimeNow();
                    item.UserDelete = userdelete;
                }
                Uow.Product.UpdateRang(ProdData);
                Uow.Commit();
            }
            return true;
        }
        public UpdateProductViewModel GetProductDetailsUpdateApi(int ProdId, string lang, string ProdPath, string VendorPath)
        {
            var PRod = Uow.Product.GetAll(z => !z.IsDeleted && z.ProductID == ProdId).FirstOrDefault();
            if (PRod != null)
            {
                var DeliveryPriceVatPercent = _blConfiguration.GetDeliveryPriceVatPercent();
                var Data = Uow.Product.GetAll(z => !z.IsDeleted && z.ProductID == ProdId, "Vendors").Select(s => new UpdateProductViewModel()
                {
                    price = Math.Round(_blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price - _bLGeneral.CalcProductNet(s.Discount, s.StartDiscountDate, s.EndDiscountDate, s.Price)).PriceWithVat, 2),
                    discount = s.StartDiscountDate <= DateTime.Now && s.EndDiscountDate >= DateTime.Now ? Math.Round(s.Discount, 2) : 0,
                    priceBefor = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price).PriceWithVat,
                    discountAmount = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price).PriceWithVat - Math.Round(_blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price - _bLGeneral.CalcProductNet(s.Discount, s.StartDiscountDate, s.EndDiscountDate, s.Price)).PriceWithVat, 2),
                    quantity = Math.Round(s.Quantity, 2),
                    weight = Math.Round(s.Weight, 2),
                    nameAr = s.NameAr,
                    nameEn = s.NameEn,
                    descAr = s.DescAr,
                    descEn = s.DescEn,
                    size = s.Size,
                    pieces = lang == "ar" ? s.PiecesAr : s.PiecesEn,
                    piecesar = s.PiecesAr,
                    piecesen = s.PiecesEn,
                    departmentsID = s.DepartmentsID,
                    sKU = s.SKU,
                    endDiscountDate = s.EndDiscountDate,
                    isAvailable = s.IsEnable,
                    isFixed = s.IsFixed,
                    isHidden = s.IsHidden,
                    islimited = s.IsLimited,
                    startDiscountDate = s.StartDiscountDate,
                    productId = s.ProductID,
                    logo = !string.IsNullOrWhiteSpace(s.Logo) ? ProdPath + s.Logo : "",
                    timeTaken = s.TimeTakenProcess.ToString(),
                    productTypeName = (s.ProductTypeId == (int)ProductTypeEnum.Food ? (lang == "ar" ? "اكل" : "Food") : (lang == "ar" ? "منتج جاهز للشحن" : "Product Ready Shipping")),
                    productType = s.ProductTypeId,
                    measurementId = s.MeasurementId,
                    measurementName = (s.MeasurementId == (int)MeasurementEnum.Gram ? (lang == "ar" ? "جرام" : "Gram") : (lang == "ar" ? "كيلو" : "Kilo")),
                }).FirstOrDefault();
                Data.productQuestion = Uow.ProductQuestion.GetAll(s => s.ProductID == ProdId && !s.IsDeleted).Select(e => new ProductQuestionList()
                {
                    answerAr = e.AnswerAr,
                    answerEn = e.AnswerEn,
                    questionAr = e.QuestionAr,
                    questionEn = e.QuestionEn
                }).ToList();
                if (!string.IsNullOrWhiteSpace(PRod.KeyWords))
                {
                    string[] listdr = PRod.KeyWords.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    var list = Uow.KeyWords.GetAll(s => listdr.Any(x => x == s.KeyWordsID.ToString())).Select(s => s.KeyWordsID).ToList();
                    Data.keyWords = list;
                }
                else
                {
                    Data.keyWords = new List<int>();
                }
                Data.newProductImages = Uow.ProductImage.GetAll(s => s.ProductID == ProdId && !s.IsDeleted).Select(e => new ProductImages() { imageId = e.ProductImageID, imagelink = !string.IsNullOrWhiteSpace(e.Image) ? ProdPath + e.Image : "" }).ToList();

                if (Data.newProductImages.Count() <= 0)
                {
                    Data.newProductImages.Add(new ProductImages()
                    {
                        imageId = 1,
                        imagelink = Data.logo
                    });

                }
                return Data;
            }
            else
            {
                return new UpdateProductViewModel();
            }
        }
        public ProductDetailsApi GetProductDetailsApi(int ProdId, string lang, string ProdPath, string VendorPath)
        {
            var DeliveryPriceVatPercent = _blConfiguration.GetDeliveryPriceVatPercent();
            var PRod = Uow.Product.GetAll(z => !z.IsDeleted && !z.Vendors.IsDeleted && z.Vendors.IsEnable && !z.Vendors.IsVac && !z.IsHidden && z.ProductID == ProdId, "Vendors").FirstOrDefault();
            if (PRod != null)
            {
                var Data = Uow.Product.GetAll(z => !z.IsDeleted && !z.Vendors.IsDeleted && z.Vendors.IsEnable && !z.Vendors.IsVac && !z.IsHidden && z.ProductID == ProdId, "Vendors,ProductQuestion").ToList().Select(s => new ProductDetailsApi()
                {
                    productId = s.ProductID,
                    image = !string.IsNullOrWhiteSpace(s.Logo) ? ProdPath + s.Logo : "",
                    descripe = lang == "ar" ? s.DescAr : s.DescEn,
                    price = Math.Round(_blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price - _bLGeneral.CalcProductNet(s.Discount, s.StartDiscountDate, s.EndDiscountDate, s.Price)).PriceWithVat, 2),
                    discount = s.StartDiscountDate <= DateTime.Now && s.EndDiscountDate >= DateTime.Now ? Math.Round(s.Discount, 2) : 0,
                    priceBefor = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price).PriceWithVat,
                    discountAmount = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price).PriceWithVat - Math.Round(_blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price - _bLGeneral.CalcProductNet(s.Discount, s.StartDiscountDate, s.EndDiscountDate, s.Price)).PriceWithVat, 2),
                    quantity = s.DailyQuantity,
                    isLimited = s.IsLimited,
                    weight = Math.Round(s.Weight, 2),
                    productName = lang == "ar" ? s.NameAr : s.NameEn,
                    vendorId = s.VendorsID,
                    vendorName = lang == "ar" ? s.Vendors.StoreNameAr : s.Vendors.StoreNameEn,
                    vendorImage = !string.IsNullOrWhiteSpace(s.Vendors.Logo) ? VendorPath + s.Vendors.Logo : "",
                    size = s.Size,
                    pieces = lang == "ar" ? s.PiecesAr : s.PiecesEn,
                    questionsCount = s.ProductQuestion.Count(s => !s.IsDeleted),
                    rate = 5,
                    timeTaken = s.TimeTakenProcess >= 60 ? ((int)(s.TimeTakenProcess / 60) + " " + (lang == "ar" ? "ساعة" : "Hour") +
                    ((s.TimeTakenProcess % 60) > 0 ? ((lang == "ar" ? " و " : " and ") + (int)(s.TimeTakenProcess % 60) + " " + (lang == "ar" ? "دقيقة" : "minute")) : ""))
                    : ((s.TimeTakenProcess % 60) > 0 ? ((int)(s.TimeTakenProcess % 60) + " " + (lang == "ar" ? "دقيقة" : "minute")) : ""),
                    productTypeName = (s.ProductTypeId == (int)ProductTypeEnum.Food ? (lang == "ar" ? "اكل" : "Food") : (lang == "ar" ? "منج جاهز للشحن" : "Product Ready Shipping")),
                    isVendorWorking = IsVendorWorking(s.Vendors.DaysWork, s.Vendors.WorkFrom, s.Vendors.WorkTo, s.Vendors.DaysVac, s.Vendors.VacWorkFrom, s.Vendors.VacWorkTo),
                    isVendorWorkingString = lang == "ar" ? (IsVendorWorking(s.Vendors.DaysWork, s.Vendors.WorkFrom, s.Vendors.WorkTo, s.Vendors.DaysVac, s.Vendors.VacWorkFrom, s.Vendors.VacWorkTo) ? "المتجر مفتوح" : "المتجر مغلق") : (IsVendorWorking(s.Vendors.DaysWork, s.Vendors.WorkFrom, s.Vendors.WorkTo, s.Vendors.DaysVac, s.Vendors.VacWorkFrom, s.Vendors.VacWorkTo) ? "Vendor Open" : "Vendor Close"),

                }).FirstOrDefault();
                var listBanners = new List<Banners>();
                if (!string.IsNullOrWhiteSpace(Data.image))
                {
                    listBanners.Add(new Banners()
                    {
                        image = Data.image
                    });
                }
                Data.banners = listBanners;
                Data.banners.AddRange(Uow.ProductImage.GetAll(s => s.ProductID == ProdId && !s.IsDeleted).Select(e => new Banners() { image = !string.IsNullOrWhiteSpace(e.Image) ? ProdPath + e.Image : "" }).ToList());
                Data.questions = Uow.ProductQuestion.GetAll(s => s.ProductID == ProdId && !s.IsDeleted).Select(e => new ProdQuestions() { answer = lang == "ar" ? e.AnswerAr : e.AnswerEn, question = lang == "ar" ? e.QuestionAr : e.QuestionEn }).ToList();
                if (!string.IsNullOrWhiteSpace(PRod.KeyWords))
                {
                    string[] listdr = PRod.KeyWords.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    Data.keyWords = Uow.KeyWords.GetAll(s => listdr.Any(x => x == s.KeyWordsID.ToString())).Select(e => new KeyWords() { word = lang == "ar" ? e.NameAR : e.NameEN }).ToList();
                }
                else
                {
                    Data.keyWords = new List<KeyWords>();
                }
                return Data;
            }
            else
            {
                return new ProductDetailsApi();
            }
        }
        public CustomerHomeViewModel GetCustomerHome(string lang, int page, string sliderpath, string DeptPath, string ProdPath, string vendorimage)
        {
            var DeliveryPriceVatPercent = _blConfiguration.GetDeliveryPriceVatPercent();
            var Response = new CustomerHomeViewModel();
            try
            {
                Response.banners = Uow.Slider.GetAll(s => s.DisplayIn == (int)DisplayInEnum.App && !s.IsDeleted && s.SliderTypeId == (int)SliderTypeEnum.Banner && s.IsEnable).Select(s => new BannerList() { image = !string.IsNullOrWhiteSpace(s.Image) ? sliderpath + s.Image : "", sliderId = s.SliderID, title = lang == "ar" ? s.NameAR : s.NameEN }).ToList();
                Response.advertisings = Uow.Slider.GetAll(s => !s.IsDeleted && s.IsEnable && s.SliderTypeId == (int)SliderTypeEnum.ADV).Select(s => new AdvertisingList() { image = !string.IsNullOrWhiteSpace(s.Image) ? sliderpath + s.Image : "" }).ToList();
                Response.departments = Uow.Departments.GetAll(x => !x.IsDeleted && x.IsEnable && x.Arrange != null).OrderBy(x => x.Arrange).Take(3).Select(s => new DeptViewModelApi() { title = lang == "ar" ? s.NameAR : s.NameEN, image = !string.IsNullOrWhiteSpace(s.Image) ? DeptPath + s.Image : "", departmentID = s.DepartmentsID }).ToList();
                var products = Uow.Product.GetAll(x => !x.IsDeleted && !x.Vendors.IsDeleted && x.IsFavourite && x.Vendors.IsEnable && !x.Vendors.IsVac && x.IsEnable && !x.IsHidden, "Vendors").ToList()
                    .OrderByDescending(x => x.CreateDate).OrderByDescending(p => IsVendorWorking(p.Vendors.DaysWork, p.Vendors.WorkFrom, p.Vendors.WorkTo, p.Vendors.DaysVac, p.Vendors.VacWorkFrom, p.Vendors.VacWorkTo)).Select(s => new ProductList()
                {
                    image = !string.IsNullOrWhiteSpace(s.Logo) ? ProdPath + s.Logo : "",
                    descripe = lang == "ar" ? s.DescAr : s.DescEn,
                    price = Math.Round(_blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price - _bLGeneral.CalcProductNet(s.Discount, s.StartDiscountDate, s.EndDiscountDate, s.Price)).PriceWithVat, 2),
                    discount = s.StartDiscountDate <= DateTime.Now && s.EndDiscountDate >= DateTime.Now ? Math.Round(s.Discount, 2) : 0,
                    priceBefor = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price).PriceWithVat,
                    discountAmount = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price).PriceWithVat - Math.Round(_blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price - _bLGeneral.CalcProductNet(s.Discount, s.StartDiscountDate, s.EndDiscountDate, s.Price)).PriceWithVat, 2),
                    productId = s.ProductID,
                    productName = lang == "ar" ? s.NameAr : s.NameEn,
                    vendorId = s.VendorsID,
                    vendorName = lang == "ar" ? s.Vendors.StoreNameAr : s.Vendors.StoreNameEn,
                    isFixed = s.IsFixed,
                    isHidden = s.IsHidden,
                    vendorLogo = !string.IsNullOrWhiteSpace(s.Vendors.Logo) ? vendorimage + s.Vendors.Logo : "",
                    weight = s.Weight,
                    quantity = s.DailyQuantity,
                    isLimited = s.IsLimited,
                    deptName = s.DepartmentsID.ToString(),
                    isVendorWorking = IsVendorWorking(s.Vendors.DaysWork, s.Vendors.WorkFrom, s.Vendors.WorkTo, s.Vendors.DaysVac, s.Vendors.VacWorkFrom, s.Vendors.VacWorkTo),
                    isVendorWorkingString = lang == "ar" ? (IsVendorWorking(s.Vendors.DaysWork, s.Vendors.WorkFrom, s.Vendors.WorkTo, s.Vendors.DaysVac, s.Vendors.VacWorkFrom, s.Vendors.VacWorkTo) ? "المتجر مفتوح" : "المتجر مغلق") : (IsVendorWorking(s.Vendors.DaysWork, s.Vendors.WorkFrom, s.Vendors.WorkTo, s.Vendors.DaysVac, s.Vendors.VacWorkFrom, s.Vendors.VacWorkTo) ? "Vendor Open" : "Vendor Close"),

                }
                ).ToList();
                var model = new ProdResonse();
                model.products = products;
                Response.productList = model;
                Response.productList.products = Response.productList.products.Skip(Convert.ToInt32(page - 1) * 10).ToList();
                var count = Response.productList.products.Count();
                Response.productList.products = Response.productList.products.Take(((Convert.ToInt32(page) * 10)) - (Convert.ToInt32(page - 1) * 10)).ToList();
                bool NextPage = false;
                if (count > 10)
                {
                    NextPage = true;
                }
                Response.productList.isNextPage = NextPage;
                return Response;
            }
            catch (Exception ex)
            {
                var list = new List<AdvertisingList>();
                list.Add(new AdvertisingList() { image = ex.Message });
                Response.advertisings = list;
                return Response;
            }
        }
        public IQueryable<ProductQuestionViewModel> GetAllProductQuestionViewModelByProduct(int? ProductID, string lang)
        {
            return Uow.ProductQuestion.GetAll(p => !p.IsDeleted && p.ProductID == ProductID).OrderByDescending(p => p.CreateDate).Select(p => new ProductQuestionViewModel()
            {
                ProductID = p.ProductID,
                ProductQuestionGuid = p.ProductQuestionGuid,
                ProductQuestionID = p.ProductQuestionID,
                IsEnable = p.IsEnable,
                EnableDate = p.EnableDate,
                IsDeleted = p.IsDeleted,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                UserEnable = p.UserEnable,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                AnswerAr = p.AnswerAr,
                AnswerEn = p.AnswerEn,
                QuestionAr = p.QuestionAr,
                QuestionEn = p.QuestionEn,
                Question = lang == "ar" ? p.QuestionAr : p.QuestionEn,
                Answer = lang == "ar" ? p.AnswerAr : p.AnswerEn,
            });
        }
        public ProductQuestionViewModelApi GetAllProductQuestionViewModelApiByProduct(int? ProductID, string lang, int page)
        {
            var Data = Uow.ProductQuestion.GetAll(p => !p.IsDeleted && p.ProductID == ProductID).OrderByDescending(p => p.CreateDate).Select(p => new ListProductQuestionViewModelApi()
            {
                productID = p.ProductID,
                productQuestionID = p.ProductQuestionID,
                question = lang == "ar" ? p.QuestionAr : p.QuestionEn,
                answer = lang == "ar" ? (!string.IsNullOrEmpty(p.AnswerAr) ? p.AnswerAr : string.Empty) : (!string.IsNullOrEmpty(p.AnswerEn) ? p.AnswerEn : string.Empty),
            }).ToList();
            Data = Data.Skip(Convert.ToInt32(page - 1) * 10).ToList();
            var DataTake = Data.Take(((Convert.ToInt32(page) * 10)) - (Convert.ToInt32(page - 1) * 10)).ToList();
            bool NextPage = false;
            if (Data.Count() > 10)
            {
                NextPage = true;
            }
            var model = new ProductQuestionViewModelApi();
            model.questions = DataTake;
            model.isNextPage = NextPage;
            return model;
        }
        public IQueryable<ProdQuestionViewModel> GetAllProdQuestionViewModelByProduct(int? ProductID, string lang)
        {
            return Uow.ProdQuestion.GetAll(p => !p.IsDeleted && p.IsEnable && p.ProductID == ProductID).OrderByDescending(p => p.CreateDate).Select(p => new ProdQuestionViewModel()
            {
                ProductID = p.ProductID,
                ProdQuestionGuid = p.ProdQuestionGuid,
                ProdQuestionID = p.ProdQuestionID,
                Question = p.Question,
                Answer = p.Answer,
                CustomersID = p.CustomersID,
                IsRepley = p.IsRepley,
                CreateDate = p.CreateDate,
            });
        }
        public List<ProductListSite> GetAllProducts(string lang, string ProdPath, string Vendorpath)
        {
            var DeliveryPriceVatPercent = _blConfiguration.GetDeliveryPriceVatPercent();
          //  var Data = Uow.Product;
           // var ADData = Uow.Product.GetAll(x => !x.IsDeleted && !x.Vendors.IsDeleted && x.Vendors.IsEnable && !x.Vendors.IsVac && !x.IsHidden && x.IsEnable, "Vendors").ToList();
            var AData = Uow.Product.GetAll(x => !x.IsDeleted && !x.Vendors.IsDeleted && x.Vendors.IsEnable && !x.Vendors.IsVac && !x.IsHidden && x.IsEnable, "Vendors").ToList()
                .OrderByDescending(x => x.ProductOrder).OrderByDescending(p => IsVendorWorking(p.Vendors.DaysWork, p.Vendors.WorkFrom, p.Vendors.WorkTo, p.Vendors.DaysVac, p.Vendors.VacWorkFrom, p.Vendors.VacWorkTo)).Take(20)
                .Select(s => new ProductListSite()
            {
                image = !string.IsNullOrWhiteSpace(s.Logo) ? ProdPath + s.Logo : "",
                descripe = lang == "ar" ? s.DescAr : s.DescEn,
                price = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price - _bLGeneral.CalcProductNet(s.Discount, s.StartDiscountDate, s.EndDiscountDate, s.Price)).PriceWithVat,
                Discount = _bLGeneral.CalcProductNet(s.Discount, s.StartDiscountDate, s.EndDiscountDate, s.Price),
                productId = s.ProductID,
                productName = lang == "ar" ? s.NameAr : s.NameEn,
                vendorId = s.VendorsID,
                vendorName = lang == "ar" ? s.Vendors.StoreNameAr : s.Vendors.StoreNameEn,
                vendorLogo = !string.IsNullOrWhiteSpace(s.Vendors.Logo) ? Vendorpath + s.Vendors.Logo : "~/assets/homeMadeSite/img/Group%208.svg",
                isFixed = false,
                isHidden = false,
                weight = s.Weight,
                VendorsGuid = s.Vendors.VendorsGuid,
                ProductGuid = s.ProductGuid,
                Pieces = lang == "ar" ? s.PiecesAr : s.PiecesEn,
                IsLimited = s.IsLimited,
                Quantity = s.Quantity,
                VendorsDaysWork = s.Vendors.DaysWork,
                VendorsWorkFrom = s.Vendors.WorkFrom,
                VendorsWorkTo = s.Vendors.WorkTo,
                VendorsDaysVac = s.Vendors.DaysVac,
                VendorsVacWorkFrom = s.Vendors.VacWorkFrom,
                VendorsVacWorkTo = s.Vendors.VacWorkTo,
            }).Take(10).ToList();
            return AData;
        }
        public MasterProductsViewModelApi GetProductBySubCatIDSerach(string ClientImageView, int? departmentsID, int ProductVendorsID, int page, string serach, string lang)
        {
            var xdata = Uow.Product.GetAll(x => !x.IsDeleted && x.VendorsID == ProductVendorsID &&
            (departmentsID != null ? x.DepartmentsID == departmentsID : true), "Departments").OrderBy(p => p.DepartmentsID).OrderByDescending(p => p.CreateDate)
             .Select(p => new ProductWithoutApiViewModel
             {
                 productID = p.ProductID,
                 productName = lang == "ar" ? p.NameAr : p.NameEn,
                 price = p.Price.ToString("N2"),
                 weight = p.Weight,
                 image = !string.IsNullOrEmpty(p.Logo) ? (ClientImageView + p.Logo) : "/Images/noImage.png",
             }).ToList();
            if (!string.IsNullOrEmpty(serach))
            {
                xdata = xdata.Where(x => x.productName.Contains(serach) || x.price.Contains(serach)).ToList();
            }
            xdata = xdata.Skip(Convert.ToInt32(page - 1) * 10).ToList();
            var DataTake = xdata.Take(((Convert.ToInt32(page) * 10)) - (Convert.ToInt32(page - 1) * 10)).ToList();
            bool NextPage = false;
            if (xdata.Count() > 10)
            {
                NextPage = true;
            }
            var model = new MasterProductsViewModelApi();
            model.products = DataTake;
            model.isNextPage = NextPage;
            return model;
        }
        #endregion
        #region Action
        public bool RemoveFromFavouriteProducts(int ProductId, int userdelete)
        {
            var ProdData = Uow.Product.GetAll(s => s.ProductID == ProductId && !s.IsDeleted).FirstOrDefault();
            if (ProdData != null)
            {
                ProdData.IsFavourite = false;
                ProdData.UpdateDate = _bLGeneral.GetDateTimeNow();
                ProdData.UserUpdate = userdelete;
                Uow.Product.Update(ProdData);
                Uow.Commit();
            }
            return true;
        }
        public bool AddToFavouriteProducts(int ProductId, int userdelete)
        {
            var ProdData = Uow.Product.GetAll(s => s.ProductID == ProductId && !s.IsDeleted).FirstOrDefault();
            if (ProdData != null)
            {
                ProdData.IsFavourite = true;
                ProdData.UpdateDate = _bLGeneral.GetDateTimeNow();
                ProdData.UserUpdate = userdelete;
                Uow.Product.Update(ProdData);
                Uow.Commit();
            }
            return true;
        }
        public bool Insert(ProductViewModel model, int UserId, out int ProductID)
        {
            var KeyWords = "";
            if (model.KeyWords != null && model.KeyWords.Count() > 0)
            {
                if (model.KeyWords.FirstOrDefault() != 0)
                {
                    foreach (var item in model.KeyWords)
                    {
                        if (!string.IsNullOrWhiteSpace(KeyWords))
                        {
                            KeyWords = KeyWords + "," + item.ToString();
                        }
                        else
                        {
                            KeyWords = item.ToString();
                        }
                    }
                }
            }
            if (string.IsNullOrEmpty(model.TimeTakenProcessHours))
            {
                model.TimeTakenProcessHours = "0";
            }
            if (string.IsNullOrEmpty(model.TimeTakenProcess))
            {
                model.TimeTakenProcess = "0";
            }
            Product Products = new Product()
            {
                CreateDate = _bLGeneral.GetDateTimeNow(),
                ProductGuid = Guid.NewGuid(),
                IsDeleted = false,
                UserId = UserId,
                DepartmentsID = model.DepartmentsID,
                DescAr = model.DescAr,
                DescEn = model.DescEn,
                Discount = model.Discount != null ? decimal.Parse(model.Discount, CultureInfo.InvariantCulture) : 0,
                IsEnable = model.IsAvailable,
                IsFixed = model.IsFixed,
                IsHidden = model.IsHidden,
                NameAr = model.NameAr,
                NameEn = !string.IsNullOrEmpty(model.NameEn) ? model.NameEn : model.NameAr,
                Price = model.Price != null ? decimal.Parse(model.Price, CultureInfo.InvariantCulture) : 0,
                SKU = model.SKU,
                VendorsID = model.VendorsID,
                Weight = model.Weight != null ? decimal.Parse(model.Weight, CultureInfo.InvariantCulture) : 0,
                Size = model.Size,
                PiecesAr = model.PiecesAr,
                PiecesEn = model.PiecesEn,
                ProductOrder = model.ProductOrder??100,
                ProductQuantity = model.ProductQuantity,
                ProductQuantityType = model.ProductQuantityType,
                KeyWords = KeyWords,
                Logo = !string.IsNullOrEmpty(model.Logo) ? model.Logo : "logo.svg",
                ProductTypeId = model.ProductTypeId,
                TimeTakenProcess = model.TimeTakenProcess != null ? (decimal.Parse(model.TimeTakenProcess, CultureInfo.InvariantCulture) + (model.TimeTakenProcess != null ? (decimal.Parse(model.TimeTakenProcessHours, CultureInfo.InvariantCulture) * 60) : 0)) :
                    model.TimeTakenProcessHours != null ? (decimal.Parse(model.TimeTakenProcessHours, CultureInfo.InvariantCulture) * 60) : 0,
                IsLimited = model.IsLimited,
                Quantity = model.IsLimited ? (model.Quantity != null ? decimal.Parse(model.Quantity, CultureInfo.InvariantCulture) : 0) : 0,
                DailyQuantity = model.IsLimited ? (model.Quantity != null ? decimal.Parse(model.Quantity, CultureInfo.InvariantCulture) : 0) : 0,
                MeasurementId = model.MeasurementId,
            };
            if (!string.IsNullOrEmpty(model.StartDiscountDate))
            {
                Products.StartDiscountDate = DateTime.ParseExact(model.StartDiscountDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrEmpty(model.EndDiscountDate))
            {
                Products.EndDiscountDate = DateTime.ParseExact(model.EndDiscountDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            Products = Uow.Product.Insert(Products);
            Uow.Commit();
            ProductID = Products.ProductID;
            return true;
        }
        public bool Update(ProductViewModel model, int UserId, string MainPath, string MainPathView)
        {

            var KeyWords = "";
            if (model.KeyWords != null && model.KeyWords.Count() > 0)
            {
                if (model.KeyWords.FirstOrDefault() != 0)
                {
                    foreach (var item in model.KeyWords)
                    {
                        if (!string.IsNullOrWhiteSpace(KeyWords))
                        {
                            KeyWords = KeyWords + "," + item.ToString();
                        }
                        else
                        {
                            KeyWords = item.ToString();
                        }
                    }
                }
            }
            var Product = GetProductById(model.ProductID);
            if (Product != null)
            {
                if (!string.IsNullOrEmpty(model.Logo))
                {
                    if (MainPathView + Product.Logo != model.Logo)
                    {
                        string FileName = Guid.NewGuid() + ".png";
                        var Photo = FileName;
                        _bLGeneral.SaveImage(new BLGeneral.SaveImageModel
                        {
                            base64 = model.Logo,
                            key = "",
                            fileName = FileName,
                            mainPath = MainPath
                        });
                        Product.Logo = Photo;
                    }
                }
                Product.UpdateDate = _bLGeneral.GetDateTimeNow();
                Product.DepartmentsID = model.DepartmentsID;
                Product.DescAr = model.DescAr;
                Product.DescEn = model.DescEn;
                Product.Discount = decimal.Parse(model.Discount, CultureInfo.InvariantCulture);
                Product.IsEnable = model.IsAvailable;
                Product.IsFixed = model.IsFixed;
                Product.IsHidden = model.IsHidden;
                if (!string.IsNullOrEmpty(model.StartDiscountDate))
                {
                    Product.StartDiscountDate = DateTime.ParseExact(model.StartDiscountDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                }
                if (!string.IsNullOrEmpty(model.EndDiscountDate))
                {
                    Product.EndDiscountDate = DateTime.ParseExact(model.EndDiscountDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                }
                Product.NameAr = model.NameAr;
                Product.NameEn = !string.IsNullOrEmpty(model.NameEn) ? model.NameEn : model.NameAr;
                Product.Price = decimal.Parse(model.Price, CultureInfo.InvariantCulture);
                Product.SKU = model.SKU;
                Product.VendorsID = model.VendorsID;
                Product.Weight = decimal.Parse(model.Weight, CultureInfo.InvariantCulture);
                Product.Size = model.Size;
                Product.PiecesAr = model.PiecesAr;
                Product.PiecesEn = model.PiecesEn;
                Product.KeyWords = KeyWords;
                Product.ProductTypeId = model.ProductTypeId;
                Product.TimeTakenProcess = model.TimeTakenProcess != null ? (decimal.Parse(model.TimeTakenProcess, CultureInfo.InvariantCulture) + (model.TimeTakenProcess != null ? (decimal.Parse(model.TimeTakenProcessHours, CultureInfo.InvariantCulture) * 60) : 0)) :
                model.TimeTakenProcessHours != null ? (decimal.Parse(model.TimeTakenProcessHours, CultureInfo.InvariantCulture) * 60) : 0;
                Product.IsLimited = model.IsLimited;
                Product.Quantity = model.IsLimited ? (model.Quantity != null ? decimal.Parse(model.Quantity, CultureInfo.InvariantCulture) : 0) : 0;
                Product.DailyQuantity = model.IsLimited ? (model.Quantity != null ? decimal.Parse(model.Quantity, CultureInfo.InvariantCulture) : 0) : 0;
                Product.MeasurementId = model.MeasurementId;
                Product.ProductOrder = model.ProductOrder??100;
                Product.ProductQuantity = model.ProductQuantity;
                Product.ProductQuantityType = model.ProductQuantityType;
                Uow.Product.Update(Product);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ProductRepeat(int productId, int UserId, int VendorsID)
        {

            var proddata = Uow.Product.GetAll(e => e.ProductID == productId, "ProductImage,ProductQuestion").FirstOrDefault();
            Product product = new Product()
            {
                CreateDate = _bLGeneral.GetDateTimeNow(),
                ProductGuid = Guid.NewGuid(),
                IsDeleted = false,
                UserId = UserId,
                DepartmentsID = proddata.DepartmentsID,
                DescAr = proddata.DescAr,
                DescEn = proddata.DescEn,
                Discount = proddata.Discount,
                IsEnable = proddata.IsEnable,
                IsLimited = proddata.IsLimited,
                IsFixed = proddata.IsFixed,
                IsHidden = proddata.IsHidden,
                EndDiscountDate = proddata.EndDiscountDate,
                Logo = proddata.Logo,
                NameAr = proddata.NameAr + " - مكرر (" + productId.ToString() + ")",
                NameEn = proddata.NameEn + " - Duplicate (" + productId.ToString() + ")",
                Price = proddata.Price,
                Quantity = proddata.Quantity,
                DailyQuantity = proddata.Quantity,
                SKU = proddata.SKU,
                StartDiscountDate = proddata.StartDiscountDate,
                VendorsID = proddata.VendorsID,
                Weight = proddata.Weight,
                Size = proddata.Size,
                PiecesAr = proddata.PiecesAr,
                PiecesEn = proddata.PiecesEn,
                KeyWords = proddata.KeyWords,
                TimeTakenProcess = proddata.TimeTakenProcess,
                ProductTypeId = proddata.ProductTypeId
            };
            foreach (var item in proddata.ProductImage)
            {
                var Image = new ProductImage()
                {
                    CreateDate = _bLGeneral.GetDateTimeNow(),
                    ProductImageGuid = Guid.NewGuid(),
                    IsDeleted = false,
                    UserId = UserId,
                    IsEnable = true,
                    Image = item.Image,
                };
                product.ProductImage.Add(Image);
            }
            foreach (var item in proddata.ProductQuestion)
            {
                var question = new ProductQuestion()
                {
                    CreateDate = _bLGeneral.GetDateTimeNow(),
                    ProductQuestionGuid = Guid.NewGuid(),
                    IsDeleted = false,
                    UserId = UserId,
                    IsEnable = true,
                    AnswerAr = item.AnswerAr,
                    AnswerEn = item.AnswerEn,
                    QuestionAr = item.QuestionAr,
                    QuestionEn = item.QuestionEn,
                };
                product.ProductQuestion.Add(question);
            }

            Uow.Product.Insert(product);
            Uow.Commit();
            return true;
        }
        public bool AddProduct(AddProductViewModel model, string logofileName, List<string> productImage, int UserId, int VendorsID)
        {
            var KeyWords = "";
            if (model.keyWords.Count() > 0)
            {
                if (model.keyWords.FirstOrDefault() != 0)
                {
                    foreach (var item in model.keyWords)
                    {
                        if (!string.IsNullOrWhiteSpace(KeyWords))
                        {
                            KeyWords = KeyWords + "," + item.ToString();
                        }
                        else
                        {
                            KeyWords = item.ToString();
                        }
                    }
                }
            }
            Product product = new Product()
            {
                CreateDate = _bLGeneral.GetDateTimeNow(),
                ProductGuid = Guid.NewGuid(),
                IsDeleted = false,
                UserId = UserId,
                DepartmentsID = model.departmentsID,
                DescAr = model.descAr,
                DescEn = model.descEn,
                Discount = model.discount != null ? (decimal)model.discount : 0,
                IsEnable = model.isAvailable,
                IsFixed = model.isFixed,
                IsHidden = model.isHidden,
                IsLimited = model.islimited != null ? (bool)model.islimited : false,
                MeasurementId = model.measurementId != null ? (int)model.measurementId : (int)MeasurementEnum.Gram,
                EndDiscountDate = model.endDiscountDate,
                Logo = logofileName,
                NameAr = model.nameAr,
                NameEn = model.nameEn,
                Price = model.price,
                Quantity = model.quantity,
                DailyQuantity = model.quantity,
                SKU = model.sKU,
                StartDiscountDate = model.startDiscountDate,
                VendorsID = VendorsID,
                Weight = model.weight,
                Size = model.size,
                PiecesAr = model.piecesar,
                PiecesEn = model.piecesen,
                KeyWords = KeyWords,
                TimeTakenProcess = Convert.ToDecimal(model.timeTaken),
                ProductTypeId = model.productType,
            };
            foreach (var item in productImage)
            {
                var Image = new ProductImage()
                {
                    CreateDate = _bLGeneral.GetDateTimeNow(),
                    ProductImageGuid = Guid.NewGuid(),
                    IsDeleted = false,
                    UserId = UserId,
                    IsEnable = true,
                    Image = item,
                };
                product.ProductImage.Add(Image);
            }
            foreach (var item in model.productQuestion)
            {
                var question = new ProductQuestion()
                {
                    CreateDate = _bLGeneral.GetDateTimeNow(),
                    ProductQuestionGuid = Guid.NewGuid(),
                    IsDeleted = false,
                    UserId = UserId,
                    IsEnable = true,
                    AnswerAr = item.answerAr,
                    AnswerEn = item.answerEn,
                    QuestionAr = item.questionAr,
                    QuestionEn = item.questionEn,
                };
                product.ProductQuestion.Add(question);
            }

            Uow.Product.Insert(product);
            Uow.Commit();
            return true;
        }
        public bool UpdateProduct(UpdateProductViewModel model, string logofileName, List<string> productImage, int UserId, int VendorsID)
        {

            var ProdData = Uow.Product.GetAll(s => s.ProductID == model.productId).FirstOrDefault();
            if (ProdData != null)
            {
                var KeyWords = "";
                if (model.keyWords.Count() > 0)
                {
                    foreach (var item in model.keyWords)
                    {
                        if (!string.IsNullOrWhiteSpace(KeyWords))
                        {
                            KeyWords = KeyWords + "," + item.ToString();
                        }
                        else
                        {
                            KeyWords = item.ToString();
                        }
                    }
                }
                if (!string.IsNullOrWhiteSpace(logofileName))
                {
                    ProdData.Logo = logofileName;
                }
                ProdData.UpdateDate = _bLGeneral.GetDateTimeNow();
                ProdData.ProductGuid = Guid.NewGuid();
                ProdData.IsDeleted = false;
                ProdData.UserId = UserId;
                ProdData.DepartmentsID = model.departmentsID;
                ProdData.DescAr = model.descAr;
                ProdData.DescEn = model.descEn;
                ProdData.Discount = model.discount != null ? (decimal)model.discount : 0;
                ProdData.IsEnable = model.isAvailable;
                ProdData.IsFixed = model.isFixed;
                ProdData.IsHidden = model.isHidden;
                ProdData.EndDiscountDate = model.endDiscountDate;
                ProdData.NameAr = model.nameAr;
                ProdData.NameEn = model.nameEn;
                ProdData.Price = model.price;
                ProdData.Quantity = model.quantity;
                ProdData.DailyQuantity = model.quantity;
                ProdData.SKU = model.sKU;
                ProdData.StartDiscountDate = model.startDiscountDate;
                ProdData.VendorsID = VendorsID;
                ProdData.Weight = model.weight;
                ProdData.Size = model.size;
                ProdData.PiecesAr = model.piecesar;
                ProdData.PiecesEn = model.piecesen;
                ProdData.KeyWords = KeyWords;
                ProdData.TimeTakenProcess = Convert.ToDecimal(model.timeTaken);
                ProdData.ProductTypeId = model.productType;
                ProdData.IsLimited = model.islimited != null ? (bool)model.islimited : false;
                ProdData.MeasurementId = model.measurementId != null ? (int)model.measurementId : (int)MeasurementEnum.Gram;
                var DataoldImages = Uow.ProductImage.GetAll(s => !s.IsDeleted && s.ProductID == model.productId);
                foreach (var oldimage in DataoldImages)
                {
                    if (!model.newProductImages.Any(x => x.imageId == oldimage.ProductImageID))
                    {
                        oldimage.IsDeleted = true;
                        oldimage.DeleteDate = _bLGeneral.GetDateTimeNow();
                        oldimage.UserDelete = UserId;
                        Uow.ProductImage.Update(oldimage);
                    }
                }

                foreach (var item in productImage)
                {
                    var Image = new ProductImage()
                    {
                        CreateDate = _bLGeneral.GetDateTimeNow(),
                        ProductImageGuid = Guid.NewGuid(),
                        IsDeleted = false,
                        UserId = UserId,
                        IsEnable = true,
                        Image = item,
                    };
                    ProdData.ProductImage.Add(Image);
                }
                var oldquestion = Uow.ProductQuestion.GetAll(s => s.ProductID == model.productId);
                Uow.ProductQuestion.DeleteRang(oldquestion);
                foreach (var item in model.productQuestion)
                {
                    var question = new ProductQuestion()
                    {
                        CreateDate = _bLGeneral.GetDateTimeNow(),
                        ProductQuestionGuid = Guid.NewGuid(),
                        IsDeleted = false,
                        UserId = UserId,
                        IsEnable = true,
                        AnswerAr = item.answerAr,
                        AnswerEn = item.answerEn,
                        QuestionAr = item.questionAr,
                        QuestionEn = item.questionEn,
                    };
                    ProdData.ProductQuestion.Add(question);
                }
                Uow.Product.Update(ProdData);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        #region ProductQuestions
        public bool InsertProductQuestion(ProductQuestionViewModel questionModel, out int productQuestionID)
        {
            ProductQuestion question = new ProductQuestion
            {
                QuestionEn = questionModel.QuestionEn,
                QuestionAr = questionModel.QuestionAr,
                AnswerEn = questionModel.AnswerEn,
                AnswerAr = questionModel.AnswerAr,
                CreateDate = _bLGeneral.GetDateTimeNow(),
                UserId = questionModel.UserId,
                IsEnable = true,
                ProductQuestionGuid = Guid.NewGuid(),
                IsDeleted = false,
                ProductID = questionModel.ProductID,
            };
            question = Uow.ProductQuestion.Insert(question);
            Uow.Commit();
            productQuestionID = question.ProductQuestionID;
            return true;
        }
        public bool AddQuestionProd(string questionMessage, int ProductID, int CustomerID, int UserId, string FireBaseKey)
        {
            var Data = new ProdQuestion()
            {
                IsDeleted = false,
                IsEnable = false,
                IsRepley = false,
                CreateDate = _bLGeneral.GetDateTimeNow(),
                CustomersID = CustomerID,
                ProductID = ProductID,
                Question = questionMessage,
                ProdQuestionGuid = Guid.NewGuid(),
                Answer = string.Empty,
                UserId = UserId
            };
            Uow.ProdQuestion.Insert(Data);
            Uow.Commit();
            var ratedate = Uow.ProdQuestion.GetAll(s => s.ProdQuestionID == Data.ProdQuestionID, "Product.Vendors").FirstOrDefault();
            var Noti = new Notification()
            {
                CreateDate = _bLGeneral.GetDateTimeNow(),
                TitleAR = "تم إرسال استفسار علي المنتج " + ratedate.Product.NameAr + "",
                TitleEN = "customer has Inquiry on product " + ratedate.Product.NameEn + "",
                DescEN = questionMessage,
                DescAR = questionMessage,
                IsDeleted = false,
                IsEnable = true,
                IsSeen = false,
                NotificationGuid = Guid.NewGuid(),
                NotificationTypeID = (int)NotificationTypeEnum.Question,
                UserId = UserId,
                NotificationDate = _bLGeneral.GetDateTimeNow(),
                ProdQuestionID = ratedate.ProdQuestionID,
                VendorsID = ratedate.Product.VendorsID,
            };
            Uow.Notification.Insert(Noti);
            var _PushListVendor = new PushList()
            {
                orderId = ratedate.ProdQuestionID,
                lat = 0,
                lng = 0,
                trackNo = "",
                profit = "",
                cusotmerAddress = "",
                vendorAddress = "",
                vendorLogo = "",
                vendorName = "",
                body = "تم إرسال استفسار علي المنتج " + ratedate.Product.NameAr + "",
                sound = "custom_sound.wav", //For IOS
                title = "استفسار علي منتج",
                content_available = "true", //For Android
                priority = "high", //For Android,
                serverKey = FireBaseKey,
                estimation = 20,
                pushType = (int)PushTypeEnum.Question
            };
            var UserDataVendor = _blTokens.GetMobileDataByUserID(ratedate.Product.Vendors.UserId);
            if (UserDataVendor != null)
            {
                var res = _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
            }

            Uow.Commit();
            return true;
        }
        public bool UpdateFavorites(int CustomersID, int ProductID, int UserId)
        {
            var data = GetCustomerFavourites(UserId, ProductID);
            if (data == null)
            {
                CustomerFavourites customerFavourites = new CustomerFavourites
                {
                    CreateDate = _bLGeneral.GetDateTimeNow(),
                    UserId = UserId,
                    IsEnable = true,
                    CustomerFavouritesGuid = Guid.NewGuid(),
                    IsDeleted = false,
                    ProductID = ProductID,
                    CustomersID = CustomersID,
                };
                Uow.CustomerFavourites.Insert(customerFavourites);
            }
            else
            {
                if (data.IsDeleted == true)
                {
                    data.IsDeleted = false;
                }
                else
                {
                    data.IsDeleted = true;
                    data.DeleteDate = _bLGeneral.GetDateTimeNow();
                    data.UserDelete = UserId;
                }
                Uow.CustomerFavourites.Update(data);
            }
            Uow.Commit();
            return true;
        }
        public bool DeleteProductQuestion(int id, int userId)
        {
            var question = GetProductQuestionById(id);
            if (question != null)
            {
                question.IsDeleted = true;
                question.UserDelete = userId;
                question.DeleteDate = _bLGeneral.GetDateTimeNow();
                Uow.ProductQuestion.Update(question);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateProductQuestion(ProductQuestionViewModel model)
        {
            var question = GetProductQuestionById(model.ProductQuestionID);
            if (question != null)
            {
                question.QuestionEn = model.QuestionEn;
                question.QuestionAr = model.QuestionAr;
                question.AnswerEn = model.AnswerEn;
                question.AnswerAr = model.AnswerAr;
                question.UpdateDate = model.UpdateDate;
                question.UserUpdate = model.UserUpdate;
                question = Uow.ProductQuestion.Update(question);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public ProductQuestion GetProductQuestionById(int id) => Uow.ProductQuestion.GetAll(x => x.ProductQuestionID == id).FirstOrDefault();
        public CustomerFavourites GetCustomerFavourites(int UserId, int ProductID) => Uow.CustomerFavourites.GetAll(x => x.ProductID == ProductID && x.Customers.UserId == UserId).FirstOrDefault();
        #endregion
        #region Product Image
        public bool InsertProductImage(int ProductID, List<string> ListProductImg, int UserId)
        {
            foreach (var Image in ListProductImg)
            {
                ProductImage productImage = new ProductImage()
                {
                    CreateDate = _bLGeneral.GetDateTimeNow(),
                    ProductImageGuid = Guid.NewGuid(),
                    IsDeleted = false,
                    UserId = UserId,
                    Image = Image,
                    ProductID = ProductID
                };
                productImage = Uow.ProductImage.Insert(productImage);
            }
            Uow.Commit();
            return true;
        }
        public bool UpdateProductLogo(int ProductID, string logo, int UserId)
        {
            var ProdData = Uow.Product.GetAll(s => s.ProductID == ProductID).FirstOrDefault();
            if (ProdData != null)
            {
                ProdData.UpdateDate = _bLGeneral.GetDateTimeNow();
                ProdData.UserUpdate = UserId;
                ProdData.Logo = logo;
                Uow.Product.Update(ProdData);
            }
            Uow.Commit();
            return true;
        }
        public IEnumerable<ProductImageViewModel> GetAllProductImageViewModelByProductID(int ProductID, string MainPathView)
        {

            var getData = Uow.ProductImage.GetAll(x => !x.IsDeleted && x.ProductID == ProductID)
            .Select(p => new ProductImageViewModel()
            {
                DescAr = p.DescAr,
                DescEn = p.DescEn,
                ProductImageGuid = p.ProductImageGuid,
                ProductImageID = p.ProductImageID,
                Image = !string.IsNullOrEmpty(p.Image) ? (MainPathView + p.Image) : "/Images/noImage.png",

            });
            return getData;
        }
        public bool DeleteProductImage(int ProductImageID, int userid)
        {
            var Data = Uow.ProductImage.GetAll(s => s.ProductImageID == ProductImageID).FirstOrDefault();
            Data.DeleteDate = _bLGeneral.GetDateTimeNow();
            Data.UserDelete = userid;
            Data.IsDeleted = true;
            Uow.ProductImage.Update(Data);
            Uow.Commit();
            return true;
        }
        #endregion
        #endregion
        #region Site
        public IEnumerable<SiteProductViewModel> GetAllSiteProductViewModelByDepartmentsID(int DepartmentsID, string lang, string VendorPathView, string ProductPathView)
        {
            var DeliveryPriceVatPercent = _blConfiguration.GetDeliveryPriceVatPercent();
            var getData = Uow.Product.GetAll(x => !x.IsDeleted && !x.Vendors.IsDeleted && x.Vendors.IsEnable && !x.Vendors.IsVac && x.IsEnable && !x.IsHidden && x.DepartmentsID == DepartmentsID
                , "Departments,Vendors").OrderByDescending(p => p.CreateDate)
            .Select(p => new SiteProductViewModel()
            {
                ProductName = lang == "ar" ? p.NameAr : p.NameEn,
                ProductLogo = !string.IsNullOrEmpty(p.Logo) ? (ProductPathView + p.Logo) : "/Images/noImage.png",
                VendorsLogo = !string.IsNullOrEmpty(p.Vendors.Logo) ? (VendorPathView + p.Vendors.Logo) : "/Images/noImage.png",
                VendorsName = lang == "ar" ? (p.Vendors.StoreNameAr) : (p.Vendors.StoreNameEn),
                DepartmentsID = p.DepartmentsID,
                ProductID = p.ProductID,
                DepartmentName = lang == "ar" ? p.Departments.NameAR : p.Departments.NameEN,
                ProductGuid = p.ProductGuid,
                ProductPrice = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, p.Price - _bLGeneral.CalcProductNet(p.Discount, p.StartDiscountDate, p.EndDiscountDate, p.Price)).PriceWithVat,
                VendorsID = p.VendorsID,
                VendorsGuid = p.Vendors.VendorsGuid,
                ProductDesc = lang == "ar" ? p.DescAr : p.DescEn,
                Pieces = lang == "ar" ? p.PiecesAr : p.PiecesEn,
                IsLimited = p.IsLimited,
                Quantity = p.Quantity,
                VendorsDaysWork = p.Vendors.DaysWork,
                VendorsWorkFrom = p.Vendors.WorkFrom,
                VendorsWorkTo = p.Vendors.WorkTo,
                VendorsDaysVac = p.Vendors.DaysVac,
                VendorsVacWorkFrom = p.Vendors.VacWorkFrom,
                VendorsVacWorkTo = p.Vendors.VacWorkTo,
            });
            return getData;

        }
        public IEnumerable<SiteProductViewModel> GetTopSiteProductViewModelByDepartmentsID(int DepartmentsID, string lang, string VendorPathView, string ProductPathView)
        {
            var DeliveryPriceVatPercent = _blConfiguration.GetDeliveryPriceVatPercent();
            var getData = Uow.Product.GetAll(x => !x.IsDeleted && !x.Vendors.IsDeleted && x.Vendors.IsEnable && !x.Vendors.IsVac && x.IsEnable && !x.IsHidden && x.DepartmentsID == DepartmentsID
                , "Departments,Vendors").OrderByDescending(p => p.CreateDate)
            .Select(p => new SiteProductViewModel()
            {
                ProductName = lang == "ar" ? p.NameAr : p.NameEn,
                ProductLogo = !string.IsNullOrEmpty(p.Logo) ? (ProductPathView + p.Logo) : "/Images/noImage.png",
                VendorsLogo = !string.IsNullOrEmpty(p.Vendors.Logo) ? (VendorPathView + p.Vendors.Logo) : "/Images/noImage.png",
                VendorsName = lang == "ar" ? (p.Vendors.StoreNameAr) : (p.Vendors.StoreNameEn),
                DepartmentsID = p.DepartmentsID,
                ProductID = p.ProductID,
                DepartmentName = lang == "ar" ? p.Departments.NameAR : p.Departments.NameEN,
                ProductGuid = p.ProductGuid,
                ProductPrice = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, p.Price - _bLGeneral.CalcProductNet(p.Discount, p.StartDiscountDate, p.EndDiscountDate, p.Price)).PriceWithVat,
                VendorsID = p.VendorsID,
                VendorsGuid = p.Vendors.VendorsGuid,
                ProductDesc = lang == "ar" ? p.DescAr : p.DescEn,
                Pieces = lang == "ar" ? p.PiecesAr : p.PiecesEn,
                IsLimited = p.IsLimited,
                Quantity = p.Quantity,
                VendorsDaysWork = p.Vendors.DaysWork,
                VendorsWorkFrom = p.Vendors.WorkFrom,
                VendorsWorkTo = p.Vendors.WorkTo,
                VendorsDaysVac = p.Vendors.DaysVac,
                VendorsVacWorkFrom = p.Vendors.VacWorkFrom,
                VendorsVacWorkTo = p.Vendors.VacWorkTo,
            }).Take(10);
            return getData;

        }
        public IEnumerable<SiteProductViewModel> GetAllSiteProductViewModelByDepartments(string searchProducts, string DepartmentsCheck, string ProductTypeCheck, int? minPrice, int? maxPrice, double? rating, string lang, string VendorPathView, string ProductPathView)
        {
            var DeliveryPriceVatPercent = _blConfiguration.GetDeliveryPriceVatPercent();
            string[] ListDepart = null;
            if (!string.IsNullOrEmpty(DepartmentsCheck))
            {
                ListDepart = DepartmentsCheck.Trim().Split(new string[] { "," }, StringSplitOptions.None);
            }
            string[] ListProductType = null;
            if (!string.IsNullOrEmpty(ProductTypeCheck))
            {
                ListProductType = ProductTypeCheck.Trim().Split(new string[] { "," }, StringSplitOptions.None);
            }
            var maxprod = Uow.OrderItems.GetAll(x => !x.IsDeleted).GroupBy(x => x.ProductID).OrderByDescending(x => x.Count()).Select(x => x.Count()).FirstOrDefault();

            var getData = Uow.Product.GetAll(x => !x.IsDeleted && !x.IsHidden && !x.Vendors.IsDeleted && x.Vendors.IsEnable && !x.Vendors.IsVac && x.IsEnable && (ListDepart != null ? ListDepart.Any(y => y == x.DepartmentsID.ToString()) : true)
            && (minPrice != null ? x.Price >= minPrice : true) && (maxPrice != null ? x.Price <= maxPrice : true)
            && (rating != null ? ((int)(5 * ((decimal)x.OrderItems.Count(x => !x.IsDeleted) / (decimal)maxprod)) == (decimal)rating) : true)
            && (!string.IsNullOrEmpty(searchProducts) ? (x.NameAr.Contains(searchProducts) || x.NameEn.Contains(searchProducts)) : true)
            && (ListProductType != null ? ListProductType.Any(y => y == x.ProductTypeId.ToString()) : true)
                , "Departments,Vendors").ToList().OrderBy(x => x.ProductOrder).OrderByDescending(p => IsVendorWorking(p.Vendors.DaysWork, p.Vendors.WorkFrom, p.Vendors.WorkTo, p.Vendors.DaysVac, p.Vendors.VacWorkFrom, p.Vendors.VacWorkTo))
            .Select(p => new SiteProductViewModel()
            {
                ProductName = lang == "ar" ? p.NameAr : p.NameEn,
                ProductLogo = !string.IsNullOrEmpty(p.Logo) ? (ProductPathView + p.Logo) : "/Images/noImage.png",
                VendorsLogo = !string.IsNullOrEmpty(p.Vendors.Logo) ? (VendorPathView + p.Vendors.Logo) : "/Images/noImage.png",
                VendorsName = lang == "ar" ? (p.Vendors.StoreNameAr) : (p.Vendors.StoreNameEn),
                DepartmentsID = p.DepartmentsID,
                ProductID = p.ProductID,
                DepartmentName = lang == "ar" ? p.Departments.NameAR : p.Departments.NameEN,
                ProductGuid = p.ProductGuid,
                ProductPrice = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, p.Price - _bLGeneral.CalcProductNet(p.Discount, p.StartDiscountDate, p.EndDiscountDate, p.Price)).PriceWithVat,
                VendorsID = p.VendorsID,
                VendorsGuid = p.Vendors.VendorsGuid,
                ProductDesc = lang == "ar" ? p.DescAr : p.DescEn,
                Pieces = lang == "ar" ? p.PiecesAr : p.PiecesEn,
                IsLimited = p.IsLimited,
                Quantity = p.DailyQuantity,
                VendorsDaysWork = p.Vendors.DaysWork,
                VendorsWorkFrom = p.Vendors.WorkFrom,
                VendorsWorkTo = p.Vendors.WorkTo,
                VendorsDaysVac = p.Vendors.DaysVac,
                VendorsVacWorkFrom = p.Vendors.VacWorkFrom,
                VendorsVacWorkTo = p.Vendors.VacWorkTo,
            });
            return getData;

        }
        public IEnumerable<SiteProductViewModel> GetAllSiteProductViewModelByDepartments(int DepartmentsID, string searchProducts, string lang, string VendorPathView, string ProductPathView)
        {
            var DeliveryPriceVatPercent = _blConfiguration.GetDeliveryPriceVatPercent();
            var getData = Uow.Product.GetAll(x => !x.IsDeleted && !x.Vendors.IsDeleted && x.Vendors.IsEnable && !x.Vendors.IsVac && !x.IsHidden && x.IsEnable && x.DepartmentsID == DepartmentsID
            && (!string.IsNullOrEmpty(searchProducts) ? (x.NameAr.Contains(searchProducts) || x.NameEn.Contains(searchProducts)) : true)
                , "Departments,Vendors").ToList().OrderByDescending(x => x.CreateDate).OrderByDescending(p =>  IsVendorWorking(p.Vendors.DaysWork, p.Vendors.WorkFrom, p.Vendors.WorkTo, p.Vendors.DaysVac, p.Vendors.VacWorkFrom, p.Vendors.VacWorkTo))
            .Select(p => new SiteProductViewModel()
            {
                ProductName = lang == "ar" ? p.NameAr : p.NameEn,
                ProductLogo = !string.IsNullOrEmpty(p.Logo) ? (ProductPathView + p.Logo) : "/Images/noImage.png",
                VendorsLogo = !string.IsNullOrEmpty(p.Vendors.Logo) ? (VendorPathView + p.Vendors.Logo) : "/Images/noImage.png",
                VendorsName = lang == "ar" ? (p.Vendors.StoreNameAr) : (p.Vendors.StoreNameEn),
                DepartmentsID = p.DepartmentsID,
                ProductID = p.ProductID,
                DepartmentName = lang == "ar" ? p.Departments.NameAR : p.Departments.NameEN,
                ProductGuid = p.ProductGuid,
                ProductPrice = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, p.Price - _bLGeneral.CalcProductNet(p.Discount, p.StartDiscountDate, p.EndDiscountDate, p.Price)).PriceWithVat,
                VendorsID = p.VendorsID,
                VendorsGuid = p.Vendors.VendorsGuid,
                ProductDesc = lang == "ar" ? p.DescAr : p.DescEn,
                Pieces = lang == "ar" ? p.PiecesAr : p.PiecesEn,
                IsLimited = p.IsLimited,
                Quantity = p.DailyQuantity,
                VendorsDaysWork = p.Vendors.DaysWork,
                VendorsWorkFrom = p.Vendors.WorkFrom,
                VendorsWorkTo = p.Vendors.WorkTo,
                VendorsDaysVac = p.Vendors.DaysVac,
                VendorsVacWorkFrom = p.Vendors.VacWorkFrom,
                VendorsVacWorkTo = p.Vendors.VacWorkTo,
            });
            return getData;

        }
        public SiteProductDetailsViewModel GetSiteProductDetailsViewModelByGuid(Guid ProductGuid, int CustomersID, string lang, string VendorPathView, string ProductPathView)
        {
            var DeliveryPriceVatPercent = _blConfiguration.GetDeliveryPriceVatPercent();
            var getData = Uow.Product.GetAll(x => !x.IsDeleted && !x.IsHidden && !x.Vendors.IsDeleted && x.Vendors.IsEnable && !x.Vendors.IsVac && x.ProductGuid == ProductGuid, "Departments,Vendors")
            .Select(p => new SiteProductDetailsViewModel()
            {
                ProductName = lang == "ar" ? p.NameAr : p.NameEn,
                ProductLogo = !string.IsNullOrEmpty(p.Logo) ? (ProductPathView + p.Logo) : "/Images/noImage.png",
                VendorsLogo = !string.IsNullOrEmpty(p.Vendors.Logo) ? (VendorPathView + p.Vendors.Logo) : "/Images/noImage.png",
                VendorsName = lang == "ar" ? p.Vendors.StoreNameAr : p.Vendors.StoreNameEn,
                DepartmentsID = p.DepartmentsID,
                ProductID = p.ProductID,
                DepartmentName = lang == "ar" ? p.Departments.NameAR : p.Departments.NameEN,
                ProductGuid = p.ProductGuid,
                ProductPrice = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, p.Price - _bLGeneral.CalcProductNet(p.Discount, p.StartDiscountDate, p.EndDiscountDate, p.Price)).PriceWithVat,
                VendorsID = p.VendorsID,
                ProductDesc = lang == "ar" ? p.DescAr : p.DescEn,
                KeyWords = p.KeyWords,
                VendorsGuid = p.Vendors.VendorsGuid,
                DepartmentsGuid = p.Departments.DepartmentsGuid,
                IsFavorites = p.CustomerFavourites.Any(x => !x.IsDeleted && x.CustomersID == CustomersID),
                TimeTakenProcessHours = p.TimeTakenProcess,
                ProductPieces = lang == "ar" ? p.PiecesAr : p.PiecesAr,
                Weight = p.Weight,
                TimeTakenProcess = p.TimeTakenProcess >= 60 ? ((int)(p.TimeTakenProcess / 60) + " " + (lang == "ar" ? "ساعة" : "Hour") +
                    ((p.TimeTakenProcess % 60) > 0 ? ((lang == "ar" ? " و " : " and ") + (int)(p.TimeTakenProcess % 60) + " " + (lang == "ar" ? "دقيقة" : "minute")) : ""))
                    : ((p.TimeTakenProcess % 60) > 0 ? ((int)(p.TimeTakenProcess % 60) + " " + (lang == "ar" ? "دقيقة" : "minute")) : ""),
                IsLimited = p.IsLimited,
                Quantity = p.DailyQuantity,
                MeasurementName = p.MeasurementId == (int)MeasurementEnum.Gram ? (lang == "ar" ? "جرام" : "Gram") : (lang == "ar" ? "كيلو" : "Kilo"),
                VendorsDaysWork = p.Vendors.DaysWork,
                VendorsWorkFrom = p.Vendors.WorkFrom,
                VendorsWorkTo = p.Vendors.WorkTo,
                VendorsDaysVac = p.Vendors.DaysVac,
                VendorsVacWorkFrom = p.Vendors.VacWorkFrom,
                VendorsVacWorkTo = p.Vendors.VacWorkTo,
            }).FirstOrDefault();
            getData.ProductImages = GetAllProductImageViewModelByProductID(getData.ProductID, ProductPathView);
            var list = !string.IsNullOrEmpty(getData.KeyWords) ? getData.KeyWords.Split(',').ToList() : null;
            if (list != null)
            {
                getData.ListKeyWordsString = Uow.KeyWords.GetAll(x => !x.IsDeleted && list.Any(y => y == x.KeyWordsID.ToString())).Select(m => lang == "ar" ? m.NameAR : m.NameEN).ToList();
            }
            getData.ProductQuestions = GetAllProductQuestionViewModelByProduct(getData.ProductID, lang);
            getData.ProdQuestion = GetAllProdQuestionViewModelByProduct(getData.ProductID, lang);
            getData.AllOtherProduct = GetAllSiteProductViewModelByDepartmentsID(getData.DepartmentsID, lang, VendorPathView, ProductPathView);

            return getData;
        }
        public SiteProductDetailsViewModel GetSiteProductDetailsViewModelByID(int ProductID, int CustomersID, string lang, string VendorPathView, string ProductPathView)
        {
            var DeliveryPriceVatPercent = _blConfiguration.GetDeliveryPriceVatPercent();
            var getData = Uow.Product.GetAll(x => !x.IsDeleted && !x.IsHidden && !x.Vendors.IsDeleted && x.Vendors.IsEnable && !x.Vendors.IsVac && x.ProductID == ProductID, "Departments,Vendors")
            .Select(p => new SiteProductDetailsViewModel()
            {
                ProductName = lang == "ar" ? p.NameAr : p.NameEn,
                ProductLogo = !string.IsNullOrEmpty(p.Logo) ? (ProductPathView + p.Logo) : "/Images/noImage.png",
                VendorsLogo = !string.IsNullOrEmpty(p.Vendors.Logo) ? (VendorPathView + p.Vendors.Logo) : "/Images/noImage.png",
                VendorsName = lang == "ar" ? (p.Vendors.StoreNameAr) : (p.Vendors.StoreNameEn),
                DepartmentsID = p.DepartmentsID,
                ProductID = p.ProductID,
                DepartmentName = lang == "ar" ? p.Departments.NameAR : p.Departments.NameEN,
                ProductGuid = p.ProductGuid,
                ProductPrice = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, p.Price - _bLGeneral.CalcProductNet(p.Discount, p.StartDiscountDate, p.EndDiscountDate, p.Price)).PriceWithVat,
                VendorsID = p.VendorsID,
                ProductDesc = lang == "ar" ? p.DescAr : p.DescEn,
                KeyWords = p.KeyWords,
                VendorsGuid = p.Vendors.VendorsGuid,
                DepartmentsGuid = p.Departments.DepartmentsGuid,
                IsFavorites = p.CustomerFavourites.Any(x => !x.IsDeleted && x.CustomersID == CustomersID),
                VendorsDaysWork = p.Vendors.DaysWork,
                VendorsWorkFrom = p.Vendors.WorkFrom,
                VendorsWorkTo = p.Vendors.WorkTo,
                VendorsDaysVac = p.Vendors.DaysVac,
                VendorsVacWorkFrom = p.Vendors.VacWorkFrom,
                VendorsVacWorkTo = p.Vendors.VacWorkTo,
            }).FirstOrDefault();
            getData.ProductImages = GetAllProductImageViewModelByProductID(getData.ProductID, ProductPathView);
            var list = !string.IsNullOrEmpty(getData.KeyWords) ? getData.KeyWords.Split(',').ToList() : null;
            if (list != null)
            {
                getData.ListKeyWordsString = Uow.KeyWords.GetAll(x => !x.IsDeleted && list.Any(y => y == x.KeyWordsID.ToString())).Select(m => lang == "ar" ? m.NameAR : m.NameEN).ToList();
            }
            getData.ProductQuestions = GetAllProductQuestionViewModelByProduct(getData.ProductID, lang);
            getData.ProdQuestion = GetAllProdQuestionViewModelByProduct(getData.ProductID, lang);
            getData.AllOtherProduct = GetAllSiteProductViewModelByDepartmentsID(getData.DepartmentsID, lang, VendorPathView, ProductPathView);
            return getData;
        }
        public IEnumerable<SiteProductViewModel> GetAllSiteProductViewModelByVendorsID(int VendorsID, string lang, string VendorPathView, string ProductPathView)
        {
            var DeliveryPriceVatPercent = _blConfiguration.GetDeliveryPriceVatPercent();
            var getData = Uow.Product.GetAll(x => !x.IsDeleted && !x.Vendors.IsDeleted && x.Vendors.IsEnable && !x.Vendors.IsVac && !x.IsHidden && x.IsEnable && x.VendorsID == VendorsID
                , "Departments,Vendors").OrderByDescending(p => p.CreateDate)
            .Select(p => new SiteProductViewModel()
            {
                ProductName = lang == "ar" ? p.NameAr : p.NameEn,
                ProductLogo = !string.IsNullOrEmpty(p.Logo) ? (ProductPathView + p.Logo) : "/Images/noImage.png",
                VendorsLogo = !string.IsNullOrEmpty(p.Vendors.Logo) ? (VendorPathView + p.Vendors.Logo) : "/Images/noImage.png",
                VendorsName = lang == "ar" ? (p.Vendors.StoreNameAr) : (p.Vendors.StoreNameEn),
                DepartmentsID = p.DepartmentsID,
                ProductID = p.ProductID,
                DepartmentName = lang == "ar" ? p.Departments.NameAR : p.Departments.NameEN,
                ProductGuid = p.ProductGuid,
                ProductPrice = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, p.Price - _bLGeneral.CalcProductNet(p.Discount, p.StartDiscountDate, p.EndDiscountDate, p.Price)).PriceWithVat,
                VendorsID = p.VendorsID,
                VendorsGuid = p.Vendors.VendorsGuid,
                ProductDesc = lang == "ar" ? p.DescAr : p.DescEn,
                Pieces = lang == "ar" ? p.PiecesAr : p.PiecesEn,
                IsFixed = p.IsFixed,
                IsHidden = p.IsHidden,
                IsLimited = p.IsLimited,
                Quantity = p.DailyQuantity,
                VendorsDaysWork = p.Vendors.DaysWork,
                VendorsWorkFrom = p.Vendors.WorkFrom,
                VendorsWorkTo = p.Vendors.WorkTo,
                VendorsDaysVac = p.Vendors.DaysVac,
                VendorsVacWorkFrom = p.Vendors.VacWorkFrom,
                VendorsVacWorkTo = p.Vendors.VacWorkTo,
            });
            return getData;
        }
        public IEnumerable<SiteProductViewModel> GetAllCustomerFavouritesByUserCustomer(int UserId, string lang, string VendorPathView, string ProductPathView)
        {
            var DeliveryPriceVatPercent = _blConfiguration.GetDeliveryPriceVatPercent();
            var getData = Uow.CustomerFavourites.GetAll(x => !x.IsDeleted && x.Customers.UserId == UserId
                , "Product.Departments,Product.Vendors").OrderByDescending(p => p.CreateDate)
            .Select(p => new SiteProductViewModel()
            {
                ProductName = lang == "ar" ? p.Product.NameAr : p.Product.NameEn,
                ProductLogo = !string.IsNullOrEmpty(p.Product.Logo) ? (ProductPathView + p.Product.Logo) : "/Images/noImage.png",
                VendorsLogo = !string.IsNullOrEmpty(p.Product.Vendors.Logo) ? (VendorPathView + p.Product.Vendors.Logo) : "/Images/noImage.png",
                VendorsName = lang == "ar" ? (p.Product.Vendors.StoreNameAr) : (p.Product.Vendors.StoreNameEn),
                DepartmentsID = p.Product.DepartmentsID,
                ProductID = p.Product.ProductID,
                DepartmentName = lang == "ar" ? p.Product.Departments.NameAR : p.Product.Departments.NameEN,
                ProductGuid = p.Product.ProductGuid,
                ProductPrice = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, p.Product.Price - _bLGeneral.CalcProductNet(p.Product.Discount, p.Product.StartDiscountDate, p.Product.EndDiscountDate, p.Product.Price)).PriceWithVat,
                VendorsID = p.Product.VendorsID,
                VendorsGuid = p.Product.Vendors.VendorsGuid,
                ProductDesc = lang == "ar" ? p.Product.DescAr : p.Product.DescEn,
                Pieces = lang == "ar" ? p.Product.PiecesAr : p.Product.PiecesEn,
                IsLimited = p.Product.IsLimited,
                Quantity = p.Product.Quantity,
                VendorsDaysWork = p.Product.Vendors.DaysWork,
                VendorsWorkFrom = p.Product.Vendors.WorkFrom,
                VendorsWorkTo = p.Product.Vendors.WorkTo,
                VendorsDaysVac = p.Product.Vendors.DaysVac,
                VendorsVacWorkFrom = p.Product.Vendors.VacWorkFrom,
                VendorsVacWorkTo = p.Product.Vendors.VacWorkTo,
            });
            return getData;

        }
        public SiteProductPriceViewModel GetSiteProductPriceViewModel()
        {
            var DeliveryPriceVatPercent = _blConfiguration.GetDeliveryPriceVatPercent();
            var getData = Uow.Product.GetAll(x => !x.IsDeleted && x.IsEnable && !x.IsHidden).OrderBy(p => p.Price);
            var model = new SiteProductPriceViewModel();
            if (getData.Any())
            {

                model.MinProductPrice = Math.Round(_blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, getData.FirstOrDefault().Price - _bLGeneral.CalcProductNet(getData.FirstOrDefault().Discount, getData.FirstOrDefault().StartDiscountDate, getData.FirstOrDefault().EndDiscountDate, getData.FirstOrDefault().Price)).PriceWithVat, 0);
                model.MaxProductPrice = Math.Round(_blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, getData.FirstOrDefault().Price - _bLGeneral.CalcProductNet(getData.FirstOrDefault().Discount, getData.FirstOrDefault().StartDiscountDate, getData.FirstOrDefault().EndDiscountDate, getData.FirstOrDefault().Price)).PriceWithVat, 0);
            }
            else
            {
                model.MinProductPrice = 10;
                model.MaxProductPrice = 2000;
            }
            return model;
        }
        public ProductPriceViewModelApi GetProductPriceViewModelApi()
        {
            var DeliveryPriceVatPercent = _blConfiguration.GetDeliveryPriceVatPercent();
            var getData = Uow.Product.GetAll(x => !x.IsDeleted && x.IsEnable && !x.IsHidden).OrderBy(p => p.Price);
            var model = new ProductPriceViewModelApi();
            if (getData.Any())
            {
                model.minPrice = Math.Round(_blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, getData.FirstOrDefault().Price - _bLGeneral.CalcProductNet(getData.FirstOrDefault().Discount, getData.FirstOrDefault().StartDiscountDate, getData.FirstOrDefault().EndDiscountDate, getData.FirstOrDefault().Price)).PriceWithVat, 0);
                model.maxPrice = Math.Round(_blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, getData.FirstOrDefault().Price - _bLGeneral.CalcProductNet(getData.FirstOrDefault().Discount, getData.FirstOrDefault().StartDiscountDate, getData.FirstOrDefault().EndDiscountDate, getData.FirstOrDefault().Price)).PriceWithVat, 0);
                if (model.maxPrice <= model.minPrice)
                {
                    model.maxPrice = model.minPrice + 10;
                }
            }
            else
            {
                model.minPrice = 10;
                model.maxPrice = 2000;
            }
            return model;
        }
        public decimal GetRateProductByMax(int ProductID)
        {
            var maxprod = Uow.OrderItems.GetAll(x => !x.IsDeleted).GroupBy(x => x.ProductID).OrderByDescending(x => x.Count()).Select(x => x.Count()).FirstOrDefault();
            var prod = Uow.OrderItems.GetAll(x => !x.IsDeleted && x.ProductID == ProductID).Count();
            var result = 5 * ((decimal)prod / (decimal)maxprod);
            return Math.Round(result, 2);
        }
        #endregion
    }
}
