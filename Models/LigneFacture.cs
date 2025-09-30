namespace FacturationApp.Models
{ 
 public class LigneFacture
{
    public int Id { get; set; }
    public int FactureId { get; set; }
    public Facture Facture { get; set; } = null!;
    public int ProduitId { get; set; }
    public Produit Produit { get; set; } = null!;
    public int Quantite { get; set; }
     public decimal PrixTotal => Quantite * Produit.PrixUnitaire;// lecture seule
}

}
