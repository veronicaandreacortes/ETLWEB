using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class ServicioTelefonico
    {
        public int IdServicio { get; set; }
        public string Nombre { get; set; }
        public int IdPlan { get; set; }
        public int IdEstado { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int Vigencia { get; set; }
        public DateTime FechaInicio { get; set; }
    }
}
