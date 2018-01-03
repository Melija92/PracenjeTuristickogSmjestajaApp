using PracenjeTuristickogSmjestaja.Models;
using PracenjeTuristickogSmjestaja.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace TelefonskiImenik.Controllers
{
    [Authorize]
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

            TempData["Success"] = "Uspješno spremnjeno!";
            return RedirectToAction("Create", "Vlasnik");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Vlasnik vlasnik = _context_vlasnik.Vlasnik.Find(id);
            if (vlasnik == null)
                return HttpNotFound();
            var vlasnikViewModel = new VlasnikViewModel()
            {
                sifra_vlasnika = vlasnik.sifra_vlasnika,
                OIB_vlasnika = vlasnik.OIB_vlasnika,
                Ime = vlasnik.Ime,
                Prezime = vlasnik.Prezime,
            };
            ViewBag.Drzavljanstvo = new SelectList(_context_vlasnik.Drzavljanstvo, "sifra_drzavljanstva", "NazivDrzavljanstva", vlasnik.sifra_drzavljanstva);
            return View(vlasnikViewModel);
        }

        [HttpPost]
        public ActionResult Edit(VlasnikViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.sifra_drzavljanstva = new SelectList(_context_vlasnik.Drzavljanstvo, "sifra_drzavljanstva", "NazivDrzavljanstva", viewModel.Drzavljanstvo);
                return View("Edit", viewModel);
            }

            var vlasnik = new Vlasnik()
            {
                sifra_vlasnika = viewModel.sifra_vlasnika,
                OIB_vlasnika = viewModel.OIB_vlasnika,
                Ime = viewModel.Ime,
                Prezime = viewModel.Prezime,
                sifra_drzavljanstva = viewModel.Drzavljanstvo
            };
            _context_vlasnik.Entry(vlasnik).State = EntityState.Modified;
            _context_vlasnik.SaveChanges();
            return RedirectToAction("Index", "Vlasnik");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var vlasnik = _context_vlasnik.Vlasnik.Find(id);
            _context_vlasnik.Vlasnik.Remove(vlasnik);
            _context_vlasnik.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }

}
