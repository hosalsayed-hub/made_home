using Homemade.BLL.General;
using Homemade.DAL.Contract;
using Homemade.Model.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.Setting
{
    public class BlTokens
    {
        #region Decleration
        private readonly IUOW Uow;
        private readonly BLGeneral _blGeneral;
        public BlTokens(IUOW _uow, BLGeneral blGeneral)
        {
            this.Uow = _uow;
            _blGeneral = blGeneral;
        }
        #endregion
        #region Actions
        public bool UpdateUserJWTToken(int userID, string JWTToken)
        {
            try
            {
                var user = Uow.User.GetAll(e => e.Id == userID).FirstOrDefault();
                user.UserJWTToken = JWTToken;
                Uow.User.Update(user);
                Uow.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// اضافة توكينز جديد
        /// </summary>
        /// <param name="_Tokens"></param>
        /// <returns></returns>
        public bool AddTokens(int userID, int deviceType, string token, int UserType)
        {
            if (GetMobileDataByUserID(userID) != null)
            {
                var USerTdata = GetMobileDataByUserID(userID);
                USerTdata.TokenVal = token;
                USerTdata.DeviceType = deviceType;
                USerTdata.UpdateDate = _blGeneral.GetDateTimeNow();
                USerTdata.UserType = UserType;
                USerTdata = Uow.Tokens.Update(USerTdata);
                Uow.Commit();
            }
            else
            {
                Tokens _Tokens = new Tokens()
                {
                    DeviceType = deviceType,
                    TokenVal = token,
                    CreateDate = _blGeneral.GetDateTimeNow(),
                    UserID = userID, 
                    UserType = UserType,
                };
                _Tokens = Uow.Tokens.Insert(_Tokens);
                Uow.Commit();
            }
            return true;

        }
        /// <summary>
        /// تحديث التوكين
        /// </summary>
        /// <param name="_Tokens"></param>
        /// <returns></returns>
        public bool UpdateTokens(int userId, int deviceType, string token)
        {
            var _Tokens = GetMobileDataByUserID(userId);
            if (_Tokens != null)
            {
                _Tokens.DeviceType = deviceType;
                _Tokens.TokenVal = token;
                _Tokens.UpdateDate = _blGeneral.GetDateTimeNow();
                Uow.Tokens.Update(_Tokens);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region GetDataToken
        /// <summary>
        /// جلب رقم الهاتف بناءا على اليوزر اي دي
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public Tokens GetMobileDataByUserID(int UserID)
        {
            return Uow.Tokens.GetAll(e => e.UserID == UserID).FirstOrDefault();
        }
        #endregion
        #region Check User Valid
        public int IsValidUserJWTTokenCustomer(string JWTToken)
        {
            try
            {
                var issaved = Uow.User.GetAll(e => e.UserType == (int)UserTypeEnum.Customer && e.UserJWTToken == JWTToken).FirstOrDefault();
                if (issaved != null)
                {
                    return issaved.Id;
                }

            }
            catch (Exception)
            {
                return 0;
            }
            return 0;
        }
        public int IsValidUserJWTTokenDriver(string JWTToken)
        {
            try
            {
                var issaved = Uow.User.GetAll(e => e.UserType == (int)UserTypeEnum.Driver && e.UserJWTToken == JWTToken).FirstOrDefault();
                if (issaved != null)
                {
                    return issaved.Id;
                }

            }
            catch (Exception)
            {
                return 0;
            }
            return 0;
        }
        public int IsValidUserJWTTokenVendor(string JWTToken)
        {
            try
            {
                var issaved = Uow.User.GetAll(e => e.UserType == (int)UserTypeEnum.Vendor && e.UserJWTToken == JWTToken).FirstOrDefault();
                if (issaved != null)
                {
                    return issaved.Id;
                }

            }
            catch (Exception)
            {
                return 0;
            }
            return 0;
        }
        #endregion

    }
}
