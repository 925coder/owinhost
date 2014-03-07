using Nancy;
using Nancy.Bootstrappers.Autofac;
using Nancy.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
  public class Bootstrapper : AutofacNancyBootstrapper
  {
    protected override Nancy.Diagnostics.DiagnosticsConfiguration DiagnosticsConfiguration
    {
      get
      {
        return new DiagnosticsConfiguration { Password="abcd"};
      }
    }

    protected override void ApplicationStartup(Autofac.ILifetimeScope container, Nancy.Bootstrapper.IPipelines pipelines)
    {
      StaticConfiguration.EnableRequestTracing = true;
    }

    protected override Autofac.ILifetimeScope GetApplicationContainer()
    {
      return base.GetApplicationContainer();
    }
  }
}
