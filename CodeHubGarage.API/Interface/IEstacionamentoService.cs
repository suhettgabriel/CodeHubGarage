public interface IEstacionamentoService
{
    bool VerificarSeUsuarioEhMensalista(string userId);
    string VerificaNomeUsuario(string userId);
    DateTime? GetDataEntrada(string userId);
    void DadosInfoUsuario(string userId, out string carroPlaca, out string carroMarca, out string carroModelo);
    decimal CalcularValorEstadia(string userId, bool isMensalista, DateTime entrada, DateTime? saida);
}