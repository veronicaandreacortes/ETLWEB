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
    public partial class perfil : PaginaBase
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
            if (permisos.Contains(Constantes.CREAR_PERFIL))
            {
                PanelPerfiles.Enabled = true;
            }
            if (permisos.Contains(Constantes.EDITAR_PERFIL))
            {
                PanelPerfiles.Enabled = true;
            }
        }

        public override void InhabilitarControles()
        {
            PanelPerfiles.Enabled = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
            else
            {

                PerfilId = string.Empty;
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
                resultado = CrearPerfil();
            }

            if (TipoAccion == Codigo.TipoAccion.Editar)
            {
                resultado = EditarPerfil();
            }

            if (resultado)
            {
                Response.Redirect(string.Format("ManejarPerfiles.aspx?id={0}&accion={1}", PerfilId, (int)TipoAccion));
            }
            else
            {
                LabelMensaje.Text = "HA OCURRIDO UN ERROR";
            }
        }

        protected void ImageButtonCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("ManejarPerfiles.aspx");
        }

        private bool CrearPerfil()
        {
            Perfiles perfil = new Perfiles();


            perfil.idusuario = Convert.ToInt32(TextBoxUsuario);
            perfil.idrol = int.Parse(TextBoxRol.Text);

            perfil = ClienteRestServicio.Instancia.CrearPerfil(perfil);

            if (perfil != null && perfil.idperfil != 0)
            {
                PerfilId = perfil.idperfil.ToString();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void LlenarFormulario(string id)
        {
            int perfilId;
            if (int.TryParse(id, out perfilId))
            {
                Perfiles perfil = ClienteRestServicio.Instancia.ObtenerPerfilPorId(PerfilId.ToString());

                if (perfil != null)
                {
                    TextBoxUsuario.Text = perfil.idusuario.ToString();
                    TextBoxRol.Text = perfil.idrol.ToString();

                    PerfilId = perfil.idperfil.ToString();
                }

            }
        }

        private bool EditarPerfil()
        {
            Perfiles perfil = ClienteRestServicio.Instancia.ObtenerPerfilPorId(PerfilId);

            if (!string.IsNullOrWhiteSpace(TextBoxUsuario.Text))
            {
                perfil.idusuario = Convert.ToInt32(TextBoxUsuario);

            }
            if (!string.IsNullOrWhiteSpace(TextBoxRol.Text))
            {
                perfil.idrol = Convert.ToInt16(TextBoxRol);

            }

            perfil = ClienteRestServicio.Instancia.ActualizarPerfil(perfil);

            if (perfil != null && perfil.idperfil != 0)
            {
                PerfilId = perfil.idperfil.ToString();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}