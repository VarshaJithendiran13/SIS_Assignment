using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSys.Exceptions
{
    public class InvalidTeacherDataException : Exception
    {
        public InvalidTeacherDataException()
            : base("The data provided for creating or updating a teacher is invalid.") { }

        public InvalidTeacherDataException(string message)
            : base(message) { }

        public InvalidTeacherDataException(string message, Exception inner)
            : base(message, inner) { }
    }
}

