using EventosPeruBack.Dao;
using EventosPeruBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace EventosPeruBack.Services
{
    /// <summary>
    /// Descripción breve de InstitucionService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class InstitucionService : System.Web.Services.WebService
    {

        [WebMethod]
        public List<Institucion> ListarTodos()
        {
            List<Institucion> instituciones = new InstitucionDao().ListarTodos();
            return instituciones;
        }

        [WebMethod]
        public GenericApiResponse<object> Registrar(string nombre, string descripcion, string direccion, string email)
        {
            GenericApiResponse<object> response = new GenericApiResponse<object>();
            InstitucionDao institucionDao = new InstitucionDao();

            Institucion institucion = new Institucion()
            {
                nombre = nombre,
                descripcion = descripcion,
                direccion = direccion,
                email = email
            };

            response.msg = institucionDao.Mantenimiento(institucion, 1);

            return response;
        }

        [WebMethod]
        public GenericApiResponse<object> Modificar(int id, string nombre, string descripcion, string direccion, string email)
        {
            GenericApiResponse<object> response = new GenericApiResponse<object>();
            InstitucionDao institucionDao = new InstitucionDao();

            Institucion institucion = new Institucion()
            {
                id = id,
                nombre = nombre,
                descripcion = descripcion,
                direccion = direccion,
                email = email
            };

            response.msg = institucionDao.Mantenimiento(institucion, 2);

            return response;
        }

        [WebMethod]
        public GenericApiResponse<object> Eliminar(int id)
        {
            GenericApiResponse<object> response = new GenericApiResponse<object>();
            InstitucionDao institucionDao = new InstitucionDao();

            Institucion institucion = new Institucion()
            {
                id = id
            };

            response.msg = institucionDao.Mantenimiento(institucion, 3);

            return response;
        }
    }
}
