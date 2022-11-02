using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Customer
{
    public class PromoCodeViewModelApi
    {
        public string message { get; set; }
        public bool status { get; set; }
        public int promoCodeID { get;  set; }
        public decimal deliveryPrice { get;  set; }
        public decimal total { get;  set; }
        public decimal discount { get;  set; }
        public decimal price { get; set; }
        public decimal vat { get; internal set; }
        public decimal vatValue { get; internal set; }
        public decimal totalWithDelivery { get; internal set; }
    }
    public class PromoCodeViewModelApi_new
    {
        public string message { get; set; }
        public bool status { get; set; }
        public int promoCodeID { get; set; }
        public decimal deliveryPrice { get; set; }
        public decimal total { get; set; }
        public decimal discount { get; set; }
        public decimal price { get; set; }
        public decimal vat { get;  set; }
        public decimal vatValue { get;  set; }
        public decimal totalWithDelivery { get;  set; }
        public decimal vatTotal { get; set; }
        public List<listvendor> listvendor { get; set; }
    }
    public class listvendor
    {
        public int vendorId { get; set; }
        public decimal deliveryPrice { get; set; }
        public decimal distanceKM { get; set; }
    }
}
