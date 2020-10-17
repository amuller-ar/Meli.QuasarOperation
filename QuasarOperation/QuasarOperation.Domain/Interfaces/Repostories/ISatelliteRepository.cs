using System;
using System.Collections.Generic;
using System.Text;

namespace QuasarOperation.Domain.Interfaces.Repostories
{
    public interface ISatelliteRepository
    {
        IEnumerable<Satellite> GetAll();
        Satellite GetByName(string name);
    }
}
