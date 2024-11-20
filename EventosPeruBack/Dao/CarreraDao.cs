using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventosPeruBack.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EventosPeruBack.Dao
{
    public class CarreraDao
    {
        string connectionString = ConfigurationManager.ConnectionStrings["EventosPeruDB"].ConnectionString;

        public List<Carrera> ListarPorInstitucion(int idInstitucion)
        {
            List<Carrera> carreras = new List<Carrera>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT id, nombre_carrera, id_institucion FROM Carrera WHERE id_institucion = @id";
                    
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", idInstitucion);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Carrera carrera = new Carrera();

                        carrera.id = (int)reader["id"];
                        carrera.nombreCarrera = (string)reader["nombre_carrera"];
                        carrera.idInstitucion = (int)reader["id_institucion"];

                        carreras.Add(carrera);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    connection.Close();
                }
            }

            return carreras;
        }

        public string Mantenimiento(Carrera carrera, int accion)
        {
            string response = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "USP_MantenimientoCarrera";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", carrera.id == 0 ? 0 : carrera.id);
                    command.Parameters.AddWithValue("@nombreCarrera", carrera.nombreCarrera == null ? "" : carrera.nombreCarrera);
                    command.Parameters.AddWithValue("@idInstitucion", carrera.idInstitucion == 0 ? 0 : carrera.idInstitucion);
                    command.Parameters.AddWithValue("@accion", accion);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        response = reader.GetString(0);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    connection.Close();
                }
            }

            return response;
        }
    }
}