using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventosPeruBack.Models
{
    public class Institucion
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string direccion { get; set; }
        public string email { get; set; }
        public int idUsuario { get; set; }
        public Usuario usuario { get; set; }

        public Institucion()
        {
            usuario = new Usuario();
        }
    }
}