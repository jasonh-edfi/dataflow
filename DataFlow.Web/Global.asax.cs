﻿using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Extras.AggregateService;
using Autofac.Integration.Mvc;
using CacheManager.Core;
using DataFlow.Common.DAL;
using DataFlow.Common.Services;
using DataFlow.Web.Helpers;
using DataFlow.Web.Services;
using NLog;

namespace DataFlow.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var cacheConfig = ConfigurationBuilder.BuildConfiguration("DataFlow.Web", settings =>
            {
                settings.WithUpdateMode(CacheUpdateMode.None)
                    .WithSystemRuntimeCacheHandle("DataFlow.Web")
                    .WithExpiration(ExpirationMode.Sliding, TimeSpan.FromHours(2));
            });
            var cacheFactory = CacheFactory.FromConfiguration<string>("DataFlow.Web", cacheConfig);

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterAggregateService<IBaseServices>();
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterType<DataFlowDbContext>().InstancePerRequest();
            builder.RegisterType<EdFiService>().InstancePerRequest();
            builder.RegisterType<ConfigurationService>().InstancePerRequest();
            builder.RegisterType<EdFiMetadataProcessor>().InstancePerRequest();
            builder.Register(c => LogManager.GetLogger("DataFlow.Web")).As<ILogger>().InstancePerRequest();
            builder.RegisterType<NLogService>().AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterType<CacheService>().WithParameters(new[]
            {
                new TypedParameter(typeof(ICacheManagerConfiguration), cacheConfig),
                new TypedParameter(typeof(ICacheManager<string>), cacheFactory)
            }).As<ICacheService>().SingleInstance();

            //builder.Register(c =>
            //{
            //    var result = new BaseController();
            //    var dep = c.Resolve<ConfigurationService>();
            //    result.ConfigurationService = dep;
            //    return result;
            //});

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
