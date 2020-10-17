using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuasarOperation.Domain;
using QuasarOperation.Domain.Contracts.Model;
using QuasarOperation.Domain.Interfaces.Services;

namespace QuasarOperation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopSecretSplitController : ControllerBase
    {
        private readonly IMessageRecovery _messageRecovery;

        public TopSecretSplitController(IMessageRecovery messageRecovery)
        {
            _messageRecovery = messageRecovery ?? throw new ArgumentNullException(nameof(messageRecovery));
        }

        [HttpPost]
        public IActionResult Post(TransmissionContract transmission)
        {
            _messageRecovery.ReceiveMessage(new ReceivedMessage() { });

            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}