using Nancy;
using Nancy.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
  public class Bootstrapper : DefaultNancyBootstrapper
  {
    protected override Nancy.Diagnostics.DiagnosticsConfiguration DiagnosticsConfiguration
    {
      get
      {
        return new DiagnosticsConfiguration { Password="abcd"};
      }
    }

    protected override void ApplicationStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
    {
      StaticConfiguration.EnableRequestTracing = true;
    }
  }
}
