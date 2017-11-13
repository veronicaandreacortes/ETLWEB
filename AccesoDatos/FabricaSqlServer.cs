using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace AccesoDatos
{
    public class FabricaSqlServer : IFabricaAccesoDatos
    {
        private string cadenaConexion;
        private int tiempoDeEspera;

        public FabricaSqlServer()
        {
            cadenaConexion = ConfigurationManager.ConnectionStrings["BD_Servicios_Conexion"].ConnectionString;
            tiempoDeEspera = Convert.ToInt32(ConfigurationManager.AppSettings["TiempoDeEspera"]);
        }

        public ISeguridadAccesoDatos SeguridadAccesoDatos
        {
            get { return new SeguridadAccesoDatos(cadenaConexion, tiempoDeEspera); }
        }
    }
}
