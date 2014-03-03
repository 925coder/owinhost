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
    public logs(IRootPathProvider rootPathProvider) : base("log")
    {
      this.rootPathProvider = rootPathProvider;

      Get["/"] = p =>
      {
        var root = this.rootPathProvider.GetRootPath();
        var logs = Directory.GetFiles(root, "*.log", SearchOption.AllDirectories);
        return View["index.cshtml", logs];
      };
    }
  }
}
