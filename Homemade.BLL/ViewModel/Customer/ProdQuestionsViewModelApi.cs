using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Customer
{
    public class ProdQuestionsViewModelApi
    {
        public List<ListProductQuestion> questions { get; set; }
        public bool isNextPage { get; set; }
    }
    public class ListProductQuestion
    {
        public string question { get; set; }
        public string questionDate { get; set; }
        public string vendorImage { get; set; }
        public string vendorName { get; set; }
        public string answer { get; set; }
        public string answerDate { get; set; }
        public bool isRepley { get; set; }
        public string productName { get; set; }
        public string productImage { get; set; }
    }
    public class ProdRatesViewModelApi
    {
        public List<ListProductRate> rates { get; set; }
        public bool isNextPage { get; set; }
    }
    public class ListProductRate
    {
        public string rateDate { get; set; }
        public string rateComment { get; set; }
        public string rateTitle { get; set; }
        public decimal rate { get; set; }
        public string vendorImage { get; set; }
        public string vendorName { get; set; }
        public string answer { get; set; }
        public string answerDate { get; set; }
        public bool isRepley { get; set; }
        public string trackNo { get;  set; }
    }
}
