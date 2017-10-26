using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracenjeTuristickogSmjestaja.Models
{
    [Table("dbo.Turist")]
    public class Turist
    {
        [Key]
        public int sifra_turista { get; set; }
        [Index(IsUnique = true)]
        [Required, StringLength(11)]
        public string OIB_turista { get; set; }
        [Required, StringLength(40)]
        public string Ime { get; set; }
        [Required, StringLength(40)]
        public string Prezime { get; set; }
        public DateTime DatumRodenja { get; set; }
        [StringLength(10)]
        public string Spol { get; set; }
        public int sifra_mjesta { get; set; }
        public virtual Mjesto Mjesto { get; set; }
        public int sifra_drzavljanstva { get; set; }
        public virtual Drzavljanstvo Drzavljanstvo { get; set; }
        public ICollection<Iznajmljivanje> Iznajmljivanja { get; set; }
    }
}