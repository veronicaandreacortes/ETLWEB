using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InterfazUsuario.ClienteRest;
using Entidades;
using System.Security.Principal;
using System.Web.Security;

namespace InterfazUsuario.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {

            Entidades.LoginPeticion login = new Entidades.LoginPeticion();

            login.Correo = UserName.Text;
            login.Clave = Password.Text;

            LoginRespuesta resultado = ClienteRestServicio.Instancia.Login(login);

            FailureText.Text = resultado.Mensaje;

            if (resultado.Mensaje == "EXITO")
            {
                FormsAuthentication.SetAuthCookie(login.Correo, false);
                
                Response.Redirect("/Admin/ManejarUsuarios.aspx");
            }
        }

    }
}
