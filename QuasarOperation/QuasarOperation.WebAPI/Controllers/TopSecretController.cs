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
    public class TopSecretController : ControllerBase
    {
        private readonly IMessageRecoveryService _messageRecovery;
        private readonly ILocatorService _locatorService;

        public TopSecretController(IMessageRecoveryService messageRecovery,
                                   ILocatorService locatorService)
        {
            _messageRecovery = messageRecovery ?? throw new ArgumentNullException(nameof(messageRecovery));
            _locatorService = locatorService ?? throw new ArgumentNullException(nameof(locatorService));
        }


        [HttpPost]
        public IActionResult Post(TopSecretRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var received = request.Satellites.Select(
                s => new ReceivedMessage()
                {
                    SatelliteName = s.Name,
                    Distance = s.Distance,
                    Message = s.Message
                });

            var recovered = _messageRecovery.GetMessage(received);
            var location = _locatorService.GetLocation(received);

            var response = new TopSecretResponse()
            {
                Location = new CoordinateContract(location.X, location.Y),
                Messsage = recovered.Message
            };

            return Ok(response);
        }
    }
}