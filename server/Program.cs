using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using coder925.library;
using log4net;
using log4net.Config;
using System.IO;
using System.Reflection;

namespace server
{
  class Program
  {
    static void Main(string[] args)
    {
      XmlConfigurator.ConfigureAndWatch(new FileInfo("_trace.config"));

      var options = new StartOptions();

      options.Urls.Add("http://"+ NetEx.GetCurrentHostName() +":8077");
      options.Urls.Add("http://localhost:8077");
      options.Urls.Add("http://*:8077");

#if DEBUG
      WatchViews();
#endif


      using (WebApp.Start<Startup>(options))
      {
        Logging.Log.Info("Starter server at : " + options.Urls.ToCsv());
        Console.ReadKey();
      }
    }

    private static void WatchViews()
    {
      string viewsDebugFolder = @"C:\code\web\owinhost\server\views";

      FileSystemWatcher watcher = new FileSystemWatcher(viewsDebugFolder,"*html");
      watcher.IncludeSubdirectories = true;
      watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.LastAccess | NotifyFilters.Attributes;

      watcher.Changed += watcher_Changed;
      watcher.Created += watcher_Changed;
      watcher.Deleted += watcher_Changed;
      
      watcher.EnableRaisingEvents = true;
    }

    static void watcher_Changed(object sender, FileSystemEventArgs e)
    {
      var f = e.FullPath;
      var d = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      var views = Path.Combine(d, "views");
      var dest = Path.Combine(d,"views",e.Name);
      if (f.EndsWith("html"))
      {
        File.Copy(f,dest,true);
      }
    }
  }

  public class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      app.UseNancy();
    }
  }
}
