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
    public class RutasController : Controller
    {
        private BDConfig db = new BDConfig();

        // GET: Rutas
        public async Task<ActionResult> Index()
        {
            var rutas = db.Rutas.Include(r => r.LocacionDestino).Include(r => r.LocacionOrigen);
            return View(await rutas.ToListAsync());
        }

        // GET: Rutas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ruta ruta = await db.Rutas.FindAsync(id);
            if (ruta == null)
            {
                return HttpNotFound();
            }
            return View(ruta);
        }

        // GET: Rutas/Create
        public ActionResult Create()
        {
            ViewBag.LocacionDestinoID = new SelectList(db.Locacions, "LocacionID", "Nombre");
            ViewBag.LocacionOrigenID = new SelectList(db.Locacions, "LocacionID", "Nombre");
            return View();
        }

        // POST: Rutas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RutaID,Descripcion,LocacionOrigenID,LocacionDestinoID,Estado, Duracion")] Ruta ruta)
        {
            if (ModelState.IsValid)
            {
                db.Rutas.Add(ruta);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.LocacionDestinoID = new SelectList(db.Locacions, "LocacionID", "Nombre", ruta.LocacionDestinoID);
            ViewBag.LocacionOrigenID = new SelectList(db.Locacions, "LocacionID", "Nombre", ruta.LocacionOrigenID);
            return View(ruta);
        }

        // GET: Rutas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ruta ruta = await db.Rutas.FindAsync(id);
            if (ruta == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocacionDestinoID = new SelectList(db.Locacions, "LocacionID", "Nombre", ruta.LocacionDestinoID);
            ViewBag.LocacionOrigenID = new SelectList(db.Locacions, "LocacionID", "Nombre", ruta.LocacionOrigenID);
            return View(ruta);
        }

        // POST: Rutas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RutaID,Descripcion,LocacionOrigenID,LocacionDestinoID,Estado,Duracion")] Ruta ruta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ruta).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.LocacionDestinoID = new SelectList(db.Locacions, "LocacionID", "Nombre", ruta.LocacionDestinoID);
            ViewBag.LocacionOrigenID = new SelectList(db.Locacions, "LocacionID", "Nombre", ruta.LocacionOrigenID);
            return View(ruta);
        }

        // GET: Rutas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ruta ruta = await db.Rutas.FindAsync(id);
            if (ruta == null)
            {
                return HttpNotFound();
            }
            return View(ruta);
        }

        // POST: Rutas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Ruta ruta = await db.Rutas.FindAsync(id);
            db.Rutas.Remove(ruta);
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
