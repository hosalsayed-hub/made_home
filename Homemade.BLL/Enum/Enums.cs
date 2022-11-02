using System.ComponentModel;

namespace Homemade.BLL
{

    public enum MainCategoryEnum
    {
        Products = 1,
    }
    public enum DisplayInEnum
    {
        App = 0,
        Site = 1
    }
    public enum TabChargeEnum
    {
        Order = 0,
        Customer_Balance = 1
    }
    public enum DiscountSettingEnum
    {
        [Description("Discount")]
        Discount = 1,
        [Description("Vat")]
        Vat = 2,
        [Description("Delivery_Vat")]
        Delivery_Vat = 3,
    }
    public enum EntryEnum
    {
        [Description("Auto")]
        Auto = 0,
        [Description("Excel")]
        Excel = 1,
    }
    public enum CancelTypeEnum
    {
        [Description("Default")]
        Default = 0,
        [Description("Auto")]
        Auto = 1,
    }
    public enum MessageReasonEnum
    {
        [Description("Activation")]
        Activation = 1,
        [Description("Forget Password")]
        ForgetPassword = 2,
        [Description("Verification")]
        Verification = 3,
        [Description("Registration")]
        Registration = 4,
        [Description("Evaluation")]
        Evaluation = 5,
        [Description("Update Profile")]
        Update_Profile = 6,
        [Description("Update Daily Quantity")]
        Update_Daily_Quantity = 7,
        [Description("Order")]
        Order = 8,
        [Description("Reply Inqueries")]
        ReplyInqueries = 9,
    }
    public enum MessageTypeEnum
    {
        [Description("SMS")]
        SMS = 1,
        [Description("Whatsapp")]
        Whatsapp = 2,
        [Description("Email")]
        Email = 3,
        [Description("Windows_Service")]
        Windows_Service = 4,
    }
    public enum DeliveryTypeEnum
    {
        HomeMade = 0,
        Store = 1
    }
    public enum PushTypeEnum
    {
        Order = 1,
        Rate = 2,
        Question = 3,
        Promo_Code = 4,
        Driver_Balance = 5,
        Pending = 6,
        PendingApprove = 7,
        PendingReject = 8,
    }
    public enum TransactionStatusEnum
    {
        OK = 1,
        Forbidden = 2,
        Error = 3,
        Progress = 4
    }
    public enum PaidStatusEnum
    {
        Created = 0,
        Invoiced = 1,
        Pending_Paid = 2,
        Cash_Transfered = 3,
        STC_Transfered = 4,
        Created_STC = 5,
        CustomerInformed_STC = 6,
        ProcessingPayment_STC = 7,
        PaymentFailed_STC = 8
    }
    public enum TypeOperationEnum
    {
        [Description("CR")]
        CR = 1,
        [Description("DR")]
        DR = 2,
    }
    public enum STCStatusEnum
    {
        Created = 0,
        Ready = 1,
        CustomerInformed = 2,
        Paid = 5,
        Cancelled = 7,
        ProcessingPayment = 8,
        PaymentFailed = 9,
        FailedValidation = 11
    }
    public enum NotificationTypeEnum
    {
        Order = 1,
        Rate = 2,
        Question = 3,
        Promo_Code = 4,
        Driver_Balance = 5,
        Warnning_Order = 6,
        Cancel_Auto = 7,
        Pending = 8,
        Pending_Reject = 9,
        Pending_Approve =10

    }
    public enum RequestStatusEnum
    {
        New = 0,
        Under_Scrutiny = 1,
        Approved = 2,
        Rejected = 3,
        Hanging = 4,
        Activated = 5
    }
    public enum PaymentTypeEnum
    {
        COD = 1,
        Payment_Card = 2,
        Bank_Transfer = 3,
        Wallet = 4
    }
    public enum InvoiceStatusEnum
    {
        Paid = 1,
        Un_Paid = 2
    }
    public enum OrderInvoiceTypeEnum
    {
        Created = 0,
        Pending_Invoice = 1,
        Non_Invoice = 2,
        Invoiced = 3,
        Cash_Transfered = 4
    }
    public enum InvoiceTypeEnum
    {
        [Description("Pending Operation")]
        Pending_Operation = 1,
        [Description("Pending Admin")]
        Pending_Admin = 2,
        [Description("Pending Paid")]
        Pending_Paid = 3,
        [Description("Paid")]
        Paid = 4,
    }
    public enum CaptainTypeEnum
    {
        Store = 0,
        Home_Made = 1,
        Shipping_Company = 2,
    }
    public enum PromoTypeEnum
    {
        On_HomeMade = 1,
        On_Store = 2
    }
    public enum QuestionRateType
    {
        Rate = 1,
        Question = 2
    }
    public enum OrderStatusEnum
    {
        Create = 1,
        Accept = 2,
        Being_Processed = 3,
        Shipping = 4,
        Being_Delivery = 5,
        Delivered = 6,
        Cancel = 7,
        Assign = 8,
        OnWay_Store = 9,
        Pending = 10,
        Drafts = 11,
        Reject_Shipping = 12
    }
    public enum HelpUserTypeEnum
    {
        Driver = 1,
        Vendor = 2
    }
    public enum SliderTypeEnum
    {
        Banner = 1,
        ADV = 2 //اعلان
    }
    public enum DeviceTypeEnum
    {
        IOS = 1,
        Andriod = 2 //اعلان
    }
    public enum RegisterType
    {
        Created = 1,
        Submitted = 2
    }
    public enum RoleTypeEnum
    {
        Master = 1,
        Entites = 2,
    }
    public enum ControllerEnum
    {
        Permission = 1,
        Role = 2,
        Country = 3,
        Region = 4,
        City = 5,
        Jobs = 6,
        Departments = 7,
        Employees = 8,
        Nationality = 9,
        RoleConfigration = 10,
        Configuration = 11,
        Banks = 12,
        PaymentWay = 13,
        Activity = 14,
        Block = 15,
        Package = 16,
        PaymentStatus = 17,
        CompanyConfiguration = 18,
        PaymentConfiguration = 19,
        MainCategory = 20,
        Sliders = 21,
        ShippingCompany = 22,
        Inqueries = 23,
        MainPages = 24,
        Vendors = 25,
        Customer = 26,
        Product = 27,
        AddressTypes = 28,
        KeyWords = 29,
        ProductOperation = 30,
        OperationOrders = 31,
        VendorOrders = 32,
        Branches = 33,
        PromoCode = 34,
        VacHistory = 35,
        EnableHistory = 36,
        Driver_New_Requests = 37,
        Driver_Rejected_Requests = 38,
        Driver_Waiting_Activation = 39,
        Driver_Under_Requests = 40,
        Invoice = 41,
        Client_Invoice = 42,
        Discount = 43,
        DriverBlance = 44,
        SendMessage = 45,
        CitiesCovered = 46,
        CaptainZone = 47,
        AssignDriver = 48,
        VendorCustomer = 49,
        VendorVacHistory = 50,
        VendorEnableHistory = 51,
        ProductQuestion = 52,
        OrderRate = 53,
        VendorProductQuestion = 54,
        VendorOrderRate = 55,
        OperationScheduleOrders = 56,
        VendorScheduleOrders = 57,
        OperationDashboard = 58,
        VendorDashboard = 59,
        DriverSupport = 60,
        Subscribe = 61,
        CustomerBalance = 62,
    }
    public enum DiscountTypeEnum
    {
        Amount = 1,
        Percent = 2
    }
    public enum ActionEnum
    {
        View = 1,
        Insert = 2,
        Update = 3,
        Delete = 4,
    }
    public enum MainPageTypeEnum
    {
        Terms_Conditions_Customer = 1,
        Policy = 2,
        FAQ = 3,
        HowWeAre = 4,
        Terms_Conditions_Vendor = 5,
        Terms_Conditions_Captain = 6,
    }
    public enum Language
    {
        Arabic, English
    }
    public enum Gender
    {
        [Description("Male")]
        Male = 0,
        [Description("Female")]
        Female = 1,
        [Description("Prefer not to specify")]
        Prefer_not_to_specify = 2,
    }
    public enum StatusEnable
    {
        [Description("Disable")]
        Disable = 0,
        [Description("Enable")]
        Enable = 1,
    }

