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
    /// Descripción breve de EstudianteService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class EstudianteService : System.Web.Services.WebService
    {

        [WebMethod]
        public List<Estudiante> ListarTodos()
        {
            List<Estudiante> estudiantes = new EstudianteDao().ListarTodos();
            return estudiantes;
        }

        [WebMethod]
        public GenericApiResponse<object> Registrar(string nombre, string email, string telefono)
        {
            GenericApiResponse<object> response = new GenericApiResponse<object>();
            EstudianteDao estudianteDao = new EstudianteDao();

            Estudiante estudiante = new Estudiante()
            {
                nombre = nombre,
                email = email,
                telefono = telefono
            };

            response.msg = estudianteDao.Mantenimiento(estudiante, 1);

            return response;
        }

        [WebMethod]
        public GenericApiResponse<object> Modificar(int id, string nombre, string email, string telefono)
        {
            GenericApiResponse<object> response = new GenericApiResponse<object>();
            EstudianteDao estudianteDao = new EstudianteDao();

            Estudiante estudiante = new Estudiante()
            {
                id = id,
                nombre = nombre,
                email = email,
                telefono = telefono
            };

            response.msg = estudianteDao.Mantenimiento(estudiante, 2);

            return response;
        }

        [WebMethod]
        public GenericApiResponse<object> Eliminar(int id)
        {
            GenericApiResponse<object> response = new GenericApiResponse<object>();
            EstudianteDao estudianteDao = new EstudianteDao();

            Estudiante estudiante = new Estudiante()
            {
                id = id
            };

            response.msg = estudianteDao.Mantenimiento(estudiante, 3);

            return response;
        }
    }
}
