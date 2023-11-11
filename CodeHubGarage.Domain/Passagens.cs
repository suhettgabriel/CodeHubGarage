namespace CodeHubGarage.Domain
{
    public class Passagens
    {
        public int Id { get; set; }
        public string Garagem { get; set; }
        public string CarroPlaca { get; set; }
        public string CarroMarca { get; set; }
        public string CarroModelo { get; set; }
        public DateTime DataHoraEntrada { get; set; }
        public DateTime? DataHoraSaida { get; set; }
        public string FormaPagamento { get; set; }
        public decimal PrecoTotal { get; set; }
        public decimal QuantidadeTempo { get; set; }
        //public ApplicationUser User { get; set; }
    }
}