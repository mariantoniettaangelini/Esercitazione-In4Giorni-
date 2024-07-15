namespace Esercitazione.Models
{
    public class Privato
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CodiceFiscale { get; set; }
        public Cliente Cliente { get; set; }
    }
}
