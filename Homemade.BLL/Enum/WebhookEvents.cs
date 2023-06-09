﻿using System.ComponentModel;

namespace Homemade.Enums
{
    public enum WebhookEvents
    {
        [Description("WebhookInvoiceStatusChange"), Localizable(true)]
        TrnasactionsStatusChanged = 1,
        [Description("WebhookRefundStatusChange"), Localizable(true)]
        RefundStatusChanged = 2,
        [Description("WebhookDeposit"), Localizable(true)]
        BalanceTransferred = 3,
        [Description("WebhookSupplierStatusChange"), Localizable(true)]
        SupplierStatusChanged = 4,
    }
}
