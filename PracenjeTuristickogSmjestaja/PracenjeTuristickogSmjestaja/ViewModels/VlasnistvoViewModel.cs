using PracenjeTuristickogSmjestaja.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PracenjeTuristickogSmjestaja.ViewModels
{
    public class VlasnistvoViewModel
    {
        [Display(Name = "Vlasnika")]
        [Required(ErrorMessage = "OIB je obvezan za unos")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "OIB mora sadržavati točno 11 znakova")]
        public int OIB_v { get; set; }

        public IEnumerable<Vlasnik> Vlasnici { get; set; }

        [Display(Name = "Objekt")]
        [Required(ErrorMessage = "Šifra objekta je obvezna za unos")]
        public int sifra_objekta { get; set; }
        [Display(Name = "% udjela u vlasništvu")]
        public int UdioUVlasnistvu { get; set; }

        public IEnumerable<Objekt> Objekti { get; set; }

    }
}