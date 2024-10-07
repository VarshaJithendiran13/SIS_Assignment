using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSys.Exceptions
{
    public class CourseNotFoundException : Exception
    {
        public CourseNotFoundException()
            : base("The specified course does not exist in the system.") { }

        public CourseNotFoundException(string message)
            : base(message) { }

        public CourseNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}
