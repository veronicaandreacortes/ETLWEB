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
    public partial class ManejarPerfiles : PaginaBase
    {
        private string PerfilId
        {
            get
            {
                return Session["PerfilId"].ToString();
            }
            set
            {
                Session["PerfilId"] = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PerfilId = "";
                base.VerificarPermisos();
                ProcesarQueryString();
                Bind(ClienteRestServicio.Instancia.ObtenerListaPerfiles());
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
                    LabelMensaje.Text = string.Format("Se ha creado el Perfil {0}", id);
                }
                if (tipoAccion == TipoAccion.Editar)
                {
                    LabelMensaje.Text = string.Format("Se ha editado el Perfil {0}", id);
                }
            }
        }

        private void Bind(IList<Perfiles> listaPerfiles)
        {
            GridViewPerfiles.DataSource = listaPerfiles;
            GridViewPerfiles.DataBind();
        }

        protected void GridViewPerfiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                Perfiles perfil = ClienteRestServicio.Instancia.ObtenerPerfilPorId(e.CommandArgument.ToString());

                PerfilId = perfil.idrol.ToString();
                Response.Redirect(string.Format("perfil.aspx?accion={0}&id={1}", (int)TipoAccion.Editar, PerfilId));
            }

            if (e.CommandName == "Eliminar")
            {
                ClienteRestServicio.Instancia.EliminarPerfil(e.CommandArgument.ToString());
                Bind(ClienteRestServicio.Instancia.ObtenerListaPerfiles());
            }

        }

        protected void GridViewPerfiles_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GridViewPerfiles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imageButtonDetails = e.Row.FindControl("ImageButtonDetails") as ImageButton;
                imageButtonDetails.Enabled =
                    TienePermiso(Constantes.EDITAR_PERFIL) ? true : false;

            }
        }

        public override void InhabilitarControles()
        {
            GridViewPerfiles.Visible = false;
            ImageButtonNuevoPerfil.Enabled = false;
        }

        public override void HabilitarControlesDadoPermisos(IList<int> permisos)
        {
            if (permisos.Contains(Constantes.CREAR_PERFIL))
            {
                GridViewPerfiles.Visible = true;
                ImageButtonNuevoPerfil.Enabled = true;
            }
            if (permisos.Contains(Constantes.EDITAR_PERFIL))
            {
                GridViewPerfiles.Visible = true;
            }
            if (permisos.Contains(Constantes.LISTAR_PERFIL))
            {
                GridViewPerfiles.Visible = true;
            }
        }



        protected void ImageButtonNuevoPerfil_Click(object sender, ImageClickEventArgs e)
        {
            PerfilId = string.Empty;
            Response.Redirect(string.Format("perfil.aspx?accion={0}", (int)TipoAccion.Crear));
        }

        protected void ImageButtonBuscar_Click(object sender, ImageClickEventArgs e)
        {
            Perfiles perfil = new Perfiles();

            perfil.idusuario = Convert.ToInt16(TextBoxUsuario.Text);
            perfil.idrol = Convert.ToInt16(TextBoxRol.Text);


            Bind(ClienteRestServicio.Instancia.BuscarPerfil(perfil));
        }

        protected void ImageButtonLimpiar_Click(object sender, ImageClickEventArgs e)
        {
            TextBoxUsuario.Text = string.Empty;
            TextBoxRol.Text = string.Empty;

            Bind(ClienteRestServicio.Instancia.ObtenerListaPerfiles());
        }

        protected void ImageButtonActivarBuscar_Click(object sender, ImageClickEventArgs e)
        {
            PanelBuscar.Visible = !PanelBuscar.Visible;
        }

       
    }
}