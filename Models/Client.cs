namespace FacturationApp.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Adresse { get; set; } = string.Empty;

        // Relation : un client peut avoir plusieurs factures
        public ICollection<Facture> Factures { get; set; } = new List<Facture>();
    }
}
