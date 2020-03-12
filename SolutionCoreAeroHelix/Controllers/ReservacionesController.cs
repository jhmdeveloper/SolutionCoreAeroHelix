using SolutionCoreAeroHelix.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SolutionCoreAeroHelix.Controllers
{
    public class ReservacionesController : Controller
    {
        private BDConfig db = new BDConfig();

        /// <summary>
        /// Identificador del usuario de la sesión actual
        /// </summary>
        private int UserId
        {
            get
            {
                return Convert.ToInt32(Session["UserId"]);
            }
        }

        // GET: Reservaciones
        public async Task<ActionResult> Index()
        {
            var reservacions = db.Reservacions.Include(r => r.Usuario).Include(r => r.LocacionDestino).Include(r => r.LocacionOrigen);
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
        public async Task<ActionResult> Create([Bind(Include = "ReservacionID,UsuarioID,FechaHora,TotalPasajeros,Comentario,StatusID,LocacionOrigenID,LocacionDestinoID,DireccionOrigen,DireccionDestino,DuracionVuelo,Equipaje")] Reservacion reservacion)
        {
            if (ModelState.IsValid)
            {
                db.Reservacions.Add(reservacion);
                db.SaveChanges();

                db.ComentarioReservacions.Add(new ComentarioReservacion
                {
                    ReservacionID = reservacion.ReservacionID,
                    UsuarioID = reservacion.UsuarioID,
                    Comentario = reservacion.Comentario,
                    StatusID = reservacion.StatusID
                });

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "UserName", reservacion.UsuarioID);
            ViewBag.LocacionDestinoID = new SelectList(db.Locacions, "LocacionID", "Nombre");
            ViewBag.LocacionOrigenID = new SelectList(db.Locacions, "LocacionID", "Nombre");
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
            ViewBag.LocacionDestinoID = new SelectList(db.Locacions, "LocacionID", "Nombre", reservacion.LocacionDestinoID);
            ViewBag.LocacionOrigenID = new SelectList(db.Locacions, "LocacionID", "Nombre", reservacion.LocacionOrigenID);

            var comentarios = db.ComentarioReservacions.Where(c => c.ReservacionID == id).Include(c => c.Usuario).ToList<ComentarioReservacion>();
            ViewBag.Comentarios = comentarios;

            return View(reservacion);
        }

        // POST: Reservaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ReservacionID,UsuarioID,FechaHora,TotalPasajeros,Comentario,StatusID,LocacionOrigenID,LocacionDestinoID,DireccionOrigen,DireccionDestino,DuracionVuelo,Equipaje")] Reservacion reservacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservacion).State = EntityState.Modified;

                db.ComentarioReservacions.Add(new ComentarioReservacion
                {
                    ReservacionID = reservacion.ReservacionID,
                    UsuarioID = reservacion.UsuarioID,
                    Comentario = reservacion.Comentario,
                    StatusID = reservacion.StatusID
                });

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "UserName", reservacion.UsuarioID);
            ViewBag.LocacionDestinoID = new SelectList(db.Locacions, "LocacionID", "Nombre", reservacion.LocacionDestinoID);
            ViewBag.LocacionOrigenID = new SelectList(db.Locacions, "LocacionID", "Nombre", reservacion.LocacionOrigenID);
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

        // GET: Reservaciones/Duracion
        public ActionResult Duracion(int origenID, int destinoID)
        {
            var rutas = db.Rutas.Where(c => c.LocacionOrigenID == origenID && c.LocacionDestinoID == destinoID);
            var duracion = 0;

            if (rutas.Any()) duracion = rutas.FirstOrDefault().Duracion;

            //  Send "Success"
            return Json(new { Duracion = duracion }, JsonRequestBehavior.AllowGet);
        }

        // GET: Reservaciones/IndexCliente
        public async Task<ActionResult> IndexCliente()
        {
            //if (UserId == 0) return RedirectToAction("Autenticar", "Usuarios");

            var reservacions = db.Reservacions.Include(r => r.Usuario).Where(c => c.UsuarioID == UserId).Include(r => r.LocacionDestino).Include(r => r.LocacionOrigen);
            return View(await reservacions.ToListAsync());
        }

        // GET: Reservaciones/DetailsCliente/5
        public async Task<ActionResult> DetailsCliente(int? id)
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
            ViewBag.LocacionDestinoID = new SelectList(db.Locacions, "LocacionID", "Nombre", reservacion.LocacionDestinoID);
            ViewBag.LocacionOrigenID = new SelectList(db.Locacions, "LocacionID", "Nombre", reservacion.LocacionOrigenID);

            var comentarios = db.ComentarioReservacions.Where(c => c.ReservacionID == id).Include(c => c.Status).ToList<ComentarioReservacion>();
            ViewBag.Comentarios = comentarios;

            ViewBag.DisplayOrigen = (reservacion.LocacionOrigenID != 0) ? "display:none" : "";
            ViewBag.DisplayDestino = (reservacion.LocacionDestinoID != 0) ? "display:none" : "";

            return View(reservacion);
        }

        // POST: Reservaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DetailsCliente([Bind(Include = "ReservacionID,UsuarioID,Comentario,StatusID")] Reservacion reservacion)
        {
            try
            {
                db.ComentarioReservacions.Add(new ComentarioReservacion
                {
                    ReservacionID = reservacion.ReservacionID,
                    UsuarioID = reservacion.UsuarioID,
                    Comentario = reservacion.Comentario,
                    StatusID = reservacion.StatusID
                });

                await db.SaveChangesAsync();
                return RedirectToAction("IndexCliente");
            }
            catch
            {
                Reservacion reservacion2 = await db.Reservacions.FindAsync(reservacion.ReservacionID);
                ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "UserName", reservacion2.UsuarioID);
                ViewBag.LocacionDestinoID = new SelectList(db.Locacions, "LocacionID", "Nombre", reservacion2.LocacionDestinoID);
                ViewBag.LocacionOrigenID = new SelectList(db.Locacions, "LocacionID", "Nombre", reservacion2.LocacionOrigenID);

                var comentarios = db.ComentarioReservacions.Where(c => c.ReservacionID == reservacion2.ReservacionID).Include(c => c.Status).ToList<ComentarioReservacion>();
                ViewBag.Comentarios = comentarios;

                ViewBag.DisplayOrigen = (reservacion2.LocacionOrigenID != 0) ? "display:none" : "";
                ViewBag.DisplayDestino = (reservacion2.LocacionDestinoID != 0) ? "display:none" : "";

                return View(reservacion2);
            }
        }

        // GET: Reservaciones/CalendarioCliente
        public async Task<ActionResult> CalendarioCliente()
        {
            ViewBag.LocacionDestinoID = new SelectList(db.Locacions, "LocacionID", "Nombre");
            ViewBag.LocacionOrigenID = new SelectList(db.Locacions, "LocacionID", "Nombre");

            var reservacions = db.Reservacions.Where(r => r.UsuarioID == UserId).Include(r => r.LocacionDestino).Include(r => r.LocacionOrigen);
            return View(await reservacions.ToListAsync());
        }

        // POST: Reservaciones/CalendarioCliente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CalendarioCliente([Bind(Include = "ReservacionID,UsuarioID,FechaHora,TotalPasajeros,Comentario,StatusID,LocacionOrigenID,LocacionDestinoID,DireccionOrigen,DireccionDestino,DuracionVuelo,Equipaje")] Reservacion reservacion)
        {
            if (UserId == 0) return Json(new { status = false, message = "La sesión finalizó o no ha sido iniciada." });

            if (ModelState.IsValid)
            {
                reservacion.UsuarioID = UserId;
                db.Reservacions.Add(reservacion);
                db.SaveChanges();

                db.ComentarioReservacions.Add(new ComentarioReservacion
                {
                    ReservacionID = reservacion.ReservacionID,
                    UsuarioID = reservacion.UsuarioID,
                    Comentario = reservacion.Comentario,
                    StatusID = reservacion.StatusID
                });

                await db.SaveChangesAsync();
            }

            return Json(new { status = true, message = "La reservación se realizó correctamente.", reservacionId = reservacion.ReservacionID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{ 
        //    if (UserId == 0)
        //        filterContext.Result = RedirectToAction("Autenticar", "Usuarios");
        //    else
        //        base.OnActionExecuting(filterContext);
        //}

        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    Logger.Write(filterContext.Exception);
        //}
    }
}
