using System.Collections.Generic;
using System.IO;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Sinks.PeriodicBatching;

namespace Serilog.Sinks.Redis.Sinks.Redis
{
  public class RedisSink : PeriodicBatchingSink
  {
    private readonly ITextFormatter _formatter;
    private readonly RedisClient _client;

    public RedisSink( RedisConfiguration configuration ) : base( configuration.BatchSizeLimit, configuration.Period )
    {
      _formatter = new RedisJsonFormatter( configuration );
      _client = new RedisClient( configuration );
    }

    protected override void EmitBatch( IEnumerable<LogEvent> events )
    {
      foreach ( var logEvent in events )
      {
        Emit( logEvent );
      }
    }

    public new void Emit( LogEvent logEvent )
    {
      var sw = new StringWriter();
      _formatter.Format( logEvent, sw );
      _client.Publish( sw.ToString() );
    }
  }
}
