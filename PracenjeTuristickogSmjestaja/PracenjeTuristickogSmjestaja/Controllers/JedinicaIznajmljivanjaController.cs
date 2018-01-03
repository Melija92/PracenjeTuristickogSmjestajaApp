using System;
using PracenjeTuristickogSmjestaja.Models;
using PracenjeTuristickogSmjestaja.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

            TempData["Success"] = "Uspješno spremnjeno!";
            return RedirectToAction("Create", "JedinicaIznajmljivanja");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            JedinicaIznajmljivanja jedinicaIznajmljivanja = _context_jedinica_iznajmljivanja.JedinicaIznajmljivanja.Find(id);
            if (jedinicaIznajmljivanja == null)
                return HttpNotFound();
            var jedinicaIznajmljivanjaViewModel = new JedinicaIznajmljivanjaViewModel()
            {
                sifra_jedinice_iznajmljivanja = jedinicaIznajmljivanja.sifra_jedinice_iznajmljivanja,
                jedinica_iznajmljivanja = jedinicaIznajmljivanja.broj_jedinice_iznajmljivanja,
                MaksimalanBrojLjudi = jedinicaIznajmljivanja.MaksimalanBrojLjudi,
                DnevnaCijena = jedinicaIznajmljivanja.DnevnaCijena
            };
            //ViewBag.luksuznost = new SelectList(Enum.GetValues(typeof(Luksuznost))), "Izaberi razinu luksuznosti");
            ViewBag.sifra_objekta = new SelectList(_context_jedinica_iznajmljivanja.Objekt, "sifra_objekta", "broj_objekta_iznajmljivanja", jedinicaIznajmljivanja.sifra_objekta);
            return View(jedinicaIznajmljivanjaViewModel);
        }

        [HttpPost]
        public ActionResult Edit(JedinicaIznajmljivanjaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.objekti = new SelectList(_context_jedinica_iznajmljivanja.Objekt, "sifra_objekta", "broj_objekta_iznajmljivanja", viewModel.sifra_objekta);
                return View("Edit", viewModel);
            }

            var jedinicaIznajmljivanja = new JedinicaIznajmljivanja()
            {
                sifra_jedinice_iznajmljivanja = viewModel.sifra_jedinice_iznajmljivanja,
                broj_jedinice_iznajmljivanja = viewModel.jedinica_iznajmljivanja,
                DnevnaCijena = viewModel.DnevnaCijena,
                MaksimalanBrojLjudi = viewModel.MaksimalanBrojLjudi,
                RazinaLuksuznosti = viewModel.RazinaLuksuznosti,
                sifra_objekta = viewModel.sifra_objekta
            };
            _context_jedinica_iznajmljivanja.Entry(jedinicaIznajmljivanja).State = EntityState.Modified;
            _context_jedinica_iznajmljivanja.SaveChanges();

            return RedirectToAction("Index", "JedinicaIznajmljivanja");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var jedinicaIznajmljivanja = _context_jedinica_iznajmljivanja.JedinicaIznajmljivanja.Find(id);
            _context_jedinica_iznajmljivanja.JedinicaIznajmljivanja.Remove(jedinicaIznajmljivanja);
            _context_jedinica_iznajmljivanja.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}