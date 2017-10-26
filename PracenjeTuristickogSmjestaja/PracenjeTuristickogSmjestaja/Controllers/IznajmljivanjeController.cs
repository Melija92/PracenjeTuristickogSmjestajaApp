﻿using PracenjeTuristickogSmjestaja.Models;
using PracenjeTuristickogSmjestaja.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace PracenjeTuristickogSmjestaja.Controllers
{
    public class IznajmljivanjeController : Controller
    {
        private readonly ApplicationDbContext _context_iznajmljivanje;

        public IznajmljivanjeController()
        {
            _context_iznajmljivanje = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var iznajmljivanje = _context_iznajmljivanje.Iznajmljivanje
                .Include(a => a.JedinicaIznajmljivanja)
                .Include(a => a.Turist)
                .ToList();
            return View(iznajmljivanje);
        }
        public ActionResult Aktualni()
        {
            var iznajmljivanje = _context_iznajmljivanje.Iznajmljivanje
                .Include(a => a.JedinicaIznajmljivanja)
                .Include(a => a.Turist)
                .Where(a => a.PocetakIznajmljivanja < DateTime.Now && a.ZavrsetakIznajmljivanja > DateTime.Now)
                .ToList();
            return View(iznajmljivanje);
        }
        public ActionResult StatistikaObjekti()
        {
            StatistikaViewModel viewmodel = new StatistikaViewModel();

            var tipObjekata = _context_iznajmljivanje.Iznajmljivanje
                .Where(a => a.PocetakIznajmljivanja < DateTime.Now && a.ZavrsetakIznajmljivanja > DateTime.Now)
                .GroupBy(a => a.JedinicaIznajmljivanja.Objekt.TipObjekta.NazivTipaObjekta)
                .Select(a => new TipOBjekataViewModel
                {
                    Naziv = a.Key,
                    Broj = a.Count()
                })
                .OrderByDescending(a => a.Broj)
                .ToList();

            var tipIznajmljivaca = _context_iznajmljivanje.Iznajmljivanje
                .Where(a => a.PocetakIznajmljivanja < DateTime.Now && a.ZavrsetakIznajmljivanja > DateTime.Now)
                .GroupBy(a => a.JedinicaIznajmljivanja.Objekt.TipIznajmljivaca.NazivTipaIznajmljivaca)
                .Select(a => new TipIznajmljivavaViewModel
                {
                    Naziv = a.Key,
                    Broj = a.Count()
                })
                .OrderByDescending(a => a.Broj)
                .ToList();

            var popunjenost = _context_iznajmljivanje.JedinicaIznajmljivanja
                .GroupBy(a => a.Objekt.broj_objekta_iznajmljivanja)
                .Select(a => new PopunjenostViewModel
                {
                    sifra_objekta = a.Key,
                    MaxBroj = a.Sum(b => b.MaksimalanBrojLjudi),
                    ukupanBroj = a.Count()
                }
                );
            var trenutnaPopunjenost = _context_iznajmljivanje.Iznajmljivanje
                .Where(a => a.PocetakIznajmljivanja < DateTime.Now && a.ZavrsetakIznajmljivanja > DateTime.Now)
                .GroupBy(a => a.JedinicaIznajmljivanja.Objekt.broj_objekta_iznajmljivanja)
                .Select(a => new TrenutnaPopunjenostViewModel
                {
                    naziv = a.Key,
                    trenutnaPopunjenost = a.Count(),
                })
                .OrderByDescending(a => a.trenutnaPopunjenost)
                .ToList();

            viewmodel.TipObjekata = tipObjekata;
            viewmodel.TipIznajmljivaca = tipIznajmljivaca;
            viewmodel.Popunjenost = popunjenost;
            viewmodel.TrenutnaPopunjenost = trenutnaPopunjenost;
            return View(viewmodel);
        }
        public ActionResult StatistikaTuristi()
        {
            StatistikaViewModel viewmodel = new StatistikaViewModel();
            var mjesta = _context_iznajmljivanje.Iznajmljivanje
              .Where(a => a.PocetakIznajmljivanja < DateTime.Now && a.ZavrsetakIznajmljivanja > DateTime.Now)
              .GroupBy(a => a.JedinicaIznajmljivanja.Objekt.Mjesto.NazivMjesta)
              .Select(a => new MjestoViewModel
              {
                  Naziv = a.Key,
                  Broj = a.Count()
              })
              .OrderByDescending(a => a.Broj)
              .ToList();

            var drzavljanstva = _context_iznajmljivanje.Iznajmljivanje
                .Where(a => a.PocetakIznajmljivanja < DateTime.Now && a.ZavrsetakIznajmljivanja > DateTime.Now)
                .GroupBy(a => a.Turist.Drzavljanstvo.NazivDrzavljanstva)
                .Select(a => new DrzavljanstvoViewModel
                {
                    Naziv = a.Key,
                    Broj = a.Count()
                })
                .OrderByDescending(a => a.Broj)
                .ToList();
            var spolovi = _context_iznajmljivanje.Iznajmljivanje
                .Where(a => a.PocetakIznajmljivanja < DateTime.Now && a.ZavrsetakIznajmljivanja > DateTime.Now)
                .GroupBy(a => a.Turist.Spol)
                .Select(a => new SpolViewModel
                {
                    nazivSpola = a.Key,
                    Broj = a.Count()
                })
                .OrderByDescending(a => a.Broj)
                .ToList();

            viewmodel.Spolovi = spolovi;
            viewmodel.Mjesta = mjesta;
            viewmodel.Drzavljanstva = drzavljanstva;
            return View(viewmodel);
        }
        public ActionResult Create()
        {
            var viewModel = new IznajmljivanjeViewModel()
            {
                Turisti = _context_iznajmljivanje.Turist.ToList(),
                JediniceIznajmljivanja = _context_iznajmljivanje.JedinicaIznajmljivanja.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(IznajmljivanjeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Turisti = _context_iznajmljivanje.Turist.ToList();
                viewModel.JediniceIznajmljivanja = _context_iznajmljivanje.JedinicaIznajmljivanja.ToList();
                return View("Create", viewModel);
            }

            var iznajmljivanje = new Iznajmljivanje()
            {
                sifra_turista = viewModel.sifra_turista,
                sifra_jedinice_iznajmljivanja = viewModel.sifra_jedinice_iznajmljivanja,
                PocetakIznajmljivanja = viewModel.PocetakIzanjljivanjaMetoda(),
                ZavrsetakIznajmljivanja = viewModel.ZavrsetakIznajmljivanjaMetoda()
            };

            _context_iznajmljivanje.Iznajmljivanje.Add(iznajmljivanje);
            _context_iznajmljivanje.SaveChanges();

            TempData["Success"] = "Uspješno spremljeno!";
            return RedirectToAction("Create", "Iznajmljivanje");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var iznajmljivanje = _context_iznajmljivanje.Iznajmljivanje.Find(id);
            if (iznajmljivanje == null)
                return HttpNotFound();
            var iznajmljivanjeViewModel = new Iznajmljivanje()
            {
                sifra_iznajmljivanja = iznajmljivanje.sifra_iznajmljivanja,
                sifra_turista = iznajmljivanje.sifra_turista,
                sifra_jedinice_iznajmljivanja = iznajmljivanje.sifra_jedinice_iznajmljivanja,
                PocetakIznajmljivanja = iznajmljivanje.PocetakIznajmljivanja,
                ZavrsetakIznajmljivanja = iznajmljivanje.ZavrsetakIznajmljivanja
            };
            ViewBag.sifra_drzavljanstva = new SelectList(_context_iznajmljivanje.Turist, "sifra_turista", "OIB_turista", iznajmljivanjeViewModel.sifra_turista);
            ViewBag.sifra_mjesta = new SelectList(_context_iznajmljivanje.JedinicaIznajmljivanja, "sifra_jedinice_iznajmljivanja", "broj_jedinice_iznajmljivanja", iznajmljivanjeViewModel.sifra_jedinice_iznajmljivanja);

            return View(iznajmljivanjeViewModel);
        }
    }
}
