using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using server.models;

namespace server.hubs
{
  public class notifyhub : Hub
  {
    public void Publish2Clients(string name, string message)
    {
      Clients.All.show(name, message);
    }
  }

  public class log4netLogHub : Hub
  {

  }

  public class applicationConnectHub : Hub
  {

  }

  public class Monitor
  {
    public Monitor()
    {
    }

    private static IHubContext hub = GlobalHost.ConnectionManager.GetHubContext<applicationConnectHub>();

    public void Start()
    {
      var connStr = ConfigurationManager.ConnectionStrings["prototype"].ConnectionString;
      var sql = "select Version from [dbo].[Applications]";
      var conn = new SqlConnection(connStr);
      try
      {
        SqlDependency.Start(connStr);
        var cmd = new SqlCommand(sql, conn);
        var dep = new SqlDependency(cmd);
        using (conn)
        using (cmd)
        {
          conn.Open();
          cmd.ExecuteNonQuery();
          dep.OnChange += dep_OnChange;
        }
        
      }
      catch (Exception ex)
      {
        
      }
    }

    void dep_OnChange(object sender, SqlNotificationEventArgs e)
    {
      using (var conn = GetConnection())
      {
        var sql = @"
SELECT [Id]
      ,[Url]
      ,[AppName]
      ,[UserName]
      ,[Running]
      ,[LastModified]
      ,[Version]
  FROM [prototypes].[dbo].[Applications]";

        var r = conn.Query<Application>(sql).ToList();
        hub.Clients.All.updateApplicationList(r);

        //resubscribe

        this.Start();
      }
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
