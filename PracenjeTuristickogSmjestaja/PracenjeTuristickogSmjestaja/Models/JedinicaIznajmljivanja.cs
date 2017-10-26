using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracenjeTuristickogSmjestaja.Models
{
    [Table("dbo.Jedinica_Iznajmljivanja")]
    public class JedinicaIznajmljivanja
    {
        [Key]
        public int sifra_jedinice_iznajmljivanja { get; set; }
        [Index(IsUnique = true)]
        [Required, StringLength(100)]
        public string broj_jedinice_iznajmljivanja { get; set; }
        public int MaksimalanBrojLjudi { get; set; }
        public decimal DnevnaCijena { get; set; }
        [StringLength(20)]
        public string RazinaLuksuznosti { get; set; }
        public int sifra_objekta { get; set; }
        public virtual Objekt Objekt { get; set; }
        public ICollection<Iznajmljivanje> Iznajmljivanja { get; set; }
    }
}