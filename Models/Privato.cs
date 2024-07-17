namespace Esercitazione.Models
{
    public class Privato
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string CodiceFiscale { get; set; }
        public Cliente Cliente { get; set; }
    }
}
