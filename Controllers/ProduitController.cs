using FacturationApp.Data;
using FacturationApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FacturationApp.Controllers
{
    public class ProduitController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProduitController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LISTE DES PRODUITS
        public async Task<IActionResult> Index()
        {
            return View(await _context.Produits.ToListAsync());
        }

        // AJOUTER UN PRODUIT (GET)
        public IActionResult Create() => View();

        // AJOUTER UN PRODUIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produit produit)
        {
            if (ModelState.IsValid)
            {
                _context.Produits.Add(produit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produit);
        }

        // MODIFIER UN PRODUIT (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var produit = await _context.Produits.FindAsync(id);
            if (produit == null) return NotFound();
            return View(produit);
        }

        // MODIFIER UN PRODUIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Produit produit)
        {
            if (id != produit.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(produit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produit);
        }

        // SUPPRIMER UN PRODUIT
        public async Task<IActionResult> Delete(int id)
        {
            var produit = await _context.Produits.FindAsync(id);
            if (produit == null) return NotFound();

            _context.Produits.Remove(produit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
