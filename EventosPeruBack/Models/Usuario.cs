using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace EventosPeruBack.Models
{
    [Serializable]
    public class Usuario
    {
        public int id { get; set; }
        public string nombreUsuario { get; set; }
        [XmlIgnore]
        public string contrasena { get; set; }
        public int idRol { get; set; }

        // Relacion con el modelo Rol
        public Rol rol { get; set; }
    }
}