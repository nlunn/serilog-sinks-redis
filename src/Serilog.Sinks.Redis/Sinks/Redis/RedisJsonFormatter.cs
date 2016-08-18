using System;
using System.Collections.Generic;
using System.IO;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Json;

namespace Serilog.Sinks.Redis.Sinks.Redis
{
  public class RedisJsonFormatter : JsonFormatter, ITextFormatter
  {
    private readonly RedisConfiguration _config;
    private readonly string _closingDelimiter;
    private string _propFieldName;

    public RedisJsonFormatter( RedisConfiguration config ) : base( true )
    {
      _config = config;
      _closingDelimiter = Environment.NewLine;
      _propFieldName = config.PropFieldName;
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

    protected override void WriteProperties( IReadOnlyDictionary<string, LogEventPropertyValue> properties, TextWriter output )
    {
      output.Write( ",\"{0}\":{{", _propFieldName );
      WritePropertiesValues( properties, output );
      output.Write( "}" );
    }

  }
}