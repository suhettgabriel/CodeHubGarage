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
        try
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            return user?.IsMensalista ?? false;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro em VerificarSeUsuarioEhMensalista para o usuário {userId}: {ex.Message}");
            throw;
        }
    }

    public DateTime GetDataEntrada(string userId)
    {
        try
        {
            var dataEntrada = _dbContext.Estacionamentos
                .Where(e => e.UserId == userId && e.Status == false)
                .OrderByDescending(e => e.DataHoraEntrada)
                .Select(e => e.DataHoraEntrada)
                .FirstOrDefault();

            return dataEntrada;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro em GetDataEntrada para o usuário {userId}: {ex.Message}");
            throw;
        }
    }

    public TimeSpan BuscarQuantidadeTempo(string userId, DateTime dataHoraSaida)
    {
        try
        {
            DateTime? dataEntrada = GetDataEntrada(userId);

            if (dataEntrada.HasValue)
            {
                return dataHoraSaida - dataEntrada.Value;
            }

            return TimeSpan.Zero;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro em BuscarQuantidadeTempo para o usuário {userId}: {ex.Message}");
            throw;
        }
    }

    public DadosUsuario ObterDadosUsuario(string userId)
    {
        try
        {
            var dadosUsuario = _dbContext.Estacionamentos
                .Where(e => e.UserId == userId && e.Status == false)
                .Select(e => new DadosUsuario
                {
                    CarroPlaca = e.CarroPlaca,
                    CarroMarca = e.CarroMarca,
                    CarroModelo = e.CarroModelo,
                    FormaPagamento = e.FormasPagamentoCodigo,
                    Garagem = e.GaragemCodigo,
                })
                .FirstOrDefault();

            if (dadosUsuario == null)
            {
                _logger.LogWarning($"Não foi encontrado nenhum carro estacionado para o usuário {userId}.");
            }

            return dadosUsuario;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro em ObterDadosUsuario para o usuário {userId}: {ex.Message}");
            throw;
        }
    }
    public void RegistrarEntradaEstacionamento(string userId, DateTime entrada)
    {
        try
        {
            Garagens garagem = _garagemService.GetGaragemByUserId(userId);
            DateTime? dataEntrada = GetDataEntrada(userId);

            // Substituindo a chamada ao método DadosInfoUsuario por ObterDadosUsuario
            DadosUsuario dadosUsuario = ObterDadosUsuario(userId);

            if (!dataEntrada.HasValue)
            {
                var novoEstacionamento = new Estacionamentos
                {
                    UserId = userId,
                    GaragemCodigo = garagem.Codigo,
                    CarroPlaca = dadosUsuario.CarroPlaca,
                    CarroMarca = dadosUsuario.CarroMarca,
                    CarroModelo = dadosUsuario.CarroModelo,
                    DataHoraEntrada = entrada,
                    FormasPagamentoCodigo = dadosUsuario.FormaPagamento,
                    Status = true
                };

                _dbContext.Estacionamentos.Add(novoEstacionamento);
                _dbContext.SaveChanges();

                _logger.LogInformation($"Estacionamento registrado para o usuário {userId} na garagem {garagem.Codigo}.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro em RegistrarEntradaEstacionamento para o usuário {userId}: {ex.Message}");
            throw;
        }
    }

    public void SalvarPassagem(string userId, DateTime dataSaida, Garagens garagem, string carroPlaca, string carroMarca, string carroModelo, string formaPagamento, TimeSpan quantidadeTempo, decimal valorEstadia)
    {
        try
        {
            DateTime dataEntrada = GetDataEntrada(userId);

            var novaPassagem = new Passagens
            {
                Garagem = garagem.Codigo,
                CarroPlaca = carroPlaca,
                CarroMarca = carroMarca,
                CarroModelo = carroModelo,
                DataHoraEntrada = dataEntrada,
                DataHoraSaida = dataSaida,
                FormaPagamento = formaPagamento,
                QuantidadeTempo = quantidadeTempo.Hours,
                PrecoTotal = valorEstadia
            };

            _dbContext.Passagens.Add(novaPassagem);
            _dbContext.SaveChanges();

            _logger.LogInformation($"Passagem registrada para o usuário {userId} na garagem {garagem.Codigo}.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro em SalvarPassagem para o usuário {userId}: {ex.Message}");
            throw;
        }
    }

    public void RegistrarSaidaEstacionamento(string userId, DateTime dataHoraSaida, string garagemCodigo, string carroPlaca, string carroMarca, string carroModelo, DateTime dataHoraEntrada, string formaPagamento)
    {
        try
        {
            var estacionamentoAberto = _dbContext.Estacionamentos
                .FirstOrDefault(e => e.UserId == userId && e.Status == false && e.DataHoraSaida == null);

            if (estacionamentoAberto != null)
            {
                estacionamentoAberto.DataHoraSaida = dataHoraSaida;
                estacionamentoAberto.Status = true;

                _dbContext.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro em RegistrarSaidaEstacionamento para o usuário {userId}: {ex.Message}");
            throw;
        }
    }

    public string VerificaNomeUsuario(string userId)
    {
        try
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            return user?.UserName ?? "Usuário não encontrado";
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro em VerificaNomeUsuario para o usuário {userId}: {ex.Message}");
            throw;
        }
    }

    public decimal CalcularValorEstadia(string userId, bool isMensalista, DateTime entrada, DateTime? dataHoraSaida)
    {
        try
        {
            decimal valorCalculado = 0;

            Garagens garagem = _garagemService.GetGaragemByUserId(userId);
            DateTime? dataEntrada = GetDataEntrada(userId);

            if (isMensalista)
            {
                valorCalculado = garagem.Preco_Mensalista;
            }
            else
            {
                decimal precoHora = garagem.Preco_1aHora;
                TimeSpan duracao = dataHoraSaida.Value - dataEntrada.Value;

                if (duracao > TimeSpan.FromHours(1))
                {
                    valorCalculado = precoHora;
                    valorCalculado += CalcularValorDemaisHoras(garagem, duracao);
                }
                else if (duracao > TimeSpan.FromMinutes(30))
                {
                    valorCalculado = precoHora + precoHora * 0.5m;
                }
                else if (duracao > TimeSpan.Zero)
                {
                    valorCalculado = precoHora * 0.5m;
                }
            }

            return valorCalculado;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro em CalcularValorEstadia para o usuário {userId}: {ex.Message}");
            throw;
        }
    }

    private decimal CalcularValorDemaisHoras(Garagens garagem, TimeSpan duracao)
    {
        decimal precoHorasExtra = garagem.Preco_HorasExtra;
        int horasCheias = (int)Math.Ceiling((duracao.TotalMinutes - 30) / 60);

        return horasCheias * precoHorasExtra;
    }
}
