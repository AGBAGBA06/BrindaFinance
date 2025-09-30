using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FacturationApp.Models;
using FacturationApp.Models.ViewModels;
using FacturationApp.Data;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class FactureController : Controller
{
    private readonly ApplicationDbContext _context;

    public FactureController(ApplicationDbContext context) => _context = context;

    // Liste des factures
    public IActionResult Index()
    {
        var factures = _context.Factures
            .Include(f => f.Client)
            .Include(f => f.Lignes)
            .ThenInclude(l => l.Produit)
            .ToList();
        return View(factures);
    }



    // GET Create
    public IActionResult Create()
    {
        ViewBag.Clients = _context.Clients.ToList();
        ViewBag.Produits = _context.Produits.ToList();
        return View(new FactureCreateVM());
    }

    // POST Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(FactureCreateVM vm)
    {
        if (ModelState.IsValid)
        {
            var facture = new Facture
            {
                ClientId = vm.ClientId,
                Date = DateTime.UtcNow,
                Numero = "FAC-" + DateTime.UtcNow.ToString("yyyyMMddHHmmss")
            };

            foreach (var ligneVM in vm.Lignes)
            {
                var produit = await _context.Produits.FindAsync(ligneVM.ProduitId);
                if (produit != null)
                {
                    facture.Lignes.Add(new LigneFacture
                    {
                        ProduitId = produit.Id,
                        Quantite = ligneVM.Quantite
                    });
                }
            }

            _context.Factures.Add(facture);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Clients = _context.Clients.ToList();
        ViewBag.Produits = _context.Produits.ToList();
        return View(vm);
    }

    // GET Edit facture
    public async Task<IActionResult> Edit(int id)
    {
        var facture = await _context.Factures
            .Include(f => f.Lignes)
            .FirstOrDefaultAsync(f => f.Id == id);
        if (facture == null) return NotFound();

        ViewBag.Clients = _context.Clients.ToList();
        ViewBag.Produits = _context.Produits.ToList();

        var vm = new FactureCreateVM
        {
            ClientId = facture.ClientId,
            Lignes = facture.Lignes.Select(l => new LigneFactureVM
            {
                ProduitId = l.ProduitId,
                Quantite = l.Quantite
            }).ToList()
        };
        return View(vm);
    }

    // POST Edit facture
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, FactureCreateVM vm)
    {
        var facture = await _context.Factures
            .Include(f => f.Lignes)
            .FirstOrDefaultAsync(f => f.Id == id);
        if (facture == null) return NotFound();

        if (ModelState.IsValid)
        {
            facture.ClientId = vm.ClientId;
            facture.Lignes.Clear();

            foreach (var ligneVM in vm.Lignes)
            {
                var produit = await _context.Produits.FindAsync(ligneVM.ProduitId);
                if (produit != null)
                {
                    facture.Lignes.Add(new LigneFacture
                    {
                        ProduitId = produit.Id,
                        Quantite = ligneVM.Quantite
                    });
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Clients = _context.Clients.ToList();
        ViewBag.Produits = _context.Produits.ToList();
        return View(vm);
    }

    // GET Delete facture
    public async Task<IActionResult> Delete(int id)
    {
        var facture = await _context.Factures
            .Include(f => f.Client)
            .FirstOrDefaultAsync(f => f.Id == id);
        if (facture == null) return NotFound();
        return View(facture);
    }

    // POST Delete facture
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var facture = await _context.Factures
            .Include(f => f.Lignes)
            .FirstOrDefaultAsync(f => f.Id == id);
        if (facture != null)
        {
            _context.LignesFacture.RemoveRange(facture.Lignes);
            _context.Factures.Remove(facture);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> GenerateFacture(int id)
    {
        var facture = await _context.Factures
            .Include(f => f.Client)
            .Include(f => f.Lignes)
            //  .Include(f => f.LignesFacture)
            .ThenInclude(l => l.Produit)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (facture == null) return NotFound();

        return View(facture);
    }
}
