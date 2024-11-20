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
    /// Descripción breve de EventoService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class EventoService : System.Web.Services.WebService
    {

        [WebMethod]
        public List<Evento> ListarTodos()
        {
            List<Evento> eventos = new EventoDao().ListarTodos();
            return eventos;
        }

        [WebMethod]
        public GenericApiResponse<object> Registrar(string nombreEvento, string descripcion, DateTime fecha)
        {
            GenericApiResponse<object> response = new GenericApiResponse<object>();
            EventoDao eventoDao = new EventoDao();

            Evento evento = new Evento()
            {
                nombreEvento = nombreEvento,
                descripcion = descripcion,
                fecha = fecha
            };

            response.msg = eventoDao.Mantenimiento(evento, 1);

            return response;
        }

        [WebMethod]
        public GenericApiResponse<object> Modificar(int id, string nombreEvento, string descripcion, DateTime fecha)
        {
            GenericApiResponse<object> response = new GenericApiResponse<object>();
            EventoDao eventoDao = new EventoDao();

            Evento evento = new Evento()
            {
                id = id,
                nombreEvento = nombreEvento,
                descripcion = descripcion,
                fecha = fecha
            };

            response.msg = eventoDao.Mantenimiento(evento, 2);

            return response;
        }

        [WebMethod]
        public GenericApiResponse<object> Eliminar(int id)
        {
            GenericApiResponse<object> response = new GenericApiResponse<object>();
            EventoDao eventoDao = new EventoDao();

            Evento evento = new Evento()
            {
                id = id
            };

            response.msg = eventoDao.Mantenimiento(evento, 3);

            return response;
        }
    }
}
