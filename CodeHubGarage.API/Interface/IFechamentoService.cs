using CodeHubGarage.Domain;

namespace CodeHubGarage.API.Interface
{
    public interface IFechamentoService
    {
        IEnumerable<Passagens> FechamentoPorPeriodo(DateTime dataHoraInicial, DateTime dataHoraFinal, string codigoGaragem);
        void RegistrarSaidaEstacionamento(string userId, DateTime dataHoraSaida, string garagemCodigo, string carroPlaca,
        string carroMarca, string carroModelo, DateTime dataHoraEntrada, string formaPagamento);
    }
}
