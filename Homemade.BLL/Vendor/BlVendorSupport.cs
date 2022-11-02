using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Vendor;
using Homemade.DAL.Contract;
using Homemade.Model.Vendor;
using System;
using System.Linq;

namespace Homemade.BLL.Vendor
{
   public class BlVendorSupport
    {
        #region Declarations
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlVendorSupport(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Actions
        public bool InsertVendorsHelp(int VendorID, int helpQuestionsID, int? tripMasterID, int userId, string Descripe, string routeImagefileName)
        {
            try
            {
                VendorSupport VendorsHelp = new VendorSupport()
                {
                    VendorSupportGuid = Guid.NewGuid(),
                    CreateDate = _blGeneral.GetDateTimeNow(),
                    IsDeleted = false,
                    VendorsID = VendorID,
                    HelpQuestionsID = helpQuestionsID,
                    OrderVendorID = tripMasterID,
                    UserId = userId,
                    Descripe = Descripe,
                    Image = routeImagefileName
                };
                Uow.VendorSupport.Insert(VendorsHelp);
                Uow.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}
