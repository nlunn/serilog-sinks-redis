using System;
using System.IO;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Json;

namespace Serilog.Sinks.Redis
{
  public class RedisJsonFormatter : JsonFormatter, ITextFormatter
  {
    private readonly RedisConfiguration _config;
    private readonly string _closingDelimiter;

    public RedisJsonFormatter( RedisConfiguration config ) : base( true, renderMessage: true )
    {
      _config = config;
      _closingDelimiter = Environment.NewLine;
    }

    public new void Format( LogEvent logEvent, TextWriter output )
    {
      output.Write( "{" );
      var delim = ",";
      base.Format( logEvent, output );

      foreach( var item in _config.MetaProperties )
      {
        WriteJsonProperty( item.Key, item.Value, ref delim, output );
      }

      output.Write( "}" );
      output.Write( _closingDelimiter );
    }
  }
}