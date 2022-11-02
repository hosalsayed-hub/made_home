using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Driver;
using Homemade.DAL.Contract;
using Homemade.Model.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.Driver
{
   public class BlDriverSupport
    {
        #region Declarations
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlDriverSupport(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Get
        /// <summary>
        /// Get DriversHelpViewModel By Guid
        /// </summary>
        /// <param name="DriversHelpGuid"></param>
        /// <param name="accLanguage"></param>
        /// <param name="ImageView"></param>
        /// <returns></returns>
        public DriverSupportViewModel GetDriversHelpViewModelByGuid(Guid DriversHelpGuid, string accLanguage, string ImageView)
        {
            var getData = Uow.DriverSupport.GetAll(p => p.DriverSupportGuid == DriversHelpGuid).Select(m => new DriverSupportViewModel()
            {
                DriversID = m.DriversID,
                DriverSupportGuid = m.DriverSupportGuid,
                DriverSupportID = m.DriverSupportID,
                Descripe = m.Descripe ?? string.Empty,
                HelpQuestionsID = m.HelpQuestionsID,
                Image = !string.IsNullOrEmpty(m.Image) ? (ImageView + m.Image) : string.Empty,
                OrderVendorID = m.OrderVendorID,
                DriverName = accLanguage == "ar" ? m.Drivers.NameAr : m.Drivers.NameEn,
                DriverIDNo = m.Drivers.IDNo,
                DriverMobileNo = m.Drivers.PhoneNumber,
                HelpQuestionsName = accLanguage == "ar" ? m.HelpQuestions.NameAR : m.HelpQuestions.NameEN,
                CreateDate = m.CreateDate.ToString("dd/MM/yyyy HH:mm tt"),
            }).FirstOrDefault();
            return getData;
        }
        public IQueryable<DriverSupportViewModel> GetAllDriversHelpViewModel(string accLanguage, string ImageView)
        {
            var getData = Uow.DriverSupport.GetAll(p => !p.IsDeleted).OrderByDescending(a => a.CreateDate).Select(m => new DriverSupportViewModel()
            {
                DriversID = m.DriversID,
                DriverSupportGuid = m.DriverSupportGuid,
                DriverSupportID = m.DriverSupportID,
                Descripe = m.Descripe ?? string.Empty,
                HelpQuestionsID = m.HelpQuestionsID,
                Image = !string.IsNullOrEmpty(m.Image) ? (ImageView + m.Image) : string.Empty,
                OrderVendorID = m.OrderVendorID,
                DriverName = accLanguage == "ar" ? m.Drivers.NameAr : m.Drivers.NameEn,
                DriverIDNo = m.Drivers.IDNo,
                DriverMobileNo = m.Drivers.PhoneNumber,
                HelpQuestionsName = accLanguage == "ar" ? m.HelpQuestions.NameAR : m.HelpQuestions.NameEN,
                CreateDate = m.CreateDate.ToString("dd/MM/yyyy HH:mm tt"),
            });
            return getData;
        }

        #endregion
        #region Actions
        public bool InsertDriversHelp(int DriverID, int helpQuestionsID, int? tripMasterID, int userId, string Descripe, string routeImagefileName)
        {
            DriverSupport DriversHelp = new DriverSupport()
            {
                DriverSupportGuid = Guid.NewGuid(),
                CreateDate = _blGeneral.GetDateTimeNow(),
                IsDeleted = false,
                DriversID = DriverID,
                HelpQuestionsID = helpQuestionsID,
                OrderVendorID = tripMasterID,
                UserId = userId,
                Descripe = Descripe,
                Image = routeImagefileName
            };
            DriversHelp = Uow.DriverSupport.Insert(DriversHelp);
            Uow.Commit();
            return true;
        }
        #endregion
    }
}
