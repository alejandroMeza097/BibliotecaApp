using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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


        //METODO PARA ELIMINAR LIBRO...
        public void EliminarLibro(int id)
        {
            datosLibros.EliminarLibro(id);
        }

        //METODO PARA OBTENER LIBRO POR ID...
        public Libro ObtenerPorId(int id)
        {
            return datosLibros.ObtenerPorId(id);
        }

        //METODO PARA EDITAR LIBRO...
        public void EditarLibro(Libro objLibro)
        {
            datosLibros.EditarLibro(objLibro);
        }




        //METODO PARA RECUPERAR EL NUMERO DE COPIAS POR ID...
        public bool ValidacionPrestamo(int id, int copiasPrestar)
        {

            Libro libro = datosLibros.ObtenerPorId(id);

            //VERIFICA QUE EXISTA EL ID INGRESADO...
            if(libro == null)
            {
                return false;
            }
            else
            {
                //EVITA QUE TENGA NEGATIVOS...
                if(copiasPrestar > libro.copias)
                {
                    return false;
                }
                else
                {
                    //LOGICA PARA ACUTALIZAR...
                    libro.copias = libro.copias - copiasPrestar;
                    datosLibros.EditarLibro(libro);
                    return true;
                    
                }

            }

        }

       
        




    }
}
