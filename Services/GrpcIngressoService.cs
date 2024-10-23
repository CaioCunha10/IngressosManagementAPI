using IngressosAPI.Protos;
using IngressosAPI.Interfaces;
using IngressosAPI.DTOs;
using System.Threading.Tasks;

namespace IngressosAPI.Services
{
    public class GrpcIngressoService
    {
        private readonly IIngressoService _ingressoService;

        public GrpcIngressoService(IIngressoService ingressoService)
        {
            _ingressoService = ingressoService;
        }

        public async Task<IngressoResponse> AdicionarIngresso(IngressoRequest request)
        {
            var ingressoDTO = new IngressoDTO
            {
                eventoId = request.EventoId,
                nomeCompletoComprador = request.NomeCompletoComprador,
                quantidadeIngressos = request.QuantidadeIngressos,
                preco = decimal.Parse(request.Preco), 
                dataCompra = DateTime.Parse(request.DataCompra)  
            };

            await _ingressoService.ProcessarIngressoAsync(ingressoDTO);

            return new IngressoResponse
            {
                Message = $"Ingresso para o evento {request.EventoId} criado com sucesso."
            };
        }

        public async Task<IngressoProcessResponse> ProcessarIngresso(IngressoProcessRequest request)
        {
            

            return new IngressoProcessResponse
            {
                Message = $"Ingresso com ID {request.Id} processado com sucesso."
            };
        }
    }
}
