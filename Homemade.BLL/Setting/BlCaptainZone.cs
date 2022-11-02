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
    public class BlCaptainZone
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlCaptainZone(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Helpers
        public CaptainZone GetById(int id) => Uow.CaptainZone.GetAll(x => x.CaptainZoneID == id).FirstOrDefault();
        public bool IsExist(int BlockID, int DriversID)
        {
            return Uow.CaptainZone.GetAll().Any(p => p.BlockID == BlockID && p.DriversID == DriversID && !p.IsDeleted);
        }
        public CaptainZone GetCaptainZoneDeletedByBlockAndDriver(int BlockID, int DriversID)
        {
            return Uow.CaptainZone.GetAll(p => p.BlockID == BlockID && p.DriversID == DriversID && p.IsDeleted).FirstOrDefault();
        }
        public List<CaptainZoneViewModel> GetAllCaptainZoneViewModelByCaptainZoneChecked(int? DriversID, string lang)
        {
            return Uow.CaptainZone.GetAll(p => !p.IsDeleted
              && (DriversID != null ? p.DriversID == DriversID : false)
              , "Block").OrderByDescending(p => p.CreateDate).Select(p => new CaptainZoneViewModel()
              {
                  BlockID = p.BlockID,
                  BlockName = lang == "ar" ? p.Block.NameAR : p.Block.NameEN,
                  DriversID = p.DriversID,
                  CaptainZoneGuid = p.CaptainZoneGuid,
                  CaptainZoneID = p.CaptainZoneID,
              }).ToList();
        }
        public List<BlockViewModel> GetAllBlockViewModelByCaptainZoneNotChecked(int? DriversID, string lang)
        {
            return Uow.Block.GetAll(p => !p.IsDeleted
              && (DriversID != null ? !p.CaptainZone.Any(x => x.DriversID == DriversID && !x.IsDeleted) : false)
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
        public bool Insert(List<int> ListCaptainID, List<int> ListBlockID, int UserId)
        {
            foreach (var DriversID in ListCaptainID)
            {
                foreach (var BlockID in ListBlockID)
                {
                    if (!IsExist(BlockID, DriversID))
                    {
                        var captainZone = GetCaptainZoneDeletedByBlockAndDriver(BlockID, DriversID);
                        if (captainZone == null)
                        {
                            CaptainZone CaptainZone = new CaptainZone
                            {
                                CreateDate = _blGeneral.GetDateTimeNow(),
                                UserId = UserId,
                                IsDeleted = false,
                                IsEnable = true,
                                BlockID = BlockID,
                                DriversID = DriversID,
                            };
                            Uow.CaptainZone.Insert(CaptainZone);
                        }
                        else
                        {
                            captainZone.IsDeleted = false;
                            Uow.CaptainZone.Update(captainZone);
                        }
                    }
                }
            }
            Uow.Commit();
            return true;
        }
        public bool Delete(int id, int userId)
        {
            var captainZone = GetById(id);
            if (captainZone != null)
            {
                captainZone.IsDeleted = true;
                captainZone.UserDelete = userId;
                captainZone.DeleteDate = _blGeneral.GetDateTimeNow();
                Uow.CaptainZone.Update(captainZone);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool AddBlock(int BlockID, int DriversID, int userId)
        {
            if (!IsExist(BlockID, DriversID))
            {
                var captainZone = GetCaptainZoneDeletedByBlockAndDriver(BlockID, DriversID);
                if (captainZone == null)
                {
                    CaptainZone CaptainZone = new CaptainZone
                    {
                        CreateDate = _blGeneral.GetDateTimeNow(),
                        UserId = userId,
                        IsDeleted = false,
                        IsEnable = true,
                        BlockID = BlockID,
                        DriversID = DriversID,
                    };
                    Uow.CaptainZone.Insert(CaptainZone);
                    Uow.Commit();
                    return true;
                }
                else
                {
                    captainZone.IsDeleted = false;
                    Uow.CaptainZone.Update(captainZone);
                    Uow.Commit();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
