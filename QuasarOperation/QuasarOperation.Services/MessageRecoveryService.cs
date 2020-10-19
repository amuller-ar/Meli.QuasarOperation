using QuasarOperation.Domain;
using QuasarOperation.Domain.Contracts.Model;
using QuasarOperation.Domain.Exceptions;
using QuasarOperation.Domain.Interfaces.Repostories;
using QuasarOperation.Domain.Interfaces.Services;
using QuasarOperation.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuasarOperation.Services
{
    public class MessageRecoveryService : IMessageRecoveryService
    {
        #region Private Members

        private readonly ISatelliteRepository _satelliteRepository;
        private readonly IReceivedMessageRepository _receivedMessageRepository;
        #endregion

        #region Constructor

        public MessageRecoveryService(ISatelliteRepository satelliteRepository,
                               IReceivedMessageRepository receivedMessageRepository)
        {
            _satelliteRepository = satelliteRepository ?? throw new ArgumentNullException(nameof(satelliteRepository));
            _receivedMessageRepository = receivedMessageRepository ?? throw new ArgumentNullException(nameof(receivedMessageRepository));
        }

        #endregion

        #region Public

        /// <summary>
        /// Obtiene el mensaje completo si es posible
        /// </summary>
        /// <param name="transmission">transmisiones recibidas</param>
        /// <returns></returns>
        public RecoveredMessage GetMessage(IEnumerable<ReceivedMessage> transmission)
        {
            if (transmission.Count() != 3)
            {
                throw new CantRecoverMessageException("No se puede decodificar el mensaje");
            }

            var satellites = transmission.Select((s) =>
            {
                var satellite = _satelliteRepository.GetByName(s.SatelliteName);

                return satellite ?? throw new CantRecoverMessageException($"No se pudo recuperar el satellite {s.SatelliteName}");
            });

            //por medio de la extensión ZipThree creo un objeto anónimo con cada una de las palabras de la misma posición
            var tr = transmission.ToList();
            var message = tr[0].Message
                               .ZipThree(tr[1].Message,
                                         tr[2].Message,
                                         (t1, t2, t3) => new { t1, t2, t3 });

            if(message.Any(m=> m.t1.AsNullIfWhiteSpace() == null && 
                               m.t2.AsNullIfWhiteSpace() == null && 
                               m.t3.AsNullIfWhiteSpace() == null))
            {
                throw new CantRecoverMessageException($"No se puede recuperar el mensaje, mensaje incompleto");
            }

            //luego elegimos la primera que tenga definida para la misma limpiando las repetidas
            var readable = message.Select(x => x.t1.AsNullIfWhiteSpace() ??
                                               x.t2.AsNullIfWhiteSpace() ??
                                               x.t3.AsNullIfWhiteSpace()).Distinct();

            return new RecoveredMessage() { Message = string.Join(' ', readable) };
        }

        

        /// <summary>
        /// Recibe los mensajes del espacio y los guarda para posterior decodificación
        /// </summary>
        /// <param name="transmission"></param>
        public void ReceiveMessage(ReceivedMessage transmission)
        {
            if (transmission == null) throw new ArgumentNullException(nameof(transmission));

            _receivedMessageRepository.Save(transmission);
        }

        /// <summary>
        /// Intenta recuperar el mensaje que se fueron recibiendo 
        /// </summary>
        /// <returns></returns>
        public RecoveredMessage TryRecoverMessage()
        {
            return GetMessage(_receivedMessageRepository.GetAll());
        }

        #endregion
    }
}
