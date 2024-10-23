using IngressosAPI.DTOs;
using IngressosAPI.Filters;
using IngressosAPI.Interfaces;
using IngressosAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace IngressosAPI.Controllers
{
    [Route("api/[controller]")]
    //[ServiceFilter(typeof(AuthorizationFilter))]
    //[ServiceFilter(typeof(ActionFilter))]
    //[ServiceFilter(typeof(ExceptionFilter))]
    [ApiController]
    public class IngressoController : ControllerBase
    {
        private readonly IIngressoService _ingressoService;
        private readonly IServiceBusService _serviceBusService;

        public IngressoController(IIngressoService ingressoService, IServiceBusService serviceBusService)
        {
            _ingressoService = ingressoService;
            _serviceBusService = serviceBusService;
        }

        /// <summary>
        /// Método para adicionar um novo ingresso.
        /// Recebe os dados do ingresso, armazena no banco de dados e envia uma mensagem ao Service Bus.
        /// </summary>
        /// <param name="ingressoDTO">Dados do ingresso a serem processados</param>
        /// <returns>Status 201 Created com os detalhes do ingresso criado</returns>
        [HttpPost]
        public async Task<IActionResult> AddIngresso([FromBody] IngressoDTO ingressoDTO)
        {
            if (ingressoDTO == null)
            {
                return BadRequest("Os dados do ingresso são inválidos.");
            }

            try
            {
                await _ingressoService.ProcessarIngressoAsync(ingressoDTO);
                await _serviceBusService.EnviarMensagemAsync(ingressoDTO);
                return CreatedAtAction(nameof(AddIngresso), new { id = ingressoDTO.eventoId }, new
                
                {
                    Message = "Ingresso criado com sucesso e enviado para processamento.",
                    IngressoDetalhes = ingressoDTO,
                    Status = "Mensagem enviada ao Service Bus",
                    HoraProcessamento = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar o ingresso: {ex.Message}");
            }
        }


    }
}

         


   