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
    public class BlJobs
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlJobs(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Helpers
        /// Check If Jobs Name Is Used Before Or Not اختبار هل اسم الوظيفة عربي موجود من قبل ام لا
        public bool IsExist(int JobsID, string JobsName, Language language) => JobsID != 0 ? (language == Language.Arabic ? Uow.Jobs.GetAll().Any(p => p.JobsID != JobsID && p.NameAR.Trim() == JobsName && !p.IsDeleted) : Uow.Jobs.GetAll().Any(p => p.JobsID != JobsID && p.NameEN.Trim() == JobsName && !p.IsDeleted)) : (language == Language.Arabic ? Uow.Jobs.GetAll().Any(p => p.NameAR.Trim() == JobsName && !p.IsDeleted) : Uow.Jobs.GetAll().Any(p => p.NameEN.Trim() == JobsName && !p.IsDeleted));
        public List<JobsViewModel> GetJobstaghelper() => Uow.Jobs.GetAll(p => !p.IsDeleted).OrderByDescending(p => p.CreateDate).Select(p => new JobsViewModel()
        {
            JobsID = p.JobsID,
            JobsNameAR = p.NameAR,
            JobsNameEN = p.NameEN,
            IsEnable = p.IsEnable,
            EnableDate = p.EnableDate,
            IsDeleted = p.IsDeleted,
            CreateDate = p.CreateDate,
            DeleteDate = p.DeleteDate,
            UserId = p.UserId,
            UserDelete = p.UserDelete,
            UserEnable = p.UserEnable,
           DescriptionAr = !string.IsNullOrEmpty(p.DescriptionAr) ? p.DescriptionAr : "",
            DescriptionEn = !string.IsNullOrEmpty(p.DescriptionEn) ? p.DescriptionEn : "",
            UpdateDate = p.UpdateDate,
            UserUpdate = p.UserUpdate,
            CreationDate = p.CreateDate,
            JobsGuid = p.JobsGuid
        }).ToList();
        #endregion
        #region Actions
        /// Add New Jobs اضافة دولة
        public bool Insert(JobsViewModel JobsModel, out int JobsID)
        {
            Jobs Jobs = new Jobs
            {
                NameAR = JobsModel.JobsNameAR,
                NameEN = JobsModel.JobsNameEN,
                CreateDate = _blGeneral.GetDateTimeNow(),
                UserId = JobsModel.UserId,
                JobTypeId = 1,
                IsDeleted = false,
                IsEnable = true,
                DescriptionAr = JobsModel.DescriptionAr,
                DescriptionEn = JobsModel.DescriptionEn,
            };
            Jobs = Uow.Jobs.Insert(Jobs);
            Uow.Commit();
            JobsID = Jobs.JobsID;
            return true;
        }
        /// Delete Jobs By ID حذف الوظيفة
        public bool Delete(int id, int userId)
        {
            var Jobs = GetById(id);
            if (Jobs != null)
            {
                Jobs.IsDeleted = true;
                Jobs.UserDelete = userId;
                Jobs.DeleteDate = _blGeneral.GetDateTimeNow();
                Uow.Jobs.Update(Jobs);
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
            var data = Uow.Jobs.GetAll(p => p.JobsID == id).FirstOrDefault();

            if (data != null)
            {
                data.UserEnable = userId;
                data.IsEnable = data.IsEnable == Convert.ToBoolean(StatusEnable.Enable) ? Convert.ToBoolean(StatusEnable.Disable) : Convert.ToBoolean(StatusEnable.Enable);
                data.EnableDate = _blGeneral.GetDateTimeNow();
                data = Uow.Jobs.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        /// Update Jobs تعديل الوظيفة
        public bool Update(JobsViewModel model)
        {
            IQueryable<Jobs> data = Uow.Jobs.GetAll(p => p.JobsID == model.JobsID);
            if (data != null)
            {
                Jobs Jobs = data.FirstOrDefault();
                Jobs.NameAR = model.JobsNameAR;
                Jobs.NameEN = model.JobsNameEN;
                Jobs.UpdateDate = model.UpdateDate;
                Jobs.UserUpdate = model.UserUpdate;
                //Jobs.JobTypeId = model.JobTypeId;
                Jobs.DescriptionEn = model.DescriptionEn;
                Jobs.DescriptionAr = model.DescriptionAr;
                Jobs = Uow.Jobs.Update(Jobs);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public Jobs GetById(int id) => Uow.Jobs.GetAll(x => x.JobsID == id).FirstOrDefault();
        #endregion
        #region GetJobs
        /// Get All Jobs جلب الوظيفة
        public List<Jobs> GetAllJobs()
        {
            var data = Uow.Jobs.GetAll(x => !x.IsDeleted).ToList();
            return data;
        }
        public List<JobsViewModel> GetJobsByJobTypeId(int JobTypeId) => Uow.Jobs.GetAll(p => p.JobTypeId == JobTypeId && !p.IsDeleted).Select(p => new JobsViewModel() { JobsID = p.JobsID, JobsNameAR = p.NameAR, JobsNameEN = p.NameEN }).ToList();
        #endregion
    }
}
