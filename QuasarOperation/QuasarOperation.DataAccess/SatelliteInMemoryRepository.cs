using QuasarOperation.Domain;
using QuasarOperation.Domain.Interfaces.Repostories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuasarOperation.DataAccess
{
    public class SatelliteInMemoryRepository : ISatelliteRepository
    {
        private static readonly IEnumerable<Satellite> satellites = new List<Satellite>() { 
            new Satellite(){ Name = "kenobi", Location = new Coordinate(-500,-200) },
            new Satellite(){ Name = "skywalker", Location = new Coordinate(100,-100) },
            new Satellite(){ Name = "sato", Location = new Coordinate(500,100) }
        };

        public IEnumerable<Satellite> GetAll()
        {
            return satellites;
        }

        public Satellite GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return satellites.Where(s => s.Name.ToUpper().Equals(name.ToUpper())).SingleOrDefault();
        }
    }
}
