using PracenjeTuristickogSmjestaja.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PracenjeTuristickogSmjestaja.ViewModels
{
    public class ObjektViewModel
    {
        public int sifra_objekta { get; set; }
        [Display(Name = "Šifra objekta iznajmljivanja")]
        [Required(ErrorMessage = "Šifra objekta je obvezno za unos")]
        public string objekt_iznajmljivanja { get; set; }
        [Display(Name = "Naziv")]
        public string naziv { get; set; }

        [Required(ErrorMessage = "Adresa je obvezna za unos")]
        public string Adresa { get; set; }

        [Display(Name = "Poštanski broj")]
        public string PostanskiBroj { get; set; }

        [Display(Name = "Tip objekta")]
        [Required(ErrorMessage = "Tip objekta je obvezan za unos")]
        public int sifra_tipa_objekta { get; set; }

        public IEnumerable<TipObjekta> TipoviObjekta { get; set; }

        [Display(Name = "Tip iznajmljivača")]
        [Required(ErrorMessage = "Tip iznajmljivanja je obvezan za unos")]
        public int sifra_tipa_iznajmljivaca { get; set; }

        public IEnumerable<TipIznajmljivaca> TipoviIznajmljivaca { get; set; }

        [Display(Name = "Mjesto")]
        [Required(ErrorMessage = "Mjesto je obvezno za unos")]
        public int sifra_mjesta { get; set; }

        public IEnumerable<Mjesto> Mjesta { get; set; }

    }
}