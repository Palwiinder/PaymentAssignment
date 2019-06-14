using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentAssignment.Models
{
    public class PayementResultViewModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string PaymentReferenceNumber { get; set; }
    }
}