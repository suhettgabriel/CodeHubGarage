namespace CodeHubGarage.Domain
{
    public class Estacionamentos
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string GaragemCodigo { get; set; }
        public string CarroPlaca { get; set; }
        public string CarroMarca { get; set; }
        public string CarroModelo { get; set; }
        public string FormasPagamentoCodigo { get; set; }
        public DateTime DataHoraEntrada { get; set; }
        public DateTime? DataHoraSaida { get; set; }
        public bool Status { get; set; }
        public bool isMensalista { get; set; }
    }
}