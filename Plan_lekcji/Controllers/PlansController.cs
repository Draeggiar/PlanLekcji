using System;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Plan_lekcji.DAL;
using Plan_lekcji.ViewModels;

namespace Plan_lekcji.Controllers
{
    public class PlansController : Controller
    {
        private readonly PlanLekcjiEntities _db = new PlanLekcjiEntities();

        // GET: Plans
        public ActionResult Index()
        {
            var plany = _db.Plan.ToList();
            return View(plany);
        }

        // GET: Plans/Details/5
        public ActionResult Details(int? id)
        { 
            if (id == null)
                return Create();

            var przedmioty = _db.Przedmiot.
                Where(x => x.PlanId == id).ToList();
            var godziny = _db.Godzina.Where(x => x.PlanId == id).OrderBy(x => x.Godzina1).ToList();
            var dni = _db.Dzien.Select(x => x.DzienTygodnia).ToList();

            if (przedmioty.Count > 0)
                ViewBag.Title = przedmioty[0].Plan.Nazwa;     

            PlanViewModel viewModel = new PlanViewModel(przedmioty, godziny, dni);
            return View(viewModel);
        }

        // GET: Plans/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PlanViewModel plan)
        {
            if (ModelState.IsValid)
            {
                _db.Plan.Add(plan);

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(plan);
        }

        // GET: Plans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var plan = _db.Plan.Find(id);
            if (plan == null)
                return HttpNotFound();

            var przedmioty = _db.Przedmiot.Where(p => p.PlanId == id).ToList();
            var godziny = _db.Godzina.Where(g => g.PlanId == id).OrderBy(g => g.Godzina1).ToList();
            var dni = _db.Dzien.Select(d => d.DzienTygodnia).ToList();

            PlanViewModel viewModel = new PlanViewModel(przedmioty, godziny, dni);
            if (przedmioty.Count > 0)
                viewModel.Nazwa = przedmioty[0].Plan.Nazwa;
            viewModel.Id = id.Value;  

            return View(viewModel);
        }

        // POST: Plans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlanViewModel planViewModel)
        {
            if (ModelState.IsValid)
            {
                var plan = _db.Plan.SingleOrDefault(p => p.Id == planViewModel.Id);
                plan.Nazwa = planViewModel.Nazwa;
                _db.Entry(plan).State = EntityState.Modified;

                var i = 0;
                var godziny = _db.Godzina.Where(g => g.PlanId == planViewModel.Id);
                foreach (var godzina in godziny)
                {
                    godzina.Godzina1 = planViewModel.Godzina[i].Godzina1;
                    _db.Entry(godzina).State = EntityState.Modified;
                    i++;
                }

                i = 0;
                var przedmioty = _db.Przedmiot.Where(p => p.PlanId == planViewModel.Id);
                foreach (var przedmiot in przedmioty)
                {
                    przedmiot.Nazwa = planViewModel.Przedmiot[i].Nazwa;
                    _db.Entry(przedmiot).State = EntityState.Modified;
                    i++;
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(planViewModel);
        }

        // GET: Plans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var plan = _db.Plan.Find(id);
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
            Plan plan = _db.Plan.Find(id);
            foreach (Przedmiot przedmiot in plan.Przedmiot)
            {
                _db.Przedmiot.Remove(przedmiot);
            }
            foreach (Godzina godzina in plan.Godzina)
            {
                _db.Godzina.Remove(godzina);
            }
            _db.Plan.Remove(plan);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }
    }
}