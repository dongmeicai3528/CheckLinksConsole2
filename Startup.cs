using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckLinksConsole2
{
    public class Startup
    {
        private IConfigurationRoot _Config;
        public void ConfigureServices(IServiceCollection services)
        {
            // var config = new Config();
            _Config = Config.Build();

            services.AddHangfire(c => c.UseMemoryStorage());
            services.AddTransient<CheckLinkJob>();  //no need
            services.AddTransient<LinkChecker>();
            services.Configure<OutputSettings>(_Config.GetSection("output"));
            services.Configure<SiteSettings>(_Config);
            //Output = configuration.GetSection("output").Get<OutputSettings>();
            //  GlobalConfiguration.Configuration.UseMemoryStorage();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env ,ILoggerFactory loggerFactory)
        {
            //var config = new Config();
            //Logs.Factory = loggerFactory;
            Logs.Init(loggerFactory, _Config);

            app.UseHangfireServer();
            app.UseHangfireDashboard();  //localhost:5000/hangfire

           // RecurringJob.AddOrUpdate<CheckLinkJob>("check-link", j => j.Execute(), Cron.Minutely);
            //job dependent on config pain - 
            // app.Run(async context => await context.Response.WriteAsync("we are doing well"));  // http://localhost:5000
        }

     }
}
