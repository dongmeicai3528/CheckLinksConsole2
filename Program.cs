using System;
using System.IO;
using System.Net.Http;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

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
            var config = new Config(args);
            Logs.Init(config.ConfigurationRoot);
            var logger = Logs.Factory.CreateLogger <Program>();
           
            Directory.CreateDirectory(config.Output.GetReportDirectory()); ;

            // return;

            // var file = Path.GetTempFileName();
            // var file = outputPath;
            //   Console.WriteLine($"Saving report to {config.Output.File}");
            logger.LogInformation($"Saving report to {config.Output.GetReportFilePath()}");

            //var site = "https://g0t4.github.io/pluralsight-dotnet-core-xplat-apps";
            var client = new HttpClient();
            var body = client.GetStringAsync(config.Site);

            //Console.WriteLine(body.Result);
            //Console.WriteLine();
            
            logger.LogDebug(body.Result);
            var links = LinkChecker.GetLinks(config.Site,body.Result);
            // links.ToList().ForEach(Console.WriteLine);
          //  return;
            //write out the links
            //File.WriteAllLines(file, links);
           
            var checkLinks = LinkChecker.CheckLinks(links);
            using (var f = File.CreateText(config.Output.GetReportFilePath()))
            {
                foreach (var link in checkLinks.OrderBy(l=>l.IsMissing))
                {
                    var status = link.IsMissing ? "missing" : "OK";
                    f.WriteLine($"{status} - {link.Link}");
                    
                }
            }

        }
    }
}
