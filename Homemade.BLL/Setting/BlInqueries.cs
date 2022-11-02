using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Setting;
using Homemade.BLL.ViewModel.Site;
using Homemade.DAL.Contract;
using Homemade.Model.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.Setting
{
   public class BlInqueries
    {

        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlInqueries(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Helper
        public Inqueries GetByID(int InqueriesID)
        {
            return Uow.Inqueries.GetAll(x => x.InqueriesID == InqueriesID).FirstOrDefault();
        }
        public IQueryable<InqueriesViewModel> GetInqueries()
        {
            var xdata = Uow.Inqueries.GetAll().OrderBy(p => p.InqueriesID).OrderByDescending(p => p.CreateDate)
             .Select(p => new InqueriesViewModel
             {
                 InqueriesID = p.InqueriesID,
                 Name = p.Name,
                 Email = p.Email,
                 MobileNo = p.MobileNo,
                 Message = p.Message,
                 CreateDate = p.CreateDate,
             });
            return xdata;
        }
        #endregion
        #region Action
        public bool Insert(InqueriesViewModel model, out int InqueriesID)
        {
            Inqueries Inqueries = new Inqueries
            {
                Name = model.Name,
                Email = model.Email,
                Message = model.Message,
                MobileNo = model.MobileNo,
                CreateDate = _blGeneral.GetDateTimeNow(),

            };
            Inqueries = Uow.Inqueries.Insert(Inqueries);
            Uow.Commit();
            InqueriesID = Inqueries.InqueriesID;
            return true; 
        }
        public bool Insert(SiteInqueriesViewModel model)
        {
            Inqueries Inqueries = new Inqueries
            {
                Name = model.Name,
                Email = model.Email,
                Message = model.Message,
                MobileNo = model.MobileNo,
                CreateDate = _blGeneral.GetDateTimeNow(),
                InqueriesGuid = Guid.NewGuid(),
            };
            Inqueries = Uow.Inqueries.Insert(Inqueries);
            Uow.Commit();
            return true; 
        }
        public bool ReplyInqueries(int InqueriesID, string Message, string EmailContent, string Subject, bool IsEmail, bool IsSMS, int UserId)
        {
            InqueriesReply inqueriesReply = new InqueriesReply
            {

                CreateDate = _blGeneral.GetDateTimeNow(),
                InqueriesReplyGuid = Guid.NewGuid(),
                IsDeleted = false,
                IsEnable = true,
                InqueriesID = InqueriesID,
                Message = Message,
                IsEmail = IsEmail,
                IsSMS = IsSMS,
                UserId = UserId,
                Subject = Subject,
                EmailContent = EmailContent,
            };
            inqueriesReply = Uow.InqueriesReply.Insert(inqueriesReply);
            Uow.Commit();
            return true;
        }
        public bool Update(InqueriesViewModel model)
        {
            var data = Uow.Inqueries.GetAll(p => p.InqueriesID == model.InqueriesID).FirstOrDefault();
            if (data != null)
            {
                data.Name = model.Name;
                data.Message = model.Message;
                data.Email = model.Email;
                data.MobileNo = model.MobileNo;

                Uow.Inqueries.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        #endregion
    }
}
