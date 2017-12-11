using PracenjeTuristickogSmjestaja.Models;
using PracenjeTuristickogSmjestaja.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace PracenjeTuristickogSmjestaja.Controllers
{
    [Authorize]
    public class JedinicaIznajmljivanjaController : Controller
    {
        private readonly ApplicationDbContext _context_jedinica_iznajmljivanja;

        public JedinicaIznajmljivanjaController()
        {
            _context_jedinica_iznajmljivanja = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var jedinicaIznajmljivanja = _context_jedinica_iznajmljivanja.JedinicaIznajmljivanja
                .Include(a => a.Objekt)
                .ToList();
            return View(jedinicaIznajmljivanja);
        }
        public ActionResult Create()
        {
            var viewModel = new JedinicaIznajmljivanjaViewModel()
            {
                Objekti = _context_jedinica_iznajmljivanja.Objekt.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(JedinicaIznajmljivanjaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Objekti = _context_jedinica_iznajmljivanja.Objekt.ToList();
                return View("Create", viewModel);
            }

            var JedinicaIznajmljivanja = new JedinicaIznajmljivanja()
            {
                broj_jedinice_iznajmljivanja = viewModel.jedinica_iznajmljivanja,
                MaksimalanBrojLjudi = viewModel.MaksimalanBrojLjudi,
                DnevnaCijena = viewModel.DnevnaCijena,
                RazinaLuksuznosti = viewModel.RazinaLuksuznosti,
                sifra_objekta = viewModel.sifra_objekta

            };

            _context_jedinica_iznajmljivanja.JedinicaIznajmljivanja.Add(JedinicaIznajmljivanja);
            _context_jedinica_iznajmljivanja.SaveChanges();

            TempData["Success"] = "Uspješno spremljeno!";
            return RedirectToAction("Create", "JedinicaIznajmljivanja");
        }
    }
}