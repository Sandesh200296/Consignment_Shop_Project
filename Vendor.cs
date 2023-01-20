using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsignmentShopLibrary
{
    public class Vendor
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Commission { get; set; }

        public decimal PaymentDue { get; set; }
        // This is constructor which is initiated only when object initialization
        // You can identify constructor , by Method with same name of class without any RETURN type.
        public Vendor()
        {
            Commission = .5M;
        }

        public string Display
        {
            get
            {
                return string.Format("{0} {1} - ${2}", FirstName, LastName, PaymentDue);
            }
        }
    }
}
