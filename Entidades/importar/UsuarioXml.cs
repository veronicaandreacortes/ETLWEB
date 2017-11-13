using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Entidades.importar
{
    [XmlInclude(typeof(UsuarioXml))]
    public class UsuarioXml
    {
        [XmlElement("correo")]
        public string correo { get; set; }
        [XmlElement("clave")]
        public string clave { get; set; }
        [XmlElement("nombrecompleto")]
        public string nombrecompleto { get; set; }
        [XmlElement("celular")]
        public string celular { get; set; }

        [XmlArray("perfiles")]
        [XmlArrayItem("perfil", Type = typeof(PerfilIXml))]
        public PerfilIXml[] perfiles { get; set; }

    }
}
