using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Entidades.importar
{
    [XmlInclude(typeof(PerfilIXml))]
    public class PerfilIXml
    {
        [XmlArray("roles")]
        [XmlArrayItem("rol", Type = typeof(RolXml))]
        public RolXml[] roles { get; set; }
    }
}
