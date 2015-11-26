using Microsoft.Azure.Documents;
using Microsoft.Owin;
using Owin;
using SmartSpaceWeb.Models;
using System;
using System.Timers;

[assembly: OwinStartupAttribute(typeof(SmartSpaceWeb.Startup))]
namespace SmartSpaceWeb
{
    public partial class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //DatabasePolling.RegisterPolling();

        }


    }
}
