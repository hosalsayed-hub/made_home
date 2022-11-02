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
    public class BlSubscribe
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlSubscribe(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Helpers
        public bool IsExistEmail(string Email)
        {
            return Uow.Subscribe.GetAll(x => x.Email == Email).Any();
        }
        public IQueryable<SubscribeViewModel> GetAllSubscribeViewModel()
        {

            var getData = Uow.Subscribe.GetAll().OrderByDescending(p => p.CreateDate)
            .Select(p => new SubscribeViewModel()
            {
                Email = p.Email,
                Name = p.Name,
                CreateDate = p.CreateDate,
                SubscribeGuid = p.SubscribeGuid,
                SubscribeID = p.SubscribeID
            });
            return getData;
        }
        public IQueryable<Subscribe> GetAllSubscribe()
        {
            var getData = Uow.Subscribe.GetAll().OrderByDescending(p => p.CreateDate);
            return getData;
        }
        public Subscribe GetById(int id) => Uow.Subscribe.GetAll(x => x.SubscribeID == id).FirstOrDefault();
        #endregion
        #region Actions
        public bool Insert(string Name, string Email)
        {
            Subscribe Subscribe = new Subscribe
            {
                Name = Name,
                Email = Email,
                CreateDate = _blGeneral.GetDateTimeNow(),
                SubscribeGuid = Guid.NewGuid(),
            };
            Uow.Subscribe.Insert(Subscribe);
            Uow.Commit();
            return true;
        }
        #endregion
    }
}
