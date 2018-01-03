using PracenjeTuristickogSmjestaja.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PracenjeTuristickogSmjestaja.ViewModels
{
    public class VlasnikViewModel
    {
        public int sifra_vlasnika { get; set; }        
        [Required(ErrorMessage = "OIB je obvezan za unos")]
        [Display(Name = "OIB vlasnika")]
        public string OIB_vlasnika { get; set; }
        [Required(ErrorMessage = "Ime je obvezno za unos")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Prezime je obvezno za unos")]
        public string Prezime { get; set; }
        [Required(ErrorMessage = "Obvezno je odabrati državljanstvo!")]
        public int Drzavljanstvo { get; set; }
        public IEnumerable<Drzavljanstvo> Drzavljanstva { get; set; }
    }
}
