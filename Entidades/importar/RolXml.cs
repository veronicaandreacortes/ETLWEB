using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Entidades.importar
{
    [XmlInclude(typeof(RolXml))]
    public class RolXml
    {
        [XmlElement("idrol")]
        public int idrol { get; set; }
        [XmlElement("desrol")]
        public string desrol { get; set; }

        [XmlArray("granulos")]
        [XmlArrayItem("granulo", Type = typeof(GranuloXml))]
        public GranuloXml[] granulos { get; set; }

    }
}
