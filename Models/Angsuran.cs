using System.ComponentModel.DataAnnotations;

namespace IMSTest.Models;

public class Angsuran 
{   
    public int Id { get; set;}

    [Required]
    public int AngsuranKe { get; set;}

    [Required]
    public decimal AngsuranPerBulan { get; set;}

    public DateTime TanggalJatuhTempo { get; set;}

    [Required]
    public Kontrak Kontrak { get; set; } = new();
}