using CodeHubGarage.API.Data;
using CodeHubGarage.API.Interface;
using CodeHubGarage.Domain;
using Microsoft.Extensions.Logging;

public class EstacionamentoService : IEstacionamentoService
{
    private readonly IGaragemService _garagemService;
    private readonly GarageDbContext _dbContext;
    private readonly ILogger<EstacionamentoService> _logger;  

    public EstacionamentoService(IGaragemService garagemService, GarageDbContext dbContext, ILogger<EstacionamentoService> logger)
    {
        _garagemService = garagemService;
        _dbContext = dbContext;
        _logger = logger;
    }

    public bool VerificarSeUsuarioEhMensalista(string userId)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
        return user?.IsMensalista ?? false;
    }    
    public string VerificaNomeUsuario(string userId)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
        return user.UserName;
    }

    public decimal CalcularValorEstadia(string userId, bool isMensalista, DateTime entrada, DateTime? saida)
    {
        decimal valorCalculado = 0;

        Garagens garagem = _garagemService.GetGaragemByUserId(userId);

        if (isMensalista)
        {
            valorCalculado = garagem.Preco_Mensalista;
        }
        else
        {
            decimal precoHora = garagem.Preco_1aHora;

            valorCalculado = precoHora;

            if (saida.HasValue)
            {
                TimeSpan duracao = saida.Value - entrada;

                if (duracao > TimeSpan.FromMinutes(30))
                {
                    valorCalculado += CalcularValorDemaisHoras(garagem, duracao);
                }
                else if (duracao > TimeSpan.Zero)
                {
                    valorCalculado += precoHora * 0.5m;
                }
            }

            var estacionamento = new Estacionamentos
            {
                UserId = userId,
                GaragemCodigo = garagem.Codigo,
                DataHoraEntrada = entrada,
                Status = true 
            };

            _dbContext.Estacionamentos.Add(estacionamento);
            _dbContext.SaveChanges();
            _logger.LogInformation($"Estacionamento registrado para o usuário {userId} na garagem {garagem.Codigo}.");

            // Log de informações sobre a estadia
            _logger.LogInformation($"Entrada: {entrada}, Saída: {saida}, Valor Estadia: {valorCalculado}");
        }

        return valorCalculado;
    }

    private decimal CalcularValorDemaisHoras(Garagens garagem, TimeSpan duracao)
    {
        decimal precoHorasExtra = garagem.Preco_HorasExtra;
        int horasCheias = (int)Math.Ceiling((duracao.TotalMinutes - 30) / 60);

        return horasCheias * precoHorasExtra;
    }
}
