using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Entidades.importar
{
    [XmlInclude(typeof(PermisoXml))]
    public class PermisoXml
    {
        [XmlElement("idpermiso")]
        public int idpermiso { get; set; }
        [XmlElement("despermiso")]
        public string despermiso { get; set; }
    }
}
