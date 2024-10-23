using IngressosAPI.Interfaces;
using IngressosAPI.Models;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;


namespace IngressosAPI.Repositories
{
    public class IngressoRepository : IIngressoRepository
    {
        private readonly Container _container; //Contêiner do Azure Cosmos onde os ingressos serão armazenados.
        public IngressoRepository(CosmosClient cosmosClient, IConfiguration configuration)
        {
            string databaseName = configuration["CosmosDb:DatabaseName"];
            string containerName = configuration["CosmosDb:ContainerName"];
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }
        public async Task AdicionarIngressoAsync(IngressoEntity ingresso)
        {
            /// Adicionando a camada de repositório a chamada para entidade ingresso e referenciando a partitionKey definida no cosmos...
            await _container.CreateItemAsync(ingresso, new PartitionKey(ingresso.eventoId));
        }
    }
}
