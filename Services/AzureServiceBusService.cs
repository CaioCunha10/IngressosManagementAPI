using Azure.Messaging.ServiceBus;
using IngressosAPI.DTOs;
using IngressosAPI.Interfaces;
using System.Text.Json;


namespace IngressosAPI.Services
{
    public class AzureServiceBusService : IServiceBusService
    {
        private readonly ServiceBusClient _client;
        private readonly string _queueName;
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        public AzureServiceBusService(IConfiguration configuration)
        {
            _connectionString = configuration["AzureServiceBus:ConnectionString"];
            _queueName = configuration["AzureServiceBus:QueueName"];
            _client = new ServiceBusClient(_connectionString);
        }
        public async Task EnviarMensagemAsync(IngressoDTO ingressoDTO)
        {
            var sender = _client.CreateSender(_queueName);

            try
            {
                var messageBody = JsonSerializer.Serialize(ingressoDTO);
                var message = new ServiceBusMessage(messageBody);
                await sender.SendMessageAsync(message);
                Console.WriteLine($"Mensagem foi enviada a fila {_queueName}: {messageBody}");
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Erro no envio da mensagem: {ex.Message}");
                throw;
            }
        }
    }
}
