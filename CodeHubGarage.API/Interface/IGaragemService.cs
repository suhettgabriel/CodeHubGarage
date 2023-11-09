using CodeHubGarage.Domain;

namespace CodeHubGarage.API.Interface
{
    public interface IGaragemService
    {
        List<Garagens> GetGaragens();
        Garagens GetGaragemByCodigo(string codigo);
    }
}