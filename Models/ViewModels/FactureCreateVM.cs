using System.ComponentModel.DataAnnotations;

namespace FacturationApp.Models.ViewModels
{
    public class FactureCreateVM
    {
        public int ClientId { get; set; }
        public decimal Tva { get; set; } = 0.2m;
        public decimal Remise { get; set; } = 0m;
        public List<LigneFactureVM> Lignes { get; set; } = new List<LigneFactureVM>();
    }

    public class LigneFactureVM
    {
        public int ProduitId { get; set; }
        public int Quantite { get; set; } = 1;
    }

        
    public class RegisterViewModel
    {
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}
