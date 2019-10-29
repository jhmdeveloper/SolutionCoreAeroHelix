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
    public class ClientesController : Controller
    {
        private BDConfig db = new BDConfig();

        // GET: Clientes
        public async Task<ActionResult> Index()
        {
            if (Session["UserID"] != null)
            {
                return View(await db.Clientes.ToListAsync());
            }
            else
            {
                return Redirect("../Usuarios/Autenticar");
            }
        }

        // GET: Clientes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Cliente cliente = await db.Clientes.FindAsync(id);
                if (cliente == null)
                {
                    return HttpNotFound();
                }
                return View(cliente);
            }
            else
            {
                return Redirect("../Usuarios/Autenticar");
            }
        }

        // GET: Clientes/Create
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

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ClienteID,Nombre,RFC,Correo,TelefonoContacto,Direccion,GeoLatitud,GeoLongitud,Estado")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Clientes.Add(cliente);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Cliente cliente = await db.Clientes.FindAsync(id);
                if (cliente == null)
                {
                    return HttpNotFound();
                }
                return View(cliente);
            }
            else
            {
                return Redirect("../Usuarios/Autenticar");
            }
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ClienteID,Nombre,RFC,Correo,TelefonoContacto,Direccion,GeoLatitud,GeoLongitud,Estado")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Cliente cliente = await db.Clientes.FindAsync(id);
                if (cliente == null)
                {
                    return HttpNotFound();
                }
                return View(cliente);
            }
            else
            {
                return Redirect("../Usuarios/Autenticar");
            }
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cliente cliente = await db.Clientes.FindAsync(id);
            db.Clientes.Remove(cliente);
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
