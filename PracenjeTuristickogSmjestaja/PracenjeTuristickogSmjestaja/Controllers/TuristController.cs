using PracenjeTuristickogSmjestaja.Models;
using PracenjeTuristickogSmjestaja.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace TelefonskiImenik.Controllers
{
    [Authorize]
    public class TuristController : Controller
    {

        private readonly ApplicationDbContext _context_turist;
        public TuristController()
        {
            _context_turist = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var iznajmljivanje = _context_turist.Turist
                .Include(a => a.Drzavljanstvo)
                .Include(a => a.Mjesto)
                .ToList();
            return View(iznajmljivanje);
        }
        public ActionResult Create()
        {
            var viewModel = new TuristViewModel
            {
                Mjesta = _context_turist.Mjesto.ToList(),
                Drzavljanstva = _context_turist.Drzavljanstvo.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(TuristViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Drzavljanstva = _context_turist.Drzavljanstvo.ToList();
                viewModel.Mjesta = _context_turist.Mjesto.ToList();
                return View("Create", viewModel);
            }


            var turist = new Turist
            {
                OIB_turista = viewModel.OIB_turista,
                Ime = viewModel.Ime,
                Prezime = viewModel.Prezime,
                DatumRodenja = viewModel.NapraviDatum(),
                Spol = viewModel.Spol,
                sifra_mjesta = viewModel.sifra_mjesta,
                sifra_drzavljanstva = viewModel.sifra_drzavljanstva
            };

            _context_turist.Turist.Add(turist);
            _context_turist.SaveChanges();

            TempData["Success"] = "Uspješno spremnjeno!";
            return RedirectToAction("Create", "Turist");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Turist turist = _context_turist.Turist.Find(id);
            if (turist == null)
                return HttpNotFound();
            var turistViewModel = new TuristViewModel()
            {
                sifra_turista = turist.sifra_turista,
                OIB_turista = turist.OIB_turista,
                Ime = turist.Ime,
                Prezime = turist.Prezime,
                DatumRodenja = turist.DatumRodenja,
                Spol = turist.Spol
            };
            ViewBag.sifra_drzavljanstva = new SelectList(_context_turist.Drzavljanstvo, "sifra_drzavljanstva", "NazivDrzavljanstva", turist.sifra_drzavljanstva);
            ViewBag.sifra_mjesta = new SelectList(_context_turist.Mjesto, "sifra_mjesta", "NazivMjesta", turist.sifra_mjesta);
            return View(turistViewModel);
        }

        [HttpPost]
        public ActionResult Edit(TuristViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.sifra_drzavljanstva = new SelectList(_context_turist.Drzavljanstvo, "sifra_drzavljanstva", "NazivDrzavljanstva", viewModel.sifra_drzavljanstva);
                ViewBag.sifra_mjesta = new SelectList(_context_turist.Mjesto, "sifra_mjesta", "NazivMjesta", viewModel.sifra_mjesta);
                return View("Edit", viewModel);
            }


            var turist = new Turist
            {
                sifra_turista = viewModel.sifra_turista,
                OIB_turista = viewModel.OIB_turista,
                Ime = viewModel.Ime,
                Prezime = viewModel.Prezime,
                DatumRodenja = viewModel.NapraviDatum(),
                Spol = viewModel.Spol,
                sifra_mjesta = viewModel.sifra_mjesta,
                sifra_drzavljanstva = viewModel.sifra_drzavljanstva
            };
            _context_turist.Entry(turist).State = EntityState.Modified;
            _context_turist.SaveChanges();
            return RedirectToAction("Index", "Turist");
        }
        
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var turist = _context_turist.Turist.Find(id);
            _context_turist.Turist.Remove(turist);
            _context_turist.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}