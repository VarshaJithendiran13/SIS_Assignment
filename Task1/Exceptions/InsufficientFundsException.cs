using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSys.Exceptions
{
    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException()
            : base("The student does not have enough funds to make the payment.") { }

        public InsufficientFundsException(string message)
            : base(message) { }

        public InsufficientFundsException(string message, Exception inner)
            : base(message, inner) { }
    }
}

