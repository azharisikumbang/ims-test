using IMSTest.Data;
using IMSTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IMSTest.Controllers
{
    public class AngsuranController : Controller
    {
        private readonly AppDbContext _context;

        public AngsuranController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Bayar
        public async Task<IActionResult> Bayar(int id)
        {
            var angsuran = await _context.Angsuran.Include(a => a.Kontrak).FirstOrDefaultAsync(a => a.Id == id && !a.Paid);
            if (angsuran == null) return NotFound();

            var form = new BayarAngsuranForm
            {
                Id = angsuran.Id,
                KontrakNo = angsuran.Kontrak.KontrakNo,
                ClientName = angsuran.Kontrak.ClientName,
                AngsuranPerBulan = angsuran.AngsuranPerBulan,
                TanggalJatuhTempo = angsuran.TanggalJatuhTempo,
                AngsuranKe = angsuran.AngsuranKe,
                JumlahBayar = angsuran.AngsuranPerBulan

            };

            return View(form);
        }

        // POST: Bayar
        [HttpPost]
        public async Task<IActionResult> Bayar(BayarAngsuranForm form)
        {
            var angsuran = await _context.Angsuran.Include(a => a.Kontrak).FirstOrDefaultAsync(a => a.Id == form.Id && !a.Paid);
            if (angsuran == null) return NotFound();

            form.KontrakNo = angsuran.Kontrak.KontrakNo;
            form.ClientName = angsuran.Kontrak.ClientName;
            form.AngsuranPerBulan = angsuran.AngsuranPerBulan;
            form.TanggalJatuhTempo = angsuran.TanggalJatuhTempo;
            form.AngsuranKe = angsuran.AngsuranKe;

            if (Decimal.Compare(
                Decimal.Round(form.JumlahBayar, 0),
                Decimal.Round(form.AngsuranPerBulan, 0)
            ) != 0)
            {
                ModelState.AddModelError(nameof(form.JumlahBayar), "Jumlah Bayar tidak sesuai dengan Angsuran Per Bulan.");
            }

            if (!ModelState.IsValid) {
                return View(form);
            }

            // Update angsuran as paid
            angsuran.Paid = true;

            _context.Update(angsuran);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Kontrak", new { id = angsuran.Kontrak.Id });
        }
    }

}