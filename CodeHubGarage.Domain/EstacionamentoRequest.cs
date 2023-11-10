namespace CodeHubGarage.Domain
{
    public class EstacionamentoRequest
    {
        public string UserId { get; set; }
        public string GaragemCodigo { get; set; }
        public string FormasPagamentoCodigo { get; set; }
        public DateTime DataHoraEntrada { get; set; }
        public bool Status { get; set; } 
    }
}
