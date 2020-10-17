using System;
using System.Collections.Generic;
using System.Text;

namespace QuasarOperation.Domain.Interfaces.Services
{
    public interface ILocatorService
    {
        Coordinate GetLocation(double distance, Coordinate location1, Coordinate location2);
        Coordinate GetLocation(IEnumerable<ReceivedMessage> transmission);
    }
}
