using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entidades;

namespace InterfazUsuario.ClienteRest
{
    public class ContextoAplicacion
    {
        private const string llaveUnicaContextoAplicacion = "uniminuto.programacionweb";

        private Usuario usuarioLogueado;

        private static object objetoBloqueo = new object();

        private static ContextoAplicacion instancia;

        public Usuario UsuarioLogueado
        {
            get
            {
                return usuarioLogueado;
            }
            set
            {
                usuarioLogueado = value;
                HttpContext _Context = HttpContext.Current;
                if (_Context != null && _Context.Session != null)
                {
                    _Context.Session[llaveUnicaContextoAplicacion] = this;
                }
            }
        }

        public static ContextoAplicacion Instancia
        {
            get
            {
                lock (objetoBloqueo)
                {
                    HttpContext _Context = HttpContext.Current;
                    if (_Context != null && _Context.Session != null)
                    {
                        instancia = _Context.Session[llaveUnicaContextoAplicacion] as ContextoAplicacion;
                    }
                    if (instancia == null)
                    {
                        instancia = new ContextoAplicacion();
                        if (_Context != null && _Context.Session != null)
                        {
                            _Context.Session[llaveUnicaContextoAplicacion] = instancia;
                        }
                    }
                    return instancia;
                }
            }
        }
    }
}