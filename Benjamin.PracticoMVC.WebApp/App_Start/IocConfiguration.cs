
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//agregados
using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using System.Reflection;

namespace Benjamin.PracticoMVC.WebApp.App_Start
{
    public class IocConfiguration
    {

        public static IContainer Container { get; set; }

        public static T GetInstance<T>()
        {
            return Container.Resolve<T>();
        }

        public static void Configure()
        {
            var builder = new ContainerBuilder();

            RegisterRepositories(builder);
            RegisterServices(builder);
            RegisterControllers(builder);

            Container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<AccesoDatos.UsuariosDAL>().As<AccesoDatos.IUsuarios>().SingleInstance();
        }

        private static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<AccesoDatos.RolesDAL>().As<AccesoDatos.IRoles>().SingleInstance();
        }

        private static void RegisterControllers(ContainerBuilder builder)
        {
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
        }

    }
}