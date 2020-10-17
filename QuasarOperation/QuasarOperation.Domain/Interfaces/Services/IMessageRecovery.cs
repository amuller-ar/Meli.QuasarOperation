using QuasarOperation.Domain.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuasarOperation.Domain.Interfaces.Services
{
    public interface IMessageRecovery
    {
        /// <summary>
        /// Obtiene el mensaje recuperado de las transmisiones recibidas por los satelites
        /// </summary>
        /// <param name="transmission"></param>
        /// <returns></returns>
        RecoveredMessage GetMessage(IEnumerable<ReceivedMessage> transmission);
    }
}
