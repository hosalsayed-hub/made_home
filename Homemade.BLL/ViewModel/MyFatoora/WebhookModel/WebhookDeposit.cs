using System;

namespace Homemade.BLL.ViewModel.MyFatoora.WebhookModel
{
    public class WebhookDeposit
    {
        public string DepositReference { get; set; }
        public decimal DepositedAmount { get; set; }
        public int NumberOfTransactions { get; set; }
    }
}
