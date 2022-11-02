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
   public class BlTransactionType
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlTransactionType(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Helpers
        public IQueryable<TransactionType> GetAllTransactionType()
        {
            return Uow.TransactionType.GetAll(query => !query.IsDeleted).OrderBy(p => p.CreateDate);
        }
        #endregion
        #region GetTransactionTypeData
        public TransactionType GetById(int id) => Uow.TransactionType.GetAll(x => x.TransactionTypeID == id).FirstOrDefault();
        #endregion
    }
}
