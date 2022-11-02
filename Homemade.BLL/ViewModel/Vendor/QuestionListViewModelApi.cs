using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Vendor
{
   public class QuestionListViewModelApi
    {
        public List<ListVendorQuestion> questions { get; set; }
        public bool isNextPage { get; set; }
    }
    public class ListVendorQuestion
    {
        public string question { get; set; }
        public string questionDate { get; set; }
        public string customerImage { get; set; }
        public string customerName { get; set; }
        public string productImage { get; set; }
        public string productName { get; set; }
        public bool isRepley { get; set; }
        public int questionId { get;  set; }
        public string price { get;  set; }
        public string answer { get;  set; }
        public string descripe { get; set; }
    }
    public class RateListViewModelApi
    {
        public List<ListVendorRate> rates { get; set; }
        public bool isNextPage { get; set; }
    }
    public class ListVendorRate
    {
        public string rateDate { get; set; }
        public string rateComment { get; set; }
        public string repley { get; set; }
        public string rateTitle { get; set; }
        public decimal rate { get; set; }
        public string customerImage { get; set; }
        public string customerName { get; set; }
        public bool isRepley { get; set; }
        public string trackNo { get; set; }
        public int rateId { get; set; }
        public List<ListProductRate> listProducts { get; set; }
    }
    public class ListProductRate
    {
        public string name { get; set; }
        public string image { get; set; }
        public string price { get; set; }
    }
}
