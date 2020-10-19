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
        private readonly IReceivedMessageRepository _receivedMessageRepository;
        #endregion

        #region constructor

        public LocatorService(ISatelliteRepository satelliteRepository,
                              IReceivedMessageRepository receivedMessageRepository)
        {
            _satelliteRepository  = satelliteRepository ?? throw new ArgumentNullException(nameof(satelliteRepository));
            _receivedMessageRepository = receivedMessageRepository ?? throw new ArgumentNullException(nameof(receivedMessageRepository));
        }

        #endregion

        #region public 

        /// <summary>
        /// Determina la ubicación de la nave
        /// </summary>
        /// <param name="shipDistance"></param>
        /// <param name="location1"></param>
        /// <param name="location2"></param>
        /// <returns></returns>
        public Coordinate GetLocation(double shipDistance, Coordinate location1, Coordinate location2)
        {
            //vectores 
            var deltaX = location1.X - location2.X;
            var deltaY = location1.Y - location2.Y;

            //distancia entre 2 satelites
            var distance = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));

            //vector dirección normalizado
            var dx = deltaX / distance;
            var dy = deltaY / distance;

            var x = location1.X + shipDistance * dx;
            var y = location1.Y + shipDistance * dy;

            return new Coordinate(x, y);
        }

        /// <summary>
        /// Determina la ubicación de la nave con las transmisiones recibidas
        /// </summary>
        /// <param name="transmission"></param>
        /// <returns></returns>
        public Coordinate GetLocation(IEnumerable<ReceivedMessage> transmission)
        {
            if (transmission.Count() < 2)
            {
                throw new CantDeterminateLocationException("No se puede obtener la localización");
            }

            var tr = transmission.ToList();

            var location1 = _satelliteRepository.GetByName(tr[0].SatelliteName).Location;
            var location2 = _satelliteRepository.GetByName(tr[1].SatelliteName).Location;

            return GetLocation(tr.First().Distance,location1,location2);
        }

        /// <summary>
        /// Intenta determinar la ubicación de la nave con las transmisiones que se fueron recibiendo
        /// </summary>
        /// <returns></returns>
        public Coordinate TryGetLocation()
        {
            return GetLocation(_receivedMessageRepository.GetAll());
        }

        #endregion
    }
}
