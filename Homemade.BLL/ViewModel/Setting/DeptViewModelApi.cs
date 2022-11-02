using Homemade.Model.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Setting
{
    public class DeptViewModelApi
    {
        public string title { get;  set; }
        public string image { get;  set; }
        public int departmentID { get;  set; }
    }

    public class DeptViewModel
    {
        public string title { get; set; }
        public string image { get; set; }
        public int departmentID { get; set; }
        public Guid DepartmentsGuid { get;  set; }
        public int? Arrange { get;  set; }
        public bool Isunique { get;  set; }
    }
    public class DepartmentsViewModelApi 
    {
        public int departmentsID { get; set; }
        public string name { get; set; }
    }
}
