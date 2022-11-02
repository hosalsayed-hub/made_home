using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Vendor
{
    public class TripItemsProc : List<InvoiceForProc>
    {
        /// <summary>
        /// Returns the items as a DataTable.
        /// </summary>
        /// <returns><c>DataTable</c></returns>

        public DataTable GetItemsAsDataTable()
        {
            // construct the empty DataTable object with columns.
            DataTable table = new DataTable();
            //table.Columns.Add("ID", typeof(Guid));
            table.Columns.Add("DeliveryRequestID", typeof(Int32));

            // add a single row for each item in the collection.
            foreach (var item in this)
            {
                table.Rows.Add(
                    item.OrderVendorID
                );
            }
            return table;
        }

    }
    public class VendorItemsProc : List<VendorForProc>
    {
        /// <summary>
        /// Returns the items as a DataTable.
        /// </summary>
        /// <returns><c>DataTable</c></returns>

        public DataTable GetItemsAsDataTable()
        {
            // construct the empty DataTable object with columns.
            DataTable table = new DataTable();
            //table.Columns.Add("ID", typeof(Guid));
            table.Columns.Add("DeliveryRequestID", typeof(Int32));

            // add a single row for each item in the collection.
            foreach (var item in this)
            {
                table.Rows.Add(
                    item.VendorsID
                );
            }
            return table;
        }

    }
    public class InvoiceForProc
    {
        public int OrderVendorID { get; set; }

    }
    public class VendorForProc
    {
        public int VendorsID { get; set; }

    }
    public class StoredResultViewModel
    {
        public string ErrorNumber { get; set; }
        public string ErrorSeverity { get; set; }
        public string ErrorState { get; set; }
        public string ErrorProcedure { get; set; }
        public string ErrorLine { get; set; }
        public string ErrorMessage { get; set; }
        public string flag { get; set; }
        public int RefID { get; set; }
    }
}
