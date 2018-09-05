using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckLinksConsole2
{
    public static class Logs
    {
       // public static ILoggerFactory Factory = new LoggerFactory();
       // static Logs() //cannot sdd parametr to ctor
         public static void Init(ILoggerFactory factory, IConfiguration configuration)
        {

            //var logger = Loggerfactory.CreateLogger("main");
            // Factory.AddConsole(LogLevel.Trace, includeScopes: true);   //LogLevel.Debug
            factory.AddConsole(configuration.GetSection("Logging"));
            factory.AddFile("logs/checklinks-{Date}.json",
                isJson: true, minimumLevel: LogLevel.Trace);
        }
       
    }
}
