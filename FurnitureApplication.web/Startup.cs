﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FurnitureApplication.web.Startup))]
namespace FurnitureApplication.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
