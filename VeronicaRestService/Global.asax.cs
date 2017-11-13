using System;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;

namespace VeronicaRestService
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes();
        }


        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        private void RegisterRoutes()
        {
            // Edit the base address of ServiciosServiciosServicioTelefonico by replacing the "ServiciosServiciosServicioTelefonico" string below
            RouteTable.Routes.Add(new ServiceRoute("ServiciosServicioTelefonico", new WebServiceHostFactory(), typeof(ServiciosServicioTelefonico)));
        }
    }
}
