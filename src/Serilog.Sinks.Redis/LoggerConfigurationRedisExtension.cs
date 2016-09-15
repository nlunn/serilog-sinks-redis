using System;
using Serilog.Configuration;
using Serilog.Formatting;
using Serilog.Sinks.Redis;

namespace Serilog
{
  public static class LoggerConfigurationRedisExtension
  {
    public static LoggerConfiguration Redis( this LoggerSinkConfiguration loggerConfiguration, RedisConfiguration redisConfiguration, ITextFormatter formatter = null )
    {
      if( loggerConfiguration == null ) throw new ArgumentNullException( nameof( loggerConfiguration ) );
      if( redisConfiguration == null ) throw new ArgumentNullException( nameof( redisConfiguration ) );

      // calls overloaded extension method
      var f = formatter ?? new RedisJsonFormatter( redisConfiguration );
      return loggerConfiguration.Sink( new RedisSink( redisConfiguration, f ) );
    }
  }
}