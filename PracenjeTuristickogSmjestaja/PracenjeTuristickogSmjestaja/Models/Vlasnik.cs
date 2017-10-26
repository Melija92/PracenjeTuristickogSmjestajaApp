using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracenjeTuristickogSmjestaja.Models
{
    [Table("dbo.Vlasnik")]
    public class Vlasnik
    {
        [Key]
        public int sifra_vlasnika { get; set; }
        [Index(IsUnique = true)]
        [Required, StringLength(11)]
        public string OIB_vlasnika { get; set; }
        [Required, StringLength(40)]
        public string Ime { get; set; }
        [Required, StringLength(40)]
        public string Prezime { get; set; }
        public int sifra_drzavljanstva { get; set; }
        public virtual Drzavljanstvo Drzavljanstvo { get; set; }
        public ICollection<Vlasnistvo> Vlasnistva { get; set; }
    }
}