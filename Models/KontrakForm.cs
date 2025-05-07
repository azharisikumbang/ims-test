using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace IMSTest.Models;

public class KontrakForm 
{
    [Required]
    [DisplayName("Nama Pembeli")]
    public string ClientName { get; set; } = string.Empty;

    [Required]
    [DisplayName("Merk / Brand")]
    public string Brand { get; set; } = string.Empty;
    
    [Required]
    [DisplayName("Nominal OTR")]
    public decimal OTR { get; set; } = decimal.Zero;
    
    [Required]
    [DisplayName("Tenor (Bulan)")]
    public int Tenor { get; set; } = 0;
    
    [Required]
    [DisplayName("Down Payment (persen)")]
    public int DownPayment { get; set; } = 0;

    [Required]
    [DisplayName("Tanggal Mulai Kontrak")]
    public DateOnly TanggalMulaiKontrak { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    [Required]
    [DisplayName("Tanggal Jatuh Tempo Bulanan")]
    [Range(1, 31)]
    public int TanggalJatuhTempoBulanan { get; set; } = 1;

}