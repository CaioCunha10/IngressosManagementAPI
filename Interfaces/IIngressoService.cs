using IngressosAPI.DTOs;
using System.Threading.Tasks;

namespace IngressosAPI.Interfaces
{
    public interface IIngressoService
    {
        Task ProcessarIngressoAsync(IngressoDTO ingressoDTO);
    }
}
