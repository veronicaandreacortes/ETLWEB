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
    public partial class ManejarPermisos : PaginaBase
    {
        private string PermisoId
        {
            get
            {
                return Session["PermisoId"].ToString();
            }
            set
            {
                Session["PermisoId"] = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PermisoId = "";
                base.VerificarPermisos();
                ProcesarQueryString();
                Bind(ClienteRestServicio.Instancia.ObtenerListaPermisos());
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
                    LabelMensaje.Text = string.Format("Se ha creado el Permiso {0}", id);
                }
                if (tipoAccion == TipoAccion.Editar)
                {
                    LabelMensaje.Text = string.Format("Se ha editado el Permiso {0}", id);
                }
            }
        }

        private void Bind(IList<Permisos> listaPermisos)
        {
            GridViewPermisos.DataSource = listaPermisos;
            GridViewPermisos.DataBind();
        }

        protected void GridViewPermisos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                Permisos permiso = ClienteRestServicio.Instancia.ObtenerPermisoPorId(e.CommandArgument.ToString());

                PermisoId = permiso.idpermiso.ToString();
                Response.Redirect(string.Format("permiso.aspx?accion={0}&id={1}", (int)TipoAccion.Editar, PermisoId));
            }

            if (e.CommandName == "Eliminar")
            {
                ClienteRestServicio.Instancia.EliminarPermiso(e.CommandArgument.ToString());
                Bind(ClienteRestServicio.Instancia.ObtenerListaPermisos());
            }

        }

        protected void GridViewPermisos_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GridViewPermisos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imageButtonDetails = e.Row.FindControl("ImageButtonDetails") as ImageButton;
                imageButtonDetails.Enabled =
                    TienePermiso(Constantes.EDITAR_PERMISO) ? true : false;

            }
        }

        public override void InhabilitarControles()
        {
            GridViewPermisos.Visible = false;
            ImageButtonNuevoPermiso.Enabled = false;
        }

        public override void HabilitarControlesDadoPermisos(IList<int> permisos)
        {
            if (permisos.Contains(Constantes.CREAR_PERMISO))
            {
                GridViewPermisos.Visible = true;
                ImageButtonNuevoPermiso.Enabled = true;
            }
            if (permisos.Contains(Constantes.EDITAR_PERMISO))
            {
                GridViewPermisos.Visible = true;
            }
            if (permisos.Contains(Constantes.LISTAR_PERMISO))
            {
                GridViewPermisos.Visible = true;
            }
        }



        protected void ImageButtonNuevoPermiso_Click(object sender, ImageClickEventArgs e)
        {
            PermisoId = string.Empty;
            Response.Redirect(string.Format("permiso.aspx?accion={0}", (int)TipoAccion.Crear));
        }

        protected void ImageButtonBuscar_Click(object sender, ImageClickEventArgs e)
        {
            Permisos permiso = new Permisos();

            permiso.despermiso = TextBoxDescripcion.Text;


            Bind(ClienteRestServicio.Instancia.BuscarPermiso(permiso));
        }

        protected void ImageButtonLimpiar_Click(object sender, ImageClickEventArgs e)
        {
            TextBoxDescripcion.Text = string.Empty;

            Bind(ClienteRestServicio.Instancia.ObtenerListaPermisos());
        }

        protected void ImageButtonActivarBuscar_Click(object sender, ImageClickEventArgs e)
        {
            PanelBuscar.Visible = !PanelBuscar.Visible;
        }
    }
}