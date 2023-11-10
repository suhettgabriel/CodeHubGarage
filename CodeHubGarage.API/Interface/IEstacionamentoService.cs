namespace CodeHubGarage.API.Interface

{
    public interface IEstacionamentoService
    {
        bool VerificarSeUsuarioEhMensalista(string userId);
        string VerificaNomeUsuario(string userId);
        decimal CalcularValorEstadia(string userId, bool isMensalista, DateTime entrada, DateTime? saida);
    }
}