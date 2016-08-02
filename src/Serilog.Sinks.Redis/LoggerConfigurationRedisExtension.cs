using System;
using Serilog.Configuration;
using Serilog.Formatting.Raw;
using Serilog.Sinks.Redis.Sinks.Redis;

namespace Serilog.Sinks.Redis
{
  public static class LoggerConfigurationRedisExtension
  {
    public static LoggerConfiguration Redis( this LoggerSinkConfiguration loggerConfiguration, RedisConfiguration redisConfiguration )
    {
      if( loggerConfiguration == null ) throw new ArgumentNullException( nameof( loggerConfiguration ) );
      if( redisConfiguration == null ) throw new ArgumentNullException( nameof( redisConfiguration ) );

      // calls overloaded extension method
      return loggerConfiguration.Sink( new RedisSink( redisConfiguration ) );
    }
  }
}