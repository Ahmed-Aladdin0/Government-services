using Government.Abstractions;

namespace Government.Errors
{
    public static class RequestErrors
    {

        public static readonly Error RequestNotFound = new Error(Code: "Not Found ", Description: " RequestId is In Valid !! ");
        public static readonly Error RequestNotCompleted = new Error (Code: "RequestNotCompleted ", Description: "An error occurred while processing your request.");
        public static readonly Error FieldDataNotFound = new Error (Code: "Not Found ", Description: "enter valid field Data Id ");
        public static readonly Error FieldNotFound = new Error (Code: "Not Found ", Description: "enter valid field Id ");
        public static readonly Error NoDataFound = new Error(Code: "Not Found ", Description: "No data found for RequestId ");
 

        public static readonly Error FailedPayment = new("Payment.Failed", "فشل الدفع.");
        public static readonly Error FailedPaymentNotenough = new("Payment.InsufficientFunds", "لا يوجد رصيد كافٍ.");
        public static readonly Error FailedPaymentStolen = new("Payment.StolenCard", "تم رفض البطاقة لأنها مسروقة.");
        public static readonly Error FailedPaymentExpiredCard = new("Payment.ExpiredCard", "❌ البطاقة منتهية الصلاحية.");
    }
}
