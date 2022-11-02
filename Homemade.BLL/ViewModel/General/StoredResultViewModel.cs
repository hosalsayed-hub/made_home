using System;
using System.Collections.Generic;
using System.Text;

namespace Homemade.BLL.ViewModel.General
{
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
