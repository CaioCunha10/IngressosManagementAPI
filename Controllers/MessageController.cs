using IngressosAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IngressosAPI.Controllers
{
    ///Controller para testar o tráfego de mensagens do AzureServiceBus
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
          private readonly IMessageServiceBusConsumerService _messageConsumerService;

        public MessageController(IMessageServiceBusConsumerService messageConsumerService)
        {
            _messageConsumerService = messageConsumerService;
        }

        [HttpGet("consume")]
        public async Task<IActionResult> ConsumeMessages()
        {
            await _messageConsumerService.ProcessarMensagensAsync();
            return Ok("Mensagens consumidas com sucesso.");
        }
    }
}
