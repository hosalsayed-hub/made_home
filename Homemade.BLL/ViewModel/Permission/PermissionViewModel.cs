using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Homemade.BLL.ViewModel
{
    public class PermissionViewModel
    {
        [Required(ErrorMessage = "مطلوب")]
        public int RoleID { get; set; }
        [Required(ErrorMessage = "مطلوب")]
        public int UserId { get; set; }
        public string CheckedItems { get; set; }
        public List<PermissionControllerViewModel> Controllers { get; set; }
    }

    public class PermissionControllerViewModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public List<PermissionActionViewModel> Actions { get; set; }
    }

    public class PermissionActionViewModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public bool Allow { get; set; }
    }
    public class PermissionControllerActionViewModel
    {
        public string ControllerID { get; set; }
        public string ControllerName { get; set; }
        public bool IsView { get; set; }
        public bool IsInsert { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
        public int PermissionControllerActionViewID { get; set; }
        public int PermissionControllerActionInsertID { get; set; }
        public int PermissionControllerActionUpdateID { get; set; }
        public int PermissionControllerActionDeleteID { get; set; }
    }
    public class RoleConfigrationViewModel
    {
        [Required(ErrorMessage = "مطلوب")]
        public int RoleID { get; set; }
        public string CheckedItems { get; set; }
        public List<PermissionControllerViewModel> Controllers { get; set; }
    }
}
