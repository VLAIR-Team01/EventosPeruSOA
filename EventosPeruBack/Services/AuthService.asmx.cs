using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using EventosPeruBack.Dao;
using EventosPeruBack.Models;

namespace EventosPeruBack.Services
{
    /// <summary>
    /// Descripción breve de AuthService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class AuthService : System.Web.Services.WebService
    {

        [WebMethod]
        public GenericApiResponse<Usuario> Autenticar(string nombreUsuario, string contrasena)
        {
            AuthDao authDao = new AuthDao();
            GenericApiResponse<Usuario> response = new GenericApiResponse<Usuario>();
            response.data = authDao.Autenticar(nombreUsuario, contrasena);

            if (response.data == null)
            {
                response.status = 404;
                response.msg = "Nombre de usuario y/o contraseña incorrecto.";
            }

            return response;
        }

        [WebMethod]
        public GenericApiResponse<object> CambiarContrasenia(int id, string contrasena)
        {
            GenericApiResponse<object> response = new GenericApiResponse<object>();
            UsuarioDao usuarioDao = new UsuarioDao();

            Usuario usuario = new Usuario()
            {
                id = id,
                contrasena = contrasena
            };

            response.msg = usuarioDao.Mantenimiento(usuario, 2);

            return response;
        }
    }
}
