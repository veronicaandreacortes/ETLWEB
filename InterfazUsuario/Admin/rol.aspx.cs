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
    public partial class rol : PaginaBase
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
            if (permisos.Contains(Constantes.CREAR_ROL))
            {
                PanelRol.Enabled = true;
            }
            if (permisos.Contains(Constantes.EDITAR_ROL))
            {
                PanelRol.Enabled = true;
            }
        }

        public override void InhabilitarControles()
        {
            PanelRol.Enabled = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
            else
            {

                RolId = string.Empty;
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
                resultado = CrearRol();
            }

            if (TipoAccion == Codigo.TipoAccion.Editar)
            {
                resultado = EditarRol();
            }

            if (resultado)
            {
                Response.Redirect(string.Format("ManejarRoles.aspx?id={0}&accion={1}", RolId, (int)TipoAccion));
            }
            else
            {
                LabelMensaje.Text = "HA OCURRIDO UN ERROR";
            }
        }

        protected void ImageButtonCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("ManejarRoles.aspx");
        }

        private bool CrearRol()
        {
            Roles rol = new Roles();


            rol.desrol = TextBoxDescripcion.Text;
            
            rol =  ClienteRestServicio.Instancia.CrearRol(rol);

            if (rol != null && rol.idrol != 0)
            {
                RolId = rol.idrol.ToString();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void LlenarFormulario(string id)
        {
            int rolId;
            if (int.TryParse(id, out rolId))
            {
                Roles rol = ClienteRestServicio.Instancia.ObtenerRolPorId(rolId.ToString());

                if (rol != null)
                {
                    TextBoxDescripcion.Text = rol.desrol;
                    
                    RolId = rol.idrol.ToString();
                }

            }
        }

        private bool EditarRol()
        {
            Roles rol = ClienteRestServicio.Instancia.ObtenerRolPorId(RolId);

            if (!string.IsNullOrWhiteSpace(TextBoxDescripcion.Text))
            {
                rol.desrol = TextBoxDescripcion.Text;
            }
            rol = ClienteRestServicio.Instancia.ActualizarRol(rol);

            if (rol != null && rol.idrol != 0)
            {
                RolId = rol.idrol.ToString();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}