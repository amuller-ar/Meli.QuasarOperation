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
            throw new NotImplementedException();
        }

        public ReceivedMessage GetBySatelliteName(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            return _receivedMessages.Where(m => m.SatelliteName.Equals(name)).FirstOrDefault();
        }

        public void Save(ReceivedMessage receivedMessage)
        {
            throw new NotImplementedException();
        }
    }
}
