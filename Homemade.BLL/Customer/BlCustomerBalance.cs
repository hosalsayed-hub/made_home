using Homemade.BLL.Driver;
using Homemade.BLL.General;
using Homemade.DAL.Contract;
using Homemade.Model.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.Customer
{
    public class BlCustomerBalance
    {
        #region Decleration
        private readonly IUOW Uow;
        private readonly BLGeneral _bLGeneral;
        private readonly BlTransactionType _blTransactionType;

        public BlCustomerBalance(IUOW _uow, BLGeneral bLGeneral, BlTransactionType blTransactionType)
        {
            _bLGeneral = bLGeneral;
            _blTransactionType = blTransactionType;
            this.Uow = _uow;
        }
        #endregion
        #region Actions
        //اخر مبلغ للسائق
        public decimal GetLastBlance(int CustomerID)
        {
            var Balance = Uow.CustomerBalance.GetAll(e => e.CustomersID == CustomerID);
            if (Balance.Count() > 0)
            {
                var last =
                    Math.Round(Balance.Where(s => s.TypeOperationID == (int)TypeOperationEnum.CR/* && s.IsPaid*/).Sum(e => e.Amount), 2)
                    -
                    Math.Round(Balance.Where(s => s.TypeOperationID == (int)TypeOperationEnum.DR/* && s.IsPaid*/).Sum(e => e.Amount), 2);
                return Math.Round(last, 2);
            }
            else
            {
                return 0;
            }
        }
        //public double GetLastTripsCharge(int CustomerID)
        //{
        //    var Trips = Uow.TripMaster.GetAll(e => e.CaptainPaidID == (int)PaidStatusEnum.Created && e.CustomerID == CustomerID && e.TripStatusID == (int)TripStatusEnum.Finish);
        //    if (Trips.Any())
        //    {
        //        var Triplist = Trips.Select(s => s.TripMasterID).ToList();
        //        return Uow.TripFinanical.GetAll(e => Triplist.Any(s => s == e.TripMasterID)).Sum(x => x.CreditCustomer);
        //    }
        //    return 0;
        //}
        //public double GettripsCharge(List<int> trips)
        //{
        //    if (trips.Any())
        //    {
        //        return Uow.TripFinanical.GetAll(e => trips.Any(s => s == e.TripMasterID)).Sum(x => x.CreditCustomer);
        //    }
        //    return 0;
        //}
        //public List<int> GetListTrips(int CustomerID)
        //{
        //    var Trips = Uow.TripMaster.GetAll(e => e.CaptainPaidID == (int)PaidStatusEnum.Created && e.TripFinanical.FirstOrDefault().CreditCustomer > 0 && e.CustomerID == CustomerID && e.TripStatusID == (int)TripStatusEnum.Finish);
        //    if (Trips.Any())
        //    {
        //        return Trips.Select(s => s.TripMasterID).ToList();
        //    }
        //    return null;
        //}
        //public List<int> GetListTripsCreatedSTC(int CustomerID)
        //{
        //    var Trips = Uow.TripMaster.GetAll(e => e.CaptainPaidID == (int)PaidStatusEnum.Created_STC && e.TripFinanical.FirstOrDefault().CreditCustomer > 0 && e.CustomerID == CustomerID && e.TripStatusID == (int)TripStatusEnum.Finish);
        //    if (Trips.Any())
        //    {
        //        return Trips.Select(s => s.TripMasterID).ToList();
        //    }
        //    return null;
        //}
        //إضافة الماليات للسائق
        public CustomerBalance InsertBalanceWithOutCommit(int oPerationType, decimal amount, int transactionType, int CustomerId, int userId, int? tripId, DateTime DateOperation, string Descripe, string Attacment,decimal currentbefore)
        {
            var Before = GetLastBlance(CustomerId)-currentbefore;
            var CustomerBalance = new CustomerBalance()
            {
                Amount = amount,
                Before = Before,
                CreateDate = _bLGeneral.GetDateTimeNow(),
                CustomersID = CustomerId,
                IsDeleted = false,
                TransactionTypeID = transactionType,
                TypeOperationID = oPerationType,
                UserId = userId,
                After = oPerationType == (int)TypeOperationEnum.CR ? (amount + Before) : (Before - amount),
                CustomerBlanceGuid = Guid.NewGuid(),
                OrderVendorID = tripId,
                DateOperation = DateOperation,
                Attachment = Attacment,
                Discripe = Descripe
            };
            CustomerBalance = Uow.CustomerBalance.Insert(CustomerBalance);
            return CustomerBalance;
        }
        public CustomerBalance InsertBalance(int oPerationType, decimal amount, int transactionType, int CustomerId, int userId, int? tripId, DateTime DateOperation, string Descripe, string Attacment)
        {
            var Before = GetLastBlance(CustomerId);
            var CustomerBalance = new CustomerBalance()
            {
                Amount = amount,
                Before = Before,
                CreateDate = _bLGeneral.GetDateTimeNow(),
                CustomersID = CustomerId,
                IsDeleted = false,
                TransactionTypeID = transactionType,
                TypeOperationID = oPerationType,
                UserId = userId,
                After = oPerationType == (int)TypeOperationEnum.CR ? (amount + Before) : (Before - amount),
                CustomerBlanceGuid = Guid.NewGuid(),
                OrderVendorID = tripId,
                DateOperation = DateOperation,
                Attachment = Attacment,
                //IsPaid = oPerationType == (int)TypeOperationEnum.DR ? true : false,
                Discripe = Descripe
            };
            CustomerBalance = Uow.CustomerBalance.Insert(CustomerBalance);
            Uow.Commit();
            return CustomerBalance;
        }

        #endregion
    }
}
