using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracenjeTuristickogSmjestaja.Models
{
    [Table("dbo.Vlasnistvo")]
    public class Vlasnistvo
    {
        public virtual Vlasnik Vlasnik { get; set; }
        public virtual Objekt Objekt { get; set; }
        [Key]
        [Column(Order = 1)]
        public int sifra_vlasnika { get; set; }
        [Key]
        [Column(Order = 2)]
        public int sifra_objekta { get; set; }
        [Required]
        public int UdioUVlasnistvu { get; set; }
    }
}