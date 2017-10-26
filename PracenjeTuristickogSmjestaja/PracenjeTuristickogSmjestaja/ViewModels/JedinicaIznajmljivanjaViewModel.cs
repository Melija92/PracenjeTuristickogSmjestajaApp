using PracenjeTuristickogSmjestaja.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PracenjeTuristickogSmjestaja.ViewModels
{
    public class JedinicaIznajmljivanjaViewModel
    {
        [Display(Name = "Šifra jedinice iznajmljivanja")]
        [Required(ErrorMessage = "Šifra jedinice iznajmljivanja je obvezna za unos")]
        public string jedinica_iznajmljivanja { get; set; }

        [Display(Name = "Maksimalan kapacitet ljudi")]
        public int MaksimalanBrojLjudi { get; set; }

        [Display(Name = "Dnevna cijena (HRK)")]
        [Required(ErrorMessage = "Dnevna cijena je obvezna za unos")]
        [Range(0, 10000, ErrorMessage ="Dnevna cijena ne može biti veća od 10,000kn")]
        public decimal DnevnaCijena { get; set; }

        [Display(Name = "Razina luksuznosti")]
        public string RazinaLuksuznosti { get; set; }

        [Display(Name = "Nadređeni objekt")]
        [Required(ErrorMessage = "Objekt je obvezan za unos")]
        public int sifra_objekta { get; set; }
        public IEnumerable<Objekt> Objekti { get; set; }
    }

    public enum Luksuznost
    {
        Niska,
        Srednja,
        Visoka
    }

}