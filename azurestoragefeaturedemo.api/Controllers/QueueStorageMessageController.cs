using azurestoragefeaturedemo.api.Services;
using azurestoragefeaturedemo.api.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace azurestoragefeaturedemo.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueStorageMessageController : ControllerBase
    {
        private readonly IAzureServiceBusService _azureServiceBusService;

        public QueueStorageMessageController(IAzureServiceBusService azureServiceBusService)
        {
            _azureServiceBusService = azureServiceBusService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(string message)
        {
            await _azureServiceBusService.SendMessageAsync(message);
            return new StatusCodeResult((int)HttpStatusCode.Created);
        }

        [HttpPost("withJsonBody")]
        public async Task<IActionResult> Post([FromBody] MessageRequest messageRequest)
        {
            await _azureServiceBusService.SendMessageAsync(messageRequest);
            return new StatusCodeResult((int)HttpStatusCode.Created);
        }

        [HttpGet]
        public async Task<IActionResult> GetMessageRecived(int msgCount=5)
        {
           var reslutl = await _azureServiceBusService.RecivedMessageAsync(msgCount);
            return Ok(reslutl);
        }

        [HttpGet("MessageRecivedDelete")]
        public async Task<IActionResult> GetMessageRecivedDelete(int msgCount = 5)
        {
            var reslutl = await _azureServiceBusService.RecivedMessageDeleteAsync(msgCount);
            return Ok(reslutl);
        }
    }
}
