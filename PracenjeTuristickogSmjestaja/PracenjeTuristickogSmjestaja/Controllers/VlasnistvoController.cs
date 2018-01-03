using PracenjeTuristickogSmjestaja.Models;
using PracenjeTuristickogSmjestaja.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace PracenjeTuristickogSmjestaja.Controllers
{
    [Authorize]
    public class VlasnistvoController : Controller
    {
        private readonly ApplicationDbContext _context_vlasnistvo;

        public VlasnistvoController()
        {
            _context_vlasnistvo = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var vlasnistvo = _context_vlasnistvo.Vlasnistvo
                .Include(a => a.Vlasnik)
                .Include(a => a.Objekt).ToList();
            return View(vlasnistvo);
        }
        public ActionResult Create()
        {
            var viewModel = new VlasnistvoViewModel()
            {
                Vlasnici = _context_vlasnistvo.Vlasnik.ToList(),
                Objekti = _context_vlasnistvo.Objekt.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(VlasnistvoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Vlasnici = _context_vlasnistvo.Vlasnik.ToList();
                viewModel.Objekti = _context_vlasnistvo.Objekt.ToList();
                return View("Create", viewModel);
            }

            var vlasnistvo = new Vlasnistvo()
            {
                sifra_vlasnika = viewModel.sifra_vlasnika,
                sifra_objekta = viewModel.sifra_objekta,
                UdioUVlasnistvu = viewModel.UdioUVlasnistvu
            };

            _context_vlasnistvo.Vlasnistvo.Add(vlasnistvo);
            _context_vlasnistvo.SaveChanges();

            TempData["Success"] = "Uspješno spremnjeno!";
            return RedirectToAction("Create", "Vlasnistvo");
        }
        public ActionResult Edit(int? sifra_vlasnika, int? sifra_objekta)
        {
            if (sifra_vlasnika == null || sifra_objekta == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var vlasnistvo = _context_vlasnistvo.Vlasnistvo.Find(sifra_vlasnika, sifra_objekta);

            if (vlasnistvo == null)
                return HttpNotFound();

            var vlasnistvoViewModel = new VlasnistvoViewModel()
            {
                UdioUVlasnistvu = vlasnistvo.UdioUVlasnistvu
            };

            ViewBag.sifra_vlasnika = new SelectList(_context_vlasnistvo.Vlasnik, "sifra_vlasnika", "OIB_vlasnika", vlasnistvo.sifra_vlasnika);
            ViewBag.sifra_objekta = new SelectList(_context_vlasnistvo.Objekt, "sifra_objekta", "broj_objekta_iznajmljivanja", vlasnistvo.sifra_objekta);

            return View(vlasnistvoViewModel);
        }
        [HttpPost]
        public ActionResult Edit(VlasnistvoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.sifra_vlasnika = new SelectList(_context_vlasnistvo.Vlasnik, "sifra_vlasnika", "OIB_vlasnika", viewModel.sifra_vlasnika);
                ViewBag.sifra_objekta = new SelectList(_context_vlasnistvo.Objekt, "sifra_objekta", "broj_objekta_iznajmljivanja", viewModel.sifra_objekta);

                return View("Edit", viewModel);
            }

            var vlasnistvo = new Vlasnistvo()
            {
                sifra_vlasnika = viewModel.sifra_vlasnika,
                sifra_objekta = viewModel.sifra_objekta,
                UdioUVlasnistvu = viewModel.UdioUVlasnistvu
            };
            _context_vlasnistvo.Entry(vlasnistvo).State = EntityState.Modified;
            _context_vlasnistvo.SaveChanges();

            return RedirectToAction("Index", "Vlasnistvo");
        }

        [HttpPost]
        public ActionResult Delete(int? sifra_vlasnika, int? sifra_objekta)
        {
            var vlasnistvo = _context_vlasnistvo.Vlasnistvo.Find(sifra_vlasnika, sifra_objekta);
            _context_vlasnistvo.Vlasnistvo.Remove(vlasnistvo);
            _context_vlasnistvo.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
