namespace FacturationApp.Models
{
    public class Produit
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public decimal PrixUnitaire { get; set; }
        public int Stock { get; set; }

        // Relation : un produit peut apparaître dans plusieurs lignes de facture
        public ICollection<LigneFacture> LignesFacture { get; set; } = new List<LigneFacture>();
    }
}
