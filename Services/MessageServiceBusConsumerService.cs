using Azure.Messaging.ServiceBus;
using IngressosAPI.Interfaces;

namespace IngressosAPI.Services
{

    public class MessageServiceBusConsumerService : IMessageServiceBusConsumerService
    {
        private readonly string _connectionString;
        private readonly string _queueName;
        private readonly ServiceBusClient _client;

        public MessageServiceBusConsumerService(IConfiguration configuration)
        {
            _connectionString = configuration["AzureServiceBus:ConnectionString"];
            _queueName = configuration["AzureServiceBus:QueueName"];
            _client = new ServiceBusClient(_connectionString);
        }

        public async Task ProcessarMensagensAsync()
        {
            var reciever = _client.CreateReceiver(_queueName);

            try
            {
                var msg = await reciever.ReceiveMessageAsync();
                if (msg != null)
                {
                    var body = msg.Body.ToString();
                    Console.WriteLine($"Menssagem recebida: {body}");
                }
                else
                {
                    Console.WriteLine("Nenhuma mensagem recebida.");
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Erro no processamento da mensagem: {ex.Message}");
                throw;

            }
        }
    }
}
 
