using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Setting
{
    public class MainPageViewModelApi
    {
        public string description { get;  set; }
        public string title { get;  set; }
    }
    public class AboutViewModel
    {
        public string description { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public int users { get; set; }
        public int orders { get; set; }
        public int cities { get; set; }
        public List<ListBranches> branchs { get; set; }
        public List<ListContactus> numbers { get; set; }
    }
    public class ListContactus
    {
        public string number { get; set; }
        public bool icon { get; set; }
    }
    public class ListBranches
    {
        public string branch { get; set; }
    }
}
