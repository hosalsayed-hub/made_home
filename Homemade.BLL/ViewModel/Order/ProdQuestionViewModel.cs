using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Order
{
    public class ProdQuestionViewModel
    {
        public int ProdQuestionID { get; set; }
        public Guid ProdQuestionGuid { get; set; }
        public int CustomersID { get; set; }
        public int ProductID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public bool IsRepley { get; set; }
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public string QuestionDate { get; set; }
        public string ProductImage { get; set; }
        public DateTime CreateDate { get; set; }
        public string VendorName { get; set; }
        public string CustomerImage { get; set; }
        public string VendorImage { get; set; }
        public Guid ProductGuid { get; set; }
        public Guid VendorsGuid { get; set; }
    }
}
