using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracenjeTuristickogSmjestaja.Models
{
    [Table("dbo.Drzava")]
    public class Drzava
    {
        [Key]
        public int sifra_drzave { get; set; }
        [StringLength(40)]
        [Display(Name = "Drzava")]
        [Required]
        public string NazivDrzave { get; set; }
        public ICollection<Mjesto> Mjesta { get; set; }
    }
}