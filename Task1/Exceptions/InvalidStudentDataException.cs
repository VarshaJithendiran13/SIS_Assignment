using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSys.Exceptions
{
    public class InvalidStudentDataException : Exception
    {
        public InvalidStudentDataException()
            : base("The data provided for creating or updating a student is invalid.") { }

        public InvalidStudentDataException(string message)
            : base(message) { }

        public InvalidStudentDataException(string message, Exception inner)
            : base(message, inner) { }
    }
}

