using System;

namespace Homemade.BLL.ViewModel.Order
{
    public class OrderItemsViewModel
    {
        public int OrderItemsID { get; set; }
        public Guid OrderItemsGuid { get; set; }
        public string ProdName { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Quantity { get; set; }
        public string ProdImage { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsEnable { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? EnableDate { get; set; }
        public int UserId { get; set; }
        public int? UserUpdate { get; set; }
        public int? UserDelete { get; set; }
        public int? UserEnable { get; set; }
        public int OrderVendorID { get; set; }
        public int ProductID { get; set; }
    }
}
