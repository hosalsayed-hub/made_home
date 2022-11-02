using System;
using System.Collections.Generic;

namespace Homemade.BLL.ViewModel.Site
{
    public class SiteReviewQeustionsViewModel
    {
        public IEnumerable<SiteCustomerQuestionViewModel> ListQuestions { get; set; }
        public IEnumerable<SiteOrderRateViewModel> ListOrderRate { get; set; }
    }
    public class SiteCustomerQuestionViewModel
    {
        public string Question { get; set; }
        public string QuestionDate { get; set; }
        public bool IsRepley { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; }
    }
    public class SiteOrderRateViewModel
    {
        public int OrdersID { get; set; }
        public string RateDate { get; set; }
        public string RateTime { get; set; }
        public string CommentDelivery { get; set; }
        public string CommentOrder { get; set; }
        public bool IsRepley { get; set; }
        public decimal RateDelivery { get; set; }
        public string AnswerRate { get; set; }
        public decimal RateOrder { get; set; }
        public string VendorsName { get; set; }
        public string VendorsLogo { get; set; }
        public Guid VendorsGuid { get; set; }
        public string AnswerDate { get; set; }
        public string TrackNo { get; set; }
        public string RateTitle { get; set; }
        public string AnswerTime { get; set; }
    }

}
