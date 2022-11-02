using Homemade.BLL.Driver;
using Homemade.BLL.General;
using Homemade.DAL.Contract;
using Homemade.Model.Vendor;
using System;
using System.Linq;

namespace Homemade.BLL.Vendor
{
    public class BlVendorBalance
    {
        #region Decleration
        private readonly IUOW Uow;
        private readonly BLGeneral _bLGeneral;
        public BlVendorBalance(IUOW _uow, BLGeneral bLGeneral)
        {
            _bLGeneral = bLGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Get
        //رصيد التاجر
        public decimal GetLastBlance(int VendorID)
        {
            var Balance = Uow.VendorBalance.GetAll(e => e.VendorsID == VendorID);
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
        #endregion
        #region Action
        public VendorBalance InsertBalanceWithOutCommit(int oPerationType, decimal amount, int transactionType, int VendorId, int userId, int? tripId, DateTime DateOperation, string Descripe, string Attacment)
        {
            var Before = GetLastBlance(VendorId);
            var VendorBalance = new VendorBalance()
            {
                Amount = amount,
                Before = Before,
                CreateDate = _bLGeneral.GetDateTimeNow(),
                VendorsID = VendorId,
                IsDeleted = false,
                TransactionTypeID = transactionType,
                TypeOperationID = oPerationType,
                UserId = userId,
                After = oPerationType == (int)TypeOperationEnum.CR ? (amount + Before) : (Before - amount),
                VendorBlanceGuid = Guid.NewGuid(),
                OrderVendorID = tripId,
                DateOperation = DateOperation,
                Attachment = Attacment,
                Discripe = Descripe
            };
            VendorBalance = Uow.VendorBalance.Insert(VendorBalance);
            return VendorBalance;
        }
        public VendorBalance InsertBalance(int oPerationType, decimal amount, int transactionType, int VendorId, int userId, int? tripId, DateTime DateOperation, string Descripe, string Attacment)
        {
            var Before = GetLastBlance(VendorId);
            var VendorBalance = new VendorBalance()
            {
                Amount = amount,
                Before = Before,
                CreateDate = _bLGeneral.GetDateTimeNow(),
                VendorsID = VendorId,
                IsDeleted = false,
                TransactionTypeID = transactionType,
                TypeOperationID = oPerationType,
                UserId = userId,
                After = oPerationType == (int)TypeOperationEnum.CR ? (amount + Before) : (Before - amount),
                VendorBlanceGuid = Guid.NewGuid(),
                OrderVendorID = tripId,
                DateOperation = DateOperation,
                Attachment = Attacment,
                Discripe = Descripe
            };
            VendorBalance = Uow.VendorBalance.Insert(VendorBalance);
            Uow.Commit();
            return VendorBalance;
        }
        #endregion
    }
}
