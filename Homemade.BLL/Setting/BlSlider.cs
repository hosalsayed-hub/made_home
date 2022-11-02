using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Setting;
using Homemade.DAL.Contract;
using Homemade.Model.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.Setting
{
    public class BlSlider
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlSlider(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Actions
        public bool Insert(SliderViewModel Model, out int SliderID)
        {
            Slider Slider = new Slider
            {
                NameAR = Model.SliderNameAR,
                NameEN = Model.SliderNameEN,
                DescAr= Model.DescAr,
                DescEN=Model.DescEN,
                Image=Model.Image,
                SliderTypeId=Model.SliderTypeId,
                CreateDate = _blGeneral.GetDateTimeNow(),
                IsEnable = true,
                UserId = Model.UserId,
                DisplayIn = Model.DisplayIn, 
            };
            Slider = Uow.Slider.Insert(Slider);
            Uow.Commit();
            SliderID = Slider.SliderID;
            return true; ;
        }
        #region Get
        public bool Update(SliderViewModel model)
        {
            var data = Uow.Slider.GetAll(p => p.SliderID == model.SliderID).FirstOrDefault();
            if (data != null)
            {
                data.NameAR = model.SliderNameAR;

                data.NameEN = model.SliderNameEN;
                data.Image = model.Image ?? data.Image;
                data.SliderTypeId = model.SliderTypeId;
                data.DescAr = model.DescAr;
                data.DescEN = model.DescEN;
                data.UserUpdate = model.UserUpdate;
                data.DisplayIn = model.DisplayIn;
                data.UpdateDate = _blGeneral.GetDateTimeNow();
                Uow.Slider.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        #endregion
        public bool Delete(int id, int userId)
        {
            var data = Uow.Slider.GetAll(p => p.SliderID == id).FirstOrDefault();
            if (data != null)
            {
                data.IsDeleted = true;
                data.DeleteDate = _blGeneral.GetDateTimeNow();
                data.UserDelete = userId;
                Uow.Slider.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        public bool ChangeStatus(int id, int userId)
        {
            var data = Uow.Slider.GetAll(p => p.SliderID == id).FirstOrDefault();

            if (data != null)
            {
                data.UserEnable = userId;
                data.IsEnable = data.IsEnable == Convert.ToBoolean(StatusEnable.Enable) ? Convert.ToBoolean(StatusEnable.Disable) : Convert.ToBoolean(StatusEnable.Enable);
                data.EnableDate = _blGeneral.GetDateTimeNow();
                data = Uow.Slider.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        #endregion
        #region GetMainCategory
        public List<Slider> GetAlSliders()
        {
            var data = Uow.Slider.GetAll(x => !x.IsDeleted).ToList();
            return data;
        }
        public IQueryable<SliderViewModel> GetSliders(string lang,string mainpath)
        {
            var xdata = Uow.Slider.GetAll(x => !x.IsDeleted).OrderBy(p => p.SliderID).OrderByDescending(p => p.CreateDate)
             .Select(p => new SliderViewModel
             {
                 SliderNameAR = p.NameAR,
                 SliderNameEN = p.NameEN,
                 DescAr = p.DescAr,
                 DescEN = p.DescEN,
                 Image = mainpath + p.Image,
                 SliderTypeId = p.SliderTypeId,
                 IsEnable = p.IsEnable,
                 DisplayIn = p.DisplayIn,
                 SliderName = lang == "ar" ? p.NameAR : p.NameEN,
                 Desc = lang == "ar" ? p.DescAr : p.DescEN,
                 UpdateDate = p.UpdateDate,


             });
            return xdata;
        }
        #endregion
    }
}
