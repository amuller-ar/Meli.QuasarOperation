using System;
using System.Collections.Generic;
using System.Text;

namespace QuasarOperation.Domain.Exceptions
{
    public class CantDeterminateLocationException: Exception
    {
        public int Satatus { get; set; } = 404;

        public CantDeterminateLocationException(string message) : base(message)
        {

        }
    }
}
