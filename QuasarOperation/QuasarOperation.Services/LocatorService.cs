using QuasarOperation.Domain;
using QuasarOperation.Domain.Exceptions;
using QuasarOperation.Domain.Interfaces.Repostories;
using QuasarOperation.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuasarOperation.Services
{
    public class LocatorService : ILocatorService
    {
        #region private members
        private readonly ISatelliteRepository _satelliteRepository;
        #endregion

        #region constructor

        public LocatorService(ISatelliteRepository satelliteRepository)
        {
            _satelliteRepository  = satelliteRepository ?? throw new ArgumentNullException(nameof(satelliteRepository));
        }

        #endregion

        #region public 


        public Coordinate GetLocation(double shipDistance, Coordinate location1, Coordinate location2)
        {
            //vectores 
            var deltaX = location1.X - location2.X;
            var deltaY = location2.Y - location2.Y;

            //distancia entre 2 satelites
            var distance = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));

            //vector dirección normalizado
            var dx = deltaX / distance;
            var dy = deltaY / distance;

            var x = location1.X + shipDistance * dx;
            var y = location1.Y + shipDistance * dy;

            return new Coordinate(x, y);
        }

        public Coordinate GetLocation(IEnumerable<ReceivedMessage> transmission)
        {
            if (transmission.Count() < 2)
            {
                throw new CantRecoverMessageException("No se puede obtener la localización");
            }

            var tr = transmission.ToList();

            var location1 = _satelliteRepository.GetByName(tr[0].SatelliteName).Location;
            var location2 = _satelliteRepository.GetByName(tr[1].SatelliteName).Location;

            return GetLocation(tr.First().Distance,location1,location2);
        }

        #endregion
    }
}
