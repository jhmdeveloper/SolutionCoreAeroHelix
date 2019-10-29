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
    public class ReservacionesController : Controller
    {
        private BDConfig db = new BDConfig();
         
        // GET: Reservaciones
        public async Task<ActionResult> Index()
        {
            var reservacions = db.Reservacions.Include(r => r.usuario).Include(r => r.LocacionDestino).Include(r => r.LocacionOrigen);
            return View(await reservacions.ToListAsync());
        }

        // GET: Reservaciones/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = await db.Reservacions.FindAsync(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            return View(reservacion);
        }

        // GET: Reservaciones/Create
        public ActionResult Create()
        { 
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "UserName");
            ViewBag.LocacionDestinoID = new SelectList(db.Locacions, "LocacionID", "Nombre");
            ViewBag.LocacionOrigenID = new SelectList(db.Locacions, "LocacionID", "Nombre");
            return View();
        }

        // POST: Reservaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ReservacionID,UsuarioID,FechaHora,TotalPasajeros,Comentarios,Status,LocacionOrigenID,LocacionDestinoID,DireccionOrigen,DireccionDestino,DuracionVuelo,Equipaje")] Reservacion reservacion)
        {
            if (ModelState.IsValid)
            {
                db.Reservacions.Add(reservacion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "UserName", reservacion.UsuarioID);
            return View(reservacion);
        }

        // GET: Reservaciones/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = await db.Reservacions.FindAsync(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "UserName", reservacion.UsuarioID);
            ViewBag.LocacionDestinoID = new SelectList(db.Locacions, "LocacionID", "Nombre", reservacion.LocacionOrigenID);
            ViewBag.LocacionOrigenID = new SelectList(db.Locacions, "LocacionID", "Nombre", reservacion.LocacionDestinoID);
            return View(reservacion);
        }

        // POST: Reservaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ReservacionID,UsuarioID,FechaHora,TotalPasajeros,Comentarios,Status,LocacionOrigenID,LocacionDestinoID,DireccionOrigen,DireccionDestino,DuracionVuelo,Equipaje")] Reservacion reservacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservacion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "UserName", reservacion.UsuarioID);
            ViewBag.LocacionDestinoID = new SelectList(db.Locacions, "LocacionID", "Nombre", reservacion.LocacionOrigenID);
            ViewBag.LocacionOrigenID = new SelectList(db.Locacions, "LocacionID", "Nombre", reservacion.LocacionDestinoID);
            return View(reservacion);
        }

        // GET: Reservaciones/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = await db.Reservacions.FindAsync(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            return View(reservacion);
        }

        // POST: Reservaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Reservacion reservacion = await db.Reservacions.FindAsync(id);
            db.Reservacions.Remove(reservacion);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //public PartialViewResult ObtenerDireccion()
        //{
        //    var locacion = new Locacion();
        //    locacion.Direccion = "Mi casa";
            

        //    return PartialView("_Direccion", locacion);
        //}

        public ActionResult ObtenerDireccion()
        {
            var locacion = new Locacion();
            locacion.Direccion = "Mi casa";

                //  Send "Success"
            return Json(new { success = true, responseText = "Your message successfuly sent!" }, JsonRequestBehavior.AllowGet);


            //return RedirectToAction("Autenticar", "usuarios");
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
