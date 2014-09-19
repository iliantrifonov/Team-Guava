using ExamSystem.Backend.Data;
using ExamSystem.Backend.Web.Infrastructure;
using Microsoft.Owin;
using Ninject;
using Ninject.Web;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using System.Reflection;
using ExamSystem.Backend.Web.PubnubCore;

[assembly: OwinStartup(typeof(ExamSystem.Backend.Web.Startup))]

namespace ExamSystem.Backend.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(GlobalConfiguration.Configuration);

        }
        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            RegisterMappings(kernel);
            return kernel;
        }

        private static void RegisterMappings(StandardKernel kernel)
        {
            kernel.Bind<IExamSystemData>().To<ExamSystemData>()
                .WithConstructorArgument("context",
                    c => new ExamSystemDbContext());

            kernel.Bind<IUserIdProvider>().To<AspNetUserIdProvider>();

            kernel.Bind<INotificationService>().To<NotificationService>();
        }
    }
}
