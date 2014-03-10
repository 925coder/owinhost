using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Appender;
using log4net.Core;
using server.hubs;
using Microsoft.AspNet.SignalR;

namespace server
{
  public class LogInfo
  {
    public string Message { get; set; }
    public Level Level { get; set; }
    public LocationInfo LocationInfo { get; set; }
    public string LoggerName { get; set; }
  }

  public class SignalRAppender : AppenderSkeleton
  {
    Autofac.ILifetimeScope container;
    static IHubContext hub = GlobalHost.ConnectionManager.GetHubContext<log4netLogHub>();
    protected override void Append(log4net.Core.LoggingEvent loggingEvent)
    {
      var logInfo = new LogInfo {Message = loggingEvent.RenderedMessage, LocationInfo = loggingEvent.LocationInformation, Level = loggingEvent.Level, LoggerName = loggingEvent.LoggerName  };
      hub.Clients.All.addLog(logInfo);
    }
  }
}
