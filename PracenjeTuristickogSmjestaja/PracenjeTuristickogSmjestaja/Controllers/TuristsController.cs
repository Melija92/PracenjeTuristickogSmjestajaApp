using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PracenjeTuristickogSmjestaja.Models;

namespace PracenjeTuristickogSmjestaja.Controllers
{
    public class TuristsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Turists
        public ActionResult Index()
        {
            var turist = db.Turist.Include(t => t.Drzavljanstvo).Include(t => t.Mjesto);
            return View(turist.ToList());
        }

        // GET: Turists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turist turist = db.Turist.Find(id);
            if (turist == null)
            {
                return HttpNotFound();
            }
            return View(turist);
        }

        // GET: Turists/Create
        public ActionResult Create()
        {
            ViewBag.sifra_drzavljanstva = new SelectList(db.Drzavljanstvo, "sifra_drzavljanstva", "NazivDrzavljanstva");
            ViewBag.sifra_mjesta = new SelectList(db.Mjesto, "sifra_mjesta", "NazivMjesta");
            return View();
        }

        // POST: Turists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sifra_turista,OIB_turista,Ime,Prezime,DatumRodenja,Spol,sifra_mjesta,sifra_drzavljanstva")] Turist turist)
        {
            if (ModelState.IsValid)
            {
                db.Turist.Add(turist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.sifra_drzavljanstva = new SelectList(db.Drzavljanstvo, "sifra_drzavljanstva", "NazivDrzavljanstva", turist.sifra_drzavljanstva);
            ViewBag.sifra_mjesta = new SelectList(db.Mjesto, "sifra_mjesta", "NazivMjesta", turist.sifra_mjesta);
            return View(turist);
        }

        // GET: Turists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turist turist = db.Turist.Find(id);
            if (turist == null)
            {
                return HttpNotFound();
            }
            ViewBag.sifra_drzavljanstva = new SelectList(db.Drzavljanstvo, "sifra_drzavljanstva", "NazivDrzavljanstva", turist.sifra_drzavljanstva);
            ViewBag.sifra_mjesta = new SelectList(db.Mjesto, "sifra_mjesta", "NazivMjesta", turist.sifra_mjesta);
            return View(turist);
        }

        // POST: Turists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sifra_turista,OIB_turista,Ime,Prezime,DatumRodenja,Spol,sifra_mjesta,sifra_drzavljanstva")] Turist turist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(turist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.sifra_drzavljanstva = new SelectList(db.Drzavljanstvo, "sifra_drzavljanstva", "NazivDrzavljanstva", turist.sifra_drzavljanstva);
            ViewBag.sifra_mjesta = new SelectList(db.Mjesto, "sifra_mjesta", "NazivMjesta", turist.sifra_mjesta);
            return View(turist);
        }

        // GET: Turists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turist turist = db.Turist.Find(id);
            if (turist == null)
            {
                return HttpNotFound();
            }
            return View(turist);
        }

        // POST: Turists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Turist turist = db.Turist.Find(id);
            db.Turist.Remove(turist);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
