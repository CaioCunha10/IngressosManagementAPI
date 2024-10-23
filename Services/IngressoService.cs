using IngressosAPI.Interfaces;
using IngressosAPI.DTOs;
using IngressosAPI.Models;
using System.Threading.Tasks;

namespace IngressosAPI.Services
{
    public class IngressoService : IIngressoService
    {
        private readonly IIngressoRepository _ingressoRepository;

        public IngressoService(IIngressoRepository ingressoRepository)
        {
            _ingressoRepository = ingressoRepository;
        }

        public async Task ProcessarIngressoAsync(IngressoDTO ingressoDTO)
        {
            var ingressoEntity = new IngressoEntity
            {
                id = Guid.NewGuid().ToString(),
                eventoId = ingressoDTO.eventoId,
                nomeCompletoComprador = ingressoDTO.nomeCompletoComprador,
                quantidadeIngressos = ingressoDTO.quantidadeIngressos,
                preco = ingressoDTO.preco,
                dataCompra = ingressoDTO.dataCompra,
            };
            await _ingressoRepository.AdicionarIngressoAsync(ingressoEntity);
        }
    }
}
