using CodeHubGarage.API.Data;
using CodeHubGarage.API.Interface;
using CodeHubGarage.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

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

    public DateTime? GetDataEntrada(string userId)
    {
        var dataEntrada = _dbContext.Estacionamentos
            .Where(e => e.UserId == userId && e.Status == false)
            .OrderByDescending(e => e.DataHoraEntrada)
            .Select(e => e.DataHoraEntrada)
            .FirstOrDefault();

        return dataEntrada;
    }

    public void DadosInfoUsuario(string userId, out string carroPlaca, out string carroMarca, out string carroModelo)
    {
        var dadosUsuario = _dbContext.Estacionamentos
            .Where(e => e.UserId == userId && e.Status == false)
            .Select(e => new
            {
                CarroPlaca = e.CarroPlaca,
                CarroMarca = e.CarroMarca,
                CarroModelo = e.CarroModelo
            })
            .FirstOrDefault();

        if (dadosUsuario != null)
        {
            carroPlaca = dadosUsuario.CarroPlaca;
            carroMarca = dadosUsuario.CarroMarca;
            carroModelo = dadosUsuario.CarroModelo;
        }
        else
        {
            carroPlaca = null;
            carroMarca = null;
            carroModelo = null;
        }
    }

    public string VerificaNomeUsuario(string userId)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
        return user?.UserName ?? "Usuário não encontrado";
    }

    public decimal CalcularValorEstadia(string userId, bool isMensalista, DateTime entrada, DateTime? dataHoraSaida)
    {
        decimal valorCalculado = 0;

        Garagens garagem = _garagemService.GetGaragemByUserId(userId);
        DateTime? dataEntrada = GetDataEntrada(userId);
        string carroPlaca, carroMarca, carroModelo;

        DadosInfoUsuario(userId, out carroPlaca, out carroMarca, out carroModelo);

        if (!dataEntrada.HasValue)
        {
            var novoEstacionamento = new Estacionamentos
            {
                UserId = userId,
                GaragemCodigo = garagem.Codigo,
                CarroPlaca = carroPlaca,
                CarroMarca = carroMarca,
                CarroModelo = carroModelo,
                DataHoraEntrada = entrada,
                Status = true
            };

            _dbContext.Estacionamentos.Add(novoEstacionamento);
            _dbContext.SaveChanges();

            _logger.LogInformation($"Estacionamento registrado para o usuário {userId} na garagem {garagem.Codigo}.");

            var novaPassagem = new Passagens
            {
                CarroPlaca = novoEstacionamento.CarroPlaca,
                CarroMarca = novoEstacionamento.CarroMarca,
                CarroModelo = novoEstacionamento.CarroModelo,
                DataHoraEntrada = entrada,
                FormaPagamento = novoEstacionamento.FormasPagamentoCodigo,
                Garagem = garagem.Codigo,
                QuantidadeTempo = 0, // Criar lógica adequada para obter a quantidade de tempo
                PrecoTotal = 0 // Criar lógica adequada para obter o preço total
            };

            _dbContext.Passagens.Add(novaPassagem);
            _dbContext.SaveChanges();

            _logger.LogInformation($"Passagem registrada para o usuário {userId} na garagem {garagem.Codigo}.");
        }
        else
        {
            if (isMensalista)
            {
                valorCalculado = garagem.Preco_Mensalista;
            }
            else
            {
                decimal precoHora = garagem.Preco_1aHora;

                valorCalculado = precoHora;

                if (dataHoraSaida.HasValue)
                {
                    TimeSpan duracao = dataHoraSaida.Value - dataEntrada.Value;

                    if (duracao > TimeSpan.FromMinutes(30))
                    {
                        valorCalculado += CalcularValorDemaisHoras(garagem, duracao);
                    }
                    else if (duracao > TimeSpan.Zero)
                    {
                        valorCalculado += precoHora * 0.5m;
                    }
                }
            }
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
