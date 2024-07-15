namespace Esercitazione.Models
{
    public class Azienda
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string PartitaIVA { get; set; }
        public Cliente Cliente { get; set; }
    }
}
