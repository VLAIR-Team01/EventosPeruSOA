using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using EventosPeruBack.Models;
using EventosPeruBack.Utils;
using System.Data;
using System.Data.SqlClient;

namespace EventosPeruBack.Dao
{
    public class UsuarioDao
    {
        string connectionString = ConfigurationManager.ConnectionStrings["EventosPeruDB"].ConnectionString;

        public string Mantenimiento(Usuario usuario, int accion)
        {
            Crypto crypto = new Crypto();
            string response = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "USP_Mantenimiento_Usuario";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", usuario.id == 0 ? 0 : usuario.id);
                    command.Parameters.AddWithValue("@contraseña", usuario.contrasena == null ? "" : crypto.CifrarConSHA256(usuario.contrasena));
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