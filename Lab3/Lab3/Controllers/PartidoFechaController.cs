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
using System.Web.Script.Serialization;
using Newtonsoft.Json;

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
                if (nuevo != null)
                {
                    Nodo<PartidoFecha> nuevou = new Nodo<PartidoFecha>(nuevo, CompararFecha);
                    int a = db.arbolFecha.Insertar(nuevou);
                    if (a == 1)
                        db.bitacora.Add("Se ha agregado el nodo");
                    else if (a == 2)
                        db.bitacora.Add("Se ha agregado el nodo y se ha balanceado el arbol");
                    db.fecha.Add(nuevo);
                }
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


        PartidoFecha a;
        // GET: PartidoFecha/Delete/5
        public ActionResult Delete(int? noPar)
        {
            if (noPar == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            a = db.fecha.Find(x => x.noPartido == noPar);
            if (a == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(a);
        }

        // POST: PartidoFecha/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "noPartido, Estadio, Pais1, Pais2, FechaPartido, Grupo")]PartidoFecha persona)
        {
            try
            {
                // TODO: Add delete logic here
                PartidoFecha pf = db.fecha.Find(x => x.noPartido == persona.noPartido);

                if (pf == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                else
                {
                    int a = db.arbolFecha.Eliminar(pf);
                    if (a == 1)
                    {
                        db.bitacora.Add("Se ha eliminado el nodo");
                    }
                    else if(a == 2)
                    {
                        db.bitacora.Add("Se ha eliminado el nodo y se ha balanceado el árbol");
                    }
                    db.fecha.Remove(pf);
                }
                return RedirectToAction("Index", db.fecha);
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
       
        
        public class ListaNodos
        {
            public List<PartidoFecha> data { get; set; }
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
                    List<PartidoFecha> partidinhos = new List<PartidoFecha>();
                    string []g ;
                    char[] separators = { '{', '}'};
                    g = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 1; i < g.Length; i++)
                    {
                        string a= "{"+ g[i]+"}";
                        partidinhos.Add(JsonConvert.DeserializeObject<PartidoFecha>(a));
                        i++;
                    }

                    foreach (var item in partidinhos)
                    {
                        Nodo<PartidoFecha> nuevo = new Nodo<PartidoFecha>(item,CompararFecha);
                        int num = db.arbolFecha.Insertar(nuevo);
                        if (num == 1)
                        {
                            db.bitacora.Add("Se ha insertado el nodo");
                            db.fecha.Add(item);
                        }
                        else if (num == 2)
                        {
                            db.bitacora.Add("Se ha insertado y balanceado el arbol");
                            db.fecha.Add(item);
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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        private int CompararFecha(PartidoFecha _actual, PartidoFecha _nuevo)
        {
            return _actual.CompareTo(_nuevo);
        }

    }
}
