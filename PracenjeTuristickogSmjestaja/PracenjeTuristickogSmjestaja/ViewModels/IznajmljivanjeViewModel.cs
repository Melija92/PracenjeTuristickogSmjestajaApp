using PracenjeTuristickogSmjestaja.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PracenjeTuristickogSmjestaja.ViewModels
{
    public class IznajmljivanjeViewModel
    {
        public int sifra_iznajmljivanja { get; set; }
        [Display(Name = "OIB turista")]
        [Required(ErrorMessage = "OIB turista je obvezan za unos")]
        public int sifra_turista { get; set; }
        public IEnumerable<Turist> Turisti { get; set; }
        [Display(Name = "Jedinica iznajmljivanja")]
        [Required(ErrorMessage = "Šifra jedinice iznajmljivanja je obvezno za unos")]
        public int sifra_jedinice_iznajmljivanja { get; set; }
        public IEnumerable<JedinicaIznajmljivanja> JediniceIznajmljivanja { get; set; }
        [Display(Name = "Početak iznajmljivanja")]
        [Required(ErrorMessage = "Datum početka iznajmljivanja je obvezno za unos")]
        public DateTime PocetakIznajmljivanja { get; set; }
        [Display(Name = "Završetak iznajmljivanja")]
        [Required(ErrorMessage = "Datum završetka iznajmljivanja je obvezno za unos")]
        public DateTime ZavrsetakIznajmljivanja { get; set; }
        public DateTime PocetakIzanjljivanjaMetoda()
        {
            return DateTime.Parse(string.Format("{0}", PocetakIznajmljivanja));
        }
        public DateTime ZavrsetakIznajmljivanjaMetoda()
        {
            return DateTime.Parse(string.Format("{0}", ZavrsetakIznajmljivanja));
        }
    }
}