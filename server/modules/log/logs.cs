using Nancy;
using Nancy.Responses;
using server.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.modules.logging
{
  public class logs : NancyModule
  {
    IRootPathProvider rootPathProvider;
    public logs(IRootPathProvider rootPathProvider) : base("logs")
    {
      this.rootPathProvider = rootPathProvider;
      var root = this.rootPathProvider.GetRootPath();
      var diRoot = new DirectoryInfo(root);


      Get["/{name}"] = p =>
        {
          string f = Path.Combine(root, p.name);
          FileStream fs = File.Open(f, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
          string content = "";

          using (fs)
          using (var sr = new StreamReader(fs))
          {
            content = sr.ReadToEnd();
          }
           
          return Response.AsText(content, @"text/plain");
        };

      Get["/"] = p =>
        {
          var logs = diRoot.GetFiles("*.log", SearchOption.AllDirectories).Select(f => new logFile { Path=f.FullName, Name =f.Name }).ToList();
          return View["index.cshtml", logs];
        };
    }
  }
}
