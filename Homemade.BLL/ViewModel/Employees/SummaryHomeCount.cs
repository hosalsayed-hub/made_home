namespace Homemade.BLL.ViewModel.Employees
{
    public class SummaryHomeCount
    {
        public int CreateCount { get; set; }
        public int AcceptCount { get; set; }
        public int BeingProcessedCount { get; set; }
        public int ShippingCount { get; set; }
        public int DeliveredCount { get; set; }
        public int BeingDeliveryCount { get; set; }
        public int CancelCount { get; set; }
        public int AssignCount { get; set; }
        public int OnWayStoreCount { get; set; }
        public int PendingCancelCount { get; set; }
        public int CertifiedCaptainCount { get; set; }
        public int NewRequestsCount { get; set; }
        public int UnderScrutinyCount { get; set; }
        public int RejectedRequestsCount { get; set; }
        public int WaitingActivationCount { get; set; }
        public int IsOnlineCount { get; set; }
        public int OrderPendingTransferCount { get; set; }
        public decimal OrderPendingTransferSum { get; set; }
        public int OrderTransferredCount { get; set; }
        public decimal OrderTransferredSum { get; set; }
        public int OrderNotInvoiceCount { get; set; }
        public decimal OrderNotInvoiceSum { get; set; }
        public int OrderInvoiceCount { get; set; }
        public decimal OrderInvoiceSum { get; set; }
        public int OrderCashTransferedCount { get; set; }
        public decimal OrderCashTransferedSum { get; set; }
        public int WaitingReviewCount { get; set; }
        public decimal WaitingReviewSum { get; set; }
        public int WaitingTransferApprovalCount { get; set; }
        public decimal WaitingTransferApprovalSum { get; set; }
        public int WaitingTransferConfirmationCount { get; set; }
        public decimal WaitingTransferConfirmationSum { get; set; }
        public int TransferredCount { get; set; }
        public decimal TransferredSum { get; set; }
        public decimal HomeMadeEarningsSum { get; set; }
        public decimal NotInvoiceEarningsSum { get; set; }
        public decimal InvoiceEarningsSum { get; set; }
        public decimal HomeMadeDeliveryPriceSum { get; set; }
        public decimal HomeMadeTotalSum { get; set; }
        public decimal DeliveryTaxSum { get; set; }
        public int ShippingCompanyOrdersCount { get; set; }
        public int ShippingCompanyPendingOrdersCount { get; set; }
        public decimal ShippingCompanyDeliveryPriceSum { get; set; }
        public int CaptainPendingTransferCount { get; set; }
        public decimal CaptainPendingTransferSum { get; set; }
        public int CaptainTransferredCount { get; set; }
        public decimal CaptainTransferredSum { get; set; }
    }
}
