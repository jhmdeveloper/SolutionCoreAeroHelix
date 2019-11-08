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
    public class CapitansController : Controller
    {
        private BDConfig db = new BDConfig();

        // GET: Capitans
        public async Task<ActionResult> Index()
        {
            if (Session["UserID"] != null)
            {
                return View(await db.Capitans.ToListAsync());
            }
            else
            {
                return Redirect("../Usuarios/Autenticar");
            }
        }

        // GET: Capitans/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Capitan capitan = await db.Capitans.FindAsync(id);
                if (capitan == null)
                {
                    return HttpNotFound();
                }
                return View(capitan);
            }
            else
            {
                return Redirect("../Usuarios/Autenticar");
            }
        }

        // GET: Capitans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Capitans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CapitanID,Nombre,Comentarios,Licencia")] Capitan capitan)
        {
            if (ModelState.IsValid)
            {
                db.Capitans.Add(capitan);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(capitan);
        }

        // GET: Capitans/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Capitan capitan = await db.Capitans.FindAsync(id);
                if (capitan == null)
                {
                    return HttpNotFound();
                }
                return View(capitan);
            }
            else
            {
                return Redirect("../Usuarios/Autenticar");
            }
        }

        // POST: Capitans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CapitanID,Nombre,Comentarios,Licencia")] Capitan capitan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(capitan).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(capitan);
        }

        // GET: Capitans/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Capitan capitan = await db.Capitans.FindAsync(id);
                if (capitan == null)
                {
                    return HttpNotFound();
                }
                return View(capitan);
            }
            else
            {
                return Redirect("../Usuarios/Autenticar");
            }
        }

        // POST: Capitans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Capitan capitan = await db.Capitans.FindAsync(id);
            db.Capitans.Remove(capitan);
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
