# serilog-sinks-redis

[![NuGet Version](http://img.shields.io/nuget/v/Serilog.Sinks.Redis.svg?style=flat)](https://www.nuget.org/packages/Serilog.Sinks.Redis/)
[![Build status](https://ci.appveyor.com/api/projects/status/9g63na664s6qsnq1/branch/master?svg=true)](https://ci.appveyor.com/project/jenshenneberg/serilog-sinks-redis-0fua9/branch/master)


A Serilog sink that writes events a Redis channel.

**Package** - [Serilog.Sinks.Redis](http://nuget.org/packages/serilog.sinks.redis)
| **Platforms** - .NET 4.5


```csharp
var config = new RedisConfiguration()
config.Host = "elk.example.net:6379"
config.MetaProperties.Add( "_index_name", "MyElasticsearchIndex" );

var logger = new LoggerConfiguration()
    .WriteTo.Redis( config )
    .CreateLogger();
```