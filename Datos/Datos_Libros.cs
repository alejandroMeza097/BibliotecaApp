using Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Datos_Libros
    {
        //CAPA DE DATOS...

        //CREDENCIALES PARA LA CADENA...
        private readonly string _connectionString;

        public Datos_Libros()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
        }

        //METODO PARA OBTENER LOS LIBROS...
        public List<Libro> ObtenerLibros()
        {
            List<Libro> listaLibros = new List<Libro>();
            using (SqlConnection sqlconnection = new SqlConnection(_connectionString))
            {

                try
                {
                    string query = " SELECT idLibro,titulo,autor,genero,copias FROM Libros";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);
                    sqlconnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        Libro libro = new Libro();
                        libro.idLibro = reader.GetInt32(0);
                        libro.titulo = reader.GetString(1);
                        libro.autor = reader.GetString(2);
                        libro.genero = reader.GetString(3);
                        libro.copias = reader.GetInt32(4);
                        listaLibros.Add(libro);
                    }

                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al recuperar la lista desde la base de datos.", ex);
                }

                catch (Exception ex){
                    throw new Exception("Error general", ex);
                }
                
                return listaLibros;
            }

           
        }


        //METODO PARA AGREGAR LIBROS...
        public void AgregarLibro(Libro libro)
        {
            using (SqlConnection sqlconnection = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = $"INSERT INTO Libros (titulo,autor,genero,copias) VALUES ('{libro.titulo}','{libro.autor}','{libro.genero}',{libro.copias});";
                    SqlCommand sqlcommand = new SqlCommand (query, sqlconnection);
                    sqlconnection.Open();
                    sqlcommand.ExecuteNonQuery();
                }
                catch(SqlException ex)
                {
                    throw new Exception("Error al añadir a tabla de sql", ex);
                }
                catch(Exception ex)
                {
                    throw new Exception("Error general", ex);
                }

            }
        }

    }
}
