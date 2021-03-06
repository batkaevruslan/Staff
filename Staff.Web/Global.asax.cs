﻿using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Autofac;
using Autofac.Integration.Mvc;

using RB.Staff.Common.Pub.Repositories;

using Staff.Data.EF;
using Staff.Data.EF.Pub;
using Staff.Data.EF.Pub.Repositories;
using Staff.Services;

namespace Staff
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes( RouteTable.Routes );
            FilterConfig.RegisterGlobalFilters( GlobalFilters.Filters );

            var builder = new ContainerBuilder();
            builder.RegisterControllers( typeof( MvcApplication ).Assembly );

            builder.Register( x => new StaffDbContext( "StaffDataConnectionString" ) );
            builder.RegisterType<PersonRepositoryEF>().As<IPersonRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PersonService>().As<IPersonService>().InstancePerLifetimeScope();
            var container = builder.Build();
            DependencyResolver.SetResolver( new AutofacDependencyResolver( container ) );

            DefaultModelBinder.ResourceClassKey = "Localization";
        }
    }
}