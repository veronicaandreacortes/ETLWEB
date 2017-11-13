using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Entidades;
using InterfazUsuario.ClienteRest;

namespace InterfazUsuario
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            //get the security cookie
            HttpCookie _Cookie = Request.Cookies.Get(FormsAuthentication.FormsCookieName);
            /*if (_Cookie != null)
            {
                //we got the cookie, so decrypt the value
                FormsAuthenticationTicket _Ticket = FormsAuthentication.Decrypt(_Cookie.Value);
                if (_Ticket.Expired)
                {
                    //the ticket has expired - force user to login
                    FormsAuthentication.SignOut();
                    Response.Redirect("default.aspx");
                }
                else
                {
                    //ticket is valid, set HttpContext user value
                    System.IO.MemoryStream _Buffer = new System.IO.MemoryStream(Convert.FromBase64String(_Ticket.UserData));
                    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _Formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    HttpContext.Current.User = (System.Security.Principal.IPrincipal)_Formatter.Deserialize(_Buffer);

                    Usuario usuario = ClienteRestServicio.Instancia.ObtenerUsuarioPorCorreo(HttpContext.Current.User.Identity.Name);
                    ContextoAplicacion.Instancia.UsuarioLogueado = usuario;
                    
                    if (ContextoAplicacion.Instancia.UsuarioLogueado == null)
                    {
                        ContextoAplicacion.Instancia.UsuarioLogueado = usuario;
                    }
                }
            }*/
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
