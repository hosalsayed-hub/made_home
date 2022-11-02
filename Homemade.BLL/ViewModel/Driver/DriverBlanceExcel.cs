using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Driver
{
    public class DriverTotalBalanceExcel
    {
        public string Driver_Name { get; set; }
        public string Total_Credit { get; set; }
        public string Total_Debit { get; set; }
        public string Net_Balance { get; set; }
    }
    public class ClientTotalBalanceExcel
    {
        public string Client_Name { get; set; }
        public string Total_Credit { get; set; }
        public string Total_Debit { get; set; }
        public string Net_Balance { get; set; }
    }
    public class DriverBlanceExcel
    {
        public string Driver_Name { get; set; }
        public string Transaction_Type { get; set; } //TransactionTypeEnum
        public string Type_Operation { get; set; } //TypeOperationEnum
        public string Amount { get; set; }
        public string Before { get; set; }
        public string After { get; set; }
        public string Date_Operation { get; set; }
    }
    public class CompanyFinanceExcel
    {
        public string Trip { get; set; }
        public string Driver { get; set; }
        public string Client { get; set; }
        public string Date { get; set; }
        public string Trip_Time { get; set; }
        public string Trip_Distance { get; set; }
        public string Starting { get; set; }
        public string Time_Cost { get; set; }
        public string Distance_Cost { get; set; }
        public string Tax { get; set; }
        public string Total { get; set; }
        public string Remaning { get; set; }
        public string Sub_Total { get; set; }
        public string Discount { get; set; }
        public string Type { get; set; }
        public string Collected { get; set; }
        public string DriverCharge { get; set; }
        public string CompanyCharge { get; set; }
    }
    public class ClientBlanceExcel
    {
        public string Client_Name { get; set; }
        public string Transaction_Type { get; set; } //TransactionTypeEnum
        public string Type_Operation { get; set; } //TypeOperationEnum
        public string Amount { get; set; }
        public string Before { get; set; }
        public string After { get; set; }
        public string Date_Operation { get; set; }
    }
}
