using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.models
{
  public class ViewFile
  {
    public string Name { get; set; }
    public string Path { get; set; }
    public string Type { get; set; }
  }

  public class log
  {
    public Severity Severity { get; set; }
    public string Message { get; set; }
  }

  public enum Severity
  {
    Info,
    Warning,
    Error,
    Critical
  }

  public class Application
  {
    public int Id { get; set; }
    public string Url { get; set; }
    public string AppName { get; set; }
    public string UserName { get; set; }
    public bool Running { get; set; }
    public DateTime LastModified { get; set; }
    public byte[] Version { get; set; }
  }
  
}
