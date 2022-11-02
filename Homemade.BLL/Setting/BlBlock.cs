using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Setting;
using Homemade.DAL.Contract;
using Homemade.Model.Setting;

namespace Homemade.BLL.Setting
{
    public class BlBlock
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlBlock(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Helpers
        /// Check If Block Name Is Used Before Or Not اختبار هل اسم الأحياء عربي موجود من قبل ام لا
        public bool IsExist(int BlockID, int CityID, string BlockName, Language language) => BlockID != 0 ? (language == Language.Arabic ? Uow.Block.GetAll().Any(p => p.BlockID != BlockID && p.CityID == CityID && p.NameAR.Trim() == BlockName && !p.IsDeleted) : Uow.Block.GetAll().Any(p => p.BlockID != BlockID && p.CityID == CityID && p.NameEN.Trim() == BlockName && !p.IsDeleted)) : (language == Language.Arabic ? Uow.Block.GetAll().Any(p => p.NameAR.Trim() == BlockName && p.CityID == CityID && !p.IsDeleted) : Uow.Block.GetAll().Any(p => p.NameEN.Trim() == BlockName && p.CityID == CityID && !p.IsDeleted));
        public List<BlockViewModel> GetBlocktaghelper(string lang) => Uow.Block.GetAll(p => !p.IsDeleted).OrderByDescending(p => p.CreateDate).Select(p => new BlockViewModel()
        {
            BlockID = p.BlockID,
            BlockNameAR = p.NameAR,
            BlockNameEN = p.NameEN,
            CityID = p.CityID , 
            CityName = p.City!=null ? lang == "ar" ? p.City.NameAR : p.City.NameEN : "", 
            IsEnable = p.IsEnable,
            EnableDate = p.EnableDate,
            IsDeleted = p.IsDeleted,
            CreateDate = p.CreateDate,
            DeleteDate = p.DeleteDate,
            UserId = p.UserId,
            UserDelete = p.UserDelete,
            UserEnable = p.UserEnable,
            Code = p.Code , 
            UpdateDate = p.UpdateDate,
            UserUpdate = p.UserUpdate,
            BlockGuid = p.BlockGuid,
            BlockName = lang == "ar" ? p.NameAR : p.NameEN,
            IsEnableString = lang == "ar" ? (p.IsEnable ? "مفعل"  : "غير مفعل") : (p.IsEnable ? "Enabled" : "Disabled"),
        }).ToList();
        public List<BlockViewModel> GetAllBlockViewModelByCity(string[] ListCityID, string lang) {
            if (ListCityID != null)
            {
                if (ListCityID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListCityID = null;
                }
            }
            return Uow.Block.GetAll(p => !p.IsDeleted
              && (ListCityID != null ? ListCityID.Any(y => p.CityID.ToString() == y) : true)
              ).OrderByDescending(p => p.CreateDate).Select(p => new BlockViewModel()
              {
                  BlockID = p.BlockID,
                  BlockNameAR = p.NameAR,
                  BlockNameEN = p.NameEN,
                  CityID = p.CityID,
                  CityName = p.City != null ? lang == "ar" ? p.City.NameAR : p.City.NameEN : "",
                  IsEnable = p.IsEnable,
                  EnableDate = p.EnableDate,
                  IsDeleted = p.IsDeleted,
                  CreateDate = p.CreateDate,
                  DeleteDate = p.DeleteDate,
                  UserId = p.UserId,
                  UserDelete = p.UserDelete,
                  UserEnable = p.UserEnable,
                  Code = p.Code,
                  UpdateDate = p.UpdateDate,
                  UserUpdate = p.UserUpdate,
                  BlockGuid = p.BlockGuid,
                  BlockName = lang == "ar" ? p.NameAR : p.NameEN,
                  IsEnableString = lang == "ar" ? (p.IsEnable ? "مفعل" : "غير مفعل") : (p.IsEnable ? "Enabled" : "Disabled"),
              }).ToList();
        }
        #endregion
        #region Actions
        /// Add New Block اضافة حي
        public bool Insert(BlockViewModel BlockModel, out int BlockID)
        {
            Block Block = new Block
            {
                NameAR = BlockModel.BlockNameAR,
                NameEN = BlockModel.BlockNameEN,
                CreateDate = _blGeneral.GetDateTimeNow(),
                UserId = BlockModel.UserId,
                IsDeleted = false,
                IsEnable = true,
               Code = BlockModel.Code , 
                CityID = BlockModel.CityID
            };
            Block = Uow.Block.Insert(Block);
            Uow.Commit();
            BlockID = Block.BlockID;
            return true;
        }
        /// Delete Block By ID حذف الأحياءة
        public bool Delete(int id, int userId)
        {
            var Block = GetById(id);
            if (Block != null)
            {
                Block.IsDeleted = true;
                Block.UserDelete = userId;
                Block.DeleteDate = _blGeneral.GetDateTimeNow();
                Uow.Block.Update(Block);
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
            var data = Uow.Block.GetAll(p => p.BlockID == id).FirstOrDefault();

            if (data != null)
            {
                data.UserEnable = userId;
                data.IsEnable = data.IsEnable == Convert.ToBoolean(StatusEnable.Enable) ? Convert.ToBoolean(StatusEnable.Disable) : Convert.ToBoolean(StatusEnable.Enable);
                data.EnableDate = _blGeneral.GetDateTimeNow();
                data = Uow.Block.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        /// Update Block تعديل الأحياءة
        public bool Update(BlockViewModel model)
        {
            IQueryable<Block> data = Uow.Block.GetAll(p => p.BlockID == model.BlockID);
            if (data != null)
            {
                Block Block = data.FirstOrDefault();
                Block.NameAR = model.BlockNameAR;
                Block.NameEN = model.BlockNameEN;
                Block.UpdateDate = model.UpdateDate;
                Block.UserUpdate = model.UserUpdate;
                Block.Code = model.Code;
                Block = Uow.Block.Update(Block);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public Block GetById(int id) => Uow.Block.GetAll(x => x.BlockID == id).FirstOrDefault();
        #endregion
        #region Get
        /// Get All Block جلب الأحياء
        public List<BlockViewModelApi> GetAllBlocksByListCityApi(string accLanguage, int cityId) => Uow.Block.GetAll(p => !p.IsDeleted && p.CityID == cityId).Select(p => new BlockViewModelApi()
        {
            blockId = p.BlockID,
            blockName = accLanguage == "ar" ? p.NameAR : p.NameEN,
        }).ToList();
        public List<Block> GetAllBlock()
        {
            var data = Uow.Block.GetAll(x => !x.IsDeleted).ToList();
            return data;
        }
        public List<Block> GetAllBlocksByListCity(string[] ListCityID)
        {
            if (ListCityID != null)
            {
                if (ListCityID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListCityID = null;
                }
            }
            return Uow.Block.GetAll(x => !x.IsDeleted && (ListCityID != null ? ListCityID.Any(y => y == x.CityID.ToString()) : false)).ToList();
        }
        public List<Block> GetAllBlocksByListCity(int CityID)
        {
            return Uow.Block.GetAll(x => !x.IsDeleted && x.CityID == CityID).ToList();
        }
        #endregion
    }
}
