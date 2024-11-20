using EventosPeruBack.Models;
using EventosPeruBack.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EventosPeruBack.Dao
{
    public class EstudianteDao
    {
        string connectionString = ConfigurationManager.ConnectionStrings["EventosPeruDB"].ConnectionString;

        public List<Estudiante> ListarTodos()
        {
            List<Estudiante> estudiantes = new List<Estudiante>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT E.id, E.nombre AS nombre_estudiante, email, telefono, " +
                        "id_usuario, id_rol, R.nombre AS nombre_rol FROM Estudiante E " +
                        "INNER JOIN Usuario U ON U.id = E.id_usuario " +
                        "INNER JOIN Rol R ON R.id = U.id_rol";

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Estudiante estudiante = new Estudiante();
                        Usuario usuario = new Usuario();
                        Rol rol = new Rol();

                        estudiante.id = (int)reader["id"];
                        estudiante.nombre = (string)reader["nombre_estudiante"];
                        estudiante.email = (string)reader["email"];
                        estudiante.telefono = (string)reader["telefono"];
                        usuario.id = (int)reader["id_usuario"];
                        rol.id = (int)reader["id_rol"];
                        rol.nombre = (string)reader["nombre_rol"];
                        usuario.rol = rol;
                        estudiante.usuario = usuario;

                        estudiantes.Add(estudiante);
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

            return estudiantes;
        }

        public string Mantenimiento(Estudiante estudiante, int accion)
        {
            Mail mail = new Mail();
            Crypto crypto = new Crypto();

            string response = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string password = "E" + DateTime.Now.ToString("yyMMddmmss");
                    string query = "USP_Mantenimiento_Estudiante";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", estudiante.id == 0 ? 0 : estudiante.id);
                    command.Parameters.AddWithValue("@nombre", estudiante.nombre == null ? "" : estudiante.nombre);
                    command.Parameters.AddWithValue("@email", estudiante.email == null ? "" : estudiante.email);
                    command.Parameters.AddWithValue("@telefono", estudiante.telefono == null ? "" : estudiante.telefono);
                    command.Parameters.AddWithValue("@idUsuario", estudiante.idUsuario == 0 ? 0 : estudiante.id);
                    command.Parameters.AddWithValue("@contraseña", crypto.CifrarConSHA256(password));
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
                        string messageBody = "Tu usuario es " + estudiante.email + " y tu contraseña es " + password;

                        mail.Send("dev.crv1@gmail.com", "mddg kvex qnsm hagq", "Eventos PERU", estudiante.email, "Acceso", messageBody, "");
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