using System;
using System.IO;
using System.Net.Http;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.Options;

namespace CheckLinksConsole2
{
    public class CheckLinkJob
    {
        private ILogger _Logger;
        private OutputSettings _Output;
       private SiteSettings _Site;
        private LinkChecker _LinkChecker;
        public CheckLinkJob(ILogger<CheckLinkJob> logger, IOptions<OutputSettings> outputOptions,
            IOptions<SiteSettings> siteOptions, LinkChecker linkChecker)
        {
            _Logger = logger;
            _Logger.LogInformation($"{Guid.NewGuid()}");
            _Output = outputOptions.Value;
            _Site = siteOptions.Value;
            _LinkChecker = linkChecker;
        }

        public void Execute()  //(string site) //, OutputSettings output)
        {
           // var logger = Logs.Factory.CreateLogger<CheckLinkJob>();

            Directory.CreateDirectory(_Output.GetReportDirectory()); ;

            // return;

            // var file = Path.GetTempFileName();
            // var file = outputPath;
            //   Console.WriteLine($"Saving report to {config.Output.File}");
            _Logger.LogInformation(200, $"Saving report to {_Output.GetReportFilePath()}");

            //var site = "https://g0t4.github.io/pluralsight-dotnet-core-xplat-apps";
            var client = new HttpClient();
            var body = client.GetStringAsync(_Site.Site);

            //Console.WriteLine(body.Result);
            //Console.WriteLine();

            _Logger.LogDebug(body.Result);
            var links = _LinkChecker.GetLinks(_Site.Site, body.Result);
            // links.ToList().ForEach(Console.WriteLine);
            //  return;
            //write out the links
            //File.WriteAllLines(file, links);

            var checkLinks = _LinkChecker.CheckLinks(links);
            using (var f = File.CreateText(_Output.GetReportFilePath()))
            using (var linksDb = new LinksDb())
            {
                foreach (var link in checkLinks.OrderBy(l => l.Exists))
                {
                    var status = link.IsMissing ? "missing" : "OK";
                    f.WriteLine($"{status} - {link.Link}");
                    linksDb.Links.Add(link);
                }
                linksDb.SaveChanges();
            }
        }
           

    }
}
