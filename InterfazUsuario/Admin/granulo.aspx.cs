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
    public partial class granulo : PaginaBase
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
            if (permisos.Contains(Constantes.CREAR_GRANULO))
            {
                PanelGranulos.Enabled = true;
            }
            if (permisos.Contains(Constantes.EDITAR_GRANULO))
            {
                PanelGranulos.Enabled = true;
            }
        }

        public override void InhabilitarControles()
        {
            PanelGranulos.Enabled = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
            else
            {

                GranuloId = string.Empty;
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
                resultado = CrearGranulo();
            }

            if (TipoAccion == Codigo.TipoAccion.Editar)
            {
                resultado = EditarGranulo();
            }

            if (resultado)
            {
                Response.Redirect(string.Format("ManejarGranulos.aspx?id={0}&accion={1}", GranuloId, (int)TipoAccion));
            }
            else
            {
                LabelMensaje.Text = "HA OCURRIDO UN ERROR";
            }
        }

        protected void ImageButtonCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("ManejarGranulos.aspx");
        }

        private bool CrearGranulo()
        {
            Granulos granulo = new Granulos();


            granulo.idrol = Convert.ToInt16(TextBoxRol);
            granulo.idpermiso = Convert.ToInt16(TextBoxPermiso.Text);

            granulo = ClienteRestServicio.Instancia.CrearGranulo(granulo);

            if (granulo != null && granulo.idgranular != 0)
            {
                GranuloId = granulo.idgranular.ToString();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void LlenarFormulario(string id)
        {
            int GranuloId;
            if (int.TryParse(id, out GranuloId))
            {
                Granulos granulo = ClienteRestServicio.Instancia.ObtenerGranuloPorId(GranuloId.ToString());

                if (granulo != null)
                {
                    TextBoxRol.Text = granulo.idrol.ToString();
                    TextBoxPermiso.Text = granulo.idpermiso.ToString();

                    GranuloId = granulo.idgranular;
                }

            }
        }

        private bool EditarGranulo()
        {
            Granulos granulo = ClienteRestServicio.Instancia.ObtenerGranuloPorId(GranuloId);

            if (!string.IsNullOrWhiteSpace(TextBoxRol.Text))
            {
                granulo.idrol = Convert.ToInt16(TextBoxRol);

            }
            if (!string.IsNullOrWhiteSpace(TextBoxPermiso.Text))
            {
                granulo.idpermiso = Convert.ToInt16(TextBoxPermiso);

            }

            granulo = ClienteRestServicio.Instancia.ActualizarGranulo(granulo);

            if (granulo != null && granulo.idgranular != 0)
            {
                GranuloId = granulo.idgranular.ToString();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}