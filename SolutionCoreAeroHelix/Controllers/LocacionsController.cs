using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SolutionCoreAeroHelix.Models;

namespace SolutionCoreAeroHelix.Controllers
{
    public class LocacionsController : Controller
    {
        private BDConfig db = new BDConfig();

        // GET: Locacions
        public async Task<ActionResult> Index()
        {
            return View(await db.Locacions.ToListAsync());
        }

        // GET: Locacions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locacion locacion = await db.Locacions.FindAsync(id);
            if (locacion == null)
            {
                return HttpNotFound();
            }
            return View(locacion);
        }

        // GET: Locacions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Locacions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LocacionID,Nombre,Descripcion,Direccion,Latitud,Longitud,HorarioServicio,Geolocalizacion")] Locacion locacion)
        {
            if (ModelState.IsValid)
            {
                db.Locacions.Add(locacion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(locacion);
        }

        // GET: Locacions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locacion locacion = await db.Locacions.FindAsync(id);
            if (locacion == null)
            {
                return HttpNotFound();
            }
            return View(locacion);
        }

        // POST: Locacions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LocacionID,Nombre,Descripcion,Direccion,Latitud,Longitud,HorarioServicio,Geolocalizacion")] Locacion locacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(locacion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(locacion);
        }

        // GET: Locacions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locacion locacion = await db.Locacions.FindAsync(id);
            if (locacion == null)
            {
                return HttpNotFound();
            }
            return View(locacion);
        }

        // POST: Locacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Locacion locacion = await db.Locacions.FindAsync(id);
            db.Locacions.Remove(locacion);
            await db.SaveChangesAsync();
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
