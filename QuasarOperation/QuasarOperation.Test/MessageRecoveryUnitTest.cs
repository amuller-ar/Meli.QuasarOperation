using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QuasarOperation.Domain;
using QuasarOperation.Domain.Contracts;
using QuasarOperation.Domain.Contracts.Model;
using QuasarOperation.Domain.Exceptions;
using QuasarOperation.Domain.Interfaces.Repostories;
using QuasarOperation.Domain.Interfaces.Services;
using QuasarOperation.Services;
using QuasarOperation.WebAPI.Controllers;
using System.Collections.Generic;

namespace QuasarOperation.Test
{
    [TestClass]
    public class UnitTests
    {
        private Mock<ISatelliteRepository> _satelliteRepository;
        private Mock<IReceivedMessageRepository> _receivedMessageRepository;

        [TestInitialize]
        public void Initialize()
        {
            _satelliteRepository = new Mock<ISatelliteRepository>(); 
            _satelliteRepository.Setup(x => x.GetAll()).Returns(new List<Satellite>() {
            new Satellite(){ Name = "kenobi", Location = new Coordinate(-500,-200) },
            new Satellite(){ Name = "skywalker", Location = new Coordinate(100,-100) },
            new Satellite(){ Name = "sato", Location = new Coordinate(500,100) }
            });

            _receivedMessageRepository = new Mock<IReceivedMessageRepository>();
        }

        [TestMethod]
        public void MessageRecoveryThrowException()
        {

            //arrange
            var _messageRecoveryService = new MessageRecoveryService(_satelliteRepository.Object, _receivedMessageRepository.Object);

            var receivedMessages = new List<ReceivedMessage>() {
                new ReceivedMessage() { Distance = 112, SatelliteName = "sato", Message = new string[]{ "este", "es", "un", "", "" } },
                new ReceivedMessage() { Distance = 120, SatelliteName = "kenobi", Message = new string[]{ "este", "es", "un", "", "" } },
                new ReceivedMessage() { Distance = 112, SatelliteName = "skywalker", Message = new string[]{ "este", "es", "un", "", "" } }
            };

            //act + assert
            Assert.ThrowsException<CantRecoverMessageException>(() => _messageRecoveryService.GetMessage(receivedMessages));
        }

        [TestMethod]
        public void DecodeMessage()
        {
            //arrange
            var _messageRecoveryService = new MessageRecoveryService(_satelliteRepository.Object, _receivedMessageRepository.Object);

            var receivedMessages = new List<ReceivedMessage>() {
                new ReceivedMessage() { Distance = 112, SatelliteName = "sato", Message = new string[]{ "este", "es", "un", "", "" } },
                new ReceivedMessage() { Distance = 120, SatelliteName = "kenobi", Message = new string[]{ "este", "es", "", "mensaje", "" } },
                new ReceivedMessage() { Distance = 112, SatelliteName = "skywalker", Message = new string[]{ "este", "", "", "", "secreto" } }
            };

            //act

            var result = _messageRecoveryService.GetMessage(receivedMessages);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Message, "este es un mensaje secreto");
        }

        [TestMethod]
        public void LocateShipThrowException()
        {
            //arrange
            var _locatorService = new LocatorService(_satelliteRepository.Object, _receivedMessageRepository.Object);
            var _receivedMessages = new List<ReceivedMessage>() {
                new ReceivedMessage() { Distance = 112, SatelliteName = "sato", Message = new string[]{ "este", "es", "un", "", "" } }
            };

            //assert
            Assert.ThrowsException<CantDeterminateLocationException>(() => _locatorService.GetLocation(_receivedMessages));
            
        }
    }
}
