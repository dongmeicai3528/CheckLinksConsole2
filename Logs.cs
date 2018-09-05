using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckLinksConsole2
{
    public static class Logs
    {
        public static LoggerFactory Factory = new LoggerFactory();
       // static Logs() //cannot sdd parametr to ctor
         public static void Init( IConfiguration configuration)
        {

            //var logger = Loggerfactory.CreateLogger("main");
            // Factory.AddConsole(LogLevel.Trace, includeScopes: true);   //LogLevel.Debug
            Factory.AddConsole(configuration.GetSection("Logging"));
            Factory.AddFile("logs/checklinks-{Date}.json",
                isJson: true, minimumLevel: LogLevel.Trace);
        }
       
    }
}
