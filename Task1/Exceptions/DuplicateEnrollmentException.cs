using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSys.Exceptions
{
    public class DuplicateEnrollmentException : Exception
    {
        public DuplicateEnrollmentException()
            : base("The student is already enrolled in this course.") { }

        public DuplicateEnrollmentException(string message)
            : base(message) { }

        public DuplicateEnrollmentException(string message, Exception inner)
            : base(message, inner) { }
    }
}
