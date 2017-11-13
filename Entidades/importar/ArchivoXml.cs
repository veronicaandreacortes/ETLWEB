using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Entidades.importar
{
    [XmlRoot("importacion")]
    public class ArchivoXml
    {
        [XmlArray("usuarios")]
        [XmlArrayItem("usuario", Type = typeof(UsuarioXml))]
        public UsuarioXml[] usuarios { get; set; }

    }
}
