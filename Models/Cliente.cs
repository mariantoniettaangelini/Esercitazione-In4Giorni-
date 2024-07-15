namespace Esercitazione.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public int? IdPrivato { get; set; }
        public int? IdAzienda { get; set; }
        public Privato Privato { get; set; }
        public Azienda Azienda { get; set; }
    }
}
