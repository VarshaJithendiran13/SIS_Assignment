using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSys.Exceptions
{
    public class InvalidCourseDataException : Exception
    {
        public InvalidCourseDataException()
            : base("The data provided for creating or updating a course is invalid.") { }

        public InvalidCourseDataException(string message)
            : base(message) { }

        public InvalidCourseDataException(string message, Exception inner)
            : base(message, inner) { }
    }
}

