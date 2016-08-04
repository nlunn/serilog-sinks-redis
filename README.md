# serilog-sinks-redis

[![NuGet Version](http://img.shields.io/nuget/v/Serilog.Sinks.EventLog.svg?style=flat)](https://www.nuget.org/packages/Serilog.Sinks.Redis/)

A Serilog sink that writes events a Redis channel.

**Package** - [Serilog.Sinks.Redis](http://nuget.org/packages/serilog.sinks.redis)
| **Platforms** - .NET 4.5

```csharp
var config = new RedisConfiguration()
config.Host = "elk-test.danskenet.net:6379"
config.MetaProperties.Add( "_index_name", "MyElasticsearchIndex" );

var logger = new LoggerConfiguration()
    .WriteTo.Redis( config )
    .CreateLogger();
```