using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracenjeTuristickogSmjestaja.Models
{
    [Table("dbo.Tip_iznajmljivaca")]
    public class TipIznajmljivaca
    {
        [Key]
        public int sifra_tipa_iznajmljivaca { get; set; }
        [Required, StringLength(40)]
        public string NazivTipaIznajmljivaca { get; set; }
        public ICollection<Objekt> Objekti { get; set; }
    }
}