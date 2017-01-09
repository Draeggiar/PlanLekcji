using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Plan_lekcji.DAL;

namespace Plan_lekcji.Controllers
{
    public class PlansController : Controller
    {
        private readonly PlanLekcjiEntities db = new PlanLekcjiEntities();

        // GET: Plans
        public ActionResult Index()
        {
            var plany = db.Plan.ToList();
            return View(plany);
        }

        // GET: Plans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return Create();
            var przedmioty = db.Przedmiot.
                Where(x => x.PlanId == id).
                OrderByDescending(x => x.Godzina).ToList();
            if (przedmioty.Count > 0)
                ViewBag.Title = przedmioty[0].Plan.Nazwa;
            return View(przedmioty);
        }

        // GET: Plans/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Plan plan)
        {
            if (ModelState.IsValid)
            {
                db.Plan.Add(plan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(plan);
        }

        // GET: Plans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var plan = db.Plan.Find(id);
            if (plan == null)
                return HttpNotFound();
            return View(plan);
        }

        // POST: Plans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nazwa")] Plan plan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(plan);
        }

        // GET: Plans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var plan = db.Plan.Find(id);
            if (plan == null)
                return HttpNotFound();
            return View(plan);
        }

        // POST: Plans/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var plan = db.Plan.Find(id);
            db.Plan.Remove(plan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}