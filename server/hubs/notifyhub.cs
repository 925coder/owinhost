using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.hubs
{
  public class notifyhub : Hub
  {
    public void Publih2Clients(string name, string message)
    {
      Clients.All.broadCast(name, message);
    }
  }
}
