using System;
using System.Collections.Generic;
using System.Text;

namespace QuasarOperation.Domain.Interfaces.Repostories
{
    public interface IReceivedMessageRepository
    {
        void Save(ReceivedMessage receivedMessage);

        ReceivedMessage GetBySatelliteName(string name);

        IEnumerable<ReceivedMessage> GetAll();
    }
}
