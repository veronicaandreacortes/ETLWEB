using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InterfazUsuario.ClienteRest;
using Entidades;
using InterfazUsuario.Codigo;

namespace InterfazUsuario.Admin
{
    public partial class usuario : PaginaBase
    {

        private string UsuarioId
        {
            get
            {
                return Session["UsuarioId"].ToString();
            }
            set
            {
                Session["UsuarioId"] = value;
            }
        }

        private TipoAccion TipoAccion
        {
            get
            {
                return (TipoAccion)Session["TipoAccion"];
            }
            set
            {
                Session["TipoAccion"] = value;
            }
        }

        public override void HabilitarControlesDadoPermisos(IList<int> permisos)
        {
            if (permisos.Contains(Constantes.CREAR_USUARIO))
            {
                PanelUsuario.Enabled = true;
            }
            if (permisos.Contains(Constantes.EDITAR_USUARIO))
            {
                PanelUsuario.Enabled = true;
            }
        }

        public override void InhabilitarControles()
        {
            PanelUsuario.Enabled = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
            else {

                UsuarioId = string.Empty;
                base.VerificarPermisos();





                TipoAccion = (TipoAccion)(int.Parse(Request.QueryString["accion"]));
                if (TipoAccion == Codigo.TipoAccion.Editar)
                {
                    LabelTitulo.Text = "Modificar";
                    LlenarFormulario(Request.QueryString["id"]);
                }
                if (TipoAccion == Codigo.TipoAccion.Crear)
                {
                    LabelTitulo.Text = "Adicionar";
                }

            }
        }

        protected void ImageButtonGuardar_Click(object sender, ImageClickEventArgs e)
        {
            bool resultado = false;

            if (TipoAccion == Codigo.TipoAccion.Crear)
            {
                resultado = CrearUsuario();
            }
            
            if (TipoAccion == Codigo.TipoAccion.Editar)
            {
                resultado = EditarUsuario();
            }

            if (resultado)
            {
                Response.Redirect(string.Format("ManejarUsuarios.aspx?id={0}&accion={1}", UsuarioId, (int)TipoAccion));
            }
            else
            {
                LabelMensaje.Text = "HA OCURRIDO UN ERROR";
            }
        }

        protected void ImageButtonCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("ManejarUsuarios.aspx");
        }

        private bool CrearUsuario()
        {
            Usuario usuario = new Usuario();

            usuario.correo = TextBoxCorreo.Text;
            usuario.clave = TextBoxClave.Text;
            usuario.nombrecompleto = TextBoxNombre.Text;
            usuario.celular = TextBoxCelular.Text;

            usuario = ClienteRestServicio.Instancia.CrearUsuario(usuario);

            if (usuario != null && usuario.idusuario != 0)
            {
                UsuarioId = usuario.idusuario.ToString();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void LlenarFormulario(string id)
        {
            int usuarioId;
            if (int.TryParse(id, out usuarioId))
            {
                Usuario usuario = ClienteRestServicio.Instancia.ObtenerUsuarioPorId(usuarioId.ToString());

                if (usuario != null)
                {
                    TextBoxCorreo.Text = usuario.correo;
                    TextBoxClave.Text = usuario.clave;
                    TextBoxNombre.Text = usuario.nombrecompleto;
                    TextBoxCelular.Text = usuario.celular;

                    UsuarioId = usuario.idusuario.ToString();
                }

            }
        }

        private bool EditarUsuario()
        {
            Usuario usuario = ClienteRestServicio.Instancia.ObtenerUsuarioPorId(UsuarioId);

            if (!string.IsNullOrWhiteSpace(TextBoxCorreo.Text))
            {
                usuario.correo = TextBoxCorreo.Text;
            }
            if (!string.IsNullOrWhiteSpace(TextBoxClave.Text))
            {
                usuario.clave = TextBoxClave.Text;
            }
            if (!string.IsNullOrWhiteSpace(TextBoxNombre.Text))
            {
                usuario.nombrecompleto = TextBoxNombre.Text;
            }
            if (!string.IsNullOrWhiteSpace(TextBoxCelular.Text))
            {
                usuario.celular = TextBoxCelular.Text;
            }

            usuario = ClienteRestServicio.Instancia.ActualizarUsuario(usuario);

            if (usuario != null && usuario.idusuario != 0)
            {
                UsuarioId = usuario.idusuario.ToString();
                return true;
            }
            else
            {
                return false;
            }
        }

      }
}