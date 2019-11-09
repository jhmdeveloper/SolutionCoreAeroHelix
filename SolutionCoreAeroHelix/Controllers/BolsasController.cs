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
    public class BolsasController : Controller
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
        /// <summary>
        /// Identificador del cliente de la sesión actual
        /// </summary>
        private int ClienteId
        {
            get
            {
                return Convert.ToInt32(Session["ClienteId"]);
            }
        }

        // GET: Bolsas
        public async Task<ActionResult> Index()
        {
            var bolsas = db.Bolsas.Include(b => b.cliente);
            return View(await bolsas.ToListAsync());
        }

        // GET: Bolsas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bolsa bolsa = await db.Bolsas.FindAsync(id);
            if (bolsa == null)
            {
                return HttpNotFound();
            }
            return View(bolsa);
        }

        // GET: Bolsas/DetailsCliente
        public async Task<ActionResult> DetailsCliente()
        {
            if (UserId == 0) return RedirectToAction("Autenticar", "Usuarios");

            var bolsas = db.Bolsas.Include(b => b.cliente).Where(c=>c.ClienteID == ClienteId);
            return View(await bolsas.ToListAsync());
        }

        // GET: Bolsas/Create
        public ActionResult Create()
        {
            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nombre");
            return View();
        }

        // POST: Bolsas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BolsaID,ClienteID,Minutos,MinutosRestantes")] Bolsa bolsa)
        {
            if (ModelState.IsValid)
            {
                db.Bolsas.Add(bolsa);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nombre", bolsa.ClienteID);
            return View(bolsa);
        }

        // GET: Bolsas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bolsa bolsa = await db.Bolsas.FindAsync(id);
            if (bolsa == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nombre", bolsa.ClienteID);
            return View(bolsa);
        }

        // POST: Bolsas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BolsaID,ClienteID,Minutos,MinutosRestantes")] Bolsa bolsa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bolsa).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nombre", bolsa.ClienteID);
            return View(bolsa);
        }

        // GET: Bolsas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bolsa bolsa = await db.Bolsas.FindAsync(id);
            if (bolsa == null)
            {
                return HttpNotFound();
            }
            return View(bolsa);
        }

        // POST: Bolsas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Bolsa bolsa = await db.Bolsas.FindAsync(id);
            db.Bolsas.Remove(bolsa);
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
