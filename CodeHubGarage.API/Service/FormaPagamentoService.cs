using System.Collections.Generic;
using System.Linq;
using CodeHubGarage.API.Data;
using CodeHubGarage.API.Interface;
using CodeHubGarage.Domain;

public class FormaPagamentoService : IFormaPagamentoService
{
    private readonly GarageDbContext _dbContext;

    public FormaPagamentoService(GarageDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<FormasPagamento> GetFormasPagamento()
    {
        return _dbContext.FormasPagamento.ToList();
    }

    public FormasPagamento GetFormaPagamentoByCodigo(string codigo)
    {
        return _dbContext.FormasPagamento.FirstOrDefault(fp => fp.Codigo == codigo);
    }
}
