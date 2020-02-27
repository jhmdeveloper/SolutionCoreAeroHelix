using SolutionCoreAeroHelix.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SolutionCoreAeroHelix.Controllers
{
    public class AeronavesController : Controller
    {
        private BDConfig db = new BDConfig();

        // GET: Aeronaves
        public async Task<ActionResult> Index()
        {
            return View(await db.Aeronaves.ToListAsync());
        }

        // GET: Aeronaves/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aeronave aeronave = await db.Aeronaves.FindAsync(id);
            if (aeronave == null)
            {
                return HttpNotFound();
            }
            return View(aeronave);
        }

        // GET: Aeronaves/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Aeronaves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AeronaveID,Nombre,Modelo,Tipo,Anio,Comentarios,Estado")] Aeronave aeronave)
        {
            if (ModelState.IsValid)
            {
                db.Aeronaves.Add(aeronave);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(aeronave);
        }

        // GET: Aeronaves/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aeronave aeronave = await db.Aeronaves.FindAsync(id);
            if (aeronave == null)
            {
                return HttpNotFound();
            }
            return View(aeronave);
        }

        // POST: Aeronaves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AeronaveID,Nombre,Modelo,Tipo,Anio,Comentarios,Estado")] Aeronave aeronave)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aeronave).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(aeronave);
        }

        // GET: Aeronaves/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aeronave aeronave = await db.Aeronaves.FindAsync(id);
            if (aeronave == null)
            {
                return HttpNotFound();
            }
            return View(aeronave);
        }

        // POST: Aeronaves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Aeronave aeronave = await db.Aeronaves.FindAsync(id);
            db.Aeronaves.Remove(aeronave);
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
