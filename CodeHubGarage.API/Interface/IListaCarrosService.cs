using CodeHubGarage.Domain;

namespace CodeHubGarage.API.Interface
{
    public interface IListaCarrosService
    {
        List<Passagens> GetCarrosNoPeriodo(DateTime dataHoraInicial, DateTime dataHoraFinal, string codigoGaragem);
        List<Passagens> GetCarrosNaGaragem(string codigoGaragem);
        List<Passagens> GetCarrosQuePassaram(string codigoGaragem);
    }
}
