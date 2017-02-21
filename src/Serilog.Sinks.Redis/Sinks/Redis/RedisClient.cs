using System;
using StackExchange.Redis;

namespace Serilog.Sinks.Redis
{
  public class RedisClient
  {
    private readonly RedisConfiguration _configuration;
    private readonly object _databaseLock = new object();
    private volatile IDatabase _database;

    private IDatabase Database
    {
      get
      {
        if ( _database != null ) return _database;

        lock ( _databaseLock )
        {
          if ( _database == null )
          {
            var redis = ConnectionMultiplexer.Connect( _configuration.Host );
            _database = redis.GetDatabase();
          }
        }

        return _database;
      }
    }


    public RedisClient( RedisConfiguration configuration )
    {
      _configuration = configuration;
    }

    public void Publish( string message )
    {
      Database.ListRightPush( _configuration.Key, message );
    }
  }
}
