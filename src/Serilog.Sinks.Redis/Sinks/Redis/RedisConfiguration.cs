using System;
using System.Collections.Generic;

namespace Serilog.Sinks.Redis.Sinks.Redis
{
  public class RedisConfiguration
  {
    public int BatchSizeLimit = 50;
    public TimeSpan Period = TimeSpan.FromSeconds( 2 );
    public string Host;
    public readonly Dictionary<string, string> MetaProperties = new Dictionary<string, string>();
  }
}
