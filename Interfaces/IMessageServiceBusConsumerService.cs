namespace IngressosAPI.Interfaces
{
    public interface IMessageServiceBusConsumerService
    {
        Task ProcessarMensagensAsync();
    }
}
