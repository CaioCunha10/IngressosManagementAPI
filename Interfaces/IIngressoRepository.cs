using IngressosAPI.Models;

namespace IngressosAPI.Interfaces
{
    public interface IIngressoRepository
    {
        Task AdicionarIngressoAsync(IngressoEntity ingresso);
    }
}
