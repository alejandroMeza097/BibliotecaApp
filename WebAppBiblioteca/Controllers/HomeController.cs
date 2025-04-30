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
            try
            {

                negocioLibros.AgregarLibro(libro);
                TempData["msg"] = "Libro agregado con exito";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["err"] = "Libro NO agregado";
                return RedirectToAction("IrAgregar");
            }
            
           
        }

        //METODO PARA ELIMINAR LIBRO...
        public ActionResult EliminarLibro(int id)
        {
            try
            {
                negocioLibros.EliminarLibro(id);
                TempData["msg"] = "Libro eliminado con exito";
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["err"] = "Libro NO eliminado";
                return RedirectToAction("Index");
            }
            
        }

        //METODO PARA IR A LA VISTA DE EDITAR...
        public ActionResult IrEditar(int id)
        {
            Libro objLibro = negocioLibros.ObtenerPorId(id);
            return View("IrEditar",objLibro);
        }


        //METODO PARA EDITAR LIBRO...
        public ActionResult EditarLibro(Libro objLibro)
        {
            negocioLibros.EditarLibro(objLibro);
            return RedirectToAction("Index");
        }

        //METODO PARA IR A LA VISTA DE PRESTAMO DE LIBRO...
        public ActionResult IrPrestarLibro()
        {
            return View("IrPrestarLibro");

        }

        //METODO PARA IR A LA VISTA DE DEVOLUCION DE LIBRO...
        public ActionResult IrDevolucionLibro()
        {
            return View("IrDevolucionLibro");

        }

        //METODO PARA PRESTAMO DE LIBRO...
        public ActionResult PrestarLibro(int idLibro, int copiasPrestar)
        {
            try
            {
                
                bool validacionDePrestamo = negocioLibros.ValidacionPrestamo(idLibro, copiasPrestar);
                if (validacionDePrestamo == false)
                {
                    throw new Exception();
                }
                TempData["msg"] = "Prestamo realizado";
                return RedirectToAction("Index");

            }
            catch(Exception ex)
            {
                TempData["err"] = "Prestamo NO realizado";
                return RedirectToAction("IrPrestarLibro");
            }



            
        }

        
    }
}