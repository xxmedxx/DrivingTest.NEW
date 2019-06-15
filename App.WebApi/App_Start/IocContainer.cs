using App.DataBaseAccess;
using App.EntityFramework.Repository;
using App.IRepository;
using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace App.WebApi
{
    public class IocContainer
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DrivingTestDBEntities>().AsSelf();
            builder.RegisterGeneric(typeof(Repository<>))
                   .As(typeof(IRepository<>))
                   .InstancePerDependency();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()); //Register WebApi Controllers

            var container = builder.Build();
            //var resolve = new AutofacWebApiDependencyResolver(container);
            //GlobalConfiguration.Configuration.DependencyResolver = resolve;

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container); //Set the WebApi DependencyResolver
        }
    }
}