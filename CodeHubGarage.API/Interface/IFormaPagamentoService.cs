using CodeHubGarage.Domain;

namespace CodeHubGarage.API.Interface
{
    public interface IFormaPagamentoService
    {
        List<FormasPagamento> GetFormasPagamento();
        FormasPagamento GetFormaPagamentoByCodigo(string codigo);
    }
}