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


        //METODO PARA ELIMINAR LIBROS..
        public void EliminarLibro(int id)
        {
            using(SqlConnection sqlconnection = new SqlConnection(_connectionString)){
                try
                {
                    string query = $"DELETE FROM Libros WHERE idLibro = {id};";
                    SqlCommand sqlcommand = new SqlCommand(query, sqlconnection);
                    sqlconnection.Open();
                    sqlcommand.ExecuteNonQuery();
                }
                catch(SqlException ex)
                {
                    throw new Exception("Error al eliminar de sql", ex);
                }
                catch(Exception ex)
                {
                    throw new Exception("Error general", ex);
                }

            }

        }


        //METODO PARA OBTENER LIBRO POR ID...
        public Libro ObtenerPorId(int id)
        {
            Libro libro = new Libro();
            using (SqlConnection sqlconnection = new SqlConnection(_connectionString))
            {
                try
                {
                    sqlconnection.Open();
                    string query = $"SELECT idLibro,titulo,autor,genero,copias FROM Libros WHERE idLibro = {id};";
                    SqlCommand sqlcommand = new SqlCommand(query,sqlconnection);
                    SqlDataReader reader = sqlcommand.ExecuteReader();
                    if (reader.Read())
                    {
                        libro.idLibro = reader.GetInt32(0);
                        libro.titulo = reader.GetString(1);
                        libro.autor = reader.GetString(2);
                        libro.genero = reader.GetString(3);
                        libro.copias = reader.GetInt32(4);
                    }
                    else
                    {
                        return null;
                    }
                    
                   

                }
                catch(SqlException ex)
                {
                    throw new Exception("Error al recuperar elemento de la base de datos", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error general",ex);
                }

                return libro;

            }
        }


        //METODO PARA EDITAR LIBRO...
        public void EditarLibro(Libro objLibro)
        {
            using (SqlConnection sqlconnection = new SqlConnection(_connectionString))
            {
                try
                {
                    sqlconnection.Open();
                    //ESTE CODIGO ES MEJOR QUE LA INTERPOLACION PORQUE EVITA UN ATAQUE POR SQLINJECTION...
                    string query = "UPDATE Libros SET titulo = @titulo, autor = @autor, genero = @genero, copias = @copias WHERE idLibro = @idLibro";

                    using (SqlCommand sqlcommand = new SqlCommand(query, sqlconnection))
                    {
                        sqlcommand.Parameters.AddWithValue("@titulo", objLibro.titulo);
                        sqlcommand.Parameters.AddWithValue("@autor", objLibro.autor);
                        sqlcommand.Parameters.AddWithValue("@genero", objLibro.genero);
                        sqlcommand.Parameters.AddWithValue("@copias", objLibro.copias);
                        sqlcommand.Parameters.AddWithValue("@idLibro", objLibro.idLibro);

                        sqlcommand.ExecuteNonQuery();
                    }
                    
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al editar elemento en la base de datos", ex);
                }
                catch(Exception ex)
                {
                    throw new Exception("Error general", ex);
                }

            }
        }




    }
}
