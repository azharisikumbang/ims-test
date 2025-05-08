using System.Text.RegularExpressions;
using IMSTest.Data;
using IMSTest.Models;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace IMSTest.Services;

public class KontrakService
{
    const string KONTRAK_NO_PREFIX = "ARG";

    private readonly AppDbContext _context;

    public KontrakService(AppDbContext _context) 
    {
        this._context = _context;
    }

    public async Task<string> GenerateNewKontrakNo()
    {
        var latest = await _context.Kontrak.OrderByDescending(k => k.Id).FirstOrDefaultAsync();
        var kontrakNo = latest == null ? "0" : Regex.Match(latest.KontrakNo, @"\d+").Value;

        return KONTRAK_NO_PREFIX + (int.Parse(kontrakNo) + 1).ToString("D5");
    }

    public decimal CalculateDownPaymentFromPercent(decimal OTR, int PercentDownPayment) 
    {
        return Math.Round(OTR * PercentDownPayment / 100, 0);
    }

    public decimal CalculateInterest(decimal PokokUtang, int Tenor)
    {
        return GetInterestRate(Tenor) * PokokUtang;
    }

    public decimal GetInterestRate(int Tenor)
    {
        if (Tenor <= 12) return 0.12m;
        if (Tenor > 12 && Tenor <= 24) return 0.14m;
        
        return 0.165m;
    }

    public decimal CalculateAngsuranPerBulan(decimal PokokUtang, decimal Bunga, int Tenor) => Math.Round((PokokUtang + Bunga) / Tenor);

    public async Task<int> CreateKontrakAsync(Kontrak kontrak, KontrakForm form)
    {
        decimal DownPayment = CalculateDownPaymentFromPercent(kontrak.OTR, form.DownPayment);
        decimal PokokUtang = kontrak.OTR - DownPayment;
        decimal Bunga = CalculateInterest(PokokUtang, form.Tenor);
        decimal AngsuranPerBulan = CalculateAngsuranPerBulan(PokokUtang, Bunga, form.Tenor);

        for (int i = 1; i <= form.Tenor; i++)
        {
            DateTime TanggalJatuhTempo = new DateTime(
                form.TanggalMulaiKontrak.Year, 
                form.TanggalMulaiKontrak.Month, 
                form.TanggalJatuhTempoBulanan
            ).AddMonths(i - 1);

            Angsuran angsuran = new() {
                AngsuranKe = i,
                AngsuranPerBulan = AngsuranPerBulan,
                TanggalJatuhTempo = TanggalJatuhTempo
            };

            kontrak.ListAngsuran.Add(angsuran);   
        }

        _context.Add(kontrak);

        await _context.SaveChangesAsync();

        return kontrak.Id;
    } 
}