using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuasarOperation.Domain;
using QuasarOperation.Domain.Contracts;
using QuasarOperation.Domain.Contracts.Model;
using QuasarOperation.Domain.Interfaces.Services;

namespace QuasarOperation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopSecretSplitController : ControllerBase
    {
        private readonly IMessageRecovery _messageRecovery;
        private readonly ILocatorService _locatorService;


        public TopSecretSplitController(IMessageRecovery messageRecovery,
                                        ILocatorService locatorService)
        {
            _messageRecovery = messageRecovery ?? throw new ArgumentNullException(nameof(messageRecovery));
            _locatorService = locatorService ?? throw new ArgumentNullException(nameof(locatorService));
        }

        [HttpPost]
        public IActionResult Post(TransmissionContract transmission)
        {
            _messageRecovery.ReceiveMessage(new ReceivedMessage()
            {
                Distance = transmission.Distance,
                Message = transmission.Message,
                SatelliteName = transmission.Name
            });

            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var message = _messageRecovery.TryRecoverMessage();
            var location = _locatorService.TryGetLocation();

            return Ok(new TopSecretSplitResopnse()
            {
                Location = new CoordinateContract(location.X, location.Y),
                Messsage = message.Message
            });
        }
    }
}