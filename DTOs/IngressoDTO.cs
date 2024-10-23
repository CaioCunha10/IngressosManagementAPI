namespace IngressosAPI.DTOs
{
    public class IngressoDTO
    {
        public string eventoId { get; set; }
        public string nomeCompletoComprador { get; set; }
        public int quantidadeIngressos { get; set; }
        public decimal preco { get; set; }

        public DateTime dataCompra { get; set; }
    }
}
