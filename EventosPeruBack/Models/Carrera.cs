using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventosPeruBack.Models
{
    public class Carrera
    {
        public int id { get; set; }
        public string nombreCarrera { get; set; }
        public int idInstitucion { get; set; }
    }
}