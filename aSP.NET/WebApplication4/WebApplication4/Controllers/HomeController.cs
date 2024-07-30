using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        Intern_prjEntities _context = new Intern_prjEntities();
        public ActionResult Index()
        {
            var listofData = _context.Pros.ToList();
            return View(listofData);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Pro model)
        {
            if (!ModelState.IsValid)
            {
             
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View(model);
            }
            _context.Pros.Add(model);
            _context.SaveChanges();
            ViewBag.Message = "Data Insert Successfully";
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = _context.Pros.FirstOrDefault(x => x.id == id);

            if (data == null)
            {
                ViewBag.ErrorMessage = "Data not found";
                return View("Error");
            }

            return View(data);
        }


        [HttpPost]
        public ActionResult Edit(Pro model)
        {
            var data = _context.Pros.FirstOrDefault(x => x.id == model.id);

            if (data == null)
            {
                ViewBag.ErrorMessage = "Data not found";
                return View("Error");
            }

            data.namepj = model.namepj;
            data.description = model.description;

            _context.SaveChanges();

            return RedirectToAction("Index"); 
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            var data = _context.Pros.FirstOrDefault(x => x.id == id);
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var data = _context.Pros.Find(id);

            if (data == null)
            {
                return HttpNotFound();
            }

            bool canDelete = !_context.Goithaus.Any(g => g.idprj == id);

            ViewBag.CanDelete = canDelete;
            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var data = _context.Pros.Find(id);

            if (data == null)
            {
                return HttpNotFound();
            }

            bool canDelete = !_context.Goithaus.Any(g => g.idprj == id);

            if (!canDelete)
            {
                ModelState.AddModelError("", "This record cannot be deleted because it has related records in another table.");
                ViewBag.CanDelete = false;
                return View(data);
            }

            _context.Pros.Remove(data);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }



    

 

}