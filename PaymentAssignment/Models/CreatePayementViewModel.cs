using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaymentAssignment.Models
{
    public class CreatePayementViewModel
    {
        public decimal Amount { get; set; }
        public string FirstName { get; set; }  
        public string LastName { get; set; }    
        public string CreditCardNumber { get; set; }

        public IEnumerable<SelectListItem> Brand { get; set; }

        public string SecurityCode { get; set; }
    }
}