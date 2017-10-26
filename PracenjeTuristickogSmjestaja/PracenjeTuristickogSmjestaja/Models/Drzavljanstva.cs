using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracenjeTuristickogSmjestaja.Models
{
    [Table("dbo.Drzavljanstvo")]
    public class Drzavljanstvo
    {
        [Key]
        public int sifra_drzavljanstva { get; set; }
        [StringLength(40)]
        public string NazivDrzavljanstva { get; set; }
        public ICollection<Vlasnik> Vlasnici { get; set; }
        public ICollection<Turist> Turisti { get; set; }

    }
}