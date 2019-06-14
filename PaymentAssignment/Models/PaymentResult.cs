namespace PaymentAssignment.Models
{
    public class PaymentResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string PaymentReferenceNumber { get; set; }
    }
}