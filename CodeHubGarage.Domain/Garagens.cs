namespace CodeHubGarage.Domain
{
    public class Garagens
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Preco_1aHora { get; set; }
        public decimal Preco_HorasExtra { get; set; }
        public decimal Preco_Mensalista { get; set; }
        //public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    }
}