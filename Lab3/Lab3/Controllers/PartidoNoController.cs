using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab3.Controllers
{
    public class PartidoNoController : Controller
    {
        // GET: PartidoNo
        public ActionResult Index()
        {
            return View();
        }

        // GET: PartidoNo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PartidoNo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PartidoNo/Create
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

        // GET: PartidoNo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PartidoNo/Edit/5
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

        // GET: PartidoNo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PartidoNo/Delete/5
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
