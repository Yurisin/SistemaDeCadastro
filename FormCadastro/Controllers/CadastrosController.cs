using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FormCadastro.Models;

namespace FormCadastro.Controllers
{
    public class CadastrosController : Controller
    {
        private readonly Context _context;

        public CadastrosController(Context context)
        {
            _context = context;
        }

        // GET: Cadastros
        public async Task<IActionResult> Index()
        {
              return _context.DbCadastro != null ? 
                          View(await _context.DbCadastro.ToListAsync()) :
                          Problem("Entity set 'Context.DbCadastro'  is null.");
        }

        // GET: Cadastros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DbCadastro == null)
            {
                return NotFound();
            }

            var dbCadastro = await _context.DbCadastro
                .FirstOrDefaultAsync(m => m.id == id);
            if (dbCadastro == null)
            {
                return NotFound();
            }

            return View(dbCadastro);
        }

        // GET: Cadastros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cadastros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nome,email,datanascimento,sexo,rua,numero,cep,cidade,estado,grauurgencia,mensagem,numerocaracteres")] DbCadastro dbCadastro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dbCadastro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dbCadastro);
        }

        // GET: Cadastros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DbCadastro == null)
            {
                return NotFound();
            }

            var dbCadastro = await _context.DbCadastro.FindAsync(id);
            if (dbCadastro == null)
            {
                return NotFound();
            }
            return View(dbCadastro);
        }

        // POST: Cadastros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nome,email,datanascimento,sexo,rua,numero,cep,cidade,estado,grauurgencia,mensagem,numerocaracteres")] DbCadastro dbCadastro)
        {
            if (id != dbCadastro.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dbCadastro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DbCadastroExists(dbCadastro.id))
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
            return View(dbCadastro);
        }

        // GET: Cadastros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DbCadastro == null)
            {
                return NotFound();
            }

            var dbCadastro = await _context.DbCadastro
                .FirstOrDefaultAsync(m => m.id == id);
            if (dbCadastro == null)
            {
                return NotFound();
            }

            return View(dbCadastro);
        }

        // POST: Cadastros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DbCadastro == null)
            {
                return Problem("Entity set 'Context.DbCadastro'  is null.");
            }
            var dbCadastro = await _context.DbCadastro.FindAsync(id);
            if (dbCadastro != null)
            {
                _context.DbCadastro.Remove(dbCadastro);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DbCadastroExists(int id)
        {
          return (_context.DbCadastro?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
