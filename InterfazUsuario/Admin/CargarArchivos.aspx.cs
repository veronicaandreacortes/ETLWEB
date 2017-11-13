using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using InterfazUsuario.ClienteRest;
using System.Xml;
using Entidades;
using InterfazUsuario.Codigo;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using Entidades.importar;


namespace InterfazUsuario.Admin
{
    public partial class CargarArchivos : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public Usuario InsertarUsuario(UsuarioXml usuarioXml)
        {
            Usuario usuario = new Usuario();
            usuario.correo = usuarioXml.correo;
            usuario.nombrecompleto = usuarioXml.nombrecompleto;
            usuario.clave = usuarioXml.clave;
            usuario.celular = usuarioXml.celular;

            return ClienteRestServicio.Instancia.CrearUsuario(usuario);
        }

        public Entidades.Roles InsertarRol(RolXml rolXml)
        {
            Entidades.Roles rol = new Entidades.Roles();
            rol.desrol = rolXml.desrol;

            // PRECAUCION
            var resultado = ClienteRestServicio.Instancia.BuscarRoles(rol);
            if (resultado == null || resultado.Count == 0)
            {
                return ClienteRestServicio.Instancia.CrearRol(rol);
            }
            else
            {
                return resultado[0];
            }
        }

        public Permisos InsertarPermiso(PermisoXml permisoXml)
        {
            Permisos permiso = new Permisos();
            permiso.despermiso = permisoXml.despermiso;

            // PRECAUCION
            //var resultado = ClienteRestServicio.Instancia.Buscar(permiso);
            //if (resultado == null || resultado.Count == 0)
            //{
            return ClienteRestServicio.Instancia.CrearPermiso(permiso);
            //}
            //else
            //{
                //return resultado[0];
            //}
        }

        public Perfiles InsertarPerfil(Usuario usuario, Entidades.Roles rol)
        {
            Perfiles perfil = new Perfiles();
            perfil.idrol = rol.idrol;
            perfil.idusuario = usuario.idusuario;

            return ClienteRestServicio.Instancia.CrearPerfil(perfil);
        }


        public Granulos InsertarGranulo(Entidades.Roles rol, Permisos permiso)
        {
            Granulos granulo = new Granulos();
            granulo.idrol = rol.idrol;
            granulo.idpermiso = permiso.idpermiso;

            return ClienteRestServicio.Instancia.CrearGranulo(granulo);
        }

        protected void CargarXML_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string filename = Path.GetFileName(FileUpload1.FileName);
                string rutaArchivoGuardo = Server.MapPath("~/archivosusuario/") + filename;
                FileUpload1.SaveAs(rutaArchivoGuardo);
                CargarXML(rutaArchivoGuardo);
            }
            else
            {
                LabelMensaje.Text = "No hay archivo para procesar";
            }
        }
        public void CargarXML(String path)
        {
            string xml = File.ReadAllText(path, Encoding.UTF8);

            ArchivoXml objetoImportacion = ObjectToXML<ArchivoXml>(xml, typeof(ArchivoXml));

            if (objetoImportacion != null)
            {
                if (objetoImportacion.usuarios != null && objetoImportacion.usuarios.Length > 0)
                {
                    foreach (var usuarioXml in objetoImportacion.usuarios)
                    {
                        Usuario usuario = InsertarUsuario(usuarioXml);
                        foreach (var perfilXml in usuarioXml.perfiles)
                        {
                            foreach (var rolXml in perfilXml.roles)
                            {
                                Entidades.Roles rol = InsertarRol(rolXml);
                                Perfiles perfil = InsertarPerfil(usuario, rol);

                                foreach (var granuloXml in rolXml.granulos)
                                {
                                    foreach (var permisoXml in granuloXml.permisos)
                                    {
                                        Permisos permiso = InsertarPermiso(permisoXml);
                                        Granulos granulo = InsertarGranulo(rol, permiso);
                                    }                                 
                                }
                            }                            
                        }
                    }

                    LabelMensaje.Text = string.Format("SE procecaron {0} registros.", objetoImportacion.usuarios.Length);
                }
            }
        }

        public T ObjectToXML<T>(string xml, Type objectType)
        {
            StringReader strReader = null;
            XmlSerializer serializer = null;
            XmlTextReader xmlReader = null;
            Object obj = null;
            try
            {
                strReader = new StringReader(xml);
                serializer = new XmlSerializer(objectType);
                xmlReader = new XmlTextReader(strReader);
                return (T)serializer.Deserialize(xmlReader);
            }
            catch (Exception exc)
            {
                LabelMensaje.Text = "Ocurrió un error" + exc.Message;
            }
            finally
            {
                if (xmlReader != null)
                {
                    xmlReader.Close();
                }
                if (strReader != null)
                {
                    strReader.Close();
                }
            }
            return default(T);
        }
    }
}


/*XmlDocument DocumentoAbierto = new XmlDocument();
DocumentoAbierto.Load(path);
XmlNodeList nodoListaUsuarios = DocumentoAbierto.GetElementsByTagName("usuarios");
foreach (XmlElement nodoUsuarioContenedor in nodoListaUsuarios)
{
    XmlNodeList nodoUsuario = ((XmlElement)nodoUsuarioContenedor).GetElementsByTagName("usuario");
    foreach (XmlElement elementosUsuario in nodoUsuario)
    {
        Usuario usuario = new Usuario();
        usuario.correo = elementosUsuario.GetElementsByTagName("correo")[0].InnerText;
        usuario.nombrecompleto = elementosUsuario.GetElementsByTagName("nombrecompleto")[0].InnerText;
        usuario.clave = elementosUsuario.GetElementsByTagName("clave")[0].InnerText;
        usuario.celular = elementosUsuario.GetElementsByTagName("celular")[0].InnerText;
        Insertar(usuario);
    }
}*/