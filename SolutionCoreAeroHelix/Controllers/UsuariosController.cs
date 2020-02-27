using SolutionCoreAeroHelix.Helpers;
using SolutionCoreAeroHelix.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace SolutionCoreAeroHelix.Controllers
{
    public class UsuariosController : Controller
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

        // GET: Usuarios
        public async Task<ActionResult> Index()
        {
            var usuarios = db.Usuarios.Include(u => u.Cliente);
            return View(await usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = await db.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nombre");
            ViewBag.PerfilID = new SelectList(db.Perfils, "PerfilID", "Descripcion");
            return View();

        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UsuarioID,UserName,Password,ConfirmPassword,Estado,ClienteID,PerfilID")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nombre", usuario.ClienteID);
            ViewBag.PerfilID = new SelectList(db.Perfils, "PerfilID", "Descripcion", usuario.PerfilID);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = await db.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nombre", usuario.ClienteID);
            ViewBag.PerfilID = new SelectList(db.Perfils, "PerfilID", "Descripcion", usuario.PerfilID);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UsuarioID,UserName,Password,ConfirmPassword,Estado,ClienteID,PerfilID")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nombre", usuario.ClienteID);
            ViewBag.PerfilID = new SelectList(db.Perfils, "PerfilID", "Descripcion", usuario.PerfilID);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = await db.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Usuario usuario = await db.Usuarios.FindAsync(id);
            db.Usuarios.Remove(usuario);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //Autenticar
        public ActionResult Autenticar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autenticar(Usuario user)
        {
            using (db)
            {
                var usr = db.Usuarios.Where(c => c.Estado == 1).Include(c => c.Perfil).Single(u => u.UserName == user.UserName && u.Password == user.Password);

                if (usr != null)
                {
                    Session["UserID"] = usr.UsuarioID;
                    Session["Username"] = usr.UserName;
                    Session["ClienteID"] = usr.ClienteID;
                    Session["PaginaInicio"] = usr.Perfil.PaginaInicio;
                    Inspector.SetSessionStart();
                    return RedirectToAction("", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "<b>Nombre o contraseña incorrectos</b>");
                }

                return View();
            }
        }

        // GET: Usuarios/LogOut
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            Inspector.ClearSessionStart();

            return RedirectToAction("Autenticar", "Usuarios");
        }

        // GET: Usuarios/Timeout
        public ActionResult Timeout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return View();
        }

        // GET: Usuarios/TableroInicial
        public ActionResult TableroInicial()
        {
            return View();
        }

        // GET: Usuarios/TableroCliente
        public ActionResult TableroCliente()
        {
            return View();
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
