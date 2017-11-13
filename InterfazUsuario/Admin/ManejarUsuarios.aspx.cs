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
    public partial class ManejarUsuarios : PaginaBase
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


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UsuarioId = "";
                base.VerificarPermisos();
                ProcesarQueryString();
                Bind(ClienteRestServicio.Instancia.ObtenerListaUsuarios());
            }
        }

        private void ProcesarQueryString()
        {
            string id = Request.QueryString["id"];

            if (!string.IsNullOrWhiteSpace(id))
            {
                TipoAccion tipoAccion = (TipoAccion)int.Parse(Request.QueryString["accion"]);

                if (tipoAccion == TipoAccion.Crear)
                {
                    LabelMensaje.Text = string.Format("Se ha creado el usuario {0}", id);
                }
                if (tipoAccion == TipoAccion.Editar)
                {
                    LabelMensaje.Text = string.Format("Se ha editado el usuario {0}", id);
                }
            }
        }

        private void Bind(IList<Usuario> listaUsuarios)
        {
            GridViewUsuarios.DataSource = listaUsuarios;
            GridViewUsuarios.DataBind();
        }

        protected void GridViewUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                Usuario usuario = ClienteRestServicio.Instancia.ObtenerUsuarioPorId(e.CommandArgument.ToString());

                UsuarioId = usuario.idusuario.ToString();
                Response.Redirect(string.Format("usuario.aspx?accion={0}&id={1}", (int)TipoAccion.Editar, UsuarioId));
            }

            if (e.CommandName == "Eliminar")
            {
                ClienteRestServicio.Instancia.EliminarUsuario(e.CommandArgument.ToString());
                Bind(ClienteRestServicio.Instancia.ObtenerListaUsuarios());
            }

        }

        protected void GridViewUsuarios_RowCreated(object sender, GridViewRowEventArgs e)
        {
            
        }

        protected void GridViewUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imageButtonDetails = e.Row.FindControl("ImageButtonDetails") as ImageButton;
                imageButtonDetails.Enabled = 
                    TienePermiso(Constantes.EDITAR_USUARIO) ? true : false;
                
            }
        }

        public override void InhabilitarControles()
        {
            GridViewUsuarios.Visible = false;
            ImageButtonNuevoUsuario.Enabled = false;
       }

        public override void HabilitarControlesDadoPermisos(IList<int> permisos)
        { 
            if (permisos.Contains(Constantes.CREAR_USUARIO))
            {
                GridViewUsuarios.Visible = true;
                ImageButtonNuevoUsuario.Enabled = true;
            }
            if (permisos.Contains(Constantes.EDITAR_USUARIO))
            {
                GridViewUsuarios.Visible = true;
            }
            if (permisos.Contains(Constantes.LISTAR_USUARIO))
            {
                GridViewUsuarios.Visible = true;
            }            
        }



        protected void ImageButtonNuevoUsuario_Click(object sender, ImageClickEventArgs e)
        {
            UsuarioId = string.Empty;
            Response.Redirect(string.Format("usuario.aspx?accion={0}", (int)TipoAccion.Crear));
        }

        protected void ImageButtonBuscar_Click(object sender, ImageClickEventArgs e)
        {
            Usuario usuario = new Usuario();

            usuario.nombrecompleto = TextBoxNombre.Text;            
            usuario.correo = TextBoxCorreo.Text;
            usuario.celular = TextBoxCelular.Text;

            Bind(ClienteRestServicio.Instancia.BuscarUsuarios(usuario));            
        }

        protected void ImageButtonLimpiar_Click(object sender, ImageClickEventArgs e)
        {
            TextBoxCelular.Text = string.Empty;
            TextBoxCorreo.Text = string.Empty;
            TextBoxNombre.Text = string.Empty;
            Bind(ClienteRestServicio.Instancia.ObtenerListaUsuarios());
        }

        protected void ImageButtonActivarBuscar_Click(object sender, ImageClickEventArgs e)
        {
            PanelBuscar.Visible = !PanelBuscar.Visible;
        }
    }
}