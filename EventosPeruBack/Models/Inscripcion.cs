using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventosPeruBack.Models
{
    public class Inscripcion
    {
        public int id { get; set; }
        public int idUsuario { get; set; }
        public int idEvento { get; set; }
        public DateTime fechaInscripcion { get; set; }
    }
}