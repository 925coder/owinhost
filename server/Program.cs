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

namespace server
{
  class Program
  {
    static void Main(string[] args)
    {
      XmlConfigurator.ConfigureAndWatch(new FileInfo("_trace.config"));

      var options = new StartOptions();
      options.Urls.Add("http://orange:8077");
      options.Urls.Add("http://localhost:8077");
      options.Urls.Add("http://*:8077");
      using (WebApp.Start<Startup>(options))
      {
        Logging.Log.Info("Starter server at : " + options.Urls.ToCsv());
        Console.ReadKey();
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
