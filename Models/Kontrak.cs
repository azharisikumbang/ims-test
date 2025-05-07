using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IMSTest.Models;

public class Kontrak 
{
    public int Id { get; set;}

    [Required]
    [DisplayName("Kontrak No")]
    public string KontrakNo { get; set;} = string.Empty;

    [Required]
    [DisplayName("Client Name")]
    public string ClientName { get; set;} = string.Empty;

    [Required] 
    [DisplayName("Nominal OTR")]
    [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
    public decimal OTR { get; set; }

    public List<Angsuran> ListAngsuran { get; set; } = new();

}
