using IngressosAPI.DTOs;

namespace IngressosAPI.Interfaces
{
    public interface IServiceBusService
    {
        Task EnviarMensagemAsync(IngressoDTO ingressoDTO);
    }
}
