using FacturationApp.Data;
using FacturationApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FacturationApp.Controllers
{
    public class LigneFactureController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LigneFactureController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LISTE DES LIGNES
        public async Task<IActionResult> Index()
        {
            var lignes = await _context.LignesFacture
                                       .Include(l => l.Facture)
                                       .Include(l => l.Produit)
                                       .ToListAsync();
            return View(lignes);
        }

        // AJOUTER UNE LIGNE (GET)
        public IActionResult Create()
        {
            ViewBag.Factures = _context.Factures.ToList();
            ViewBag.Produits = _context.Produits.ToList();
            return View();
        }

        // AJOUTER UNE LIGNE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LigneFacture ligne)
        {
            if (ModelState.IsValid)
            {
                // Calculer le prix total
                var produit = await _context.Produits.FindAsync(ligne.ProduitId);
                ligne.PrixTotal = ligne.Quantite * produit!.PrixUnitaire;

                _context.LignesFacture.Add(ligne);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Factures = _context.Factures.ToList();
            ViewBag.Produits = _context.Produits.ToList();
            return View(ligne);
        }

        // MODIFIER UNE LIGNE
        public async Task<IActionResult> Edit(int id)
        {
            var ligne = await _context.LignesFacture.FindAsync(id);
            if (ligne == null) return NotFound();
            ViewBag.Factures = _context.Factures.ToList();
            ViewBag.Produits = _context.Produits.ToList();
            return View(ligne);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LigneFacture ligne)
        {
            if (id != ligne.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var produit = await _context.Produits.FindAsync(ligne.ProduitId);
                ligne.PrixTotal = ligne.Quantite * produit!.PrixUnitaire;

                _context.Update(ligne);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Factures = _context.Factures.ToList();
            ViewBag.Produits = _context.Produits.ToList();
            return View(ligne);
        }

        // SUPPRIMER UNE LIGNE
        public async Task<IActionResult> Delete(int id)
        {
            var ligne = await _context.LignesFacture.FindAsync(id);
            if (ligne == null) return NotFound();

            _context.LignesFacture.Remove(ligne);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
