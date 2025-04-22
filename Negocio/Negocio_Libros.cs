using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Negocio_Libros
    {
        //CAPA DE NEOGOCIOS...

        //INSTANCIA DE LA CAPA DE DATOS...
        Datos_Libros datosLibros = new Datos_Libros();


        //METODO PARA OBTENER LIBROS...
        public List<Libro> ObtenerLibros()
        {
            return datosLibros.ObtenerLibros();
        }


        //METODO PARA AGREGAR LIBRO...
        public void AgregarLibro(Libro libro)
        {
            datosLibros.AgregarLibro(libro);
        }


    }
}
