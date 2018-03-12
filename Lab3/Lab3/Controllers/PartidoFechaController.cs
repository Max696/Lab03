using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab3.Controllers
{
    public class PartidoFechaController : Controller
    {
        // GET: PartidoFecha
        public ActionResult Index()
        {
            return View();
        }

        // GET: PartidoFecha/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PartidoFecha/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PartidoFecha/Create
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

        // GET: PartidoFecha/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PartidoFecha/Edit/5
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

        // GET: PartidoFecha/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PartidoFecha/Delete/5
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
