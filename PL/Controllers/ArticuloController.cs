using ML;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace PL.Controllers
{
    public class ArticuloController : Controller
    {
        // GET: Articulo/GetAll
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Articulo.GetAllEF();

            ML.Articulo articulo = new ML.Articulo();
            articulo.Articulos = result.Objects; // lista de artículos

            if (result.Correct)
            {
                articulo.Articulos = result.Objects;
            }

            return View(articulo);
        }

        [HttpGet] //VistaFormulario
        public ActionResult Formulario()
        {
            ML.Articulo articulo = new ML.Articulo();

            return View(articulo);
        }

        [HttpPost] //Agregar
        public ActionResult Formulario(ML.Articulo articulo)
        {
            ML.Result result = BL.Articulo.AddEF(articulo);

            if (result.Correct)
            {
                TempData["Message"] = "Artículo agregado correctamente";
            }
            else
            {
                TempData["Message"] = "Error: " + result.ErrorMessage;
            }

            return RedirectToAction("GetAll");
        }
    }
}
