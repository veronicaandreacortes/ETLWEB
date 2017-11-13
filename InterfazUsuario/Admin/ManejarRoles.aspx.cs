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
    public partial class ManejarRoles : PaginaBase
    {
        private string RolId
        {
            get
            {
                return Session["RolId"].ToString();
            }
            set
            {
                Session["RolId"] = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RolId = "";
                base.VerificarPermisos();
                ProcesarQueryString();
                Bind(ClienteRestServicio.Instancia.ObtenerListaRoles());
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
                    LabelMensaje.Text = string.Format("Se ha creado el Rol {0}", id);
                }
                if (tipoAccion == TipoAccion.Editar)
                {
                    LabelMensaje.Text = string.Format("Se ha editado el Rol {0}", id);
                }
            }
        }

        private void Bind(IList<Roles> listaRoles)
        {
            GridViewRoles.DataSource = listaRoles;
            GridViewRoles.DataBind();
        }

        protected void GridViewRoles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                Roles rol = ClienteRestServicio.Instancia.ObtenerRolPorId(e.CommandArgument.ToString());

                RolId = rol.idrol.ToString();
                Response.Redirect(string.Format("rol.aspx?accion={0}&id={1}", (int)TipoAccion.Editar, RolId));
            }

            if (e.CommandName == "Eliminar")
            {
                ClienteRestServicio.Instancia.EliminarRol(e.CommandArgument.ToString());
                Bind(ClienteRestServicio.Instancia.ObtenerListaRoles());
            }

        }

        protected void GridViewRoles_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GridViewRoles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imageButtonDetails = e.Row.FindControl("ImageButtonDetails") as ImageButton;
                imageButtonDetails.Enabled =
                    TienePermiso(Constantes.EDITAR_ROL) ? true : false;

            }
        }

        public override void InhabilitarControles()
        {
            GridViewRoles.Visible = false;
            ImageButtonNuevoRol.Enabled = false;
        }

        public override void HabilitarControlesDadoPermisos(IList<int> permisos)
        {
            if (permisos.Contains(Constantes.CREAR_ROL))
            {
                GridViewRoles.Visible = true;
                ImageButtonNuevoRol.Enabled = true;
            }
            if (permisos.Contains(Constantes.EDITAR_ROL))
            {
                GridViewRoles.Visible = true;
            }
            if (permisos.Contains(Constantes.LISTAR_ROL))
            {
                GridViewRoles.Visible = true;
            }
        }



        protected void ImageButtonNuevoRol_Click(object sender, ImageClickEventArgs e)
        {
            RolId = string.Empty;
            Response.Redirect(string.Format("rol.aspx?accion={0}", (int)TipoAccion.Crear));
        }

        protected void ImageButtonBuscar_Click(object sender, ImageClickEventArgs e)
        {
            Roles rol = new Roles();

            rol.desrol = TextBoxDescripcion.Text;
            

            Bind(ClienteRestServicio.Instancia.BuscarRoles(rol));
        }

        protected void ImageButtonLimpiar_Click(object sender, ImageClickEventArgs e)
        {
            TextBoxDescripcion.Text = string.Empty;
            
            Bind(ClienteRestServicio.Instancia.ObtenerListaRoles());
        }

        protected void ImageButtonActivarBuscar_Click(object sender, ImageClickEventArgs e)
        {
            PanelBuscar.Visible = !PanelBuscar.Visible;
        }
    }
}