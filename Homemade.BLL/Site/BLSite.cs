using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Cart;
using Homemade.DAL.Contract;
using Homemade.Model.Order;
using Homemade.Model.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.Site
{
    public class BLSite
    {
        #region Declartion
        private readonly IUOW Uow;
        private readonly BLGeneral _bLGeneral;
        public BLSite(IUOW _uow, BLGeneral bLGeneral)
        {
            this.Uow = _uow;
            _bLGeneral = bLGeneral;
        }
        #endregion
        #region Get
        /// <summary>
        /// Check Is Session Exist By Session Id
        /// </summary>
        /// <param name="SessionID"></param>
        /// <returns></returns>
        public bool IsExistSession(string SessionID, bool IsOrder) => Uow.CartMaster.GetAll(e => !e.IsDeleted && e.SessionID == SessionID && e.ExpiryDate > DateTime.UtcNow.AddHours(3) && e.IsAnOrder == IsOrder).Any();
        public bool IsExistMaster(int SessionID, bool IsOrder) => Uow.CartMaster.GetAll(e => !e.IsDeleted && e.CartMasterID == SessionID && e.ExpiryDate > DateTime.UtcNow.AddHours(3) && e.IsAnOrder == IsOrder).Any();
        public bool IsExistMaster(int SessionID, int CustomersID, bool IsOrder) => Uow.CartMaster.GetAll(e => !e.IsDeleted && e.CustomersID == CustomersID && e.CartMasterID == SessionID && e.ExpiryDate > DateTime.UtcNow.AddHours(3) && e.IsAnOrder == IsOrder).Any();
        public CartMaster GetCartMaster(int CustomersID, bool IsOrder) => Uow.CartMaster.GetAll(e => !e.IsDeleted && e.CustomersID == CustomersID && e.IsAnOrder == IsOrder).OrderBy(x => x.CreateDate).FirstOrDefault();
        public CartMaster GetCartMaster(int CartMasterID) => Uow.CartMaster.GetAll(e => !e.IsDeleted && e.CartMasterID == CartMasterID).FirstOrDefault();
        public CartMaster GetCartMasterIsDeleted(int CartMasterID) => Uow.CartMaster.GetAll(e => e.IsDeleted && e.CartMasterID == CartMasterID).FirstOrDefault();
        public CartMaster GeCartMasterByID(int cartMasterID) => Uow.CartMaster.GetAll(e => e.CartMasterID == cartMasterID && !e.IsDeleted, "CartDetails,Customers").FirstOrDefault();
        public CartMaster GeCartMasterBySessionID(string SessionID) => Uow.CartMaster.GetAll(e => e.SessionID == SessionID && !e.IsDeleted, "CartDetails,Customers").FirstOrDefault();
        /// <summary>
        /// Get Cart Master Data By Session ID
        /// </summary>
        /// <param name="sessionID"></param>
        /// <returns></returns>
        public CartMaster GeCartMaster(string sessionID, bool IsOrder) => Uow.CartMaster.GetAll(e => e.SessionID == sessionID && !e.IsDeleted && e.ExpiryDate <= DateTime.UtcNow.AddHours(3) && e.IsAnOrder == IsOrder).FirstOrDefault();
        /// <summary>
        /// Get List Of Item By Cart ID
        /// </summary>
        /// <param name="cartMasterID"></param>
        /// <returns></returns>
        public IQueryable<CartDetails> GeCartDetails(int cartMasterID) => Uow.CartDetails.GetAll(e => e.CartMasterID == cartMasterID && !e.IsDeleted, "Product,CartMaster.Customers,Product.Vendors");
        public IQueryable<CartDetails> GeAllCartDetails(int cartMasterID) => Uow.CartDetails.GetAll(e => e.CartMasterID == cartMasterID, "Product,CartMaster.Customers,Product.Vendors");
        public CartDetails GeCartDetailsByID(int cartDetailsID) => Uow.CartDetails.GetAll(e => e.CartDetailsID == cartDetailsID && !e.IsDeleted, "Product,CartMaster.Customers,Product.Vendors").FirstOrDefault();
        public IQueryable<CartDetailsVM> GeCartDetailsVM(int cartMasterID, string lang)
        {
            var ConfPrice = Uow.Configuration.GetAll(s => !s.IsDeleted).FirstOrDefault() != null ? Uow.Configuration.GetAll(s => !s.IsDeleted).FirstOrDefault().DeliveryPrice : 0;

            return Uow.CartDetails.GetAll(e => e.CartMasterID == cartMasterID && !e.IsDeleted, "Product,CartMaster.Customers,Product.Vendors").Select(e => new CartDetailsVM()
            {
                ProductID = e.ProductID,
                ProductName = lang == "en" ? e.ProductNameEn : e.ProductNameAr,
                ProductPrice = e.ProductPrice,
                ProductDiscount = e.ProductDiscount,
                ProductQuantity = e.ProductQuantity,
                ProductImage = e.ProductImage,
                IsDeleted = e.IsDeleted,
                CreateDate = e.CreateDate,
                UpdateDate = e.UpdateDate,
                DeleteDate = e.DeleteDate,
                CartMasterID = e.CartMasterID,
                CartDetailsID = e.CartDetailsID,
                Note = e.Note,
                VendorsID = e.Product.VendorsID,
                VendorsGuid = e.Product.Vendors.VendorsGuid,
                StoreName = lang == "en" ? e.Product.Vendors.StoreNameEn : e.Product.Vendors.StoreNameAr,
                Logo = e.Product.Vendors.Logo,
                DeliveryPrice = e.deliveryprice,
                distanceKM = e.distanceKM,
                AboutStore = lang == "en" ? e.Product.Vendors.AboutStoreEn : e.Product.Vendors.AboutStoreAr,
            });
        }
        public IQueryable<CartDetailsVM> GeCartDetailsVM(int cartMasterID, string lang, int userid)
        {
            // ارجاع الكونفجريشن الخاص بالشركه
            var companyConfigration = Uow.Configuration.GetAll().FirstOrDefault();
            return Uow.CartDetails.GetAll(e => e.CartMasterID == cartMasterID && !e.IsDeleted, "Product,CartMaster.Customers,Product.Vendors").Select(e => new CartDetailsVM()
            {
                ProductID = e.ProductID,
                ProductName = lang == "en" ? e.ProductNameEn : e.ProductNameAr,
                ProductPrice = e.ProductPrice,
                ProductDiscount = e.ProductDiscount,
                ProductQuantity = e.ProductQuantity,
                ProductImage = e.ProductImage,
                IsDeleted = e.IsDeleted,
                CreateDate = e.CreateDate,
                UpdateDate = e.UpdateDate,
                DeleteDate = e.DeleteDate,
                CartMasterID = e.CartMasterID,
                CartDetailsID = e.CartDetailsID,
                Note = e.Note,
                VendorsID = e.Product.VendorsID,
                VendorsGuid = e.Product.Vendors.VendorsGuid,
                StoreName = lang == "en" ? e.Product.Vendors.StoreNameEn : e.Product.Vendors.StoreNameAr,
                Logo = e.Product.Vendors.Logo,
                DeliveryPrice = e.Product.Vendors.DeliveryType == (int)DeliveryTypeEnum.Store ? e.Product.Vendors.DeliveryPrice : companyConfigration.DeliveryPrice,
                AboutStore = lang == "en" ? e.Product.Vendors.AboutStoreEn : e.Product.Vendors.AboutStoreAr,
                IsLimited = e.Product.IsLimited,
                Quantity = e.Product.DailyQuantity,

            });
        }
        public IQueryable<CartDetailsVM> GeCartDetailsVMByOrder(int OrdersID, string lang)
        {
            var ConfPrice = Uow.Configuration.GetAll(s => !s.IsDeleted).FirstOrDefault() != null ? Uow.Configuration.GetAll(s => !s.IsDeleted).FirstOrDefault().DeliveryPrice : 0;

            return Uow.OrderItems.GetAll(e => e.OrderVendor.OrdersID == OrdersID && !e.IsDeleted, "Product,Product.Vendors,OrderVendor").Select(e => new CartDetailsVM()
            {
                ProductID = e.ProductID,
                ProductName = lang == "en" ? e.Product.NameEn : e.Product.NameAr,
                ProductPrice = e.Price,
                ProductDiscount = e.Discount,
                ProductQuantity = e.Quantity,
                ProductImage = e.ProdImage,
                IsDeleted = e.IsDeleted,
                CreateDate = e.CreateDate,
                UpdateDate = e.UpdateDate,
                DeleteDate = e.DeleteDate,
                CartMasterID = e.OrderVendor.OrdersID,
                CartDetailsID = e.OrderVendorID,
                Note = e.OrderVendor.Notes,
                VendorsID = e.Product.VendorsID,
                VendorsGuid = e.Product.Vendors.VendorsGuid,
                StoreName = lang == "en" ? e.Product.Vendors.StoreNameEn : e.Product.Vendors.StoreNameAr,
                Logo = e.Product.Vendors.Logo,
                DeliveryPrice = e.OrderVendor.DeliveryPrice,
                distanceKM = e.OrderVendor.DistanceKM ?? 0,
                AboutStore = lang == "en" ? e.Product.Vendors.AboutStoreEn : e.Product.Vendors.AboutStoreAr,
                ApprovalQuantity = e.OrderVendor.ApprovalQuantity,
            });
        }
        public IQueryable<CartDetailsVM> GeCartDetailsVMByOrder(int OrdersID, string lang, int userid)
        {
            // ارجاع الكونفجريشن الخاص بالشركه
            var companyConfigration = Uow.Configuration.GetAll().FirstOrDefault();
            return Uow.OrderItems.GetAll(e => e.OrderVendor.OrdersID == OrdersID && !e.IsDeleted, "Product,Product.Vendors,OrderVendor").Select(e => new CartDetailsVM()
            {
                ProductID = e.ProductID,
                ProductName = lang == "en" ? e.Product.NameEn : e.Product.NameAr,
                ProductPrice = e.Price,
                ProductDiscount = e.Discount,
                ProductQuantity = e.Quantity,
                ProductImage = e.ProdImage,
                IsDeleted = e.IsDeleted,
                CreateDate = e.CreateDate,
                UpdateDate = e.UpdateDate,
                DeleteDate = e.DeleteDate,
                CartMasterID = e.OrderVendor.OrdersID,
                CartDetailsID = e.OrderVendorID,
                Note = e.OrderVendor.Notes,
                VendorsID = e.Product.VendorsID,
                VendorsGuid = e.Product.Vendors.VendorsGuid,
                StoreName = lang == "en" ? e.Product.Vendors.StoreNameEn : e.Product.Vendors.StoreNameAr,
                Logo = e.Product.Vendors.Logo,
                DeliveryPrice = e.Product.Vendors.DeliveryType == (int)DeliveryTypeEnum.Store ? e.Product.Vendors.DeliveryPrice : companyConfigration.DeliveryPrice,
                AboutStore = lang == "en" ? e.Product.Vendors.AboutStoreEn : e.Product.Vendors.AboutStoreAr,
                IsLimited = e.Product.IsLimited,
                Quantity = e.Product.DailyQuantity,
                ApprovalQuantity = e.OrderVendor.ApprovalQuantity,
            });
        }
        /// <summary>
        /// Get Specific Product By Cart Id And Product ID
        /// </summary>
        /// <param name="cartMasterID"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        public CartDetails GeCartDetails(int cartMasterID, int productID) => Uow.CartDetails.GetAll(e => e.CartMasterID == cartMasterID && e.ProductID == productID && !e.IsDeleted).FirstOrDefault();
        /// <summary>
        /// Get Specific Product By ID
        /// </summary>
        /// <param name="cartDetailsID"></param>
        /// <returns></returns>
        public CartDetails GeCartDetailsById(int cartDetailsID) => Uow.CartDetails.GetAll(e => e.CartDetailsID == cartDetailsID && !e.IsDeleted).FirstOrDefault();
        #endregion
        #region Action
        /// <summary>
        /// Create New Session
        /// </summary>
        /// <param name="cartMaster"></param>
        /// <returns></returns>
        public CartMaster InsertSession(CartMaster cartMaster)
        {
            cartMaster.CartMasterGUID = Guid.NewGuid();
            cartMaster.IsAnOrder = false;
            cartMaster.IsDeleted = false;
            cartMaster = Uow.CartMaster.Insert(cartMaster);
            Uow.Commit();
            return cartMaster;
        }
        /// <summary>
        /// Add Item To Cart
        /// </summary>
        /// <param name="cartDetails"></param>
        /// <returns></returns>
        public CartDetails AddItemToCart(CartDetails cartDetails)
        {
            cartDetails.CreateDate = _bLGeneral.GetDateTimeNow();
            cartDetails.CartDetailsGUID = Guid.NewGuid();
            cartDetails.IsDeleted = false;
            cartDetails = Uow.CartDetails.Insert(cartDetails);
            Uow.Commit();
            return cartDetails;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cartDetails"></param>
        /// <returns></returns>
        public CartDetails updateCartDetails(CartDetails cartDetails)
        {
            cartDetails.UpdateDate = _bLGeneral.GetDateTimeNow();
            cartDetails = Uow.CartDetails.Update(cartDetails);
            Uow.Commit();
            return cartDetails;
        }
        public CartMaster updateCartMaster(CartMaster cartMaster)
        {
            cartMaster.UpdateDate = _bLGeneral.GetDateTimeNow();
            cartMaster = Uow.CartMaster.Update(cartMaster);
            Uow.Commit();
            return cartMaster;
        }
        public IQueryable<CartMaster> GetAllCartMaster(int CustomersID, int CartMasterID)
        {
            return Uow.CartMaster.GetAll(x => !x.IsDeleted && x.CustomersID == CustomersID && x.CartMasterID != CartMasterID);
        }
        public IQueryable<CartDetails> GetAllCartDetails(int CartMasterID)
        {
            return Uow.CartDetails.GetAll(x => !x.IsDeleted && x.CartMasterID == CartMasterID);
        }
        public CartMaster updateCartMasterWithNewCustomer(CartMaster cartMaster)
        {
            cartMaster.UpdateDate = _bLGeneral.GetDateTimeNow();
            cartMaster = Uow.CartMaster.Update(cartMaster);
            if (cartMaster.CustomersID != null)
            {
                foreach (var item in GetAllCartMaster((int)cartMaster.CustomersID, cartMaster.CartMasterID))
                {
                    item.IsDeleted = true;
                    item.DeleteDate = _bLGeneral.GetDateTimeNow();
                    Uow.CartMaster.Update(item);
                }
            }
            Uow.Commit();
            return cartMaster;
        }
        public bool updateCartMasterWithCustomer(int OldCartMasterID, int NewCartMasterID, int CustomersID)
        {
            var newCartMaster = GetCartMaster(NewCartMasterID);
            var AllNewCartDetails = GetAllCartDetails(NewCartMasterID);
            if (newCartMaster != null)
            {
                foreach (var item in GetAllCartMaster((int)CustomersID, NewCartMasterID))
                {
                    item.IsDeleted = true;
                    item.DeleteDate = _bLGeneral.GetDateTimeNow();
                    Uow.CartMaster.Update(item);
                }
                newCartMaster.UpdateDate = _bLGeneral.GetDateTimeNow();
                newCartMaster.CustomersID = CustomersID;
                Uow.CartMaster.Update(newCartMaster);
            }
            else
            {
                newCartMaster = GetCartMasterIsDeleted(NewCartMasterID);
                if (newCartMaster != null)
                {
                    newCartMaster.UpdateDate = _bLGeneral.GetDateTimeNow();
                    newCartMaster.CustomersID = CustomersID;
                    newCartMaster.IsDeleted = false;
                    Uow.CartMaster.Update(newCartMaster);
                }
            }
            if (AllNewCartDetails.Count() > 0)
            {
                var AllCartDetails = GetAllCartDetails(OldCartMasterID);
                foreach (var item in AllCartDetails)
                {
                    item.DeleteDate = _bLGeneral.GetDateTimeNow();
                    item.IsDeleted = true;
                    Uow.CartDetails.Update(item);
                }
            }
            else
            {
                var AllCartDetails = GetAllCartDetails(OldCartMasterID);
                foreach (var item in AllCartDetails)
                {
                    item.CartMasterID = NewCartMasterID;
                    item.UpdateDate = _bLGeneral.GetDateTimeNow();
                    Uow.CartDetails.Update(item);
                }
            }

            Uow.Commit();

            return true;
        }
        public bool updateCartMasterSchedule(CartMaster cartMaster, DateTime? ScheduleDate, DateTime? ScheduleTime)
        {
            cartMaster.UpdateDate = _bLGeneral.GetDateTimeNow();
            cartMaster.OrderTypeId = (int)OrderTypeEnum.Schedule;
            cartMaster.ScheduleDate = ScheduleDate;
            cartMaster.ScheduleTime = ScheduleTime;
            cartMaster = Uow.CartMaster.Update(cartMaster);
            Uow.Commit();
            return true;
        }
        public bool CancelCartMasterSchedule(CartMaster cartMaster)
        {
            cartMaster.UpdateDate = _bLGeneral.GetDateTimeNow();
            cartMaster.OrderTypeId = (int)OrderTypeEnum.Now;
            cartMaster.ScheduleDate = null;
            cartMaster.ScheduleTime = null;
            cartMaster = Uow.CartMaster.Update(cartMaster);
            Uow.Commit();
            return true;
        }
        /// <summary>
        /// Remove Item From Cart By ID
        /// </summary>
        /// <param name="cartDetailsID"></param>
        /// <returns></returns>
        public bool RemoveItemFromCart(int cartDetailsID)
        {
            try
            {
                var cartdata = GeCartDetailsById(cartDetailsID);
                cartdata.IsDeleted = true;
                cartdata.DeleteDate = _bLGeneral.GetDateTimeNow();
                Uow.CartDetails.Update(cartdata);
                Uow.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool RemoveCart(int cartMasterID)
        {
            try
            {
                var masterdata = GeCartMasterByID(cartMasterID);
                var cartdata = GeAllCartDetails(masterdata.CartMasterID).ToList();

                Uow.CartDetails.DeleteRang(cartdata);
                Uow.CartMaster.Delete(masterdata);
                Uow.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IEnumerable<CartDetails> updateRangeCartDetails(List<CartDetails> cartDetails)
        {
            Uow.CartDetails.UpdateRang(cartDetails);
            Uow.Commit();
            return cartDetails;
        }
        public IEnumerable<Orders> updateRangeOrders(List<Orders> orders)
        {
            Uow.Orders.UpdateRang(orders);
            Uow.Commit();
            return orders;
        }
        #endregion
    }
}
