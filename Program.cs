using System;
using System.IO;
using System.Net.Http;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Hosting;

namespace CheckLinksConsole2
{
    partial class Program
    {

        static void Main(string[] args)
        {


            //use the class OutputSettings
            //var outputFolder = configuration["output:folder"]; ////"reports";
            //var outputFile = configuration["output:file"]; //"report.txt"; */

            //override
            //dotnet run /output:folder=blah

            //dotnet run /output:folder=foo /output:file=bar.txt

            //configuration.GetSection("output:folder"]; //

            // var outputPath = $"{currentDirectory}/{outputFolder}/{outputFile}";

            //var outputFolder = outputSettings.Folder;
            //var outputFile = outputSettings.File;

            //var outputPath = outputSettings.GetReportFilePath();//Path.Combine(currentDirectory,outputFolder,outputFile);            
            //var directory = Path.GetDirectoryName(outputPath);

            //  factory.AddDebug();
            //var logger = Logs.Factory.CreateLogger("main");

            //var config = new Config(args);
            //Logs.Init(config.ConfigurationRoot);

            GlobalConfiguration.Configuration.UseMemoryStorage();

            // RecurringJob.AddOrUpdate(() => Console.WriteLine("\n\nRecurring Job\n\n"), Cron.Minutely);
           

            var host = new WebHostBuilder()
               .UseKestrel()                   
               .UseContentRoot(Directory.GetCurrentDirectory())
               .UseStartup<Startup>()
               .Build();

            //var log = host.Services.GetService<ILogger<Program>>();
            //log.LogInformation("test");

            //RecurringJob.AddOrUpdate<CheckLinkJob>("check-link", j => j.Execute(config.Site, config.Output), Cron.Minutely);
            // RecurringJob.AddOrUpdate<CheckLinkJob>("check-link", j => j.Execute(config.Site, config.Output), Cron.Minutely);
            RecurringJob.AddOrUpdate<CheckLinkJob>("check-link", j => j.Execute(), Cron.Minutely);
            RecurringJob.Trigger("check-link");

            //RecurringJob.AddOrUpdate(() => Console.Write("simple"), Cron.Minutely);

            //using (var server = new BackgroundJobServer())//in start up hangfire server
            //{
            //  Console.WriteLine("Hangfire server started.");
            //Console.ReadKey();//add a web endpoint
            host.Run();
            //}
           
              
           

        }
    }
}
