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
using Newtonsoft.Json;

namespace Lab3.Controllers
{
    public class PartidoNoController : Controller
    {

        DefaultConnection db = DefaultConnection.getInstance;

        // GET: PartidoNo
        public ActionResult Index()
        {
            db.arbolNo.Pre(db.arbolNo._raiz);

            return View(db.arbolNo.PreList);
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
        public ActionResult Create([Bind(Include = "NoPartido, FechaPartido, Grupo, Pais1, Pais2,Estadio")] PartidoNo nuevo)
        {
            try
            {
                // TODO: Add insert logic here
                if (nuevo != null)
                {
                    Nodo<PartidoNo> nuevou = new Nodo<PartidoNo>(nuevo, CompararNo);
                    int a = db.arbolNo.Insertar(nuevou);
                    if (a == 1)
                        db.bitacora.Add("Se ha agregado el nodo");
                    else if (a == 2)
                        db.bitacora.Add("Se ha agregado el nodo y se ha balanceado el arbol");
                
                }
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
                    List<PartidoNo> partidinhos = new List<PartidoNo>();
                    string[] g;
                    char[] separators = { '{', '}' };
                    g = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 1; i < g.Length; i++)
                    {
                        string a = "{" + g[i] + "}";
                        partidinhos.Add(JsonConvert.DeserializeObject<PartidoNo>(a));
                        i++;
                    }

                    foreach (var item in partidinhos)
                    {
                        Nodo<PartidoNo> nuevo = new Nodo<PartidoNo>(item, CompararNo);
                        int num = db.arbolNo.Insertar(nuevo);
                        if (num == 1)
                        {
                            db.bitacora.Add("Se ha insertado el nodo");
                           
                        }
                        else if (num == 2)
                        {
                            db.bitacora.Add("Se ha insertado y balanceado el arbol");
                            
                        }
                    }
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

        private int CompararNo(PartidoNo _actual, PartidoNo _nuevo)
        {
            return _actual.CompareTo(_nuevo);
        }
 

    }
}
