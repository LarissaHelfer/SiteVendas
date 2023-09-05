using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LanchesMac.Context;
using LanchesMac.Models;
using Microsoft.AspNetCore.Authorization;
using ReflectionIT.Mvc.Paging;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminTortasController : Controller
    {
        private readonly AppDbContext _context;

        public AdminTortasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminTortas
        //public async Task<IActionResult> Index()
        //{
        //    var appDbContext = _context.Tortas.Include(t => t.Categoria);
        //    return View(await appDbContext.ToListAsync());
        //}

        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Nome")
        {
            var resultado = _context.Tortas.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                resultado = resultado.Where(p => p.Nome.Contains(filter));
            }

            var model = await PagingList.CreateAsync(resultado, 5, pageindex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(model);
        }

        // GET: Admin/AdminTortas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tortas == null)
            {
                return NotFound();
            }

            var torta = await _context.Tortas
                .Include(t => t.Categoria)
                .FirstOrDefaultAsync(m => m.DoceId == id);
            if (torta == null)
            {
                return NotFound();
            }

            return View(torta);
        }

        // GET: Admin/AdminTortas/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome");
            return View();
        }

        // POST: Admin/AdminTortas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoceId,Nome,DescricaoCurta,DescricaoDetalhada,Preco,ImagemUrl,ImagemThumbnailUrl,IsLanchePreferido,EmEstoque,CategoriaId")] Torta torta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(torta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", torta.CategoriaId);
            return View(torta);
        }

        // GET: Admin/AdminTortas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tortas == null)
            {
                return NotFound();
            }

            var torta = await _context.Tortas.FindAsync(id);
            if (torta == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", torta.CategoriaId);
            return View(torta);
        }

        // POST: Admin/AdminTortas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoceId,Nome,DescricaoCurta,DescricaoDetalhada,Preco,ImagemUrl,ImagemThumbnailUrl,IsLanchePreferido,EmEstoque,CategoriaId")] Torta torta)
        {
            if (id != torta.DoceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(torta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TortaExists(torta.DoceId))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", torta.CategoriaId);
            return View(torta);
        }

        // GET: Admin/AdminTortas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tortas == null)
            {
                return NotFound();
            }

            var torta = await _context.Tortas
                .Include(t => t.Categoria)
                .FirstOrDefaultAsync(m => m.DoceId == id);
            if (torta == null)
            {
                return NotFound();
            }

            return View(torta);
        }

        // POST: Admin/AdminTortas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tortas == null)
            {
                return Problem("Entity set 'AppDbContext.Tortas'  is null.");
            }
            var torta = await _context.Tortas.FindAsync(id);
            if (torta != null)
            {
                _context.Tortas.Remove(torta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TortaExists(int id)
        {
          return _context.Tortas.Any(e => e.DoceId == id);
        }
    }
}
