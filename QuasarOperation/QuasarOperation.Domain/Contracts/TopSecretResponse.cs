using QuasarOperation.Domain.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuasarOperation.Domain.Contracts
{
    public class TopSecretResponse
    {
        public CoordinateContract Location { get; set; }
        public string Messsage { get; set; }
    }
}
