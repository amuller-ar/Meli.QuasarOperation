using QuasarOperation.Domain;
using QuasarOperation.Domain.Interfaces.Repostories;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuasarOperation.DataAccess
{
    public class ReceivedMessageInMemoryRepository : IReceivedMessageRepository
    {
        public IEnumerable<ReceivedMessage> GetAll()
        {
            throw new NotImplementedException();
        }

        public ReceivedMessage GetBySatelliteName(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
        }

        public void Save(ReceivedMessage receivedMessage)
        {
            throw new NotImplementedException();
        }
    }
}
