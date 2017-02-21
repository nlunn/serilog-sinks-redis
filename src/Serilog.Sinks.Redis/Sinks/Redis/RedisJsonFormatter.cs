using System;
using System.IO;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Json;
using Serilog.Sinks.Redis;

namespace SerilogToElkExample
{
  public class RedisCompactJsonFormatter : ITextFormatter
  {
    private readonly IFormatterConfiguration _config;
    private readonly RedisConfiguration _redisConfig;
    readonly JsonValueFormatter _valueFormatter;

    /// <summary>
    /// Construct a <see cref="RedisCompactJsonFormatter"/>, optionally supplying a formatter for
    /// <see cref="LogEventPropertyValue"/>s on the event.
    /// </summary>
    /// <param name="config">Formatter configuration</param>
    /// <param name="redisConfig">Redis configuration</param>
    /// <param name="valueFormatter">A value formatter, or null.</param>
    public RedisCompactJsonFormatter( IFormatterConfiguration config, RedisConfiguration redisConfig,
      JsonValueFormatter valueFormatter = null )
    {
      _config = config;
      _redisConfig = redisConfig;
      _valueFormatter = valueFormatter ?? new JsonValueFormatter( typeTagName: "$type" );
    }

    /// <summary>
    /// Format the log event into the output. Subsequent events will be newline-delimited.
    /// </summary>
    /// <param name="logEvent">The event to format.</param>
    /// <param name="output">The output.</param>
    public void Format( LogEvent logEvent, TextWriter output )
    {
      if ( logEvent == null ) throw new ArgumentNullException( nameof( logEvent ) );

      if ( output == null ) throw new ArgumentNullException( nameof( output ) );

      output.Write( "{" );
      output.WriteJson( "@t", logEvent.Timestamp.UtcDateTime.ToString( "O" ) );
      output.AppendJson( "@mt", logEvent.MessageTemplate.Text );

      if ( _config.WriteRenderedMessage )
        output.AppendJson( "@m", logEvent.MessageTemplate.Render( logEvent.Properties ) );

      foreach ( var item in _redisConfig.MetaProperties )
      {
        output.AppendJson( item.Key, item.Value );
      }

      output.WriteRenderings( logEvent );
      output.AppendJson( "@l", logEvent.Level );

      if ( logEvent.Exception != null )
        output.AppendJson( "@x", logEvent.Exception );


      output.WriteProperties( logEvent.Properties, _valueFormatter );

      output.WriteLine( '}' );
    }
  }
}