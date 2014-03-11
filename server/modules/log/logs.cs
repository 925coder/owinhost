using Nancy;
using Nancy.Responses;
using server.models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;

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
        this.Context.Trace.TraceLog.WriteLog(s => s.AppendLine("In logs module filetype=" + type));

        if (type.StartsWith("log"))
        {
          type = "log";
        }
        else if (type.StartsWith("config"))
        {
          type = "config";
        }
        var logs = diRoot.GetFiles("*." + type, SearchOption.AllDirectories).Select(f => new ViewFile { Path = f.FullName, Name = f.Name, Type = type }).ToList();
        return View["filelist.cshtml", logs];
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

      Get["{type}/{name}/live", ctx => !ctx.Request.Url.Path.EndsWith("css") && !ctx.Request.Url.Path.EndsWith(".js")] = p =>
      {
        string path = Path.Combine(root, p.name);
        return View["livelog.cshtml", path];
      };

      Post["{type}/{name}"] = p =>
        {
          string s = this.Request.Form.edited;
          return s;
        };


      Get["/"] = p =>
        {
          using (var conn = GetConnection())
          {
            var r = conn.Query<Application>("select * from applications").ToList();
            return View["home.cshtml",r];
          }
        };
    
    }

    SqlConnection GetConnection()
    {
      var connstr = ConfigurationManager.ConnectionStrings["prototype"].ConnectionString;
      var conn = new SqlConnection(connstr);
      conn.Open();

      return conn;
    }
  }
}
