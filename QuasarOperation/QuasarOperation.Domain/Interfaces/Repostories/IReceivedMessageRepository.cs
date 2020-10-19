using System.Collections.Generic;

namespace QuasarOperation.Domain.Interfaces.Repostories
{
    public interface IReceivedMessageRepository
    {
        void Save(ReceivedMessage receivedMessage);

        ReceivedMessage GetBySatelliteName(string name);

        IEnumerable<ReceivedMessage> GetAll();
    }
}
