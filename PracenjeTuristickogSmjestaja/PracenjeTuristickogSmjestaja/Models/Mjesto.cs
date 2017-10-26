using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracenjeTuristickogSmjestaja.Models
{
    [Table("dbo.Mjesto")]
    public class Mjesto
    {
        [Key]
        public int sifra_mjesta { get; set; }
        [StringLength(40)]
        [Required]
        public string NazivMjesta { get; set; }
        public int sifra_drzave { get; set; }
        public virtual Drzava Drzava { get; set; }
        public ICollection<Turist> Turisti { get; set; }
        public ICollection<Objekt> Objekti { get; set; }
    }
}