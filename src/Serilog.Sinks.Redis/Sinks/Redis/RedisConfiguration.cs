using System;
using System.Collections.Generic;

namespace Serilog.Sinks.Redis
{
  public class RedisConfiguration
  {
    public string Host { get; set; }
    public Dictionary<string, string> MetaProperties { get; }
    public IFormatterConfiguration FormatterConfig { get; set; }
    public string Key { get; set; }

    public RedisConfiguration()
    {
      FormatterConfig = new FormatterConfiguration();
      MetaProperties = new Dictionary<string, string>();
    }
  }
}
