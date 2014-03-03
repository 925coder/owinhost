﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.models
{
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
}