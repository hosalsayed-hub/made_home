using Homemade.BLL.General;
using Homemade.DAL.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.SMS
{
   public class BlLogTextMessage
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlLogTextMessage(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Actions
        /// Add New LogTextMessage -- إضافة سجل الرسالة
        public bool Insert(string MobileNo, string Message, int UserTypeId, int MessageReasonId, int MessageTypeId, string email = "", string response = "", bool IsSend = true)
        {
            try
            {
                var LogTextMessage = new Model.Setting.LogTextMessage()
                {
                    LogTextMessageGuid = Guid.NewGuid(),
                    Message = Message,
                    MobileNo = MobileNo,
                    DateAdded = _blGeneral.GetDateTimeNow(),
                    SendTime = _blGeneral.GetDateTimeNow(),
                    UserTypeId = UserTypeId,
                    MessageReasonId = MessageReasonId,
                    MessageTypeId = MessageTypeId,
                    Email = email,
                    Response = response,
                    IsSend = IsSend
                };
                Uow.LogTextMessage.Insert(LogTextMessage);
                Uow.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool InsertWithOutCommit(string MobileNo, string Message, int UserTypeId, int MessageReasonId, int MessageTypeId, string email = "", string response = "", bool IsSend = true)
        {
            try
            {
                var LogTextMessage = new Model.Setting.LogTextMessage()
                {
                    LogTextMessageGuid = Guid.NewGuid(),
                    Message = Message,
                    MobileNo = MobileNo,
                    DateAdded = _blGeneral.GetDateTimeNow(),
                    SendTime = _blGeneral.GetDateTimeNow(),
                    UserTypeId = UserTypeId,
                    MessageReasonId = MessageReasonId,
                    MessageTypeId = MessageTypeId,
                    Email = email,
                    Response = response,
                    IsSend = IsSend


                };
                Uow.LogTextMessage.Insert(LogTextMessage);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}
