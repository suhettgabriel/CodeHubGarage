using CodeHubGarage.Domain;
using System.Security.Cryptography;

public interface IEstacionamentoService
{
    bool VerificarSeUsuarioEhMensalista(string userId);
    string VerificaNomeUsuario(string userId);
    DateTime? GetDataEntrada(string userId);
    DadosUsuario ObterDadosUsuario(string userId);
    void DadosInfoUsuario(string userId, out string carroPlaca, out string carroMarca, out string carroModelo, out string formaPagamento);
    decimal CalcularValorEstadia(string userId, bool isMensalista, DateTime entrada, DateTime? saida);
    TimeSpan BuscarQuantidadeTempo(string userId, DateTime dataHoraSaida);
    void RegistrarEntradaEstacionamento(string userId, DateTime entrada);
    public void RegistrarSaidaEstacionamento(string userId, DateTime dataHoraSaida, string garagemCodigo, string carroPlaca, string carroMarca, string carroModelo, DateTime dataHoraEntrada, string formaPagamento);
    void SalvarPassagem(string userId, DateTime entrada, Garagens garagem, string carroPlaca, string carroMarca, string carroModelo, string formaPagamento, TimeSpan quantidadeTempo, decimal valorEstadia);
}
