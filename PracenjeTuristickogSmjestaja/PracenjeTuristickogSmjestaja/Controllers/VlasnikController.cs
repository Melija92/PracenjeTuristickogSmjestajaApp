using PracenjeTuristickogSmjestaja.Models;
using PracenjeTuristickogSmjestaja.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace TelefonskiImenik.Controllers
{
    public class VlasnikController : Controller
    {

        private readonly ApplicationDbContext _context_vlasnik;
        public VlasnikController()
        {
            _context_vlasnik = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var vlasnici = _context_vlasnik.Vlasnik.Include(a => a.Drzavljanstvo).ToList();
            return View(vlasnici);
        }

        public ActionResult Create()
        {
            var viewModel = new VlasnikViewModel
            {
                Drzavljanstva = _context_vlasnik.Drzavljanstvo.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(VlasnikViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Drzavljanstva = _context_vlasnik.Drzavljanstvo.ToList();
                return View("Create", viewModel);
            }

            var vlasnik = new Vlasnik
            {
                OIB_vlasnika = viewModel.OIB_vlasnika,
                Ime = viewModel.Ime,
                Prezime = viewModel.Prezime,
                sifra_drzavljanstva = viewModel.Drzavljanstvo
            };

            _context_vlasnik.Vlasnik.Add(vlasnik);
            _context_vlasnik.SaveChanges();

            TempData["Success"] = "Uspješno spremljeno!";
            return RedirectToAction("Create", "Vlasnik");
        }
    }

}
