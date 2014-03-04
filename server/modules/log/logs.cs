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
    public logs(IRootPathProvider rootPathProvider)
    {
      this.rootPathProvider = rootPathProvider;
      var root = this.rootPathProvider.GetRootPath();
      var diRoot = new DirectoryInfo(root);

      Get["edit"] = p =>
        {
          return View["editor.cshtml"];
        };

      Get["{type}"] = p =>
      {
        string type = p.type;

        if (type.StartsWith("log"))
        {
          type = "log";
        }
        else if (type.StartsWith("config"))
        {
          type = "config";
        }
        var logs = diRoot.GetFiles("*." + type, SearchOption.AllDirectories).Select(f => new ViewFile { Path = f.FullName, Name = f.Name, Type = type }).ToList();
        return View["index.cshtml", logs];
      };

      Get["{type}/{name}", ctx => !ctx.Request.Url.Path.EndsWith("css") && !ctx.Request.Url.Path.EndsWith(".js")] = p =>
        {
          string f = Path.Combine(root, p.name);
          FileStream fs = File.Open(f, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
          string content = "";

          using (fs)
          using (var sr = new StreamReader(fs))
          {
            content = sr.ReadToEnd();
          }
          //content = Nancy.Helpers.HttpUtility.HtmlEncode(content);
          return View["editor.cshtml", content];
        };

      Post["{type}/{name}"] = p =>
        {
          string s = this.Request.Form.edited;
          return s;
        };
    
      

    
    }
  }
}
