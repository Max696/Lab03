using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab3.DBContext;
using Lab3.Models;
using System.Net;
using AVL;

namespace Lab3.Controllers
{
    public class PartidoFechaController : Controller
    {

        DefaultConnection db = DefaultConnection.getInstance;

        // GET: PartidoFecha
        public ActionResult Index()
        {
            return View(db.fecha.ToList());
        }

        // GET: PartidoFecha/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PartidoFecha/Create
        public ActionResult Create() 
        {
            //Añadir nodo manualmente
            return View();
        }

        // POST: PartidoFecha/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "NoPartido, FechaPartido, Grupo, Pais1, Pais2,Estadio")] PartidoFecha nuevo)
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
            // Carga de archivos
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
