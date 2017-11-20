using CourseApp.DataAccess.Models;
using CourseApp.DataAccess.Repositories;
using CourseApp.Web.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CourseApp.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly CourseRepository repository;

        public CourseController()
        {
            this.repository = new CourseRepository();
        }

        // GET: Course
        public ActionResult Index()
        {
            IEnumerable<Course> courses = this.repository.Get();

            return View("Index", courses);
        }

        // GET: Course/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Course/Create
        public ActionResult Create(CourseModel model)
        {
            if (ModelState.IsValid)
            {
                this.repository.Create(new Course(model.Id, model.Name, model.Price));

                return RedirectToAction("Index");
            }

            return View("Create", model);
        }

        // POST: Course/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Course/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Course/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Course/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
