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
    public partial class permiso : PaginaBase
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
            if (permisos.Contains(Constantes.CREAR_PERMISO))
            {
                PanelPermiso.Enabled = true;
            }
            if (permisos.Contains(Constantes.EDITAR_PERMISO))
            {
                PanelPermiso.Enabled = true;
            }
        }

        public override void InhabilitarControles()
        {
            PanelPermiso.Enabled = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
            else
            {

                PermisoId = string.Empty;
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
                resultado = CrearPermiso();
            }

            if (TipoAccion == Codigo.TipoAccion.Editar)
            {
                resultado = EditarPermiso();
            }

            if (resultado)
            {
                Response.Redirect(string.Format("ManejarPermisos.aspx?id={0}&accion={1}", PermisoId, (int)TipoAccion));
            }
            else
            {
                LabelMensaje.Text = "HA OCURRIDO UN ERROR";
            }
        }

        protected void ImageButtonCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("ManejarPermisos.aspx");
        }

        private bool CrearPermiso()
        {
            Permisos permiso = new Permisos();


            permiso.despermiso = TextBoxDescripcion.Text;

            permiso = ClienteRestServicio.Instancia.CrearPermiso(permiso);

            if (permiso != null && permiso.idpermiso != 0)
            {
                PermisoId = permiso.idpermiso.ToString();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void LlenarFormulario(string id)
        {
            int PermisoId;
            if (int.TryParse(id, out PermisoId))
            {
                Permisos permiso = ClienteRestServicio.Instancia.ObtenerPermisoPorId(PermisoId.ToString());

                if (permiso != null)
                {
                    TextBoxDescripcion.Text = permiso.despermiso;

                    PermisoId = permiso.idpermiso;
                }

            }
        }

        private bool EditarPermiso()
        {
            Permisos permiso = ClienteRestServicio.Instancia.ObtenerPermisoPorId(PermisoId);

            if (!string.IsNullOrWhiteSpace(TextBoxDescripcion.Text))
            {
                permiso.despermiso = TextBoxDescripcion.Text;
            }
            permiso = ClienteRestServicio.Instancia.ActualizarPermiso(permiso);

            if (permiso != null && permiso.idpermiso != 0)
            {
                PermisoId = permiso.idpermiso.ToString();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}