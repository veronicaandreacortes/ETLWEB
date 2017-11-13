using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Entidades.importar
{
    [XmlInclude(typeof(GranuloXml))]
    public class GranuloXml
    {
        [XmlArray("permisos")]
        [XmlArrayItem("permiso", Type = typeof(PermisoXml))]
        public PermisoXml[] permisos { get; set; }
    }
}
