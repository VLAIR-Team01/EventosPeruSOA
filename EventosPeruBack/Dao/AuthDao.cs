using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using EventosPeruBack.Models;
using EventosPeruBack.Utils;

namespace EventosPeruBack.Dao
{
    public class AuthDao
    {
        string connectionString = ConfigurationManager.ConnectionStrings["EventosPeruDB"].ConnectionString;

        public Usuario Autenticar(string nombreUsuario, string contrasena)
        {
            Usuario usuario = null;
            Crypto crypto = new Crypto();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT U.id, nombre_usuario,contraseña, id_rol, nombre AS nombreRol " +
                                    "FROM Usuario U INNER JOIN Rol R ON U.id_rol = R.ID " +
                                    "WHERE nombre_usuario = @nombreUsuario and contraseña = @contrasena";
                    
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                    command.Parameters.AddWithValue("@contrasena", crypto.CifrarConSHA256(contrasena));

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Rol rol = new Rol();
                        usuario = new Usuario();
                        usuario.id = (int)reader["id"];
                        usuario.nombreUsuario = (string)reader["nombre_usuario"];
                        usuario.contrasena = (string)reader["contraseña"];
                        usuario.idRol = (int)reader["id_rol"];
                        rol.id = (int)reader["id_rol"];
                        rol.nombre = (string)reader["nombreRol"];
                        usuario.rol = rol;
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

            return usuario;
        }
    }
}