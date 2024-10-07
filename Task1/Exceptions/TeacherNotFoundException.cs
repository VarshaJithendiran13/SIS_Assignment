using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSys.Exceptions
{
    public class TeacherNotFoundException : Exception
    {
        public TeacherNotFoundException()
            : base("The specified teacher does not exist in the system.") { }

        public TeacherNotFoundException(string message)
            : base(message) { }

        public TeacherNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}
