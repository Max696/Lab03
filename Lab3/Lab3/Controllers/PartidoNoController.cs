using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AVL;
using Lab3.DBContext;
using Lab3.Models;
using System.IO;
using System.Net;

namespace Lab3.Controllers
{
    public class PartidoNoController : Controller
    {

        DefaultConnection db = DefaultConnection.getInstance;

        // GET: PartidoNo
        public ActionResult Index()
        {
            return View(db.no.ToList());
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
        // GET: PartidoFecha/Load
        public ActionResult Load()
        {
            return View();
        }

        //POST: PartidoFecha/Load
        [HttpPost]
        public ActionResult Load(HttpPostedFileBase jsonFile)
        {
            try
            {
                // TODO: Add insert logic here
                if (Path.GetFileName(jsonFile.FileName).EndsWith(".json"))
                {
                    jsonFile.SaveAs(Server.MapPath("~/JSONFiles" + Path.GetFileName(jsonFile.FileName)));
                    StreamReader sr = new StreamReader(Server.MapPath("~/JSONFiles" + Path.GetFileName(jsonFile.FileName)));
                    string data = sr.ReadToEnd();

                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
