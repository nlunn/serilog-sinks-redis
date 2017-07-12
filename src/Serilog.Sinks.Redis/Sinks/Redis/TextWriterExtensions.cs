using System.Collections.Generic;
using System.IO;
using System.Linq;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Parsing;

namespace Serilog.Sinks.Redis
{
  internal static class TextWriterExtensions
  {
    public static void WriteJson( this TextWriter writer, string key, object value, JsonValueFormatter valueFormatter = null )
    {
      writer.Write( $"\"{key}\":" );

      if ( valueFormatter != null )
      {
        valueFormatter.Format( (LogEventPropertyValue)value, writer );
      }
      else
      {
        JsonValueFormatter.WriteQuotedJsonString( value.ToString(), writer );
      }
    }

    public static void AppendJson( this TextWriter writer, string key, object value, JsonValueFormatter valueFormatter = null )
    {
      writer.Write( "," );
      writer.WriteJson( key, value, valueFormatter );
    }

    public static void WriteRenderings( this TextWriter writer, LogEvent logEvent )
    {
      var tokensWithFormat = logEvent.MessageTemplate.Tokens
        .OfType<PropertyToken>()
        .Where(pt => pt.Format != null);

      // Better not to allocate an array in the 99.9% of cases where this is false
      // ReSharper disable once PossibleMultipleEnumeration
      if ( tokensWithFormat.Any() )
      {
        writer.Write( ",\"@r\":[" );
        var delim = "";

        // ReSharper disable once PossibleMultipleEnumeration
        foreach ( var r in tokensWithFormat )
        {
          writer.Write( delim );
          delim = ",";
          var space = new StringWriter();
          r.Render( logEvent.Properties, space );
          JsonValueFormatter.WriteQuotedJsonString( space.ToString(), writer );
        }

        writer.Write( ']' );
      }
    }

    public static void WriteProperties( this TextWriter output, IReadOnlyDictionary<string, LogEventPropertyValue> properties, JsonValueFormatter valueFormatter )
    {
      foreach ( var property in properties )
      {
        var name = property.Key;

        if ( name.Length > 0 && name[ 0 ] == '@' )
        {
          // Escape first '@' by doubling
          name = '@' + name;
        }

        output.AppendJson( name, property.Value, valueFormatter );
      }
    }

  }
}