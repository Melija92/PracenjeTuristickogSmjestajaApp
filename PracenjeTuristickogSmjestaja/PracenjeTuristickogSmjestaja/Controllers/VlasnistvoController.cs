using PracenjeTuristickogSmjestaja.Models;
using PracenjeTuristickogSmjestaja.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace PracenjeTuristickogSmjestaja.Controllers
{
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
                sifra_vlasnika = viewModel.OIB_v,
                sifra_objekta = viewModel.sifra_objekta,
                UdioUVlasnistvu = viewModel.UdioUVlasnistvu
            };

            _context_vlasnistvo.Vlasnistvo.Add(vlasnistvo);
            _context_vlasnistvo.SaveChanges();

            TempData["Success"] = "Uspješno spremljeno!";
            return RedirectToAction("Create", "Vlasnistvo");
        }
    }
}
