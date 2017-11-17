using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace AccesoDatos
{
    public interface ISeguridadAccesoDatos
    {
        #region usuario

        LoginRespuesta Login(LoginPeticion login);

        IList<Usuario> ObtenerListaUsuarios();

        IList<Usuario> BuscarUsuarios(Usuario usuario);

        Usuario Crear(Usuario usuario);

        Usuario ObtenerUsuarioPorCorreo(string correo);

        Usuario ObtenerUsuarioPorId(int id);

        Usuario Actualizar(Usuario usuario);

        void EliminarUsuario(int id);

        #endregion

        #region roles

        IList<Roles> ObtenerListaroles();

        IList<Roles> BuscarRoles(Roles rol);

        Roles Crear(Roles rol);

        Roles ObtenerRolPorId(int id);

        Roles Actualizar(Roles rol);

        void EliminarRol(int id);


        #endregion

        #region Perfiles

        IList<Perfiles> ObtenerListaPerfiles();

        Perfiles Crear(Perfiles Perfil);

        Perfiles ObtenerPerfilPorId(int id);

        Perfiles Actualizar(Perfiles perfil);

        void EliminarPerfil(int id);

        IList<Perfiles> BuscarPerfil(Perfiles perfil);

        #endregion

        #region Granulos

        IList<Granulos> ObtenerListaGranulos();

        Granulos Crear(Granulos Granulo);

        Granulos ObtenerGranuloPorId(int id);

        Granulos Actualizar(Granulos perfil);

        void EliminarGranulo(int id);

        IList<Granulos> BuscarGranulo(Granulos granulo);

        #endregion

        #region Permisos

        IList<Permisos> ObtenerListaPermisosPorUsuario(String correo);

        IList<Permisos> ObtenerListaPermisos();

        Permisos Crear(Permisos permiso);

        Permisos ObtenerPermisoPorId(int id);

        Permisos Actualizar(Permisos permiso);

        void EliminarPermiso(int id);

        IList<Permisos> BuscarPermiso(Permisos permiso);

        #endregion


        //#region ciudades

        //IList<Ciudades> ObtenerListaCiudades();

        //IList<Ciudades> BuscarRoles(Ciudades ciudad);

        //Roles Crear(Ciudades rol);

        //Roles ObtenerCiudadPorId(int id);

        //Roles Actualizar(Ciudades rol);

        //void EliminarCiudades(int id);
    }
}
