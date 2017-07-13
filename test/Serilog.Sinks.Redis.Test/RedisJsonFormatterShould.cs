using System;
using System.Collections.Generic;
using System.IO;
using Assent;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serilog.Events;
using Serilog.Parsing;


namespace Serilog.Sinks.Redis.Test
{
  [TestClass]
  public class RedisJsonFormatterShould
  {
    [TestMethod]
    public void MatchFormat()
    {
      var stream = new MemoryStream();
      TextWriter writer = new StreamWriter(stream);

      var config = new RedisConfiguration();
      config.MetaProperties.Add( "_index_name", "MyIndex" );
      var formatter = new RedisCompactJsonFormatter( new FormatterConfiguration(), config );
      var template = new MessageTemplateParser().Parse( "{Song}++ @{Complex}" );
      var dt = new DateTimeOffset( 2001, 2, 3, 4, 5, 6, TimeSpan.Zero );
      var properties = new List<LogEventProperty> { new LogEventProperty("Song", new ScalarValue("New Macabre")) };

      var logEvent = new LogEvent( dt, LogEventLevel.Information, null, template, properties );

      formatter.Format( logEvent, writer );

      writer.Flush();
      stream.Position = 0;
      var reader = new StreamReader( stream );
      var result = reader.ReadToEnd();
      this.Assent(result);
    }
  }
}
