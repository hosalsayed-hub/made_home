using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Setting;
using Homemade.DAL.Contract;
using Homemade.Model.Setting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.Setting
{
    public class BlCompanyWorkingHours
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlCompanyWorkingHours(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Helpers
        public CompanyWorkingHoursViewModel GetCompanyWorkingHoursViewModel()
        {
            var data = Uow.CompanyWorkingHours.GetAll(x => !x.IsDeleted).OrderBy(p => p.CompanyWorkingHoursID).OrderByDescending(p => p.CreateDate)
             .Select(p => new CompanyWorkingHoursViewModel
             {
                 CompanyWorkingHoursID = p.CompanyWorkingHoursID,
                 CompanyWorkingHoursGuid = p.CompanyWorkingHoursGuid,
                 DaysVac = p.DaysVac,
                 DaysWork = p.DaysWork,
                 FirstShiftVacWorkFrom = p.FirstShiftVacWorkFrom,
                 FirstShiftWorkTo = p.FirstShiftWorkTo,
                 FirstShiftWorkFrom = p.FirstShiftWorkFrom,
                 FirstShiftVacWorkTo = p.FirstShiftVacWorkTo,
                 SecondShiftVacWorkFrom = p.SecondShiftVacWorkFrom,
                 SecondShiftVacWorkTo = p.SecondShiftVacWorkTo,
                 SecondShiftWorkFrom = p.SecondShiftWorkFrom,
                 SecondShiftWorkTo = p.SecondShiftWorkTo,
                 FirstShiftWorkFromString = p.FirstShiftWorkFrom.ToString("HH:mm"),
                 FirstShiftWorkToString = p.FirstShiftWorkTo.ToString("HH:mm"),
                 FirstShiftVacWorkFromString = p.FirstShiftVacWorkFrom != null ? p.FirstShiftVacWorkFrom.Value.ToString("HH:mm") : string.Empty,
                 FirstShiftVacWorkToString = p.FirstShiftVacWorkTo != null ? p.FirstShiftVacWorkTo.Value.ToString("HH:mm") : string.Empty,
                 SecondShiftWorkFromString = p.SecondShiftWorkFrom != null ? p.SecondShiftWorkFrom.Value.ToString("HH:mm") : string.Empty,
                 SecondShiftWorkToString = p.SecondShiftWorkTo != null ? p.SecondShiftWorkTo.Value.ToString("HH:mm") : string.Empty,
                 SecondShiftVacWorkFromString = p.SecondShiftVacWorkFrom != null ? p.SecondShiftVacWorkFrom.Value.ToString("HH:mm") : string.Empty,
                 SecondShiftVacWorkToString = p.SecondShiftVacWorkTo != null ? p.SecondShiftVacWorkTo.Value.ToString("HH:mm") : string.Empty,
             }).FirstOrDefault();
            if (data != null)
            {
                data.ListDaysWork = !string.IsNullOrEmpty(data.DaysWork) ? data.DaysWork.Split(',') : null;
                data.ListDaysVac = !string.IsNullOrEmpty(data.DaysVac) ? data.DaysVac.Split(',') : null;
            }
            return data;
        }
        public CompanyWorkingHours GetCompanyWorkingHours(int CompanyWorkingHoursID)
        {
            return Uow.CompanyWorkingHours.GetAll(p => !p.IsDeleted && p.CompanyWorkingHoursID == CompanyWorkingHoursID).FirstOrDefault();
        }
        public CompanyWorkingHours GetFirstCompanyWorkingHours()
        {
            return Uow.CompanyWorkingHours.GetAll(p => !p.IsDeleted).OrderBy(p => p.CompanyWorkingHoursID).OrderByDescending(p => p.CreateDate).FirstOrDefault();
        }
        public bool IsCompanyWorking()
        {
            var companyWorkingHours = GetFirstCompanyWorkingHours();
            if (companyWorkingHours != null)
            {
                if (!string.IsNullOrEmpty(companyWorkingHours.DaysWork) || !string.IsNullOrEmpty(companyWorkingHours.DaysVac))
                {
                    var datetimenow = _blGeneral.GetDateTimeNow().TimeOfDay;
                    var ListDaysWork = !string.IsNullOrEmpty(companyWorkingHours.DaysWork) ? companyWorkingHours.DaysWork.Split(',') : null;
                    var ListDaysVac = !string.IsNullOrEmpty(companyWorkingHours.DaysVac) ? companyWorkingHours.DaysVac.Split(',') : null;
                    if (ListDaysWork != null)
                    {
                        if (ListDaysWork.Where(x => !string.IsNullOrEmpty(x)).Any(x => Convert.ToInt32(x) == (int)DateTime.Now.DayOfWeek))
                        {

                            TimeSpan FirstShiftWorkFrom = DateTime.ParseExact(companyWorkingHours.FirstShiftWorkFrom.ToString("HH:mm"), "HH:mm", System.Globalization.CultureInfo.InvariantCulture).TimeOfDay;
                            TimeSpan FirstShiftWorkTo = DateTime.ParseExact(companyWorkingHours.FirstShiftWorkTo.ToString("HH:mm"), "HH:mm", System.Globalization.CultureInfo.InvariantCulture).TimeOfDay;

                            if (FirstShiftWorkFrom < FirstShiftWorkTo)
                            {
                                if (FirstShiftWorkFrom < datetimenow && FirstShiftWorkTo > datetimenow)
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                if (FirstShiftWorkFrom < datetimenow || FirstShiftWorkTo > datetimenow)
                                {
                                    return true;
                                }

                            }

                            TimeSpan SecondShiftWorkFrom = (companyWorkingHours.SecondShiftWorkFrom != null ? (DateTime.ParseExact(companyWorkingHours.SecondShiftWorkFrom.Value.ToString("HH:mm"), "HH:mm", System.Globalization.CultureInfo.InvariantCulture)) : new DateTime()).TimeOfDay;
                            TimeSpan SecondShiftWorkTo = (companyWorkingHours.SecondShiftWorkTo != null ? (DateTime.ParseExact(companyWorkingHours.SecondShiftWorkTo.Value.ToString("HH:mm"), "HH:mm", System.Globalization.CultureInfo.InvariantCulture)) : new DateTime()).TimeOfDay;

                            if (SecondShiftWorkFrom != new TimeSpan() && SecondShiftWorkTo != new TimeSpan())
                            {
                                if (SecondShiftWorkFrom < SecondShiftWorkTo)
                                {
                                    if (SecondShiftWorkFrom < datetimenow && SecondShiftWorkTo > datetimenow)
                                    {
                                        return true;
                                    }
                                }
                                else
                                {
                                    if (SecondShiftWorkFrom < datetimenow || SecondShiftWorkTo > datetimenow)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                    if (ListDaysVac != null)
                    {
                        if (ListDaysVac.Where(x => !string.IsNullOrEmpty(x)).Any(x => Convert.ToInt32(x) == (int)DateTime.Now.DayOfWeek))
                        {

                            TimeSpan FirstShiftVacWorkFrom = (companyWorkingHours.FirstShiftVacWorkFrom != null ? (DateTime.ParseExact(companyWorkingHours.FirstShiftVacWorkFrom.Value.ToString("HH:mm"), "HH:mm", System.Globalization.CultureInfo.InvariantCulture)) : new DateTime()).TimeOfDay;
                            TimeSpan FirstShiftVacWorkTo = (companyWorkingHours.FirstShiftVacWorkTo != null ? (DateTime.ParseExact(companyWorkingHours.FirstShiftVacWorkTo.Value.ToString("HH:mm"), "HH:mm", System.Globalization.CultureInfo.InvariantCulture)) : new DateTime()).TimeOfDay;

                            if (FirstShiftVacWorkFrom != new TimeSpan() && FirstShiftVacWorkTo != new TimeSpan())
                            {
                                if (FirstShiftVacWorkFrom < FirstShiftVacWorkTo)
                                {
                                    if (FirstShiftVacWorkFrom < datetimenow && FirstShiftVacWorkTo > datetimenow)
                                    {
                                        return true;
                                    }
                                }
                                else
                                {
                                    if (FirstShiftVacWorkFrom < datetimenow || FirstShiftVacWorkTo > datetimenow)
                                    {
                                        return true;
                                    }
                                }
                            }

                            TimeSpan SecondShiftVacWorkFrom = (companyWorkingHours.SecondShiftVacWorkFrom != null ? (DateTime.ParseExact(companyWorkingHours.SecondShiftVacWorkFrom.Value.ToString("HH:mm"), "HH:mm", System.Globalization.CultureInfo.InvariantCulture)) : new DateTime()).TimeOfDay;
                            TimeSpan SecondShiftVacWorkTo = (companyWorkingHours.SecondShiftVacWorkTo != null ? (DateTime.ParseExact(companyWorkingHours.SecondShiftVacWorkTo.Value.ToString("HH:mm"), "HH:mm", System.Globalization.CultureInfo.InvariantCulture)) : new DateTime()).TimeOfDay;

                            if (SecondShiftVacWorkFrom != new TimeSpan() && SecondShiftVacWorkTo != new TimeSpan())
                            {
                                if (SecondShiftVacWorkFrom < SecondShiftVacWorkTo)
                                {
                                    if (SecondShiftVacWorkFrom < datetimenow && SecondShiftVacWorkTo > datetimenow)
                                    {
                                        return true;
                                    }
                                }
                                else
                                {
                                    if (SecondShiftVacWorkFrom < datetimenow || SecondShiftVacWorkTo > datetimenow)
                                    {
                                        return true;
                                    }
                                }
                            }

                        }
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Action
        public bool Update(CompanyWorkingHoursViewModel model)
        {
            var companyWorkingHours = GetCompanyWorkingHours(model.CompanyWorkingHoursID);
            if (companyWorkingHours == null)
            {
                companyWorkingHours = new CompanyWorkingHours
                {
                    CreateDate = _blGeneral.GetDateTimeNow(),
                    IsEnable = true,
                    IsDeleted = false,
                    CompanyWorkingHoursGuid = Guid.NewGuid(),
                    UserId = model.UserId,
                    DaysVac = model.DaysVac,
                    DaysWork = model.DaysWork,
                    FirstShiftVacWorkFrom = model.FirstShiftVacWorkFrom,
                    FirstShiftWorkTo = model.FirstShiftWorkTo,
                    FirstShiftWorkFrom = model.FirstShiftWorkFrom,
                    FirstShiftVacWorkTo = model.FirstShiftVacWorkTo,
                    SecondShiftVacWorkFrom = model.SecondShiftVacWorkFrom,
                    SecondShiftVacWorkTo = model.SecondShiftVacWorkTo,
                    SecondShiftWorkFrom = model.SecondShiftWorkFrom,
                    SecondShiftWorkTo = model.SecondShiftWorkTo,
                };
                companyWorkingHours = Uow.CompanyWorkingHours.Insert(companyWorkingHours);
                Uow.Commit();
            }
            else
            {
                companyWorkingHours.UpdateDate = _blGeneral.GetDateTimeNow();
                companyWorkingHours.UserUpdate = model.UserId;
                companyWorkingHours.DaysVac = model.DaysVac;
                companyWorkingHours.DaysWork = model.DaysWork;
                companyWorkingHours.FirstShiftVacWorkFrom = model.FirstShiftVacWorkFrom;
                companyWorkingHours.FirstShiftWorkTo = model.FirstShiftWorkTo;
                companyWorkingHours.FirstShiftWorkFrom = model.FirstShiftWorkFrom;
                companyWorkingHours.FirstShiftVacWorkTo = model.FirstShiftVacWorkTo;
                companyWorkingHours.SecondShiftVacWorkFrom = model.SecondShiftVacWorkFrom;
                companyWorkingHours.SecondShiftVacWorkTo = model.SecondShiftVacWorkTo;
                companyWorkingHours.SecondShiftWorkFrom = model.SecondShiftWorkFrom;
                companyWorkingHours.SecondShiftWorkTo = model.SecondShiftWorkTo;

                Uow.CompanyWorkingHours.Update(companyWorkingHours);
                Uow.Commit();
            }
            return true;
        }
        #endregion
    }
}
