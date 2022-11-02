using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Cart
{
    public class CartDetailsVM
    {
        internal string receiverAddress;

        public int ProductID { get;   set; }
        public string ProductName { get;   set; }
        public decimal ProductPrice { get;   set; }
        public decimal ProductDiscount { get;   set; }
        public decimal ProductQuantity { get;   set; }
        public string ProductImage { get;   set; }
        public bool IsDeleted { get;   set; }
        public DateTime? UpdateDate { get;   set; }
        public DateTime? DeleteDate { get;   set; }
        public int CartMasterID { get;   set; }
        public int VendorsID { get;   set; }
        public Guid VendorsGuid { get;   set; }
        public string StoreName { get;   set; }
        public string Logo { get;   set; }
        public decimal? DeliveryPrice { get;   set; }
        public decimal distanceKM { get;   set; }
        public string AboutStore { get;   set; }
        public DateTime CreateDate { get;   set; }
        public string Customername { get;   set; }
        public string ReceiverMobile { get;   set; }
        public int CartDetailsID { get;   set; }
        public string Note { get;   set; }
        public string Promocode { get;   set; }
        public bool IsLimited { get; set; }
        public decimal Quantity { get; set; }
        public int ApprovalQuantity { get; set; }
    }
}
