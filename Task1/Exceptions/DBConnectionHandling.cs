﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSys.Exceptions
{
    public class DBConnectionHandling : Exception
    {
        public DBConnectionHandling()
            : base("Database connection error occurred.") // Default message
        {
        }

        public DBConnectionHandling(string message)
            : base(message) // Message from the user
        {
        }

        public DBConnectionHandling(string message, Exception inner)
            : base(message, inner) // Message with inner exception
        {
        }
    }
}
