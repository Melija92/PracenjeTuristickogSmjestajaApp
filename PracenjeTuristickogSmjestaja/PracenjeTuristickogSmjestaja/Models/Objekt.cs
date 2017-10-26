using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracenjeTuristickogSmjestaja.Models
{
    [Table("dbo.Objekt")]
    public class Objekt
    {
        [Key]
        public int sifra_objekta { get; set; }
        [Index(IsUnique = true)]
        [Required, StringLength(100)]
        public string broj_objekta_iznajmljivanja { get; set; }
        [StringLength(100)]
        public string naziv { get; set; }
        [StringLength(40)]
        public string Adresa { get; set; }
        [StringLength(40)]
        public string PostanskiBroj { get; set; }
        public int sifra_tipa_objekta { get; set; }
        public virtual TipObjekta TipObjekta { get; set; }
        public int sifra_tipa_iznajmljivaca { get; set; }
        public virtual TipIznajmljivaca TipIznajmljivaca { get; set; }
        public int sifra_mjesta { get; set; }
        public virtual Mjesto Mjesto { get; set; }
        public ICollection<JedinicaIznajmljivanja> JediniceIznajmljivanja { get; set; }
        public ICollection<Vlasnistvo> Vlasnistva { get; set; }
    }
}