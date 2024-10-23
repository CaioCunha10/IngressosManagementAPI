namespace IngressosAPI.Models
{
    public class IngressoEntity
    {
        public string id { get; set; }
        public string eventoId { get; set; }
        public string nomeCompletoComprador { get; set; }
        
        public int quantidadeIngressos { get; set; }

        public decimal preco { get; set; }

        public DateTime dataCompra { get; set; }

    }
}
