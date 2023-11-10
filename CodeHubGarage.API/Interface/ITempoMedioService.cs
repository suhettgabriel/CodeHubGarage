using CodeHubGarage.Domain;

namespace CodeHubGarage.API.Interface
{
    public interface ITempoMedioService
    {
        decimal GetTempoMedioEstadiaMensalistas(string codigoGaragem);
        decimal GetTempoMedioEstadiaClientes(string codigoGaragem);
    }
}
