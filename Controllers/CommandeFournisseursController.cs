using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParapharmacieApp.Models;

namespace ParapharmacieApp.Controllers
{
    public class CommandeFournisseursController : Controller
    {
        private readonly ParapharmacieDbContext _context;

        public CommandeFournisseursController(ParapharmacieDbContext context)
        {
            _context = context;
        }

        // GET: CommandeFournisseurs
        public async Task<IActionResult> Index()
        {
            var parapharmacieDbContext = _context.CommandeFournisseurs.Include(c => c.IdFournisseurNavigation);
            return View(await parapharmacieDbContext.ToListAsync());
        }

        // GET: CommandeFournisseurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commandeFournisseur = await _context.CommandeFournisseurs
                .Include(c => c.IdFournisseurNavigation)
                .FirstOrDefaultAsync(m => m.IdCommande == id);
            if (commandeFournisseur == null)
            {
                return NotFound();
            }

            return View(commandeFournisseur);
        }

        // GET: CommandeFournisseurs/Create
        public IActionResult Create()
        {
            ViewData["IdFournisseur"] = new SelectList(_context.Fournisseurs, "IdFournisseur", "IdFournisseur");
            return View();
        }

        // POST: CommandeFournisseurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCommande,DateCommande,Statut,IdFournisseur")] CommandeFournisseur commandeFournisseur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commandeFournisseur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdFournisseur"] = new SelectList(_context.Fournisseurs, "IdFournisseur", "IdFournisseur", commandeFournisseur.IdFournisseur);
            return View(commandeFournisseur);
        }

        // GET: CommandeFournisseurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commandeFournisseur = await _context.CommandeFournisseurs.FindAsync(id);
            if (commandeFournisseur == null)
            {
                return NotFound();
            }
            ViewData["IdFournisseur"] = new SelectList(_context.Fournisseurs, "IdFournisseur", "IdFournisseur", commandeFournisseur.IdFournisseur);
            return View(commandeFournisseur);
        }

        // POST: CommandeFournisseurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCommande,DateCommande,Statut,IdFournisseur")] CommandeFournisseur commandeFournisseur)
        {
            if (id != commandeFournisseur.IdCommande)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commandeFournisseur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommandeFournisseurExists(commandeFournisseur.IdCommande))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdFournisseur"] = new SelectList(_context.Fournisseurs, "IdFournisseur", "IdFournisseur", commandeFournisseur.IdFournisseur);
            return View(commandeFournisseur);
        }

        // GET: CommandeFournisseurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commandeFournisseur = await _context.CommandeFournisseurs
                .Include(c => c.IdFournisseurNavigation)
                .FirstOrDefaultAsync(m => m.IdCommande == id);
            if (commandeFournisseur == null)
            {
                return NotFound();
            }

            return View(commandeFournisseur);
        }

        // POST: CommandeFournisseurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commandeFournisseur = await _context.CommandeFournisseurs.FindAsync(id);
            if (commandeFournisseur != null)
            {
                _context.CommandeFournisseurs.Remove(commandeFournisseur);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommandeFournisseurExists(int id)
        {
            return _context.CommandeFournisseurs.Any(e => e.IdCommande == id);
        }
    }
}
