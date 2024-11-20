using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventosPeruBack.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using EventosPeruBack.Utils;

namespace EventosPeruBack.Dao
{
    public class EventoDao
    {
        string connectionString = ConfigurationManager.ConnectionStrings["EventosPeruDB"].ConnectionString;

        public List<Evento> ListarTodos()
        {
            List<Evento> eventos = new List<Evento>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT id, nombre_evento, descripcion, fecha FROM Evento";

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Evento evento = new Evento();

                        evento.id = (int)reader["id"];
                        evento.nombreEvento = (string)reader["nombre_evento"];
                        evento.descripcion = (string)reader["descripcion"];
                        evento.fecha = (DateTime)reader["fecha"];

                        eventos.Add(evento);
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

            return eventos;
        }

        public string Mantenimiento(Evento evento, int accion)
        {
            Mail mail = new Mail();

            string response = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "USP_Mantenimiento_Evento";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", evento.id == 0 ? 0 : evento.id);
                    command.Parameters.AddWithValue("@nombreEvento", evento.nombreEvento == null ? "" : evento.nombreEvento);
                    command.Parameters.AddWithValue("@descripcion", evento.descripcion == null ? "" : evento.descripcion);
                    command.Parameters.AddWithValue("@fecha", evento.fecha == null ? DateTime.Today : evento.fecha);
                    command.Parameters.AddWithValue("@accion", accion);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        response = reader.GetString(0);
                    }

                    reader.Close();

                    if (response == "OK" && accion == 1)
                    {
                        List<Estudiante> estudiantes = new EstudianteDao().ListarTodos();

                        foreach (Estudiante estudiante in estudiantes)
                        {
                            string messageBody = "Hola " + estudiante.nombre + " \n " +
                                "Se ha creado el evento " + evento.nombreEvento + " para el dia " + DateTime.Today.ToString("dd/MM/yyyy") + " \n " +
                                evento.descripcion + " \n " +
                                "Te esperamos";

                            mail.Send("dev.crv1@gmail.com", "mddg kvex qnsm hagq", "Eventos PERU", estudiante.email, "Notificacion", messageBody, "");
                        }
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