using System.IO;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace Serilog.Sinks.Redis
{
  public class RedisSink : ILogEventSink
  {
    private readonly ITextFormatter _formatter;
    private readonly RedisClient _client;

    public RedisSink( RedisConfiguration configuration, ITextFormatter formatter )
    {
      _formatter = formatter;
      _client = new RedisClient( configuration );
    }

    public virtual void Emit( LogEvent logEvent )
    {
      var sw = new StringWriter();
      _formatter.Format( logEvent, sw );
      _client.Publish( sw.ToString() );
    }
  }
}
