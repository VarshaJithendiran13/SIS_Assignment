using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSys.Exceptions
{
    public class InvalidEnrollmentDataException : Exception
    {
        public InvalidEnrollmentDataException()
            : base("The data provided for creating an enrollment is invalid.") { }

        public InvalidEnrollmentDataException(string message)
            : base(message) { }

        public InvalidEnrollmentDataException(string message, Exception inner)
            : base(message, inner) { }
    }
}
