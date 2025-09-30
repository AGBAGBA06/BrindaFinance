namespace FacturationApp.Models
{
    public class Facture
    {
       public int Id { get; set; }
    public string Numero { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.UtcNow;

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;
    public ICollection<LigneFacture> Lignes { get; set; } = new List<LigneFacture>();
    
        // TVA et remise
        public decimal Tva { get; set; } = 0.2m;     // 20% par défaut
        public decimal Remise { get; set; } = 0m;    // 0% par défaut

        // Calcul du total HT et TTC
        public decimal TotalHT => Lignes.Sum(l => l.PrixTotal);
        public decimal TotalTTC => (TotalHT * (1 + Tva)) * (1 - Remise);
    }
}
