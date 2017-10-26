using PracenjeTuristickogSmjestaja.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PracenjeTuristickogSmjestaja.ViewModels
{
    public class TuristViewModel
    {
        public int sifra_turista { get; set; }
        [Display(Name = "OIB turista")]
        [Required(ErrorMessage = "OIB je obvezan za unos")]
        [StringLength(11, MinimumLength = 11, ErrorMessage ="OIB mora sadržavati točno 11 znakova")]
        public string OIB_turista { get; set; }
        [Required(ErrorMessage = "Ime je obvezno za unos")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Prezime je obvezno za unos")]
        public string Prezime { get; set; }
        [Display(Name = "Datum rođenja")]
        [Required(ErrorMessage = "Datum rođenja je obvezno za unos")]
        public DateTime DatumRodenja { get; set; }
        public string Spol { get; set; }
        [Display(Name = "Mjesto prebivališta")]
        [Required(ErrorMessage = "Mjesto prebivališta je obvezno za znos")]
        public int sifra_mjesta { get; set; }
        public string NazivMjesta { get; set; }
        public IEnumerable<Mjesto> Mjesta { get; set; }
        [Required(ErrorMessage = "Državljanstvo je obvezno za unos")]
        [Display(Name = "Državljanstvo")]
        public int sifra_drzavljanstva { get; set; }
        public IEnumerable<Drzavljanstvo> Drzavljanstva { get; set; }
        public string NazivDrzavljanstva { get; set; }
        public DateTime NapraviDatum()
        {

            return DateTime.Parse(string.Format("{0}", DatumRodenja));
        }
    }
}