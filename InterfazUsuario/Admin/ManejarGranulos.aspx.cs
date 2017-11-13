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
    public partial class ManejarGranulos : PaginaBase
    {
        private string GranuloId
        {
            get
            {
                return Session["GranuloId"].ToString();
            }
            set
            {
                Session["GranuloId"] = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GranuloId = "";
                base.VerificarPermisos();
                ProcesarQueryString();
                Bind(ClienteRestServicio.Instancia.ObtenerListaGranulos());
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
                    LabelMensaje.Text = string.Format("Se ha creado el granulo {0}", id);
                }
                if (tipoAccion == TipoAccion.Editar)
                {
                    LabelMensaje.Text = string.Format("Se ha editado el granulo {0}", id);
                }
            }
        }

        private void Bind(IList<Granulos> listaGranulos)
        {
            GridViewGranulos.DataSource = listaGranulos;
            GridViewGranulos.DataBind();
        }

        protected void GridViewGranulos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                Granulos granulo = ClienteRestServicio.Instancia.ObtenerGranuloPorId(e.CommandArgument.ToString());

                GranuloId = granulo.idgranular.ToString();
                Response.Redirect(string.Format("granulo.aspx?accion={0}&id={1}", (int)TipoAccion.Editar, GranuloId));
            }

            if (e.CommandName == "Eliminar")
            {
                ClienteRestServicio.Instancia.EliminarGranulo(e.CommandArgument.ToString());
                Bind(ClienteRestServicio.Instancia.ObtenerListaGranulos());
            }

        }

        protected void GridViewGranulos_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GridViewGranulos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imageButtonDetails = e.Row.FindControl("ImageButtonDetails") as ImageButton;
                imageButtonDetails.Enabled =
                    TienePermiso(Constantes.EDITAR_GRANULO) ? true : false;

            }
        }

        public override void InhabilitarControles()
        {
            GridViewGranulos.Visible = false;
            ImageButtonNuevoGranulo.Enabled = false;
        }

        public override void HabilitarControlesDadoPermisos(IList<int> permisos)
        {
            if (permisos.Contains(Constantes.CREAR_GRANULO))
            {
                GridViewGranulos.Visible = true;
                ImageButtonNuevoGranulo.Enabled = true;
            }
            if (permisos.Contains(Constantes.EDITAR_GRANULO))
            {
                GridViewGranulos.Visible = true;
            }
            if (permisos.Contains(Constantes.LISTAR_GRANULO))
            {
                GridViewGranulos.Visible = true;
            }
        }



        protected void ImageButtonNuevoGranulo_Click(object sender, ImageClickEventArgs e)
        {
            GranuloId = string.Empty;
            Response.Redirect(string.Format("granulo.aspx?accion={0}", (int)TipoAccion.Crear));
        }

        protected void ImageButtonBuscar_Click(object sender, ImageClickEventArgs e)
        {
            Granulos granulo = new Granulos();

            granulo.idrol = Convert.ToInt16(TextBoxRol.Text);
            granulo.idpermiso = Convert.ToInt16(TextBoxPermiso.Text);


            Bind(ClienteRestServicio.Instancia.BuscarGranulo(granulo));
        }

        protected void ImageButtonLimpiar_Click(object sender, ImageClickEventArgs e)
        {
            TextBoxRol.Text = string.Empty;
            TextBoxPermiso.Text = string.Empty;

            Bind(ClienteRestServicio.Instancia.ObtenerListaGranulos());
        }

        protected void ImageButtonActivarBuscar_Click(object sender, ImageClickEventArgs e)
        {
            PanelBuscar.Visible = !PanelBuscar.Visible;
        }
    }
}