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
            if (Session["UserID"] != null)
            {
                return View(await db.Locacions.ToListAsync());
            }
            else
            {
                return Redirect("../Usuarios/Autenticar");
            }
        }

        // GET: Locacions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (Session["UserID"] != null)
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
            else
            {
                return Redirect("../Usuarios/Autenticar");
            }
        }

        // GET: Locacions/Create
        public ActionResult Create()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("../Usuarios/Autenticar");
            }
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
            if (Session["UserID"] != null)
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
            else
            {
                return Redirect("../Usuarios/Autenticar");
            }
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
            if (Session["UserID"] != null)
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
            else
            {
                return Redirect("../Usuarios/Autenticar");
            }
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
