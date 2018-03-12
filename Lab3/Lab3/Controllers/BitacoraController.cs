using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab3.DBContext;

namespace Lab3.Controllers
{
    public class BitacoraController : Controller
    {
        DefaultConnection db = DefaultConnection.getInstance;
        // GET: Bitacora
        public ActionResult Index()
        {
            return View(db.bitacora.ToList());
        }
    }
}
