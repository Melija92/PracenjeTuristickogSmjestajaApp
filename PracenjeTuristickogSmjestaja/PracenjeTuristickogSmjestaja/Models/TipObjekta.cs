using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracenjeTuristickogSmjestaja.Models
{
    [Table("dbo.Tip_Objekta")]
    public class TipObjekta
    {
        [Key]
        public int sifra_tipa_objekta { get; set; }
        [Required, StringLength(40)]
        public string NazivTipaObjekta { get; set; }
        public ICollection<Objekt> Objekti { get; set; }
    }
}