using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using RestSharp;
using Entidades;

namespace InterfazUsuario.ClienteRest
{
    public class ClienteRestServicio
    {
        private static ClienteRestServicio _instancia;

        private string urlServicio;

        private ClienteRestServicio()
        {
            urlServicio = ConfigurationManager.AppSettings["ServicioUrl"];
        }

        public static ClienteRestServicio Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new ClienteRestServicio();
                }
                return _instancia;
            }
        }

        public LoginRespuesta Login(LoginPeticion login)
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("login", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(login);

            IRestResponse<LoginRespuesta> response = client.Execute<LoginRespuesta>(request);
            return response.Data; 
        }

        public Usuario ObtenerUsuarioPorCorreo(string correo)
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("ObtenerUsuarioPorCorreo/{correo}", Method.GET);
            request.AddUrlSegment("correo", correo);

            IRestResponse<Usuario> response = client.Execute<Usuario>(request);
            return response.Data;
        }

        public IList<Usuario> ObtenerListaUsuarios()
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("ObtenerListaUsuarios", Method.GET);

            IRestResponse<List<Usuario>> response = client.Execute<List<Usuario>>(request);
            return response.Data;
        }

        public IList<Usuario> BuscarUsuarios(Usuario usuario)
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("BuscarUsuarios?n={nombrecompleto}&e={correo}&c={celular}", Method.GET);
            request.AddUrlSegment("nombrecompleto", !string.IsNullOrWhiteSpace(usuario.nombrecompleto) ? usuario.nombrecompleto : string.Empty);
            request.AddUrlSegment("correo", !string.IsNullOrWhiteSpace(usuario.correo) ? usuario.correo : string.Empty);
            request.AddUrlSegment("celular", !string.IsNullOrWhiteSpace(usuario.celular) ? usuario.celular : string.Empty);

            IRestResponse<List<Usuario>> response = client.Execute<List<Usuario>>(request);
            return response.Data;
        }
        
        public IList<Permisos> ObtenerListaPermisosPorUsuario(String correo)
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("ObtenerListaPermisosPorUsuario/{correo}", Method.GET);
            request.AddUrlSegment("correo", correo);

            IRestResponse<List<Permisos>> response = client.Execute<List<Permisos>>(request);
            return response.Data;
        }

       
        #region Usuario

        public Usuario CrearUsuario(Usuario usuario)
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("CrearUsuario", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(usuario);

            IRestResponse<Usuario> response = client.Execute<Usuario>(request);
            return response.Data; 
        }

        public Usuario ActualizarUsuario(Usuario usuario)
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("ActualizarUsuario", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(usuario);

            IRestResponse<Usuario> response = client.Execute<Usuario>(request);
            return response.Data; 
        }

        public void EliminarUsuario(string id)
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("EliminarUsuario", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(id);

            client.Execute<Usuario>(request);
        }

        public Usuario ObtenerUsuarioPorId(string id)
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("ObtenerUsuarioPorId/{id}", Method.GET);
            request.AddUrlSegment("id", id);

            IRestResponse<Usuario> response = client.Execute<Usuario>(request);
            return response.Data;
        }
        #endregion
        
        #region Roles

        public IList<Roles> ObtenerListaRoles()
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("ObtenerListaRoles", Method.GET);

            IRestResponse<List<Roles>> response = client.Execute<List<Roles>>(request);
            return response.Data;
        }

        public Roles CrearRol(Roles rol)
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("CrearRol", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(rol);

            IRestResponse<Roles> response = client.Execute<Roles>(request);
            return response.Data;
        }

        public Roles ActualizarRol(Roles rol)
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("ActualizarRol", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(rol);

            IRestResponse<Roles> response = client.Execute<Roles>(request);
            return response.Data;
        }

        public Roles ObtenerRolPorId(string id)
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("ObtenerRolPorId/{id}", Method.GET);
            request.AddUrlSegment("id", id);

            IRestResponse<Roles> response = client.Execute<Roles>(request);
            return response.Data;
        }
        public IList<Roles> BuscarRoles(Roles rol)
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("BuscarRoles?n={desrol}", Method.GET);
            request.AddUrlSegment("desrol", !string.IsNullOrWhiteSpace(rol.desrol) ? rol.desrol : string.Empty);
       
            IRestResponse<List<Roles>> response = client.Execute<List<Roles>>(request);
            return response.Data;
        }

        public void EliminarRol(string id)
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("EliminarRol", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(id);

            client.Execute<Roles>(request);
        }
        #endregion

        #region Permisos

        public IList<Permisos> ObtenerListaPermisos()
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("ObtenerListaPermisos", Method.GET);

            IRestResponse<List<Permisos>> response = client.Execute<List<Permisos>>(request);
            return response.Data;
        }
       
        public Permisos CrearPermiso(Permisos Permiso)
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("CrearPermiso", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(Permiso);

            IRestResponse<Permisos> response = client.Execute<Permisos>(request);
            return response.Data;
        }

        public Permisos ActualizarPermiso(Permisos Permiso)
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("ActualizarPermiso", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(Permiso);

            IRestResponse<Permisos> response = client.Execute<Permisos>(request);
            return response.Data;
        }

        public Permisos ObtenerPermisoPorId(string id)
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("ObtenerPermisoPorId/{id}", Method.GET);
            request.AddUrlSegment("id", id);

            IRestResponse<Permisos> response = client.Execute<Permisos>(request);
            return response.Data;
        }

        public IList<Permisos> BuscarPermiso(Permisos permiso)
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("BuscarPermiso?n={despermiso}", Method.GET);
            request.AddUrlSegment("desrol", !string.IsNullOrWhiteSpace(permiso.despermiso) ? permiso.despermiso : string.Empty);

            IRestResponse<List<Permisos>> response = client.Execute<List<Permisos>>(request);
            return response.Data;
        }

        public void EliminarPermiso(string id)
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("EliminarPermiso", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(id);

            client.Execute<Permisos>(request);
        }
        #endregion

        #region Perfiles
        public Perfiles CrearPerfil(Perfiles Perfil)
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("CrearPerfil", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(Perfil);

            IRestResponse<Perfiles> response = client.Execute<Perfiles>(request);
            return response.Data;
        }

        public Perfiles ActualizarPerfil(Perfiles Perfil)
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("ActualizarPerfil", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(Perfil);

            IRestResponse<Perfiles> response = client.Execute<Perfiles>(request);
            return response.Data;
        }

        public Perfiles ObtenerPerfilPorId(string id)
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("ObtenerPerfilPorId/{id}", Method.GET);
            request.AddUrlSegment("id", id);

            IRestResponse<Perfiles> response = client.Execute<Perfiles>(request);
            return response.Data;
        }

        public IList<Perfiles> BuscarPerfil(Perfiles perfil)
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("BuscarPerfil?n={idusuario}&e={idperfil}", Method.GET);
            request.AddUrlSegment("idusuario", !string.IsNullOrWhiteSpace(perfil.idusuario.ToString()) ? perfil.idusuario.ToString() : string.Empty);
            request.AddUrlSegment("idperfil", !string.IsNullOrWhiteSpace(perfil.idrol.ToString()) ? perfil.idrol.ToString() : string.Empty);
            
            IRestResponse<List<Perfiles>> response = client.Execute<List<Perfiles>>(request);
            return response.Data;
        }

        public IList<Perfiles> ObtenerListaPerfiles()
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("ObtenerListaPerfiles", Method.GET);

            IRestResponse<List<Perfiles>> response = client.Execute<List<Perfiles>>(request);
            return response.Data;
        }

        public void EliminarPerfil(string id)
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("EliminarPerfil", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(id);

            client.Execute<Perfiles>(request);
        }

        #endregion

        #region Granulos
        public Granulos CrearGranulo(Granulos Granulo)
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("CrearGranulos", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(Granulo);

            IRestResponse<Granulos> response = client.Execute<Granulos>(request);
            return response.Data;
        }

        public Granulos ActualizarGranulo(Granulos Granulo)
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("ActualizarGranulo", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(Granulo);

            IRestResponse<Granulos> response = client.Execute<Granulos>(request);
            return response.Data;
        }

        public Granulos ObtenerGranuloPorId(string id)
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("ObtenerGranuloPorId/{id}", Method.GET);
            request.AddUrlSegment("id", id);

            IRestResponse<Granulos> response = client.Execute<Granulos>(request);
            return response.Data;
        }

        public IList<Granulos> BuscarGranulo(Granulos granulo)
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("BuscarGranulo?n={idrol}&e={idpermiso}", Method.GET);
            request.AddUrlSegment("idrol", !string.IsNullOrWhiteSpace(granulo.idrol.ToString()) ? granulo.idrol.ToString() : string.Empty);
            request.AddUrlSegment("idpermiso", !string.IsNullOrWhiteSpace(granulo.idpermiso.ToString()) ? granulo.idpermiso.ToString() : string.Empty);

            IRestResponse<List<Granulos>> response = client.Execute<List<Granulos>>(request);
            return response.Data;
        }

        public IList<Granulos> ObtenerListaGranulos()
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("ObtenerListaGranulos", Method.GET);

            IRestResponse<List<Granulos>> response = client.Execute<List<Granulos>>(request);
            return response.Data;
        }

        public void EliminarGranulo(string id)
        {
            var client = new RestClient(urlServicio);

            var request = new RestRequest("EliminarGranulo", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(id);

            client.Execute<Granulos>(request);
        }

        #endregion
    }
}