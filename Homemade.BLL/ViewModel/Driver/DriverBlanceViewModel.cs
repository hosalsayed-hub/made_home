using Homemade.BLL.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homemade.Model.Driver;

namespace Homemade.BLL.ViewModel.Driver
{
    public class DriverBlanceViewModel
    {
        public int DriverBlanceID { get; set; }
        public int? OrderVendorID { get; set; }
        public Guid DriverBlanceGuid { get; set; }
        public Guid OrderVendorGuid { get; set; }
        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.AmountRequierd), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [RegularExpression("^[0-9]+$", ErrorMessageResourceName = nameof(HomemadeErrorMessages.AmountValid), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public decimal Amount { get; set; }

        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.TransactionTypeRequierd), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public int TransactionTypeID { get; set; } //fk
        public decimal Before { get; set; }
        public decimal After { get; set; }
        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.DriverRequierd), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public int DriverID { get; set; }  //fk
        public int TypeOperationID { get; set; } //TypeOperationEnum
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public List<Drivers> LstDrivers { get; set; }
        public List<TransactionType> LstTransactionType { get; set; }
        public string DriverName { get; set; }
        public string MobileNo { get; set; }
        public string Discripe { get; set; }
        public string Attachment { get; set; }
        public int TotalCount { get; set; }
        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.DateOperationRequierd), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy, hh.mm tt}", ApplyFormatInEditMode = true)]
        public DateTime DateOperation { get; set; }
        public int UserId { get; set; }
        public string TransactionTypeName { get; set; }
        public string DateOperationString { get; set; }
        public string OrderNumber { get; set; }
        public int VerifyStc { get; set; }
        public bool OpenTransaction { get; set; }
    }
}
