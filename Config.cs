using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CheckLinksConsole2
{
    public class Config
    {
        // public Config()
        public static IConfigurationRoot Build()
        {
            var inMemory = new Dictionary<string, string>
            {
                { "site", "https://g0t4.github.io/pluralsight-dotnet-core-xplat-apps" },
                 {"output:folder", "reports1" } ,
                 //{"output:file", "report1.txt"} //remove this to the class OutputSettings
            };
           
            //provider agnostic
            var configBuilder = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemory)
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("checksettings.json", true)
                .AddCommandLine(Environment.GetCommandLineArgs().Skip(1).ToArray())  //args
                .AddEnvironmentVariables();  //noramally env is the last resort

           return configBuilder.Build();
          //  ConfigurationRoot = configuration;

            //dotnet run /site=https://goo.gl/kIU02q
            //dotnet run /site=https://google.com
            //$env:site="https://goo.gl/kIU02q"
            //dotnet run
            //what if both env and commandline var
            //dotnet run /site=https://goo.gl/9RU0BE
            //Order matter - the last win

            //var currentDirectory = Directory.GetCurrentDirectory();

            //var outputSettings = new OutputSettings();
            //configuration.GetSection("output").Bind(outputSettings);
            //turn the above 2 lines to:
          //  Site = configuration["site"];
         //   Output  = configuration.GetSection("output").Get<OutputSettings>();
        }
      //  public string Site { get; set; }
      //  public OutputSettings Output { get; set; }
      //  public IConfigurationRoot ConfigurationRoot { get; set; }

        
    }
}
