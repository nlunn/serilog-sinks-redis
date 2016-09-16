using System;
using StackExchange.Redis;

namespace Serilog.Sinks.Redis
{
  public class RedisClient
  {
    private readonly RedisConfiguration _configuration;
    private ISubscriber _subscriber;
    private readonly object _subscriberLock = new object();

    private ISubscriber Subscriber
    {
      get
      {
        if( _subscriber != null ) return _subscriber;

        lock( _subscriberLock )
        {
          if( _subscriber == null )
          {
            var redis = ConnectionMultiplexer.Connect( _configuration.Host );
            _subscriber = redis.GetSubscriber();
          }
        }

        return _subscriber;
      }
    }

    public RedisClient( RedisConfiguration configuration )
    {
      _configuration = configuration;
    }

    public void Publish( string message )
    {
      var key = Guid.NewGuid().ToString();
      Subscriber.Publish( key, message );
    }
  }
}
