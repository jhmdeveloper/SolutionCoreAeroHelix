using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using SolutionCoreAeroHelix.Models;

namespace SolutionCoreAeroHelix.Controllers
{
    public class UsuariosController : Controller
    {
        private BDConfig db = new BDConfig();

        // GET: Usuarios
        public async Task<ActionResult> Index()
        {
            if (Session["UserID"] != null)
            {
                var usuarios = db.Usuarios.Include(u => u.Cliente);
                return View(await usuarios.ToListAsync());
            }
            else
            {
                return Redirect("../Usuarios/Autenticar");
            }
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
            if (Session["UserID"] != null)
            {
                ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nombre");
                ViewBag.PerfilID = new SelectList(db.Perfils, "PerfilID", "Descripcion");
                return View();
            }
            else
            {
                return Redirect("../Usuarios/Autenticar");
            }

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
            if (Session["UserID"] != null)
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
            else
            {
                return Redirect("../Usuarios/Autenticar");
            }
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
            if (Session["UserID"] != null)
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
            else
            {
                return Redirect("../Usuarios/Autenticar");
            }
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
            try
            {
                using (db)
                {
                    var usr = db.Usuarios.Where(c => c.Estado == 1).Include(c => c.Perfil).Single(u => u.UserName == user.UserName && u.Password == user.Password);
                     
                    if (usr != null)
                    {
                        Session["UserID"] = usr.UsuarioID.ToString();
                        Session["Username"] = usr.UserName.ToString();
                        Session["ClienteID"] = usr.ClienteID.ToString();
                        return RedirectToAction(usr.Perfil.PaginaInicio, "usuarios");
                    }
                    else
                    {
                        ModelState.AddModelError("", "<b>Nombre o contraseña incorrectos</b>");
                    }

                    return View();
                }
            }
            catch
            {
                ModelState.AddModelError("UsuarioInexistente", "Usuario y/o contraseña incorrectos");
                return View();
            }
        }

        //Salir
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Autenticar", "usuarios");
        }

        // GET: Usuarios/TableroInicial
        public ActionResult TableroInicial()
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

        // GET: Usuarios/TableroCliente
        public ActionResult TableroCliente()
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

        //Demo
        public ActionResult Demo()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("~/RecursoNoEncontrado.html");
            }
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
