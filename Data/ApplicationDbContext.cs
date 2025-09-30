using Microsoft.EntityFrameworkCore;
using FacturationApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace FacturationApp.Data
{
   
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Facture> Factures { get; set; }
        public DbSet<LigneFacture> LignesFacture { get; set; }
    }
    
}
