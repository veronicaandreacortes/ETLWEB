using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccesoDatos
{
    public interface IFabricaAccesoDatos
    {
        ISeguridadAccesoDatos SeguridadAccesoDatos { get; } 
    }
}
