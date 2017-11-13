using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AccesoDatos;
using Entidades;

namespace Servicios
{
    public class Servicio
    {
        private IFabricaAccesoDatos fabricaAccesoDatos;

        public Servicio()
        {
            fabricaAccesoDatos = new FabricaSqlServer();
        }

        #region usuario

        public LoginRespuesta Login(LoginPeticion login)
        {
            return fabricaAccesoDatos.SeguridadAccesoDatos.Login(login);
        }

        public IList<Usuario> ObtenerListaUsuarios()
        {
            return fabricaAccesoDatos.SeguridadAccesoDatos.ObtenerListaUsuarios();
        }

        public IList<Usuario> BuscarUsuarios(Usuario usuario)
        {
            return fabricaAccesoDatos.SeguridadAccesoDatos.BuscarUsuarios(usuario);
        }

        public Usuario Crear(Usuario usuario)
        {
            return fabricaAccesoDatos.SeguridadAccesoDatos.Crear(usuario);
        }


        public Usuario ObtenerUsuarioPorCorreo(string correo)
        {
            return fabricaAccesoDatos.SeguridadAccesoDatos.ObtenerUsuarioPorCorreo(correo);
        }

        public Usuario ObtenerUsuarioPorId(int id)
        {
            return fabricaAccesoDatos.SeguridadAccesoDatos.ObtenerUsuarioPorId(id);
        }

        public Usuario Actualizar(Usuario usuario)
        {
            return fabricaAccesoDatos.SeguridadAccesoDatos.Actualizar(usuario);
        }
        public void EliminarUsuario(int id)
        {
            fabricaAccesoDatos.SeguridadAccesoDatos.EliminarUsuario(id);
        }
        #endregion

        #region Roles

        public IList<Roles> ObtenerListaroles()
        {
            return fabricaAccesoDatos.SeguridadAccesoDatos.ObtenerListaroles();
        }
        public Roles Crear(Roles rol)
        {
            return fabricaAccesoDatos.SeguridadAccesoDatos.Crear(rol);
        }
        public Roles ObtenerRolPorId(int id)
        {
            return fabricaAccesoDatos.SeguridadAccesoDatos.ObtenerRolPorId(id);
        }
        public Roles Actualizar(Roles rol)
        {
            return fabricaAccesoDatos.SeguridadAccesoDatos.Actualizar(rol);
        }
        public void EliminarRol(int id)
        {
            fabricaAccesoDatos.SeguridadAccesoDatos.EliminarRol(id);
        }

        public IList<Roles> BuscarRoles(Roles rol)
        {
            return fabricaAccesoDatos.SeguridadAccesoDatos.BuscarRoles(rol);
        }
        #endregion

        #region Perfiles
        public IList<Perfiles> ObtenerListaPerfiles()
        {
            return fabricaAccesoDatos.SeguridadAccesoDatos.ObtenerListaPerfiles();
        }

        public Perfiles Crear(Perfiles Perfil)
        {
            return fabricaAccesoDatos.SeguridadAccesoDatos.Crear(Perfil);
        }
        public Perfiles ObtenerPerfilPorId(int id)
        {
            return fabricaAccesoDatos.SeguridadAccesoDatos.ObtenerPerfilPorId(id);
        }
        public Perfiles Actualizar(Perfiles perfil)
        {
            return fabricaAccesoDatos.SeguridadAccesoDatos.Actualizar(perfil);
        }

        public void EliminarPerfil(int id)
        {
            fabricaAccesoDatos.SeguridadAccesoDatos.EliminarRol(id);
        }

        public IList<Perfiles> BuscarPerfil(Perfiles perfil)
        {
            return fabricaAccesoDatos.SeguridadAccesoDatos.BuscarPerfil(perfil);
        }

        #endregion

        #region Granulos

        public IList<Granulos> ObtenerListaGranulos()
        {
            return fabricaAccesoDatos.SeguridadAccesoDatos.ObtenerListaGranulos();
        }
        public Granulos Crear(Granulos Granulo)
        {
            return fabricaAccesoDatos.SeguridadAccesoDatos.Crear(Granulo);
        }
        public Granulos ObtenerGranuloPorId(int id)
        {
            return fabricaAccesoDatos.SeguridadAccesoDatos.ObtenerGranuloPorId(id);
        }
        public Granulos Actualizar(Granulos granulo)
        {
            return fabricaAccesoDatos.SeguridadAccesoDatos.Actualizar(granulo);
        }
        public void EliminarGranulo(int id)
        {
            fabricaAccesoDatos.SeguridadAccesoDatos.EliminarGranulo(id);
        }
        public IList<Granulos> BuscarGranulo(Granulos granulo)
        {
            return fabricaAccesoDatos.SeguridadAccesoDatos.BuscarGranulo(granulo);
        }
        #endregion

        #region Permisos

        public IList<Permisos> ObtenerListaPermisosPorUsuario(String correo)
        {
            return fabricaAccesoDatos.SeguridadAccesoDatos.ObtenerListaPermisosPorUsuario(correo);
        }

        public IList<Permisos> ObtenerListaPermisos()
        {
            return fabricaAccesoDatos.SeguridadAccesoDatos.ObtenerListaPermisos();
        }
        public Permisos Crear(Permisos permiso)
        {
            return fabricaAccesoDatos.SeguridadAccesoDatos.Crear(permiso);
        }
        public Permisos ObtenerPermisoPorId(int id)
        {
            return fabricaAccesoDatos.SeguridadAccesoDatos.ObtenerPermisoPorId(id);
        }
        public Permisos Actualizar(Permisos permiso)
        {
            return fabricaAccesoDatos.SeguridadAccesoDatos.Actualizar(permiso);
        }
        public void EliminarPermiso(int id)
        {
            fabricaAccesoDatos.SeguridadAccesoDatos.EliminarPermiso(id);
        }
        public IList<Permisos> BuscarPermiso(Permisos permiso)
        {
            return fabricaAccesoDatos.SeguridadAccesoDatos.BuscarPermiso(permiso);
        }
        #endregion
    }
}
