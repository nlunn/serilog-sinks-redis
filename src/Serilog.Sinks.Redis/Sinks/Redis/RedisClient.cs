using System;
using StackExchange.Redis;

namespace Serilog.Sinks.Redis.Sinks.Redis
{
  public class RedisClient
  {
    private readonly ISubscriber _subscriber;

    public RedisClient( RedisConfiguration configuration )
    {
      var redis = ConnectionMultiplexer.Connect( configuration.Host );
      _subscriber = redis.GetSubscriber();
    }

    public void Publish( string message )
    {
      var key = Guid.NewGuid().ToString();
      _subscriber.Publish( key, message );
    }
  }
}
