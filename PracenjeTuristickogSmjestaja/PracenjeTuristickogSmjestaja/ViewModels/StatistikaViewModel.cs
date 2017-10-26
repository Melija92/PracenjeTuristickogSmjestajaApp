using System.Collections.Generic;

namespace PracenjeTuristickogSmjestaja.ViewModels
{
    public class StatistikaViewModel
    {
        public IEnumerable<MjestoViewModel> Mjesta { get; set; }
        public IEnumerable<DrzavljanstvoViewModel> Drzavljanstva { get; set; }
        public IEnumerable<TipOBjekataViewModel> TipObjekata { get; set; }
        public IEnumerable<TipIznajmljivavaViewModel> TipIznajmljivaca { get; set; }
        public IEnumerable<PopunjenostViewModel> Popunjenost { get; set; }
        public IEnumerable<TrenutnaPopunjenostViewModel> TrenutnaPopunjenost { get; set; }
        public IEnumerable<SpolViewModel> Spolovi { get; set; }
    }
}