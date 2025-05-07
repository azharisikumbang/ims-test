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

namespace IMSTest.Controllers
{
    public class KontrakController : Controller
    {
        private readonly AppDbContext _context;

        public KontrakController(AppDbContext context)
        {
            _context = context;
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
            var latestKontrak = await _context.Kontrak.OrderByDescending(k => k.Id).FirstOrDefaultAsync();
            var kontrakNo = latestKontrak == null ? "0" : Regex.Match(latestKontrak.KontrakNo, @"\d+").Value;
            kontrakNo = "ARG" + (int.Parse(kontrakNo) + 1).ToString("D5");

            Kontrak kontrak = new() {
                KontrakNo = kontrakNo,
                ClientName = form.ClientName,
                OTR = form.OTR,
            };

            decimal DownPayment = Math.Round(form.OTR * form.DownPayment / 100, 0);
            decimal PokokUtang = kontrak.OTR - DownPayment;
            decimal Bunga;

            if (form.Tenor <= 12)
            {
                Bunga = (decimal) 0.12 * PokokUtang;
            }
            else if (form.Tenor > 12 && form.Tenor <= 24)
            {
                Bunga = (decimal) 0.14 * PokokUtang;
            }
            else
            {
                Bunga = (decimal) 0.165 * PokokUtang;
            }

            decimal AngsuranPerBulan = Math.Round((PokokUtang + Bunga) / form.Tenor);

            for (int i = 1; i <= form.Tenor; i++)
            {
                DateTime TanggalJatuhTempo = new DateTime(form.TanggalMulaiKontrak.Year, form.TanggalMulaiKontrak.Month, form.TanggalJatuhTempoBulanan).AddMonths(i - 1);

                Angsuran angsuran = new Angsuran() {
                    AngsuranKe = i,
                    AngsuranPerBulan = AngsuranPerBulan,
                    TanggalJatuhTempo = TanggalJatuhTempo
                };

                kontrak.ListAngsuran.Add(angsuran);   
            }

            _context.Add(kontrak);
            await _context.SaveChangesAsync();

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
