﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AnimeRecs.RecService.ClientLib;
using AnimeRecs.Web.MvcExtensions;
using MalApi;
using AnimeRecs.DAL;
using AnimeRecs.Web.Controllers;
using StackExchange.Profiling;

namespace AnimeRecs.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static bool m_errorOnStarting = false;
        public static bool ErrorOnStarting { get { return m_errorOnStarting; } set { m_errorOnStarting = value; } }
        
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //IExceptionFilter
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            MvcHandler.DisableMvcResponseHeader = true;

            GlobalFilters.Filters.Add(new LoggingExceptionFilter());
            
            List<IDisposable> disposablesInitialized = new List<IDisposable>();
            try
            {
                AreaRegistration.RegisterAllAreas();

                RegisterGlobalFilters(GlobalFilters.Filters);
                RegisterRoutes(RouteTable.Routes);

                ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
                ModelBinders.Binders.Add(typeof(decimal?), new NullableDecimalModelBinder());

                AppGlobals.Config = Config.FromWebConfig();
                AppGlobals.RecClientFactory = new RecClientFactory(AppGlobals.Config.RecServicePort, AppGlobals.Config.SpecialRecSourcePorts);
                disposablesInitialized.Add(AppGlobals.RecClientFactory);

                IMyAnimeListApi api;
                if (!AppGlobals.Config.UseLocalDbMalApi)
                {
                    api = new MyAnimeListApi()
                    {
                        UserAgent = AppGlobals.Config.MalApiUserAgentString,
                        TimeoutInMs = AppGlobals.Config.MalTimeoutInMs
                    };
                }
                else
                {
                    api = new PgMyAnimeListApi(AppGlobals.Config.PostgresConnectionString);
                }

                disposablesInitialized.Add(api);

                CachingMyAnimeListApi cachingApi = new CachingMyAnimeListApi(api, AppGlobals.Config.AnimeListCacheExpiration, ownApi: true);
                disposablesInitialized.Add(cachingApi);

                AppGlobals.MalApiFactory = new SingleMyAnimeListApiFactory(cachingApi);
                disposablesInitialized.Add(AppGlobals.MalApiFactory);

                IAnimeRecsDbConnectionFactory rawConnectionFactory = new AnimeRecsDbConnectionFactory(AppGlobals.Config.PostgresConnectionString);
                IAnimeRecsDbConnectionFactory profiledConnectionFactory = new MiniProfilerAnimeRecsDbConnectionFactory(rawConnectionFactory);
                AppGlobals.DbConnectionFactory = profiledConnectionFactory;
            }
            catch
            {
                ErrorOnStarting = true;
                foreach (IDisposable disposable in disposablesInitialized)
                {
                    disposable.Dispose();
                }
                throw;
            }
        }

        protected void Application_End()
        {
            if (AppGlobals.RecClientFactory != null)
            {
                AppGlobals.RecClientFactory.Dispose();
            }
            
            if (AppGlobals.MalApiFactory != null)
            {
                AppGlobals.MalApiFactory.Dispose();
            }
        }

        protected void Application_Error()
        {
            Exception exception = Server.GetLastError();
            HttpException httpException = exception as HttpException;
            Response.Clear();
            Server.ClearError();
            RouteData routeData = new RouteData();
            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = "Error";
            routeData.Values["exception"] = exception;
            Response.StatusCode = 500;
            if (httpException != null)
            {
                Response.StatusCode = httpException.GetHttpCode();
            }
            routeData.Values["statusCode"] = Response.StatusCode;
            Response.TrySkipIisCustomErrors = true;
            IController errorsController = new ErrorController();
            HttpContextWrapper wrapper = new HttpContextWrapper(Context);
            RequestContext requestContext = new RequestContext(wrapper, routeData);
            errorsController.Execute(requestContext);
        }

        protected void Application_BeginRequest()
        {
            if (ErrorOnStarting)
            {
                Response.StatusCode = 503;
                Response.Write("Oops! Something went terribly wrong. A site admin will fix this problem shortly.");
                Response.End();
            }
            else
            {
                if (Request.IsLocal)
                {
                    MiniProfiler.Start();
                }
            }
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }
    }
}

// Copyright (C) 2012 Greg Najda
//
// This file is part of AnimeRecs.Web.
//
// AnimeRecs.Web is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// AnimeRecs.Web is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with AnimeRecs.Web.  If not, see <http://www.gnu.org/licenses/>.