using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSys.Exceptions
{
    public class PaymentValidationException : Exception
    {
        public PaymentValidationException()
            : base("There was an issue with payment validation.") { }

        public PaymentValidationException(string message)
            : base(message) { }

        public PaymentValidationException(string message, Exception inner)
            : base(message, inner) { }
    }
}
