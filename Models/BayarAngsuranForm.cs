using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IMSTest.Models;

public class BayarAngsuranForm
{
    [Required]
    public int Id { get; set; }
    public string KontrakNo { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;

    [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
    public decimal AngsuranPerBulan { get; set;}

    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime TanggalJatuhTempo { get; set;}
    public int AngsuranKe { get; set;}

    [Required]
    [DisplayName("Jumlah Bayar")]
    [DisplayFormat(DataFormatString = "{0:00.###}", ApplyFormatInEditMode = true)]
    public decimal JumlahBayar { get; set; }
}