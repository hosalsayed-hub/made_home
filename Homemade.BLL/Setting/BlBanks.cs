using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Setting;
using Homemade.DAL.Contract;
using Homemade.Model.Setting;

namespace Homemade.BLL.Setting
{
    public class BlBanks
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlBanks(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Helpers
        /// Check If Bank Name Is Used Before Or Not اختبار هل اسم البنك عربي موجود من قبل ام لا
        public bool IsExist(int bankID, string bankName, Language language) => bankID != 0 ? (language == Language.Arabic ? Uow.Banks.GetAll().Any(p => p.BanksID != bankID && p.NameAR.Trim() == bankName && !p.IsDeleted) : Uow.Banks.GetAll().Any(p => p.BanksID != bankID && p.NameEN.Trim() == bankName && !p.IsDeleted)) : (language == Language.Arabic ? Uow.Banks.GetAll().Any(p => p.NameAR.Trim() == bankName && !p.IsDeleted) : Uow.Banks.GetAll().Any(p => p.NameEN.Trim() == bankName && !p.IsDeleted));
        #endregion
        #region Actions
        /// Add New Bank اضافة البنك
        public bool Insert(BanksViewModel bankModel, out int bankID)
        {
            Banks bank = new Banks
            {
                NameAR = bankModel.BanksNameAR,
                NameEN = bankModel.BanksNameEN,
                CreateDate = _blGeneral.GetDateTimeNow(),
                UserId = bankModel.UserId , 
                IsEnable = true
                
            };
            bank = Uow.Banks.Insert(bank);
            Uow.Commit();
            bankID = bank.BanksID;
            return true;
        }
        /// Delete Bank By ID حذف البنكة
        public bool Delete(int id, int userId)
        {
            var Bank = GetById(id);
            if (Bank != null)
            {
                Bank.IsDeleted = true;
                Bank.UserDelete = userId;
                Bank.DeleteDate = _blGeneral.GetDateTimeNow();
                Uow.Banks.Update(Bank);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ChangeStatus(int id, int userId)
        {
            var data = Uow.Banks.GetAll(p => p.BanksID == id).FirstOrDefault();

            if (data != null)
            {
                data.UserEnable = userId;
                data.IsEnable = data.IsEnable == Convert.ToBoolean(StatusEnable.Enable) ? Convert.ToBoolean(StatusEnable.Disable) : Convert.ToBoolean(StatusEnable.Enable);
                data.EnableDate = _blGeneral.GetDateTimeNow();
                data = Uow.Banks.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        /// Update Bank تعديل البنكة
        public bool Update(BanksViewModel model)
        {
            IQueryable<Banks> data = Uow.Banks.GetAll(p => p.BanksID == model.BanksID);
            if (data != null)
            {
                Banks bank = data.FirstOrDefault();
                bank.NameAR = model.BanksNameAR;
                bank.NameEN = model.BanksNameEN;
                bank.UpdateDate = model.UpdateDate;
                bank.UserUpdate = model.UserUpdate;
                bank = Uow.Banks.Update(bank);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public Banks GetById(int id) => Uow.Banks.GetAll(x => x.BanksID == id).FirstOrDefault();
        #endregion
        #region GetBanks
        /// Get All Banks جلب البنك
        public List<BankViewModelApi> GetBanks(string accLanguage) => Uow.Banks.GetAll(p => !p.IsDeleted).OrderByDescending(p => p.CreateDate).Select(p => new BankViewModelApi()
        {
            bankID = p.BanksID,
            name = accLanguage == "ar" ? p.NameAR : p.NameEN
        }).ToList();
        public class BankViewModelApi
        {
            public int bankID { get; set; }
            public string name { get; set; }
        }
        public List<BanksViewModel> GetBankstaghelper() => Uow.Banks.GetAll(p => !p.IsDeleted).OrderByDescending(p => p.CreateDate).Select(p => new BanksViewModel()
        { BanksID = p.BanksID, BanksNameAR = p.NameAR, BanksNameEN = p.NameEN , IsEnable = p.IsEnable}).ToList();
        public List<BanksViewModelApi> BanksViewModelApiList(string lang) => Uow.Banks.GetAll(p => !p.IsDeleted).OrderByDescending(p => p.CreateDate).Select(p => new BanksViewModelApi()
        { BankId = p.BanksID, BankName = lang == "ar" ? p.NameAR : p.NameEN}).ToList();
        public List<Banks> GetAllBanks()
        {
            var data = Uow.Banks.GetAll(x => !x.IsDeleted).ToList();
            return data;
        }
        #endregion
    }
}