    public enum UserTypeEnum
    {
        [Description("Admin")]
        Admin = 1,
        [Description("Vendor")]
        Vendor = 2,
        [Description("Customer")]
        Customer = 3,
        [Description("Employee")]
        Employee = 4,
        [Description("Driver")]
        Driver = 5,
        [Description("Shiping Company")]
        ShipingCompany = 6,
    }

    public enum ResultMessageType
    {
        success,
        error,
        info,
        warning,
        valid
    }


    public enum SwalActions
    {
        Add,
        Edit,
        Delete,
        Activate,
        Disable,
        Error,
        Success,
        ChangeStaute,
        ExportExcel,
        SendReq,
        SaveReq,
        SaveScheduleReq,
        CancelReq,
        EndCall,
        JoinCall,
        AssignOrder
    }
    public enum MessageTypes
    {
        Email, SMS
    }

    public enum CountryEnum
    {
        [Description("SA")]
        SA = 1,
        [Description("Egypt")]
        Egypt = 2,
    }

    public enum Rolenum
    {
        Admin = 1,
        Vendor = 2,
        Customer = 3,
        Employee = 4,
    }

    public enum JobTypeEnum
    {
        Admin = 1,
        Employees = 2,
    }

    public enum TypeIDEnum
    {
        About = 1,
        CEO = 2,
        Vision = 3,
    }

