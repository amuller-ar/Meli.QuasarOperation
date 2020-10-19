using QuasarOperation.Domain.Contracts.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QuasarOperation.Domain.Contracts
{
    public class TopSecretRequest
    {
        [MaxLength(3)]
        [MinLength(3)]
        public  IList<TransmissionContract> Satellites { get; set; }
    }
}
