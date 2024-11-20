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
    /// <summary>
    /// Descripción breve de CarreraService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class CarreraService : System.Web.Services.WebService
    {

        [WebMethod]
        public List<Carrera> ListarPorInstitucion(int idInstitucion)
        {
            List<Carrera> carreras = new CarreraDao().ListarPorInstitucion(idInstitucion);
            return carreras;
        }

        [WebMethod]
        public GenericApiResponse<object> Registrar(string nombreCarrera, int idInstitucion)
        {
            GenericApiResponse<object> response = new GenericApiResponse<object>();
            CarreraDao carreraDao = new CarreraDao();

            Carrera carrera = new Carrera()
            {
                nombreCarrera = nombreCarrera,
                idInstitucion = idInstitucion
            };

            response.msg = carreraDao.Mantenimiento(carrera, 1);

            return response;
        }

        [WebMethod]
        public GenericApiResponse<object> Modificar(int id, string nombreCarrera, int idInstitucion)
        {
            GenericApiResponse<object> response = new GenericApiResponse<object>();
            CarreraDao carreraDao = new CarreraDao();

            Carrera carrera = new Carrera()
            {
                id = id,
                nombreCarrera = nombreCarrera,
                idInstitucion = idInstitucion
            };

            response.msg = carreraDao.Mantenimiento(carrera, 2);

            return response;
        }

        [WebMethod]
        public GenericApiResponse<object> Eliminar(int id)
        {
            GenericApiResponse<object> response = new GenericApiResponse<object>();
            CarreraDao carreraDao = new CarreraDao();

            Carrera carrera = new Carrera()
            {
                id = id
            };

            response.msg = carreraDao.Mantenimiento(carrera, 3);

            return response;
        }
    }
}
