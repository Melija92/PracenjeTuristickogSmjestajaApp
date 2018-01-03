using PracenjeTuristickogSmjestaja.Models;
using PracenjeTuristickogSmjestaja.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
namespace PracenjeTuristickogSmjestaja.Controllers
{
    public class ObjektController : Controller
    {
        private readonly ApplicationDbContext _context_objekt;

        public ObjektController()
        {
            _context_objekt = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var jedinicaIznajmljivanja = _context_objekt.Objekt
                .Include(a => a.Mjesto)
                .Include(a => a.TipIznajmljivaca)
                .Include(a => a.TipObjekta)
                .ToList();
            return View(jedinicaIznajmljivanja);
        }
        public ActionResult Create()
        {
            var viewModel = new ObjektViewModel()
            {
                TipoviObjekta = _context_objekt.TipObjekta.ToList(),
                TipoviIznajmljivaca = _context_objekt.TipIznajmljivaca.ToList(),
                Mjesta = _context_objekt.Mjesto.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(ObjektViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.TipoviIznajmljivaca = _context_objekt.TipIznajmljivaca.ToList();
                viewModel.TipoviObjekta = _context_objekt.TipObjekta.ToList();
                viewModel.Mjesta = _context_objekt.Mjesto.ToList();
                return View("Create", viewModel);
            }

            var objekt = new Objekt()
            {
                broj_objekta_iznajmljivanja = viewModel.objekt_iznajmljivanja,
                naziv = viewModel.naziv,
                Adresa = viewModel.Adresa,
                PostanskiBroj = viewModel.PostanskiBroj,
                sifra_tipa_objekta = viewModel.sifra_tipa_objekta,
                sifra_tipa_iznajmljivaca = viewModel.sifra_tipa_iznajmljivaca,
                sifra_mjesta = viewModel.sifra_mjesta
            };

            _context_objekt.Objekt.Add(objekt);
            _context_objekt.SaveChanges();

            TempData["Success"] = "Uspješno spremnjeno!";
            return RedirectToAction("Create", "Objekt");
        }
        public ActionResult Details(int? id)
        {
            
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var detalji = _context_objekt.JedinicaIznajmljivanja.Where(a => a.sifra_objekta == id)
                .Select(a => new JedinicaIznajmljivanjaViewModel
                {
                    jedinica_iznajmljivanja = a.broj_jedinice_iznajmljivanja,
                    MaksimalanBrojLjudi = a.MaksimalanBrojLjudi,
                    DnevnaCijena = a.DnevnaCijena,
                    RazinaLuksuznosti = a.RazinaLuksuznosti
                });
            return View(detalji);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var objekt = _context_objekt.Objekt.Find(id);
            if (objekt == null)
                return HttpNotFound();
            var objektViewModel = new ObjektViewModel()
            {
                sifra_objekta = objekt.sifra_objekta,
                objekt_iznajmljivanja = objekt.broj_objekta_iznajmljivanja,
                naziv = objekt.naziv,
                Adresa = objekt.Adresa,
                PostanskiBroj = objekt.PostanskiBroj,
                sifra_mjesta = objekt.sifra_mjesta,
                sifra_tipa_iznajmljivaca = objekt.sifra_tipa_iznajmljivaca,
                sifra_tipa_objekta = objekt.sifra_tipa_objekta
            };
            ViewBag.sifra_mjesta = new SelectList(_context_objekt.Mjesto, "sifra_mjesta", "NazivMjesta", objekt.sifra_mjesta);
            ViewBag.sifra_tipa_iznajmljivaca = new SelectList(_context_objekt.TipIznajmljivaca, "sifra_tipa_iznajmljivaca", "NazivTipaIznajmljivaca", objekt.sifra_tipa_iznajmljivaca);
            ViewBag.sifra_tipa_objekta = new SelectList(_context_objekt.TipObjekta, "sifra_tipa_objekta", "NazivTipaObjekta", objekt.sifra_tipa_objekta);
            return View(objektViewModel);
        }

        [HttpPost]
        public ActionResult Edit(ObjektViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.sifra_mjesta = new SelectList(_context_objekt.Mjesto, "sifra_mjesta", "NazivMjesta", viewModel.sifra_objekta);
                ViewBag.sifra_tipa_iznajmljivaca = new SelectList(_context_objekt.TipIznajmljivaca, "sifra_tipa_iznajmljivaca", "NazivTipaIznajmljivaca", viewModel.sifra_tipa_iznajmljivaca);
                ViewBag.sifra_tipa_objekta = new SelectList(_context_objekt.TipObjekta, "sifra_tipa_objekta", "NazivTipaObjekta", viewModel.sifra_tipa_objekta);
                return View("Edit", viewModel);
            }
            
            var objekt = new Objekt
            {
                sifra_objekta = viewModel.sifra_objekta,
                broj_objekta_iznajmljivanja = viewModel.objekt_iznajmljivanja,
                naziv = viewModel.naziv,
                Adresa = viewModel.Adresa,
                PostanskiBroj = viewModel.PostanskiBroj,
                sifra_mjesta = viewModel.sifra_mjesta,
                sifra_tipa_iznajmljivaca = viewModel.sifra_tipa_iznajmljivaca,
                sifra_tipa_objekta = viewModel.sifra_tipa_objekta
            };
            _context_objekt.Entry(objekt).State = EntityState.Modified;
            _context_objekt.SaveChanges();
            return RedirectToAction("Index", "Objekt");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var objekt = _context_objekt.Objekt.Find(id);
            _context_objekt.Objekt.Remove(objekt);
            _context_objekt.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}