using CodeHubGarage.Domain;

namespace CodeHubGarage.API.Interface
{
    public interface IPassagemService
    {
        List<Passagens> GetPassagensByGaragem(string codigoGaragem);
        List<Passagens> GetPassagensByPeriod(DateTime dataHoraInicial, DateTime dataHoraFinal, string codigoGaragem);
        List<Passagens> GetCarrosNoPeriodo(DateTime dataHoraInicial, DateTime dataHoraFinal, string codigoGaragem);
        List<Passagens> GetCarrosNaGaragem(string codigoGaragem);
        List<Passagens> GetCarrosQuePassaram(string codigoGaragem);
        List<Passagens> GetFechamentoPorPeriodo(DateTime dataHoraInicial, DateTime dataHoraFinal, string codigoGaragem);
        List<Passagens> GetTempoMedioEstadiaMensalistas(string codigoGaragem);
        List<Passagens> GetTempoMedioEstadiaClientes(string codigoGaragem);
    }
}