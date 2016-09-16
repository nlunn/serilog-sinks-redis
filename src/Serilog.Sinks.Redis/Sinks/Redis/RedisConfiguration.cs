using System;
using System.Collections.Generic;

namespace Serilog.Sinks.Redis
{
  public class RedisConfiguration
  {
    public string Host;
    public readonly Dictionary<string, string> MetaProperties = new Dictionary<string, string>();
  }
}
