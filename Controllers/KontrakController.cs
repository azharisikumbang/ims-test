using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IMSTest.Data;
using IMSTest.Models;
using System.Text.RegularExpressions;
using IMSTest.Services;

namespace IMSTest.Controllers
{
    public class KontrakController : Controller
    {
        private readonly AppDbContext _context;
        private readonly KontrakService _kontrakService;

        public KontrakController(AppDbContext context)
        {
            _context = context;
            _kontrakService = new KontrakService(context);
        }

        // GET: Kontrak
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kontrak.ToListAsync());
        }

        // GET: Kontrak/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var kontrak = await _context.Kontrak.Include(k => k.ListAngsuran).FirstOrDefaultAsync(m => m.Id == id);
            if (kontrak == null)
            {
                return NotFound();
            }

            return View(kontrak);
        }

        // GET: Kontrak/Create
        public IActionResult Create()
        {
            return View(new KontrakForm());
        }

        // POST: Kontrak/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KontrakForm form)
        {
            if (!ModelState.IsValid) return View(form);

            // get kontrak no
            var kontrakNo = await _kontrakService.GenerateNewKontrakNo();

            Kontrak kontrak = new() {
                KontrakNo = kontrakNo,
                ClientName = form.ClientName,
                OTR = form.OTR,
            };

            await _kontrakService.CreateKontrakAsync(kontrak, form);

            return RedirectToAction(nameof(Index));

        }

        // GET: Kontrak/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var kontrak = await _context.Kontrak.FindAsync(id);
            if (kontrak == null)
            {
                return NotFound();
            }
            return View(kontrak);
        }

        // POST: Kontrak/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Kontrak kontrak)
        {
            if (id != kontrak.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kontrak);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KontrakExists(kontrak.Id))
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
            return View(kontrak);
        }

        // GET: Kontrak/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var kontrak = await _context.Kontrak.FindAsync(id);
            if (kontrak == null)
            {
                return NotFound();
            }

            return View(kontrak);
        }

        // POST: Kontrak/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kontrak = await _context.Kontrak.FindAsync(id);
            if (kontrak != null)
            {
                _context.Kontrak.Remove(kontrak);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KontrakExists(int id)
        {
            return _context.Kontrak.Any(e => e.Id == id);
        }
    }
}
