using QuasarOperation.Domain;
using QuasarOperation.Domain.Interfaces.Repostories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuasarOperation.DataAccess
{
    public class ReceivedMessageInMemoryRepository : IReceivedMessageRepository
    {
        private static IEnumerable<ReceivedMessage> _receivedMessages = new List<ReceivedMessage>();

        public IEnumerable<ReceivedMessage> GetAll()
        {
            return _receivedMessages;
        }

        public ReceivedMessage GetBySatelliteName(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            return _receivedMessages.Where(m => m.SatelliteName.Equals(name)).FirstOrDefault();
        }

        public void Save(ReceivedMessage receivedMessage)
        {
            if (receivedMessage == null) throw new ArgumentNullException(nameof(receivedMessage));

            if (_receivedMessages.Any(m => m.SatelliteName.Equals(receivedMessage.SatelliteName)))
            {
                _receivedMessages.Where(m => m.SatelliteName.Equals(receivedMessage.SatelliteName))
                                .Select((m) =>
                                {
                                    m.Message = receivedMessage.Message;
                                    m.Distance = receivedMessage.Distance;
                                    return m;
                                });
            }
            else
            {
                _receivedMessages = _receivedMessages.Append(receivedMessage);
            }
        }
    }
}
