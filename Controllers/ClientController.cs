using FacturationApp.Data;
using FacturationApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FacturationApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ClientController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LISTE DES CLIENTS
        public async Task<IActionResult> Index()
        {
            var clients = await _context.Clients.ToListAsync();
            return View(clients);
        }

        // AJOUTER UN CLIENT (GET)
        public IActionResult Create()
        {
            return View();
        }

        // AJOUTER UN CLIENT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // MODIFIER UN CLIENT (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null) return NotFound();
            return View(client);
        }

        // MODIFIER UN CLIENT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Client client)
        {
            if (id != client.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // SUPPRIMER UN CLIENT
        public async Task<IActionResult> Delete(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null) return NotFound();

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
