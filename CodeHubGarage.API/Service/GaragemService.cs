using CodeHubGarage.API.Data;
using CodeHubGarage.API.Interface;
using CodeHubGarage.Domain;


public class GaragemService : IGaragemService
{
    private readonly GarageDbContext _dbContext;

    public GaragemService(GarageDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Garagens> GetGaragens()
    {
        return _dbContext.Garagens.ToList();
    }

    public Garagens GetGaragemByCodigo(string codigo)
    {
        return _dbContext.Garagens.FirstOrDefault(g => g.Codigo == codigo);
    }

    public Garagens GetGaragemByUserId(string userId)
    {

        return _dbContext.Garagens.FirstOrDefault();
    }
}