    public enum DisplayIn
    {
        Application = 0,
        WebSite = 1
    }

    public enum LastStatusUpdateFromEnum
    {
        Redirect = 1,
        Web_Hook = 2,
        Web = 3,
        Mobile = 4,
        Web_Hook_Mobile = 5,


    }
    public enum TransactionEnum
    {
        Execute_Process_Begin = 1,
        Execute_Process_InProgress = 2,
        Execute_Process_Completed = 3,
        Execute_Process_Failed = 4,
        Direct_Process_Begin = 5,
        Direct_Process_InProgress = 6,
        Direct_Process_Completed = 7,
        Direct_Process_Failed = 8,
        SendPayment_Process_Begin = 9,
        SendPayment_Process_InProgress = 10,
        SendPayment_Process_Completed = 11,
        SendPayment_Process_Failed = 12,
        BeginRedirect = 13,
        SuccessRedirect = 14,
        FailedRedirect = 15,

    }
    public enum VerifyStcEnum
    {
        Not_Verified = 0,
        Verified = 1,
        Cash = 2
    }
    public enum CartMasterStatusEnum
    {
        Cart_New = 1,
        Cart_Complete = 2,
        Cart_Canceled = 2,

    }
    public enum CancelReasonEnum
    {
        Return_to_Wallet = 1,
        Without = 2
    }
    public enum TransactionTypeEnum
    {
        [Description("Deposit")]
        Deposit = 1,
        [Description("Withdrawal")]
        Withdrawal = 2,
        [Description("Bouns")]
        Bouns = 3,
        [Description("Punishment")]
        Punishment = 4,
        [Description("Pay to Captain STC")]
        Pay_to_Captain_STC = 5,
        [Description("Delivery Order")]
        Delivery_Order = 6,
        [Description("Pay Order")]
        Pay_Order = 7,
        [Description("Pay Invoice")]
        Pay_Invoice = 8,
        [Description("Cash Transfered")]
        Cash_Transfered = 9,
        [Description("Charge Wallet")]
        Charge_Wallet = 10,
    }

    public enum PackageTypeEnum
    {
        Default = 1,
        Other = 2
    }

