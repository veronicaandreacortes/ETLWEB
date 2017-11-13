using Entidades;
using InterfazUsuario.ClienteRest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterfazUsuario.Admin
{
    public class PaginaBase : System.Web.UI.Page
    {
        public IList<int> ObtenerPermisosDeUsuario()
        {
            IList<Permisos> listaPermisos = ClienteRestServicio.Instancia.ObtenerListaPermisos();

            string email = User.Identity.Name;

            IList<Permisos> permisosUsuario = ClienteRestServicio.Instancia.ObtenerListaPermisosPorUsuario(email);

            return listaPermisos.Select(p => p.idpermiso).Intersect(permisosUsuario.Select(pu => pu.idpermiso)).ToList();
        }

        public void VerificarPermisos()
        {
            InhabilitarControles();
            var temp = ObtenerPermisosDeUsuario();
            HabilitarControlesDadoPermisos(temp);
        }

        public bool TienePermiso(int permiso)
        {
            var permisos = ObtenerPermisosDeUsuario();
            return permisos.Contains(permiso);
        }

        public virtual void InhabilitarControles()
        {
            // hacer nada :)
        }

        public virtual void HabilitarControlesDadoPermisos(IList<int> permisos)
        {
            // hacer nada :)
        }
    }
}