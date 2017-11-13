using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using Servicios;
using Entidades;

namespace VeronicaRestService
{
    // Start the service and browse to http://<machine_name>:<port>/Service1/help to view the service's generated help page
    // NOTE: By default, a new instance of the service is created for each call; change the InstanceContextMode to Single if you want
    // a single instance of the service to process all calls.	
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ServiciosServicioTelefonico
    {
        private Servicio servicio = new Servicio();

        ///////////////////////////////////

        #region usuario

        [WebInvoke(UriTemplate = "Login", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public LoginRespuesta Login(LoginPeticion login)
        {
            return servicio.Login(login);
        }

        [WebGet(UriTemplate = "ObtenerListaUsuarios", ResponseFormat = WebMessageFormat.Json)]
        public List<Usuario> ObtenerListaUsuarios()
        {
            return servicio.ObtenerListaUsuarios().ToList();
        }

        [WebGet(UriTemplate = "BuscarUsuarios?n={nombrecompleto}&e={correo}&c={celular}", ResponseFormat = WebMessageFormat.Json)]
        public IList<Usuario> BuscarUsuarios(string nombrecompleto, string correo, string celular)
        {
            Usuario usuario = new Usuario() { nombrecompleto = nombrecompleto, correo = correo, celular = celular };
            return servicio.BuscarUsuarios(usuario);
        }

        [WebInvoke(UriTemplate = "CrearUsuario", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Usuario CrearUsuario(Usuario usuario)
        {
            return servicio.Crear(usuario);
        }

        [WebGet(UriTemplate = "ObtenerUsuarioPorCorreo/{correo}", ResponseFormat = WebMessageFormat.Json)]
        public Usuario ObtenerUsuarioPorCorreo(string correo)
        {
            return servicio.ObtenerUsuarioPorCorreo(correo);
        }

        [WebGet(UriTemplate = "ObtenerUsuarioPorId/{id}", ResponseFormat = WebMessageFormat.Json)]
        public Usuario ObtenerUsuarioPorId(string id)
        {
            int usuarioId = 0;
            usuarioId = int.Parse(id);
            return servicio.ObtenerUsuarioPorId(usuarioId);
        }

        [WebInvoke(UriTemplate = "ActualizarUsuario", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Usuario ActualizaUsuario(Usuario usuario)
        {
            return servicio.Actualizar(usuario);
        }

        [WebInvoke(UriTemplate = "EliminarUsuario", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void EliminarUsuario(string id)
        {
            int usuarioId = 0;
            usuarioId = int.Parse(id);
            servicio.EliminarUsuario(usuarioId);
        }
        #endregion

        #region roles


        [WebGet(UriTemplate = "ObtenerListaRoles", ResponseFormat = WebMessageFormat.Json)]
        public IList<Roles> ObtenerListaroles()
        {
            return servicio.ObtenerListaroles().ToList();
        }

        [WebInvoke(UriTemplate = "CrearRol", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Roles CrearRol(Roles rol)
        {
            return servicio.Crear(rol);
        }

        [WebGet(UriTemplate = "ObtenerRolPorId/{id}", ResponseFormat = WebMessageFormat.Json)]
        public Roles ObtenerRolPorId(string id)
        {
            int rolId = 0;
            rolId = int.Parse(id);
            return servicio.ObtenerRolPorId(rolId);
        }

        [WebInvoke(UriTemplate = "ActualizarRol", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Roles ActualizarRol(Roles rol)
        {
            return servicio.Actualizar(rol);
        }

        [WebInvoke(UriTemplate = "EliminarRol", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void EliminarRol(int id)
        {
            servicio.EliminarRol(id);
        }
        [WebGet(UriTemplate = "BuscarRoles?n={desrol}", ResponseFormat = WebMessageFormat.Json)]
        public IList<Roles> BuscarRoles(string desrol)
        {
            Roles rol = new Roles() { desrol = desrol };
            return servicio.BuscarRoles(rol);
        }

        #endregion

        #region Perfiles


        [WebGet(UriTemplate = "ObtenerListaPerfiles", ResponseFormat = WebMessageFormat.Json)]
        public IList<Perfiles> ObtenerListaPerfiles()
        {
            return servicio.ObtenerListaPerfiles().ToList();
        }

        [WebInvoke(UriTemplate = "CrearPerfil", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Perfiles CrearPerfil(Perfiles Perfil)
        {
            return servicio.Crear(Perfil);
        }

        [WebGet(UriTemplate = "ObtenerPerfilPorId/{id}", ResponseFormat = WebMessageFormat.Json)]
        public Perfiles ObtenerPerfilPorId(string id)
        {
            int perfilId = 0;
            perfilId = int.Parse(id);
            return servicio.ObtenerPerfilPorId(perfilId);
        }

        [WebInvoke(UriTemplate = "ActualizaPerfil", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Perfiles ActualizaPerfil(Perfiles perfil)
        {
            return servicio.Actualizar(perfil);
        }

        [WebInvoke(UriTemplate = "EliminarPerfil", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void EliminarPerfil(int id)
        {
            servicio.EliminarPerfil(id);
        }

        [WebGet(UriTemplate = "BuscarPerfil?n={idusuario}&e={idrol}", ResponseFormat = WebMessageFormat.Json)]
        public IList<Perfiles> BuscarPerfil(int idusuario, int idrol)
        {
            Perfiles perfil = new Perfiles() { idusuario = idusuario, idrol = idrol };
            return servicio.BuscarPerfil(perfil);
        }
        #endregion

        #region Granulos

        [WebGet(UriTemplate = "ObtenerListaGranulos", ResponseFormat = WebMessageFormat.Json)]
        public IList<Granulos> ObtenerListaGranulos()
        {
            return servicio.ObtenerListaGranulos().ToList();
        }

        [WebInvoke(UriTemplate = "CrearGranulos", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Granulos CrearGranulos(Granulos Granulo)
        {
            return servicio.Crear(Granulo);
        }

        [WebGet(UriTemplate = "ObtenerGranuloPorId/{id}", ResponseFormat = WebMessageFormat.Json)]
        public Granulos ObtenerGranuloPorId(string id)
        {
            int granuloId = 0;
            granuloId = int.Parse(id);
            return servicio.ObtenerGranuloPorId(granuloId);
        }

        [WebInvoke(UriTemplate = "ActualizaGranulo", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Granulos ActualizaGranulo(Granulos granulo)
        {
            return servicio.Actualizar(granulo);
        }

        [WebInvoke(UriTemplate = "EliminarGranulo", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void EliminarGranulo(int id)
        {
            servicio.EliminarGranulo(id);
        }

        [WebGet(UriTemplate = "BuscarGranulo?n={idrol}&e={idpermiso}", ResponseFormat = WebMessageFormat.Json)]
        public IList<Granulos> BuscarGranulo(int idrol, int idpermiso)
        {
            Granulos granulo = new Granulos() { idrol = idrol, idpermiso = idpermiso };
            return servicio.BuscarGranulo(granulo);
        }

        #endregion

        #region Permisos

        [WebGet(UriTemplate = "ObtenerListaPermisosPorUsuario/{correo}", ResponseFormat = WebMessageFormat.Json)]
        IList<Permisos> ObtenerListaPermisosPorUsuario(String correo)
        {
            return servicio.ObtenerListaPermisosPorUsuario(correo).ToList();
        }

        [WebGet(UriTemplate = "ObtenerListaPermisos", ResponseFormat = WebMessageFormat.Json)]
        public IList<Permisos> ObtenerListaPermisos()
        {
            return servicio.ObtenerListaPermisos().ToList();
        }

        [WebInvoke(UriTemplate = "CrearPermiso", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Permisos CrearPermiso(Permisos permiso)
        {
            return servicio.Crear(permiso);
        }

        [WebGet(UriTemplate = "ObtenerPermisoPorId/{id}", ResponseFormat = WebMessageFormat.Json)]
        public Permisos ObtenerPermisoPorId(string id)
        {
            int permisoId = 0;
            permisoId = int.Parse(id);
            return servicio.ObtenerPermisoPorId(permisoId);
        }

        [WebInvoke(UriTemplate = "ActualizarPermiso", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Permisos ActualizarPermiso(Permisos permiso)
        {
            return servicio.Actualizar(permiso);
        }

        [WebInvoke(UriTemplate = "EliminarPermiso", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public  void EliminarPermiso(string id)
        {
                             
            int permisoId = 0;
            permisoId = int.Parse(id);
            servicio.EliminarUsuario(permisoId);
        }

        [WebGet(UriTemplate = "BuscarPermiso?n={despermiso}", ResponseFormat = WebMessageFormat.Json)]
        public IList<Permisos> BuscarPermiso(string despermiso)
        {
            Permisos permiso = new Permisos() { despermiso = despermiso };
            return servicio.BuscarPermiso(permiso);
        }
        #endregion
    }

  
}
