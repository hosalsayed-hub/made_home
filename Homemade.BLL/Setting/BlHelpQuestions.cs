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
   public class BlHelpQuestions
    {
        #region Decleration
        private readonly IUOW Uow;
        public BlHelpQuestions(IUOW _uow)
        {
            this.Uow = _uow;
        }
        #endregion
        #region Get
        public IQueryable<HelpQuestionsViewModelApi> GetAllHelpQuestionsViewModelApiByIsForTrip(bool IsForTrip, string accLanguage, int UserTypeId) => Uow.HelpQuestions.GetAll(x => !x.IsDeleted && 
        x.IsForOrder == IsForTrip && x.HelpUserType == UserTypeId).Select(x => new HelpQuestionsViewModelApi()
        {
            HelpQuestionsID = x.HelpQuestionsID,
            name = accLanguage == "ar" ? x.NameAR : x.NameEN,
        });
        public HelpQuestions GetById(int id)
        {
            return Uow.HelpQuestions.GetAll(x => x.HelpQuestionsID == id).FirstOrDefault();
        }
        #endregion
    }
}
