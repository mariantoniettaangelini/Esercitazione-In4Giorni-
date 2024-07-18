namespace Esercitazione.Models
{
    public class Spedizioni
    {
        public int id_spedizione { get; set; }
        public int ClienteId { get; set; }
        public DateTime DataSpedizione { get; set; }
        public decimal Peso { get; set; }
        public string CittaDestinataria { get; set; }
        public string Indirizzo { get; set; }
        public string Destinatario { get; set; }
        public decimal Costo { get; set; }
        public DateTime DataConsegna { get; set; }
        public Cliente Cliente { get; set; }
    }
}
