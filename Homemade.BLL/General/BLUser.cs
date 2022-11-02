using Homemade.DAL.Contract;
using Homemade.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.General
{
    public class BLUser : BaseEntity
    {
        #region Declartion
        private readonly IUOW Uow;
        public BLUser(IUOW _uow)
        {
            this.Uow = _uow;
        }
        #endregion
        /// <summary>
        /// Get User Data By User Name
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public User GetUserInfo(string userName)
        {
            return Uow.User.GetAll(e => e.UserName == userName || e.Email == userName).FirstOrDefault();
        }
        /// <summary>
        /// Get User Data By User Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUserInfo(int userId)
        {
            return Uow.User.GetAll(e => e.Id == userId).FirstOrDefault();
        }
        /// <summary>
        /// Get User Info By User Id
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        //public User GetUserInfo(int userID)
        //{
        //    return Uow.User.DbSet.FirstOrDefault(e => e.Id==userID);
        //}
        public User GetUserInfoByPasswordHash(string passwordHash)
        {
            return Uow.User.GetAll(e => e.PasswordHash == passwordHash).FirstOrDefault();
        }
        public bool IsExistPhoneNumberInUser(string PhoneNumber)
        {
            return Uow.User.GetAll(e => e.PhoneNumber == PhoneNumber).Any();
        }
        public bool IsExistIDNoInUser(string UserName)
        {
            return Uow.User.GetAll(e => e.UserName == UserName).Any();
        }
        public int GetFirstUserId()
        {
            return Uow.User.GetAll().FirstOrDefault().Id;
        }
    }
}
