using System;
using StackExchange.Redis;

namespace Serilog.Sinks.Redis.Sinks.Redis
{
  public class RedisClient
  {
    private RedisConfiguration _configuration;
    private ConnectionMultiplexer _redis;
    private ISubscriber _subscriber;

    public RedisClient( RedisConfiguration configuration )
    {
      _configuration = configuration;
      _redis = ConnectionMultiplexer.Connect( _configuration.Host );
      _subscriber = _redis.GetSubscriber();
    }

    public void Publish( string message )
    {
      var key = Guid.NewGuid().ToString();
      _subscriber.Publish( key, message );
    }
  }
}
