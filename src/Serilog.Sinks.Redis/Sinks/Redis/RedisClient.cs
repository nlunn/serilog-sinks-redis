using System.Collections.Generic;
using StackExchange.Redis;

namespace Serilog.Sinks.Redis
{
  public class RedisClient
  {
    private static readonly Dictionary<string, IDatabase> Databases = new Dictionary<string, IDatabase>();
    private static readonly object RedisLock = new object();

    private readonly IDatabase _database;
    private readonly string _key;

    public RedisClient(RedisConfiguration configuration)
    {
      if (!Databases.ContainsKey(configuration.Host))
      {
        lock (RedisLock)
        {
          if (!Databases.ContainsKey(configuration.Host))
            Databases[configuration.Host] = ConnectionMultiplexer.Connect(configuration.Host).GetDatabase();
        }
      }

      _database = Databases[configuration.Host];
      _key = configuration.Key;
    }

    public void Publish( string message )
    {
      _database.ListRightPush(_key, message );
    }
  }
}
