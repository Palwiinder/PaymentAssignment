using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentAssignment.Models
{
    public class BrandViewModel
    {
        public string Code { get; }

        public string Name { get; }

        public BrandViewModel(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}