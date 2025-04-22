using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppBiblioteca.Controllers
{
    public class HomeController : Controller
    {

        //INSTANCIA DE LA CAPA DE NEGOCIOS...
        Negocio_Libros negocioLibros = new Negocio_Libros();    

        public ActionResult Index()
        {
            List<Libro> listaLibros = new List<Libro>();
            listaLibros = negocioLibros.ObtenerLibros();
            return View("Index",listaLibros);
        }


        //METODO PARA IR A LA VISTA DE AGREGAR...
        public ActionResult IrAgregar()
        {
            return View("IrAgregar");
        }

        //METODO PARA AGREGAR LIBRO...
        public ActionResult Agregar(Libro libro)
        {
            negocioLibros.AgregarLibro(libro);
            return RedirectToAction("Index");
        }

    }
}