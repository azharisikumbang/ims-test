using System.ComponentModel.DataAnnotations;

namespace IMSTest.Models;

public class Kontrak 
{
    public int Id { get; set;}

    public string KontrakNo => $"ARG{Id.ToString().PadLeft(5, '0')}";

    [Required]
    public string ClientName { get; set;} = string.Empty;

    [Required]
    [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = false)]
    public decimal OTR { get; set; }

    public List<Angsuran> ListAngsuran { get; set; } = new();

}
