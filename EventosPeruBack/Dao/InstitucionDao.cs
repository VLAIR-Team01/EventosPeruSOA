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
    public class InstitucionDao
    {
        string connectionString = ConfigurationManager.ConnectionStrings["EventosPeruDB"].ConnectionString;

        public List<Institucion> ListarTodos()
        {
            List<Institucion> instituciones = new List<Institucion>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT I.id, I.nombre AS nombre_institucion, descripcion, direccion, " +
                        "email, id_usuario, id_rol, R.nombre AS nombre_rol FROM Institucion I " +
                        "INNER JOIN Usuario U ON U.id = I.id_usuario " +
                        "INNER JOIN Rol R ON R.id = U.id_rol";

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Institucion institucion = new Institucion();
                        Usuario usuario = new Usuario();
                        Rol rol = new Rol();

                        institucion.id = (int)reader["id"];
                        institucion.nombre = (string)reader["nombre_institucion"];
                        institucion.descripcion = (string)reader["descripcion"];
                        institucion.direccion = (string)reader["direccion"];
                        institucion.email = (string)reader["email"];
                        usuario.id = (int)reader["id_usuario"];
                        rol.id = (int)reader["id_rol"];
                        rol.nombre = (string)reader["nombre_rol"];
                        usuario.rol = rol;
                        institucion.usuario = usuario;

                        instituciones.Add(institucion);
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

            return instituciones;
        }

        public string Mantenimiento(Institucion institucion, int accion)
        {
            Mail mail = new Mail();
            Crypto crypto = new Crypto();

            string response = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string password = "E" + DateTime.Now.ToString("yyMMddmmss");
                    string query = "USP_Mantenimiento_Institucion";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", institucion.id == 0 ? 0 : institucion.id);
                    command.Parameters.AddWithValue("@nombre", institucion.nombre == null ? "" : institucion.nombre);
                    command.Parameters.AddWithValue("@descripcion", institucion.descripcion == null ? "" : institucion.descripcion);
                    command.Parameters.AddWithValue("@direccion", institucion.direccion == null ? "" : institucion.direccion);
                    command.Parameters.AddWithValue("@email", institucion.email == null ? "" : institucion.email);
                    command.Parameters.AddWithValue("@idUsuario", institucion.idUsuario == 0 ? 0 : institucion.id);
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
                        string messageBody = "Tu usuario es " + institucion.email + " y tu contraseña es " + password;

                        mail.Send("dev.crv1@gmail.com", "mddg kvex qnsm hagq", "Eventos PERU", institucion.email, "Acceso", messageBody, "");
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