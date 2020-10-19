using System;
using System.Collections.Generic;
using System.Text;

namespace QuasarOperation.Domain.Exceptions
{
    public class CantRecoverMessageException : Exception
    {
        public int Status { get; set; } = 404;

        public CantRecoverMessageException(string message) : base(message)
        {
            
        }
    }
}
