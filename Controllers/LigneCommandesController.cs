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
    public class LigneCommandesController : Controller
    {
        private readonly ParapharmacieDbContext _context;

        public LigneCommandesController(ParapharmacieDbContext context)
        {
            _context = context;
        }

        // GET: LigneCommandes
        public async Task<IActionResult> Index()
        {
            var parapharmacieDbContext = _context.LigneCommandes.Include(l => l.IdCommandeNavigation).Include(l => l.IdProduitNavigation);
            return View(await parapharmacieDbContext.ToListAsync());
        }

        // GET: LigneCommandes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ligneCommande = await _context.LigneCommandes
                .Include(l => l.IdCommandeNavigation)
                .Include(l => l.IdProduitNavigation)
                .FirstOrDefaultAsync(m => m.IdLigneCommande == id);
            if (ligneCommande == null)
            {
                return NotFound();
            }

            return View(ligneCommande);
        }

        // GET: LigneCommandes/Create
        public IActionResult Create()
        {
            ViewData["IdCommande"] = new SelectList(_context.CommandeFournisseurs, "IdCommande", "IdCommande");
            ViewData["IdProduit"] = new SelectList(_context.Produits, "IdProduit", "IdProduit");
            return View();
        }

        // POST: LigneCommandes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLigneCommande,IdCommande,IdProduit,Quantite")] LigneCommande ligneCommande)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ligneCommande);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCommande"] = new SelectList(_context.CommandeFournisseurs, "IdCommande", "IdCommande", ligneCommande.IdCommande);
            ViewData["IdProduit"] = new SelectList(_context.Produits, "IdProduit", "IdProduit", ligneCommande.IdProduit);
            return View(ligneCommande);
        }

        // GET: LigneCommandes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ligneCommande = await _context.LigneCommandes.FindAsync(id);
            if (ligneCommande == null)
            {
                return NotFound();
            }
            ViewData["IdCommande"] = new SelectList(_context.CommandeFournisseurs, "IdCommande", "IdCommande", ligneCommande.IdCommande);
            ViewData["IdProduit"] = new SelectList(_context.Produits, "IdProduit", "IdProduit", ligneCommande.IdProduit);
            return View(ligneCommande);
        }

        // POST: LigneCommandes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLigneCommande,IdCommande,IdProduit,Quantite")] LigneCommande ligneCommande)
        {
            if (id != ligneCommande.IdLigneCommande)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ligneCommande);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LigneCommandeExists(ligneCommande.IdLigneCommande))
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
            ViewData["IdCommande"] = new SelectList(_context.CommandeFournisseurs, "IdCommande", "IdCommande", ligneCommande.IdCommande);
            ViewData["IdProduit"] = new SelectList(_context.Produits, "IdProduit", "IdProduit", ligneCommande.IdProduit);
            return View(ligneCommande);
        }

        // GET: LigneCommandes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ligneCommande = await _context.LigneCommandes
                .Include(l => l.IdCommandeNavigation)
                .Include(l => l.IdProduitNavigation)
                .FirstOrDefaultAsync(m => m.IdLigneCommande == id);
            if (ligneCommande == null)
            {
                return NotFound();
            }

            return View(ligneCommande);
        }

        // POST: LigneCommandes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ligneCommande = await _context.LigneCommandes.FindAsync(id);
            if (ligneCommande != null)
            {
                _context.LigneCommandes.Remove(ligneCommande);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LigneCommandeExists(int id)
        {
            return _context.LigneCommandes.Any(e => e.IdLigneCommande == id);
        }
    }
}
