﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventosPeruBack.Models
{
    public class Evento
    {
        public int id { get; set; }
        public string nombreEvento { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha { get; set; }
    }
}