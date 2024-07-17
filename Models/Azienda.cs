namespace Esercitazione.Models
{
    public class Azienda
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string PartitaIVA { get; set; }
        public Cliente Cliente { get; set; }
    }
}
