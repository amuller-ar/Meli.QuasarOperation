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
        //private static IEnumerable<ReceivedMessage> _receivedMessages = new List<ReceivedMessage>();
        private static IDictionary<string, ReceivedMessage> _receivedMessages = new Dictionary<string, ReceivedMessage>();

        public IEnumerable<ReceivedMessage> GetAll()
        {
            return _receivedMessages.Values;
        }

        public ReceivedMessage GetBySatelliteName(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            return _receivedMessages[name];
        }

        public void Save(ReceivedMessage receivedMessage)
        {
            if (receivedMessage == null) throw new ArgumentNullException(nameof(receivedMessage));

            if (_receivedMessages.ContainsKey(receivedMessage.SatelliteName.ToLower()))
            {
                _receivedMessages[receivedMessage.SatelliteName.ToLower()] = receivedMessage;
            }
            else
            {
                _receivedMessages.Add(receivedMessage.SatelliteName.ToLower(), receivedMessage);
            }

            //if (_receivedMessages.Any(m => m.SatelliteName.Equals(receivedMessage.SatelliteName)))
            //{
            //    _receivedMessages.Where(m => m.SatelliteName.Equals(receivedMessage.SatelliteName))
            //                    .Select((m) =>
            //                    {
            //                        m.Message = receivedMessage.Message;
            //                        m.Distance = receivedMessage.Distance;
            //                        return m;
            //                    });
            //}
            //else
            //{
            //    _receivedMessages = _receivedMessages.Append(receivedMessage);
            //}
        }
    }
}
