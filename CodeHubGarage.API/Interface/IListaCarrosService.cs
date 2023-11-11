using CodeHubGarage.Domain;

namespace CodeHubGarage.API.Interface
{
    public interface IListaCarrosService
    {
        List<Estacionamentos> GetCarrosNoPeriodo(DateTime dataHoraInicial, DateTime dataHoraFinal, string codigoGaragem);
        List<Estacionamentos> GetCarrosNaGaragem(string codigoGaragem);
        List<Estacionamentos> GetCarrosQuePassaram(string codigoGaragem);
    }
}
