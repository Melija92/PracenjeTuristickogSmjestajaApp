using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracenjeTuristickogSmjestaja.Models
{
    [Table("dbo.Iznajmljivanje")]
    public class Iznajmljivanje
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int sifra_iznajmljivanja { get; set; }
        public virtual Turist Turist { get; set; }
        public virtual JedinicaIznajmljivanja JedinicaIznajmljivanja { get; set; }
        [Key]
        [Column(Order = 2)]
        public int sifra_turista { get; set; }
        [Key]
        [Column(Order = 3)]
        public int sifra_jedinice_iznajmljivanja { get; set; }
        public DateTime PocetakIznajmljivanja { get; set; }
        public DateTime ZavrsetakIznajmljivanja { get; set; }
    }
}