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
    public class LigneVentesController : Controller
    {
        private readonly ParapharmacieDbContext _context;

        public LigneVentesController(ParapharmacieDbContext context)
        {
            _context = context;
        }

        // GET: LigneVentes
        public async Task<IActionResult> Index()
        {
            var parapharmacieDbContext = _context.LigneVentes.Include(l => l.IdProduitNavigation).Include(l => l.IdVenteNavigation);
            return View(await parapharmacieDbContext.ToListAsync());
        }

        // GET: LigneVentes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ligneVente = await _context.LigneVentes
                .Include(l => l.IdProduitNavigation)
                .Include(l => l.IdVenteNavigation)
                .FirstOrDefaultAsync(m => m.IdLigneVente == id);
            if (ligneVente == null)
            {
                return NotFound();
            }

            return View(ligneVente);
        }

        // GET: LigneVentes/Create
        public IActionResult Create()
        {
            ViewData["IdProduit"] = new SelectList(_context.Produits, "IdProduit", "IdProduit");
            ViewData["IdVente"] = new SelectList(_context.Ventes, "IdVente", "IdVente");
            return View();
        }

        // POST: LigneVentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLigneVente,IdVente,IdProduit,Quantite,PrixUnitaire")] LigneVente ligneVente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ligneVente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProduit"] = new SelectList(_context.Produits, "IdProduit", "IdProduit", ligneVente.IdProduit);
            ViewData["IdVente"] = new SelectList(_context.Ventes, "IdVente", "IdVente", ligneVente.IdVente);
            return View(ligneVente);
        }

        // GET: LigneVentes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ligneVente = await _context.LigneVentes.FindAsync(id);
            if (ligneVente == null)
            {
                return NotFound();
            }
            ViewData["IdProduit"] = new SelectList(_context.Produits, "IdProduit", "IdProduit", ligneVente.IdProduit);
            ViewData["IdVente"] = new SelectList(_context.Ventes, "IdVente", "IdVente", ligneVente.IdVente);
            return View(ligneVente);
        }

        // POST: LigneVentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLigneVente,IdVente,IdProduit,Quantite,PrixUnitaire")] LigneVente ligneVente)
        {
            if (id != ligneVente.IdLigneVente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ligneVente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LigneVenteExists(ligneVente.IdLigneVente))
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
            ViewData["IdProduit"] = new SelectList(_context.Produits, "IdProduit", "IdProduit", ligneVente.IdProduit);
            ViewData["IdVente"] = new SelectList(_context.Ventes, "IdVente", "IdVente", ligneVente.IdVente);
            return View(ligneVente);
        }

        // GET: LigneVentes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ligneVente = await _context.LigneVentes
                .Include(l => l.IdProduitNavigation)
                .Include(l => l.IdVenteNavigation)
                .FirstOrDefaultAsync(m => m.IdLigneVente == id);
            if (ligneVente == null)
            {
                return NotFound();
            }

            return View(ligneVente);
        }

        // POST: LigneVentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ligneVente = await _context.LigneVentes.FindAsync(id);
            if (ligneVente != null)
            {
                _context.LigneVentes.Remove(ligneVente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LigneVenteExists(int id)
        {
            return _context.LigneVentes.Any(e => e.IdLigneVente == id);
        }
    }
}
