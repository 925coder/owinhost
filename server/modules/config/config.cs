using Nancy;
using server.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.modules.config
{
  public class config : NancyModule
  {
    IRootPathProvider rootPathProvider;

    public config(IRootPathProvider rootPathProvider)
      : base("config")
    {
      this.rootPathProvider = rootPathProvider;
      var root = this.rootPathProvider.GetRootPath();
      var diRoot = new DirectoryInfo(root);

      Get["/"] = p =>
      {
        var configs = diRoot.GetFiles("*.config", SearchOption.AllDirectories).Select(f => new logFile { Path = f.FullName, Name = f.Name }).ToList();
        return View["index.cshtml", configs];
      };
    }
  }
}
