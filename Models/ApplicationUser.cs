using Microsoft.AspNetCore.Identity;

namespace FacturationApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Tu peux ajouter d’autres champs si besoin
     public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;

    }
}
