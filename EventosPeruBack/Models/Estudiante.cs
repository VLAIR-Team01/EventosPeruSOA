using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventosPeruBack.Models
{
    public class Estudiante
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public int idUsuario { get; set; }
        public Usuario usuario { get; set; }

        public Estudiante()
        {
            usuario = new Usuario();
        }
    }
}