    public enum OrderStatusTypeEnum
    {
        HomeMade = 0,
        Store = 1
    }
    public enum ProductTypeEnum
    {
        [Description("Food")]
        Food = 0,
        [Description("Product Ready Shipping")]
        Product_Ready_Shipping = 1,
    }
    public enum OrderTypeEnum
    {
        Now = 0,
        Schedule = 1
    }
    public enum TermsConditionsAPIEnum
    {
        Customer = 0,
        Vendor = 1,
        Captain = 2,
    }
    public enum MeasurementEnum
    {
        Kilo = 0,
        Gram = 1
    }
    public enum ShippingCompanyEnum
    {
        Shourq = 1,
        Lavender = 2,
        Barq =3
    }
    public enum DayOfWeekEnum
    {
        [Description("Sunday")]
        Sunday = 0,
        [Description("Monday")]
        Monday = 1,
        [Description("Tuesday")]
        Tuesday = 2,
        [Description("Wednesday")]
        Wednesday = 3,
        [Description("Thursday")]
        Thursday = 4,
        [Description("Friday")]
        Friday = 5,
        [Description("Saturday")]
        Saturday = 6,
    }
    public enum ApprovalQuantityEnum
    {
        [Description("NotAction")]
        NotAction = 0,
        [Description("Approve")]
        Approve = 1,
        [Description("Reject")]
        Reject = 2,
    }

    public enum ErrorFromTypeEnum
    {
        OurSystem = 1,
        PayTap = 2,
    }
    public enum InsertIntoLogTypeEnum
    {
        request = 1,
        responce = 2,
        Errorresponce = 3,
        responceRedirect = 4,
    }
    public enum PaymentResponseCodesEnum
    {
        [Description("Captured")]
        Captured = 000,
        [Description("Authorized")]
        Authorized = 001,
        [Description("Initiated")]
        Initiated = 100,
        [Description("In Progress")]
        InProgress = 200,
        [Description("Abandoned")]
        Abandoned = 301,
        [Description("Cancelled")]
        Cancelled = 302,
        [Description("Deferred")]
        Deferred = 303,
        [Description("Expired")]
        Expired = 304,
        [Description("Failed")]
        Failed = 401,
        [Description("Failed, Invalid Parameter")]
        FailedInvalidParameter = 402,
        [Description("Failed, Duplicate")]
        FailedDuplicate = 403,
        [Description("Failed, Locked")]
        FailedLocked = 404,
        [Description("Failed, Invalid Card No")]
        FailedInvalidCardNo = 405,
        [Description("Failed, Invalid Expiry")]
        FailedInvalidExpiry = 406,
        [Description("Failed, Expired Card")]
        FailedExpiredCard = 407,
        [Description("Failed, Unspecified Failure")]
        FailedUnspecifiedFailure = 408,
        [Description("Declined")]
        Declined = 501,
        [Description("Declined, Incorrect CSC/CVV")]
        DeclinedIncorrectCSCCVV = 502,
        [Description("Declined, 3D Security - Incorrect")]
        Declined3DSecurityIncorrect = 503,
        [Description("Declined, 3D Security - Card not Enrolled")]
        Declined3DSecuritynotEnrolled = 504,
        [Description("Declined, Insufficient Funds")]
        DeclinedInsufficientFunds = 505,
        [Description("Declined, Transaction Type not Supported")]
        DeclinedTransactionTypenotSupported = 506,
        [Description("Declined, Card Issuer")]
        DeclinedCardIssuer = 507,
        [Description("Declined, Card Issuer - No Reply")]
        DeclinedCardIssuerNoReply = 508,
        [Description("Declined, Card Issuer - Do not Contact")]
        DeclinedCardIssuerDonotContact = 509,
        [Description("Declined, Card Issuer - Referral Response")]
        DeclinedCardIssuerReferralResponse = 510,
        [Description("Declined, Card Issuer - Error")]
        DeclinedCardIssuerError = 511,
        [Description("Declined, Not Authenticated")]
        DeclinedNotAuthenticated = 512,
        [Description("Declined, Card Acquirer - Error")]
        DeclinedCardAcquirerError = 513,
        [Description(" Declined, Card Issuer - Risk Check ")]
        DeclinedCardIssuerRiskCheck = 514,
        [Description("Declined, Tap")]
        DeclinedTap = 515,
        [Description("Void")]
        Void = 601,
        [Description("Restricted")]
        Restricted = 701,
        [Description("Restricted, Retry Limit Exceeded")]
        RestrictedRetryLimitExceeded = 702,
        [Description("Restricted, Bank")]
        RestrictedBank = 703,
        [Description("Restricted, Tap")]
        RestrictedTap = 704,
        [Description("Timed Out")]
        TimedOut = 801,
        [Description("Unknown")]
        Unknown = 901,
    }
}